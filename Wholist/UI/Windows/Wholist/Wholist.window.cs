using System.Globalization;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;
using Sirensong.UserInterface.Components;
using Wholist.Base;

namespace Wholist.UI.Windows.Wholist
{
    internal sealed class WholistWindow : Window
    {
        /// <summary>
        ///     The search text to apply to the object table.
        /// </summary>
        private string searchText = string.Empty;

        /// <summary>
        ///     The presenter for the window.
        /// </summary>
        internal WholistPresenter Presenter { get; set; } = new();
        /// <summary>
        ///     Creates a new instance of the <see cref="WholistWindow" />.
        /// </summary>
        internal WholistWindow() : base(LStrings.WholistWindow.WindowName)
        {
            this.Size = new Vector2(450, 400);
            this.SizeCondition = ImGuiCond.FirstUseEver;
        }

        /// <inheritdoc cref="Window.OnClose" />
        public override void OnClose()
        {
            this.Presenter.ClearTells();
            base.OnClose();
        }

        /// <summary>
        ///     Draws the window.
        /// </summary>
        public override void Draw()
        {
            if (WholistPresenter.ClientState.IsPvPExcludingDen)
            {
                ImGui.TextWrapped(LStrings.WholistWindow.CantUseInPvP);
                return;
            }
            var playersToDraw = WholistPresenter.GetOTPlayers(this.searchText);

            if (ImGui.BeginChild("##NearbyChild", new Vector2(0, -105), true))
            {
                if (ImGui.BeginTable("##NearbyTable", 4, ImGuiTableFlags.ScrollY | ImGuiTableFlags.BordersInner | ImGuiTableFlags.Hideable))
                {
                    ImGui.TableSetupColumn(LStrings.WholistWindow.Name, ImGuiTableColumnFlags.WidthStretch, 250);
                    ImGui.TableSetupColumn(LStrings.WholistWindow.Company, ImGuiTableColumnFlags.WidthStretch, 100);
                    ImGui.TableSetupColumn(LStrings.WholistWindow.Level, ImGuiTableColumnFlags.WidthStretch, 50);
                    ImGui.TableSetupColumn(LStrings.WholistWindow.Class, ImGuiTableColumnFlags.WidthStretch, 150);
                    ImGui.TableSetupScrollFreeze(0, 1);
                    ImGui.TableHeadersRow();

                    // Draw players.
                    foreach (var obj in playersToDraw)
                    {
                        var classJob = WholistPresenter.ClassJobCache.GetRow(obj.ClassJob.Id);

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(obj.Name.ToString());

                        if (ImGui.BeginPopupContextItem(obj.ObjectId + "##WholistPopContext"))
                        {
                            // Heading.
                            ImGui.TextDisabled(LStrings.WholistWindow.ActionsFor($"{obj.Name}@{WholistPresenter.WorldCache.GetRow(obj.HomeWorld.Id)?.Name}"));
                            switch (obj.OnlineStatus.Id)
                            {
                                case (uint)OnlineStatuses.AFK:
                                    ImGui.TextColored(ImGuiColors.DalamudOrange, LStrings.WholistWindow.PlayerIsAFK);
                                    break;
                                case (uint)OnlineStatuses.Busy:
                                    ImGui.TextColored(ImGuiColors.DalamudRed, LStrings.WholistWindow.PlayerIsBusy);
                                    break;
                                default:
                                    break;
                            }
                            ImGui.Separator();
                            ImGui.Dummy(new Vector2(0, 5));

                            // Selectable items.
                            if (ImGui.Selectable(LStrings.WholistWindow.Examine))
                            {
                                obj.OpenExamine();
                            }
                            if (ImGui.Selectable(LStrings.WholistWindow.ViewAdventurerPlate))
                            {
                                obj.OpenCharaCard();
                            }
                            if (ImGui.Selectable(LStrings.WholistWindow.Target))
                            {
                                obj.SetAsLPTarget();
                            }

                            if (ImGui.BeginMenu(LStrings.WholistWindow.Tell))
                            {
                                var message = this.Presenter.GetTell(obj.ObjectId);
                                var canSendMessage = WholistPresenter.IsMessageValid(message);

                                if (ImGui.InputText("##TellMessage", ref message, WholistPresenter.MaxMsgLength))
                                {
                                    this.Presenter.SetTell(obj.ObjectId, message);
                                }
                                if (ImGui.IsItemDeactivated())
                                {
                                    if (ImGui.IsKeyPressed(ImGuiKey.Enter) && canSendMessage)
                                    {
                                        WholistPresenter.SendTell(obj, message);
                                        this.Presenter.RemoveTell(obj.ObjectId);
                                    }
                                }

                                ImGui.BeginDisabled(!canSendMessage);
                                if (ImGui.Button(LStrings.WholistWindow.SendMessage))
                                {
                                    WholistPresenter.SendTell(obj, message);
                                    this.Presenter.RemoveTell(obj.ObjectId);
                                }
                                ImGui.EndDisabled();

                                ImGui.SameLine();
                                ImGui.TextUnformatted($"({message.Length}/{WholistPresenter.MaxMsgLength})");
                                ImGui.EndMenu();
                            }
                            ImGui.EndPopup();
                        }

                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(obj.CompanyTag.ToString());
                        ImGui.TableNextColumn();
                        ImGui.TextUnformatted(obj.Level.ToString());
                        ImGui.TableNextColumn();
                        ImGui.TextColored(ClassJobRoleExtensions.GetColourForRole((ClassJobRole?)classJob?.Role ?? 0), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(classJob?.Name.ToString() ?? string.Empty));
                    }
                    ImGui.EndTable();
                }
                ImGui.EndChild();
            }

            // Draw the "total: x" text.
            var totalText = LStrings.WholistWindow.Total(playersToDraw.Count());
            var totalTextSize = ImGui.CalcTextSize(totalText);
            ImGui.SetCursorPosX((ImGui.GetWindowWidth() - totalTextSize.X) / 2);
            ImGui.TextUnformatted(totalText);

            // Draw the search box.
            ImGui.SetNextItemWidth(-1);
            ImGui.InputTextWithHint("##NearbySearch", LStrings.WholistWindow.SearchFor, ref this.searchText, 100);

            // Draw the hide afk checkbox.
            var hideAfkPlayers = WholistPresenter.Configuration.FilterAfk;
            if (ImGui.Checkbox(LStrings.WholistWindow.HideAfkPlayers, ref hideAfkPlayers))
            {
                WholistPresenter.Configuration.FilterAfk = hideAfkPlayers;
                WholistPresenter.Configuration.Save();
            }

            // Version info.
            SiGUIComponent.VersionInfo(Constants.Version, Constants.GitCommitHash);

#if DEBUG
            ImGui.SameLine();
            this.Presenter.DialogManager.Draw();
            if (ImGui.Button(LStrings.WholistWindow.ExportLocalization))
            {
                this.Presenter.DialogManager.OpenFolderDialog(LStrings.WholistWindow.ExportLocalization, WholistPresenter.OnDirectoryPicked);
            }
#endif
        }
    }
}
