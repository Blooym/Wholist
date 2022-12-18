using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Wholist.Base;
using Wholist.Localization;
using Wholist.UI.ImGuiBasicComponents;
using Wholist.Utils;

namespace Wholist.UI.Windows.Wholist
{
    public sealed class WholistWindow : Window, IDisposable
    {
        public WholistPresenter Presenter { get; set; } = new();

        /// <summary>
        ///     Constructor.
        /// </summary>
        public WholistWindow() : base(TWindowNames.Wholist)
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
        ///     Draws the window.
        /// </summary>
        public override void Draw()
        {
            if (PluginService.ClientState.IsPvPExcludingDen)
            {
                ImGui.TextWrapped(TWholistWindow.CantUseInPvP);
                return;
            }

            // Filter the player character objects down to the query and then take 50 of them.
            var objectsToDraw = Objects.PlayerCharacters.Where(x => x.Name.ToString().Contains(this.searchText, StringComparison.OrdinalIgnoreCase) ||
                                                             x.CompanyTag.ToString().Contains(this.searchText, StringComparison.OrdinalIgnoreCase) ||
                                                             x.Level.ToString(CultureInfo.InvariantCulture).Contains(this.searchText, StringComparison.OrdinalIgnoreCase) ||
                                                             x.ClassJob.GameData?.Name.ToString().Contains(this.searchText, StringComparison.OrdinalIgnoreCase) == true)
                                                    .Take(50);

            // Draw the main characters child.
            ImGui.BeginChild("##NearbyChild", new Vector2(0, -60), true);

            // If there are objects to draw, draw the table.
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
                        ImGui.TextDisabled(TWholistWindow.ActionsFor($"{obj.Name}@{obj.HomeWorld.GameData?.Name}"));
                        ImGui.Separator();
                        ImGui.Dummy(new Vector2(0, 5));

                        if (ImGui.Selectable(TWholistWindow.Examine))
                        {
                            PluginService.Common.Functions.Examine.OpenExamineWindow(obj);
                        }
                        if (ImGui.Selectable(TWholistWindow.Target))
                        {
                            PluginService.TargetManager.SetTarget(obj);
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
            // Otherwise, draw the no results text.
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

            ImGui.SetNextItemWidth(-1);
            ImGui.InputTextWithHint("##NearbySearch", TWholistWindow.SearchForPlayer, ref this.searchText, 100);

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
