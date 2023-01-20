using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;
using Sirensong.UserInterface;
using Wholist.Localization;

namespace Wholist.UI.Windows.Wholist
{
    internal sealed class WholistWindow : Window, IDisposable
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
        internal WholistWindow() : base(TWindowNames.Wholist)
        {
            this.Size = new Vector2(450, 400);
            this.SizeCondition = ImGuiCond.FirstUseEver;
        }

        /// <summary>
        ///     Disposes of the window.
        /// </summary>
        public void Dispose() { }

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
                ImGui.TextWrapped(TWholistWindow.CantUseInPvP);
                return;
            }
            var playersToDraw = WholistPresenter.GetOTPlayers(this.searchText);

            ImGui.BeginChild("##NearbyChild", new Vector2(0, -80), true);
            ImGui.BeginTable("##NearbyTable", 4, ImGuiTableFlags.ScrollY | ImGuiTableFlags.BordersInner | ImGuiTableFlags.Hideable);
            ImGui.TableSetupColumn(TWholistWindow.Name, ImGuiTableColumnFlags.WidthStretch, 250);
            ImGui.TableSetupColumn(TWholistWindow.Company, ImGuiTableColumnFlags.WidthStretch, 100);
            ImGui.TableSetupColumn(TWholistWindow.Level, ImGuiTableColumnFlags.WidthStretch, 50);
            ImGui.TableSetupColumn(TWholistWindow.Class, ImGuiTableColumnFlags.WidthStretch, 150);
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
                    ImGui.TextDisabled(TWholistWindow.ActionsFor($"{obj.Name}@{WholistPresenter.WorldCache.GetRow(obj.HomeWorld.Id)?.Name}"));
                    switch (obj.OnlineStatus.Id)
                    {
                        case (uint)OnlineStatuses.AFK:
                            ImGui.TextColored(ImGuiColors.DalamudOrange, TWholistWindow.PlayerIsAFK);
                            break;
                        case (uint)OnlineStatuses.Busy:
                            ImGui.TextColored(ImGuiColors.DalamudRed, TWholistWindow.PlayerIsBusy);
                            break;
                        default:
                            break;
                    }
                    ImGui.Separator();
                    ImGui.Dummy(new Vector2(0, 5));

                    // Selectable items.
                    if (ImGui.Selectable(TWholistWindow.Examine))
                    {
                        obj.OpenExamine();
                    }
                    if (ImGui.Selectable(TWholistWindow.ViewAdventurerPlate))
                    {
                        obj.OpenCharaCard();
                    }
                    if (ImGui.Selectable(TWholistWindow.Target))
                    {
                        obj.SetAsLPTarget();
                    }

                    if (ImGui.BeginMenu(TWholistWindow.Tell))
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
                        if (ImGui.Button(TWholistWindow.SendMessage))
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
                ImGui.TextColored(SiUI.GetColourForRole(classJob?.Role ?? 0), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(classJob?.Name.ToString() ?? string.Empty));
            }

            ImGui.EndTable();
            ImGui.EndChild();

            // Draw the "total: x" text.
            var totalTextSize = ImGui.CalcTextSize(TWholistWindow.Total(playersToDraw?.Count() ?? 0));
            ImGui.SetCursorPosX((ImGui.GetWindowWidth() - totalTextSize.X) / 2);
            ImGui.TextUnformatted(TWholistWindow.Total(playersToDraw?.Count() ?? 0));

            // Draw the search box.
            ImGui.SetNextItemWidth(-1);
            ImGui.InputTextWithHint("##NearbySearch", TWholistWindow.SearchFor, ref this.searchText, 100);

            // Draw the hide afk checkbox.
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
