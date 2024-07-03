using Sirensong.UserInterface;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.TableParts.Sidebar;

namespace Wholist.UserInterface.Windows.Settings.TableParts
{
    internal static class SettingsActive
    {
        /// <summary>
        ///     Draws the active tab of the settings window.
        /// </summary>
        /// <param name="logic"></param>
        internal static void Draw(SettingsLogic logic)
        {
            switch (logic.SelectedTab)
            {
                case SettingsLogic.ConfigurationTabs.Appearance:
                    SiGui.Heading("Appearance");
                    AppearanceTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Filtering:
                    SiGui.Heading("Filtering");
                    FilteringTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Window:
                    SiGui.Heading("Window");
                    WindowTab.Draw(logic);
                    break;
                default:
                    SiGui.TextDisabledWrapped(Strings.UserInterface_Settings_UnknownTab);
                    break;
            }
        }
    }
}
