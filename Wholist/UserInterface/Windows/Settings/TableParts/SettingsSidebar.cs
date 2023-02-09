using System;
using ImGuiNET;
using Sirensong.UserInterface;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings.TableParts
{
    internal sealed class SettingsSidebar
    {
        public static void Draw(SettingsLogic logic)
        {
            SiGui.Heading(Strings.UserInterface_Settings_Heading);

            foreach (var tab in Enum.GetValues(typeof(SettingsLogic.ConfigurationTabs)))
            {
                if (tab is SettingsLogic.ConfigurationTabs configurationTab)
                {
                    if (ImGui.Selectable(SettingsLogic.ConfigTabName(configurationTab), logic.SelectedTab == configurationTab))
                    {
                        logic.SelectedTab = configurationTab;
                    }
                }
            }
        }
    }
}