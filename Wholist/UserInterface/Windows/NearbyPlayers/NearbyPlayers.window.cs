using System.Collections.Generic;
using System.Numerics;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Sirensong.Game.Helpers;
using Sirensong.UserInterface;
using Wholist.Common;
using Wholist.DataStructures;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.NearbyPlayers
{
    internal sealed class NearbyPlayersWindow : Window
    {
        /// <inheritdoc cref="NearbyPlayersLogic" />
        private readonly NearbyPlayersLogic logic = new();

        /// <summary>
        ///     Creates a new instance of the <see cref="NearbyPlayersWindow" />.
        /// </summary>
        internal NearbyPlayersWindow() : base(string.Format(Strings.Windows_Who_Title, Constants.PluginName))
        {
            this.Size = new Vector2(450, 400);
            this.SizeCondition = ImGuiCond.FirstUseEver;
        }

        public override bool DrawConditions()
        {
            if (NearbyPlayersLogic.Configuration.NearbyPlayers.HideInCombat && NearbyPlayersLogic.Condition[ConditionFlag.InCombat])
            {
                return false;
            }

            if (NearbyPlayersLogic.Configuration.NearbyPlayers.HideInInstance && (ConditionHelper.IsBoundByDuty() || ConditionHelper.IsInIslandSanctuary()))
            {
                return false;
            }

            if (NearbyPlayersLogic.IsPvP)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Draws the window.
        /// </summary>
        public override void Draw()
        {
            this.Flags = NearbyPlayersLogic.ApplyFlagConfiguration(this.Flags);
            this.RespectCloseHotkey = !NearbyPlayersLogic.ShouldDisableEscClose;

            var playersToDraw = this.logic.GetNearbyPlayers();
            var childSize = NearbyPlayersLogic.Configuration.NearbyPlayers.MinimalMode ? new Vector2(0, 0) : new Vector2(0, -60);

            if (ImGui.BeginChild("##NearbyChild", childSize))
            {
                DrawNearbyPlayersTable(playersToDraw);
            }
            ImGui.EndChild();

            // Draw the search box & total players text if not in minimal mode
            if (!NearbyPlayersLogic.Configuration.NearbyPlayers.MinimalMode)
            {
                DrawSearchBar(ref this.logic.SearchText);
                DrawTotalPlayers(playersToDraw.Count);
            }
        }

        /// <summary>
        ///     Draws the nearby players table.
        /// </summary>
        /// <param name="playersToDraw"></param>
        private static void DrawNearbyPlayersTable(List<PlayerInfoSlim> playersToDraw)
        {
            if (ImGui.BeginTable("##NearbyTable", 5, ImGuiTableFlags.ScrollY | ImGuiTableFlags.Borders | ImGuiTableFlags.Hideable | ImGuiTableFlags.Reorderable | ImGuiTableFlags.Resizable))
            {
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Name, ImGuiTableColumnFlags.WidthStretch, 220);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Job, ImGuiTableColumnFlags.WidthStretch, 150);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Level, ImGuiTableColumnFlags.WidthStretch, 80);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Homeworld, ImGuiTableColumnFlags.WidthStretch, 150);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Company, ImGuiTableColumnFlags.WidthStretch, 120);
                ImGui.TableSetupScrollFreeze(0, 1);
                ImGui.TableHeadersRow();

                foreach (var obj in playersToDraw)
                {
                    DrawPlayer(obj);
                }

                ImGui.EndTable();
            }
        }

        /// <summary>
        ///     Draws the total players text.
        /// </summary>
        /// <param name="totalPlayers"></param>
        private static void DrawTotalPlayers(int totalPlayers) => ImGuiHelpers.CenteredText(string.Format(Strings.UserInterface_NearbyPlayers_Players_Total, totalPlayers.ToString()));

        /// <summary>
        ///     Draws the search bar.
        /// </summary>
        /// <param name="searchText"></param>
        private static void DrawSearchBar(ref string searchText)
        {
            ImGui.SetNextItemWidth(-1);
            ImGui.InputTextWithHint("##NearbySearch", Strings.UserInterface_NearbyPlayers_Search, ref searchText, 100);
        }

        /// <summary>
        ///     Draws a player in the table.
        /// </summary>
        /// <param name="obj"></param>
        private static void DrawPlayer(PlayerInfoSlim obj)
        {
            ImGui.TableNextColumn();

            // Name.
            SiGui.TextColoured(obj.NameColour, obj.Name);
            DrawPlayerContextMenu(obj);
            ImGui.TableNextColumn();

            // Job.
            SiGui.TextColoured(NearbyPlayersLogic.GetJobColour(obj.Job), NearbyPlayersLogic.GetJobName(obj.Job));
            ImGui.TableNextColumn();

            // Level.
            SiGui.Text(obj.Level.ToString());
            ImGui.TableNextColumn();

            // Homeworld.
            SiGui.Text(obj.Homeworld.Name);
            ImGui.TableNextColumn();

            // Company.
            SiGui.Text(obj.CompanyTag);
        }

        /// <summary>
        ///     Draws the context menu for a player.
        /// </summary>
        /// <param name="obj"></param>
        private static void DrawPlayerContextMenu(PlayerInfoSlim obj)
        {
            if (ImGui.BeginPopupContextItem($"{obj.Name}##WholistPopContext"))
            {
                // Heading.
                SiGui.Heading(string.Format(Strings.UserInterface_NearbyPlayers_Players_Submenu_Heading, $"{obj.Name}@{obj.Homeworld.Name}"));

                // Examine.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_Examine))
                {
                    obj.OpenExamine();
                }

                // View Character Card.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_AdventurePlate))
                {
                    obj.OpenCharaCard();
                }

                // Target.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_Target))
                {
                    obj.Target();
                }

                // Send Tell.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_Tell))
                {
                    NearbyPlayersLogic.SetChatTellTarget(obj.Name, obj.Homeworld.Name);
                }

                // Find on Map.
                ImGui.BeginDisabled(obj.Position == null);
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_OpenOnMap))
                {
                    NearbyPlayersLogic.FlagAndOpen(obj.Position!.Value, obj.Name);
                }
                ImGui.EndDisabled();

                // Find on Lodestone.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_Lodestone))
                {
                    NearbyPlayersLogic.SearchPlayerOnLodestone(obj.Name, obj.Homeworld.Name);
                }

                // External integrations / 3rd-party.
                ImGui.Separator();
                DrawExternPlayerIntegrationsMenu(obj);

                ImGui.EndPopup();
            }
        }

        /// <summary>
        ///     Draws the integrations menu.
        /// </summary>
        /// <param name="obj"></param>
        private static void DrawExternPlayerIntegrationsMenu(PlayerInfoSlim obj)
        {
            if (ImGui.BeginMenu(Strings.UserInterface_NearbyPlayers_Players_Submenu_Integrations))
            {
                var integrations = NearbyPlayersLogic.GetExternContextMenuItems();
                if (integrations.Count != 0)
                {
                    foreach (var item in integrations)
                    {
                        if (ImGui.BeginMenu($"{item.Value}##{item.Key}"))
                        {
                            NearbyPlayersLogic.InvokeExternPlayerContextMenu(item.Key, obj.GetPlayerCharacter());
                            ImGui.EndMenu();
                        }
                    }
                }
                else
                {
                    SiGui.TextDisabled(Strings.UserInterface_NearbyPlayers_Players_Submenu_Integrations_None);
                }
                ImGui.EndMenu();
            }
        }
    }
}
