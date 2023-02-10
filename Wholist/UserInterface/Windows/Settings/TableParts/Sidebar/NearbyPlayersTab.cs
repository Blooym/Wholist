using Sirensong.UserInterface;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal sealed class NearbyPlayersTab
    {
        /// <summary>
        /// Draws the nearby players tab of the settings window.
        /// </summary>
        /// <param name="_"></param>
        public static void Draw(SettingsLogic _)
        {
            // Heading.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Heading);

            // View checkboxes.
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin, Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin_Description, ref SettingsLogic.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInCombat, Strings.UserInterface_Settings_NearbyPlayers_HideInCombat_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInInstance, Strings.UserInterface_Settings_NearbyPlayers_HideInInstance_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInInstance);

            // Positioning checkboxes.
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockPosition, Strings.UserInterface_Settings_NearbyPlayers_LockPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockPosition);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockSize, Strings.UserInterface_Settings_NearbyPlayers_LockSize_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockSize);

            // Filtering checkboxes.
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers, Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
            if (Slider.Draw(Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers, Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.MaxPlayersToShow, 5, 100))
            {
                SettingsLogic.Configuration.Save();
            }
        }
    }
}
