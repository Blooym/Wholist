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
        /// <param name="logic"></param>
        internal static void Draw(SettingsLogic logic)
        {
            // View options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Window);
            DrawViewOptions(logic);

            // Filtering options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Filtering);
            DrawFilteringOptions(logic);
        }

        /// <summary>
        ///     Draws the view options of the nearby players tab.
        /// </summary>
        /// <param name="_"></param>
        private static void DrawViewOptions(SettingsLogic _)
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin, Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin_Description, ref SettingsLogic.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInCombat, Strings.UserInterface_Settings_NearbyPlayers_HideInCombat_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInInstance, Strings.UserInterface_Settings_NearbyPlayers_HideInInstance_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInInstance);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockPosition, Strings.UserInterface_Settings_NearbyPlayers_LockPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockPosition);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockSize, Strings.UserInterface_Settings_NearbyPlayers_LockSize_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockSize);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations, Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations_Description,
                ref SettingsLogic.Configuration.NearbyPlayers.UseJobAbbreviations);
        }

        /// <summary>
        ///     Draws the filtering options of the nearby players tab.
        /// </summary>
        /// <param name="_"></param>
        private static void DrawFilteringOptions(SettingsLogic _)
        {
            TextWithDescription.Draw(Strings.UserInterface_Settings_NearbyPlayers_ShownRows, Strings.UserInterface_Settings_NearbyPlayers_ShownRows_Description);
            Slider.Draw(Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers, Strings.UserInterface_Settings_NearbyPlayers_MaxPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.MaxPlayersToShow, 5, 100);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers, Strings.UserInterface_Settings_NearbyPlayers_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown, Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown_Description, ref SettingsLogic.Configuration.NearbyPlayers.PrioritizeKnown);
        }
    }
}
