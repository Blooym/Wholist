using System.Collections.Generic;
using System.Numerics;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Interface;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
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
        internal NearbyPlayersWindow() : base(Strings.Windows_Who_Title)
        {
            this.Size = new Vector2(450, 400);
            this.SizeCondition = ImGuiCond.FirstUseEver;
            this.TitleBarButtons = new()
            {
                new() {
                    Icon = FontAwesomeIcon.Cog,
                    ShowTooltip = () => SiGui.AddTooltip("Settings"),
                    Click = (btn) => Services.WindowManager.ToggleConfigWindow()
                },
                new() {
                    Icon = FontAwesomeIcon.Heart,
                    ShowTooltip = () => SiGui.AddTooltip("Donate"),
                    Click = (btn) => Util.OpenLink(Constants.KoFiLink)
        }
            };
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
            var childSize = NearbyPlayersLogic.Configuration.NearbyPlayers.ShowSearchBar ? new Vector2(0, -55) : new Vector2(0, -25);

            if (ImGui.BeginChild("##NearbyChild", childSize))
            {
                DrawNearbyPlayersTable(playersToDraw);
            }
            ImGui.EndChild();

            // Draw the search box & total players text if not in minimal mode
            if (NearbyPlayersLogic.Configuration.NearbyPlayers.ShowSearchBar)
            {
                DrawSearchBar(ref this.logic.SearchText);
            }

            DrawTotalPlayers(playersToDraw.Count);
        }

        /// <summary>
        ///     Draws the nearby players table.
        /// </summary>
        /// <param name="playersToDraw"></param>
        private static void DrawNearbyPlayersTable(List<PlayerInfoSlim> playersToDraw)
        {
            if (ImGui.BeginTable("##NearbyTable", 6, ImGuiTableFlags.ScrollY | ImGuiTableFlags.Borders | ImGuiTableFlags.Hideable | ImGuiTableFlags.Reorderable | ImGuiTableFlags.Resizable))
            {
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Name, ImGuiTableColumnFlags.WidthStretch | ImGuiTableColumnFlags.NoHide, 220);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Job, ImGuiTableColumnFlags.WidthStretch, 150);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Level, ImGuiTableColumnFlags.WidthStretch, 80);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Homeworld, ImGuiTableColumnFlags.WidthStretch, 150);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Company, ImGuiTableColumnFlags.WidthStretch, 120);
                ImGui.TableSetupColumn(Strings.UserInterface_NearbyPlayers_Players_Distance, ImGuiTableColumnFlags.WidthStretch | ImGuiTableColumnFlags.DefaultHide, 80);
                ImGui.TableSetupScrollFreeze(0, 1);
                ImGui.TableHeadersRow();

                ImGuiClip.ClippedDraw(playersToDraw, DrawPlayer, ImGui.GetTextLineHeightWithSpacing());

                ImGui.EndTable();
            }
        }

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

            // HomeWorld.
            SiGui.Text(obj.HomeWorld);
            ImGui.TableNextColumn();

            // Company.
            SiGui.Text(obj.CompanyTag);
            ImGui.TableNextColumn();

            // Distance.
            SiGui.Text($"{obj.Distance} yalms");
        }

        private static void DrawTotalPlayers(int totalPlayers) => ImGuiHelpers.CenteredText(string.Format(Strings.UserInterface_NearbyPlayers_Players_Total, totalPlayers.ToString()));

        /// <summary>
        ///     Draws the context menu for a player.
        /// </summary>
        /// <param name="obj"></param>
        private static void DrawPlayerContextMenu(PlayerInfoSlim obj)
        {
            if (ImGui.BeginPopupContextItem($"{obj.Name}{obj.HomeWorld}##WholistPopContext"))
            {
                // Heading.
                SiGui.Heading(string.Format(Strings.UserInterface_NearbyPlayers_Players_Submenu_Heading, $"{obj.Name}@{obj.HomeWorld}"));

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
                // FIXME: This has been removed due to a lack of knowledge on sending tells
                // to other players natively. PRs welcome.
                // ImGui.BeginDisabled();
                // if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_Tell))
                // {
                // NearbyPlayersLogic.SetChatTellTarget(obj.Name, obj.HomeWorld);
                // }
                // ImGui.EndDisabled();

                // Find on Map.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_OpenOnMap))
                {
                    NearbyPlayersLogic.FlagAndOpen(obj.Position, obj.Name);
                }

                // Find on Lodestone.
                if (ImGui.Selectable(Strings.UserInterface_NearbyPlayers_Players_Submenu_Lodestone))
                {
                    NearbyPlayersLogic.SearchPlayerOnLodestone(obj.Name, obj.HomeWorld);
                }

                ImGui.EndPopup();
            }
        }
    }
}
