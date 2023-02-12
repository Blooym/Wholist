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
        internal static void Draw(SettingsLogic logic)
        {
            // View options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Window);
            DrawViewOptions(logic);

            // Filtering options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Filtering);
            DrawFilteringOptions(logic);
        }

        private static void DrawViewOptions(SettingsLogic _)
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin, Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin_Description, ref SettingsLogic.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInCombat, Strings.UserInterface_Settings_NearbyPlayers_HideInCombat_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInInstance, Strings.UserInterface_Settings_NearbyPlayers_HideInInstance_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInInstance);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockPosition, Strings.UserInterface_Settings_NearbyPlayers_LockPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockPosition);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockSize, Strings.UserInterface_Settings_NearbyPlayers_LockSize_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockSize);
        }

        private static void DrawFilteringOptions(SettingsLogic _)
        {
            Slider.Draw(Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers, Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.MaxPlayersToShow, 5, 100);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers, Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
            TextWithDescription.Draw(Strings.UserInterface_Settings_NearbyPlayers_ShownRows, Strings.UserInterface_Settings_NearbyPlayers_ShownRows_Description);
        }
    }
}
