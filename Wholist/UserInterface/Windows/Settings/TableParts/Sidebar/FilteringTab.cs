using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class FilteringTab
    {
        internal static void Draw(SettingsLogic logic) => DrawFilteringOptions(logic);

        private static void DrawFilteringOptions(SettingsLogic _)
        {
            TextWithDescription.Draw(Strings.UserInterface_Settings_NearbyPlayers_ShownRows, Strings.UserInterface_Settings_NearbyPlayers_ShownRows_Description);
            Slider.Draw(Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers, Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.MaxPlayersToShow, 5, 100);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers, Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
        }
    }
}
