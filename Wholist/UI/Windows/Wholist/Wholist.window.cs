using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Wholist.Localization;
using Wholist.UI.ImGuiBasicComponents;

namespace Wholist.UI.Windows.Wholist
{
    internal sealed class WholistWindow : Window, IDisposable
    {
        internal WholistPresenter Presenter { get; set; } = new();

        /// <summary>
        ///     Constructor.
        /// </summary>
        internal WholistWindow() : base(TWindowNames.Wholist)
        {
            this.Size = new Vector2(450, 400);
            this.SizeCondition = ImGuiCond.Always;
            this.Flags |= ImGuiWindowFlags.NoResize;
        }

        /// <summary>
        ///     Disposes of the window.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        ///     The text to search for.
        /// </summary>
        private string searchText = string.Empty;

        /// <summary>
        ///     When the window is opened, start the update timer.
        /// </summary>
        public override void OnOpen()
        {
            this.Presenter.UpdatePlayerList();
            this.Presenter.UpdateTimer.Start();
        }

        /// <summary>
        ///     When the window is closed, stop the update timer.
        /// </summary>
        public override void OnClose()
        {
            this.Presenter.UpdateTimer.Stop();
            this.Presenter.RemoveAllTells();
        }

        /// <summary>
        ///     Draws the window.
        /// </summary>
        public override void Draw()
        {
            // Do not allow the plugin to be used in PvP.
            if (WholistPresenter.ClientState.IsPvPExcludingDen)
            {
                ImGui.TextWrapped(TWholistWindow.CantUseInPvP);
                return;
            }

            // Get the bot objects and player objects and filter appropriately.
            var objectsToDraw = this.Presenter.PlayerCharacters
                .Where(WholistPresenter.Configuration.FilterBots ? o => WholistPresenter.IsPlayerBot(o) == false : o => true)
                .Where(WholistPresenter.Configuration.FilterAfk ? o => WholistPresenter.IsPlayerAfk(o) == false : o => true)
                .Where(this.searchText.Length > 0 ? o => o.Name.ToString().Contains(this.searchText, StringComparison.OrdinalIgnoreCase)
                            || o.CompanyTag.ToString().Contains(this.searchText, StringComparison.OrdinalIgnoreCase)
                            || o.Level.ToString(CultureInfo.InvariantCulture).Contains(this.searchText, StringComparison.OrdinalIgnoreCase)
                            || o.ClassJob.GameData?.Name.ToString().Contains(this.searchText, StringComparison.OrdinalIgnoreCase) == true : o => true)
                .Take(100);

            // Draw the object table or "no players found" text.
            ImGui.BeginChild("##NearbyChild", new Vector2(0, -80), true);
            if (objectsToDraw?.Any() == true)
            {
                ImGui.BeginTable("##NearbyTable", 4, ImGuiTableFlags.ScrollY | ImGuiTableFlags.BordersInner);
                ImGui.TableSetupColumn(TWholistWindow.Name, ImGuiTableColumnFlags.WidthStretch, 250);
                ImGui.TableSetupColumn(TWholistWindow.Company, ImGuiTableColumnFlags.WidthStretch, 100);
                ImGui.TableSetupColumn(TWholistWindow.Level, ImGuiTableColumnFlags.WidthStretch, 50);
                ImGui.TableSetupColumn(TWholistWindow.Class, ImGuiTableColumnFlags.WidthStretch, 150);
                ImGui.TableSetupScrollFreeze(0, 1);
                ImGui.TableHeadersRow();

                // Draw the objects.
                foreach (var obj in objectsToDraw)
                {
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(obj.Name.ToString());

                    if (ImGui.BeginPopupContextItem($"##NearbyTableRightClickMenu{obj.Name}"))
                    {
                        var isAfk = WholistPresenter.IsPlayerAfk(obj);
                        var isBusy = WholistPresenter.IsPlayerBusy(obj);

                        ImGui.TextDisabled(TWholistWindow.ActionsFor($"{obj.Name}@{obj.HomeWorld.GameData?.Name}"));

                        if (isAfk)
                        {
                            ImGui.TextColored(ImGuiColors.DalamudOrange, TWholistWindow.PlayerIsAFK);
                        }

                        else if (isBusy)
                        {
                            ImGui.TextColored(ImGuiColors.DalamudRed, TWholistWindow.PlayerIsBusy);
                        }

                        ImGui.Separator();
                        ImGui.Dummy(new Vector2(0, 5));

                        if (ImGui.Selectable(TWholistWindow.Examine))
                        {
                            WholistPresenter.GameFunctions.Examine.OpenExamineWindow(obj);
                        }
                        if (ImGui.Selectable(TWholistWindow.ViewAdventurerPlate))
                        {
                            WholistPresenter.OpenCharaCardFromAddress(obj.Address);
                        }
                        if (ImGui.Selectable(TWholistWindow.Target))
                        {
                            WholistPresenter.TargetManager.SetTarget(obj);
                        }

                        if (isBusy)
                        {
                            ImGui.TextDisabled(TWholistWindow.Tell);
                        }
                        else
                        {
                            if (ImGui.BeginMenu(TWholistWindow.Tell))
                            {
                                var message = this.Presenter.GetTell(obj.ObjectId);
                                var maxMsgLength = (uint)380;

                                if (ImGui.InputText("##TellMessage", ref message, maxMsgLength))
                                {
                                    this.Presenter.SetTell(obj.ObjectId, message);
                                }

                                ImGui.BeginDisabled(string.IsNullOrWhiteSpace(message) || message.Length > maxMsgLength);
                                if (ImGui.Button("Send Message"))
                                {
                                    try
                                    {
                                        WholistPresenter.GameFunctions.Chat.SendMessage($"/tell {obj.Name}@{obj.HomeWorld.GameData?.Name} {message}");
                                    }
                                    catch (Exception e)
                                    {
                                        WholistPresenter.ChatGui.PrintError($"Error sending tell: {e.Message}");
                                    }
                                    this.Presenter.RemoveTell(obj.ObjectId);
                                }
                                ImGui.EndDisabled();

                                ImGui.SameLine();
                                ImGui.Text($"({message.Length}/{maxMsgLength})");
                                ImGui.EndMenu();
                            }
                        }
                        ImGui.EndPopup();
                    }

                    ImGui.TableSetColumnIndex(1);
                    ImGui.Text(obj.CompanyTag.ToString());
                    ImGui.TableSetColumnIndex(2);
                    ImGui.Text(obj.Level.ToString());
                    ImGui.TableSetColumnIndex(3);
                    ImGui.TextColored(Colours.GetColourForRole(obj.ClassJob.GameData?.Role ?? 0), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(obj.ClassJob.GameData?.Name.ToString() ?? string.Empty));
                }

                ImGui.EndTable();
            }
            else
            {
                ImGui.SetCursorPosX((ImGui.GetWindowWidth() - ImGui.CalcTextSize(TWholistWindow.NoPlayersFound).X) / 2);
                ImGui.Text(TWholistWindow.NoPlayersFound);
            }
            ImGui.EndChild();


            // Draw the "total: x" text.
            var totalTextSize = ImGui.CalcTextSize(TWholistWindow.Total(objectsToDraw?.Count() ?? 0));
            ImGui.SetCursorPosX((ImGui.GetWindowWidth() - totalTextSize.X) / 2);
            ImGui.Text(TWholistWindow.Total(objectsToDraw?.Count() ?? 0));

            // Draw the search box;
            ImGui.SetNextItemWidth(-1);
            ImGui.InputTextWithHint("##NearbySearch", TWholistWindow.SearchFor, ref this.searchText, 100);

            // Draw the hide bots checkbox.
            var hideSuspectedBots = WholistPresenter.Configuration.FilterBots;
            if (ImGui.Checkbox(TWholistWindow.HideSuspectedBots, ref hideSuspectedBots))
            {
                WholistPresenter.Configuration.FilterBots = hideSuspectedBots;
                WholistPresenter.Configuration.Save();
            }
            ImGui.SameLine();

            var hideAfkPlayers = WholistPresenter.Configuration.FilterAfk;
            if (ImGui.Checkbox(TWholistWindow.HideAfkPlayers, ref hideAfkPlayers))
            {
                WholistPresenter.Configuration.FilterAfk = hideAfkPlayers;
                WholistPresenter.Configuration.Save();
            }
            ImGui.SameLine();

#if DEBUG
            this.Presenter.DialogManager.Draw();
            if (ImGui.Button("Export Localization"))
            {
                this.Presenter.DialogManager.OpenFolderDialog("Export LOC", WholistPresenter.OnDirectoryPicked);
            }
#endif
        }
    }
}
