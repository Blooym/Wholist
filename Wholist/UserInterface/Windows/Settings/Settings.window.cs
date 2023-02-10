using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using ImGuiNET;
using Sirensong.UserInterface.Windowing;
using Wholist.Common;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.TableParts;

namespace Wholist.UserInterface.Windows.Settings
{
    internal sealed class SettingsWindow : Window
    {
        /// <inheritdoc/>
        public SettingsLogic Logic { get; } = new();

        /// <inheritdoc/>
        public SettingsWindow() : base(Strings.Windows_Settings_Title.Format(Constants.PluginName))
        {
            this.Size = new(700, 450);
            this.SizeCondition = ImGuiCond.FirstUseEver;
            this.Flags = ImGuiWindowFlagExtras.NoScroll;
        }

        /// <inheritdoc/>
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
                    SettingsSidebar.Draw(this.Logic);
                }
                ImGui.EndChild();

                // Listings
                ImGui.TableNextColumn();
                if (ImGui.BeginChild("PluginSettingsListChild"))
                {
                    SettingsActive.Draw(this.Logic);
                }
                ImGui.EndChild();

                ImGui.EndTable();
            }
        }
    }
}