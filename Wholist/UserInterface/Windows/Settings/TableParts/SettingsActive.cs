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
                    SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Appearance);
                    AppearanceTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Window:
                    SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Window);
                    WindowTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Behaviour:
                    SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Behaviour);
                    BehaviourTab.Draw(logic);
                    break;
                default:
                    SiGui.TextDisabledWrapped(Strings.UserInterface_Settings_UnknownTab);
                    break;
            }
        }
    }
}
