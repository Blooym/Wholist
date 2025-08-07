using System;
using Dalamud.Bindings.ImGui;
using Sirensong.UserInterface;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings.TableParts
{
    internal static class SettingsSidebar
    {
        /// <summary>
        ///     Draws the sidebar of the settings window.
        /// </summary>
        /// <param name="logic"></param>
        internal static void Draw(SettingsLogic logic)
        {
            SiGui.Heading(Strings.UserInterface_Settings_Heading);

            foreach (var tab in Enum.GetValues<SettingsLogic.ConfigurationTabs>())
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
