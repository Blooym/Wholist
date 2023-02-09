using Wholist.UserInterface.Windows.Settings.TableParts.Sidebar;

namespace Wholist.UserInterface.Windows.Settings.TableParts
{
    internal sealed class SettingsActive
    {
        public static void Draw(SettingsLogic logic)
        {
            switch (logic.SelectedTab)
            {
                case SettingsLogic.ConfigurationTabs.General:
                    GeneralTab.Draw(logic);
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