using Sirensong.UserInterface;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class NearbyPlayersTab
    {
        /// <summary>
        ///     Draws the nearby players tab of the settings window.
        /// </summary>
        /// <param name="_"></param>
        internal static void Draw(SettingsLogic _)
        {
            // Heading.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Heading);

            // View options.
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin, Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin_Description, ref SettingsLogic.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInCombat, Strings.UserInterface_Settings_NearbyPlayers_HideInCombat_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInInstance, Strings.UserInterface_Settings_NearbyPlayers_HideInInstance_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInInstance);

            // Positioning options.
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockPosition, Strings.UserInterface_Settings_NearbyPlayers_LockPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockPosition);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockSize, Strings.UserInterface_Settings_NearbyPlayers_LockSize_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockSize);

            // Filtering options.
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers, Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
            if (Slider.Draw(Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers, Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.MaxPlayersToShow, 5, 100))
            {
                SettingsLogic.Configuration.Save();
            }
            TextWithDescription.Draw(Strings.UserInterface_Settings_NearbyPlayers_ShownContent, Strings.UserInterface_Settings_NearbyPlayers_ShownContent_Description);
        }
    }
}
