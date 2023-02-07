using System.Globalization;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using ImGuiNET;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;
using Sirensong.UserInterface;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.WhoWindow
{
    internal sealed class WhoWindow : Window
    {
        /// <summary>
        /// The search text to apply to the object table.
        /// </summary>
        private string searchText = string.Empty;

        /// <summary>
        /// The logic for the window.
        /// </summary>
        private WhoWindowLogic Logic { get; set; } = new();

        /// <summary>
        /// Creates a new instance of the <see cref="WhoWindow" />.
        /// </summary>
        internal WhoWindow() : base(Strings.Windows_Who_Title.Format(Constants.PluginName))
        {
            this.Size = new Vector2(450, 400);
            this.SizeCondition = ImGuiCond.FirstUseEver;
        }

        /// <inheritdoc cref="Window.OnClose" />
        public override void OnClose()
        {
            this.Logic.ClearTells();
            base.OnClose();
        }

        /// <summary>
        /// Draws the window.
        /// </summary>
        public override void Draw()
        {
            if (WhoWindowLogic.ClientState.IsPvPExcludingDen)
            {
                SiGui.TextWrapped(Strings.Errors_NoUseInPvP);
                return;
            }

            var playersToDraw = WhoWindowLogic.GetOTPlayers(this.searchText).Take(50).ToList();

            if (ImGui.BeginChild("##NearbyChild", new Vector2(0, -105), true))
            {
                if (ImGui.BeginTable("##NearbyTable", 4, ImGuiTableFlags.ScrollY | ImGuiTableFlags.BordersInner | ImGuiTableFlags.Hideable))
                {
                    ImGui.TableSetupColumn(Strings.UserInterface_Who_Players_Name, ImGuiTableColumnFlags.WidthStretch, 250);
                    ImGui.TableSetupColumn(Strings.UserInterface_Who_Players_Company, ImGuiTableColumnFlags.WidthStretch, 100);
                    ImGui.TableSetupColumn(Strings.UserInterface_Who_Players_Level, ImGuiTableColumnFlags.WidthStretch, 50);
                    ImGui.TableSetupColumn(Strings.UserInterface_Who_Players_Class, ImGuiTableColumnFlags.WidthStretch, 150);
                    ImGui.TableSetupScrollFreeze(0, 1);
                    ImGui.TableHeadersRow();

                    // Draw players.
                    foreach (var obj in playersToDraw)
                    {
                        var classJob = WhoWindowLogic.ClassJobCache.GetRow(obj.ClassJob.Id);

                        ImGui.TableNextColumn();
                        SiGui.Text(obj.Name.ToString());

                        if (ImGui.BeginPopupContextItem($"{obj.ObjectId}##WholistPopContext"))
                        {
                            // Heading.
                            ImGui.TextDisabled(Strings.UserInterface_Who_Players_Submenu_Heading.Format($"{obj.Name}@{WhoWindowLogic.WorldCache.GetRow(obj.HomeWorld.Id)?.Name}"));
                            switch (obj.OnlineStatus.Id)
                            {
                                case (uint)OnlineStatusType.AFK:
                                    SiGui.TextColoured(ImGuiColors.DalamudOrange, Strings.UserInterface_Who_Players_Submenu_AFK);
                                    break;
                                case (uint)OnlineStatusType.Busy:
                                    SiGui.TextColoured(ImGuiColors.DalamudRed, Strings.UserInterface_Who_Players_Submenu_Busy);
                                    break;
                                default:
                                    break;
                            }
                            ImGui.Separator();
                            ImGui.Dummy(new Vector2(0, 5));

                            // Selectable items.
                            if (ImGui.Selectable(Strings.UserInterface_Who_Players_Submenu_Examine))
                            {
                                obj.OpenExamine();
                            }
                            if (ImGui.Selectable(Strings.UserInterface_Who_Players_AdventurePlate))
                            {
                                obj.OpenCharaCard();
                            }
                            if (ImGui.Selectable(Strings.UserInterface_Who_Players_Submenu_Target))
                            {
                                obj.Target();
                            }

                            if (ImGui.BeginMenu(Strings.UserInterface_Who_Players_Submenu_Tell))
                            {
                                var message = this.Logic.GetTell(obj.ObjectId);
                                var canSendMessage = WhoWindowLogic.IsMessageValid(message);

                                if (ImGui.InputText("##TellMessage", ref message, WhoWindowLogic.MaxMsgLength))
                                {
                                    this.Logic.SetTell(obj.ObjectId, message);
                                }
                                if (ImGui.IsItemDeactivated())
                                {
                                    if (ImGui.IsKeyPressed(ImGuiKey.Enter) && canSendMessage)
                                    {
                                        WhoWindowLogic.SendTell(obj, message);
                                        this.Logic.RemoveTell(obj.ObjectId);
                                    }
                                }

                                ImGui.BeginDisabled(!canSendMessage);
                                if (ImGui.Button(Strings.UserInterface_Who_Players_Submenu_Tell_Send))
                                {
                                    WhoWindowLogic.SendTell(obj, message);
                                    this.Logic.RemoveTell(obj.ObjectId);
                                }
                                ImGui.EndDisabled();

                                ImGui.SameLine();
                                SiGui.Text($"({message.Length}/{WhoWindowLogic.MaxMsgLength})");
                                ImGui.EndMenu();
                            }
                            ImGui.EndPopup();
                        }

                        ImGui.TableNextColumn();
                        SiGui.Text(obj.CompanyTag.TextValue);
                        ImGui.TableNextColumn();
                        SiGui.Text(obj.Level.ToString());
                        ImGui.TableNextColumn();
                        SiGui.TextColoured(((ClassJobRole?)classJob?.Role ?? 0).GetColourForRole(), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(classJob?.Name.ToString() ?? string.Empty));
                    }
                    ImGui.EndTable();
                }
                ImGui.EndChild();
            }

            // Draw the "total: x" text.
            var totalText = Strings.UserInterface_Who_Players_Total.Format(playersToDraw.Count);
            var totalTextSize = ImGui.CalcTextSize(totalText);
            ImGui.SetCursorPosX((ImGui.GetWindowWidth() - totalTextSize.X) / 2);
            SiGui.Text(totalText);

            // Draw the search box.
            ImGui.SetNextItemWidth(-1);
            ImGui.InputTextWithHint("##NearbySearch", Strings.UserInterface_Who_Search, ref this.searchText, 100);

            // Draw the hide afk checkbox.
            var hideAfkPlayers = WhoWindowLogic.Configuration.FilterAfk;
            if (ImGui.Checkbox(Strings.UserInterface_Who_Configuration_HideAfkPlayers, ref hideAfkPlayers))
            {
                WhoWindowLogic.Configuration.FilterAfk = hideAfkPlayers;
                WhoWindowLogic.Configuration.Save();
            }
        }
    }
}
