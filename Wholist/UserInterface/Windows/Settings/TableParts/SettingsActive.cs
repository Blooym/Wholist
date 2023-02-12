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
                case SettingsLogic.ConfigurationTabs.NearbyPlayers:
                    SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Heading);
                    NearbyPlayersTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Colours:
                    SiGui.Heading(Strings.UserInterface_Settings_Colours_Heading);
                    ColoursTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Donation:
                    SiGui.Heading(Strings.UserInterface_Settings_Donate_Heading);
                    DonationTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Debug:
                    SiGui.Heading(Strings.UserInterface_Settings_Debug_Heading);
                    DebugTab.Draw(logic);
                    break;
                default:
                    SiGui.TextDisabledWrapped(Strings.UserInterface_Settings_UnknownTab);
                    break;
            }
        }
    }
}
