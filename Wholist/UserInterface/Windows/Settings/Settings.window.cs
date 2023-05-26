using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Sirensong.UserInterface.Windowing;
using Wholist.Common;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.TableParts;

namespace Wholist.UserInterface.Windows.Settings
{
    internal sealed class SettingsWindow : Window
    {
        /// <inheritdoc cref="SettingsLogic" />
        private readonly SettingsLogic logic = new();

        /// <inheritdoc />
        public SettingsWindow() : base(string.Format(Strings.Windows_Settings_Title, Constants.PluginName))
        {
            this.Size = new Vector2(700, 450);
            this.SizeCondition = ImGuiCond.FirstUseEver;
            this.Flags = ImGuiWindowFlagExtra.NoScroll;
        }

        /// <inheritdoc />
        public override void Draw()
        {
            if (ImGui.BeginTable("PluginSettings", 2, ImGuiTableFlags.BordersInnerV))
            {
                ImGui.TableSetupColumn("PluginSettingsSidebar", ImGuiTableColumnFlags.WidthFixed, ImGui.GetContentRegionAvail().X * 0.25f);
                ImGui.TableSetupColumn("PluginSettingsList", ImGuiTableColumnFlags.WidthFixed, ImGui.GetContentRegionAvail().X * 0.75f);
                ImGui.TableNextRow();

                // Sidebar
                ImGui.TableNextColumn();
                if (ImGui.BeginChild("PluginSettingsSidebarChild"))
                {
                    SettingsSidebar.Draw(this.logic);
                    ImGui.Dummy(Spacing.SidebarSectionSpacing);

                    SiGui.Heading("Notice");
                    SiGui.TextWrappedColoured(Colours.Warning, "Friend detection is currently disabled due to a game update that broke it, it will be re-enabled as soon as possible.");
                    ImGui.Dummy(Spacing.ReadableSpacing);
                    SiGui.TextWrappedColoured(Colours.Warning, "If you experience any issues with the plugin after the latest game update, please report them on the plugin's GitHub page.");
                }
                ImGui.EndChild();

                // Listings
                ImGui.TableNextColumn();
                if (ImGui.BeginChild("PluginSettingsListChild"))
                {
                    SettingsActive.Draw(this.logic);
                }
                ImGui.EndChild();

                ImGui.EndTable();
            }
        }
    }
}
