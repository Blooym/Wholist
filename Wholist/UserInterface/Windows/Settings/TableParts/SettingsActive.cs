using Wholist.UserInterface.Windows.Settings.TableParts.Sidebar;

namespace Wholist.UserInterface.Windows.Settings.TableParts
{
    internal static class SettingsActive
    {
        /// <summary>
        /// Draws the active tab of the settings window.
        /// </summary>
        /// <param name="logic"></param>
        internal static void Draw(SettingsLogic logic)
        {
            switch (logic.SelectedTab)
            {
                case SettingsLogic.ConfigurationTabs.NearbyPlayers:
                    NearbyPlayersTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Colours:
                    ColoursTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Donation:
                    DonationTab.Draw(logic);
                    break;
                case SettingsLogic.ConfigurationTabs.Debug:
                    DebugTab.Draw(logic);
                    break;
                default:
                    break;
            }
        }
    }
}
