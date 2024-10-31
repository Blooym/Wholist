using Sirensong.UserInterface;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.NearbyPlayers;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class BehaviourTab
    {
        internal static void Draw(SettingsLogic _)
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown, Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown_Description, ref SettingsLogic.Configuration.NearbyPlayers.PrioritizeKnown);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers, Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
            Slider.Draw(Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers, Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.MaxPlayersToShow, 5, 100);
            EnumCombo.Draw(Strings.UserInterface_Settings_NearbyPlayers_LodestoneRegion, Strings.UserInterface_Settings_NearbyPlayers_LodestoneRegion_Description, ref NearbyPlayersLogic.Configuration.NearbyPlayers.LodestonePlayerSearchRegion);
        }
    }
}
