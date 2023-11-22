using ImGuiNET;
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
            DrawWindowOptions(logic);

            // Appearance options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Appearance);
            DrawAppearanceOptions(logic);

            // Filtering options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Filtering);
            DrawFilteringOptions(logic);
        }

        /// <summary>
        ///     Draws the view options of the nearby players tab.
        /// </summary>
        /// <param name="_"></param>
        private static void DrawWindowOptions(SettingsLogic _)
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin, Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin_Description, ref SettingsLogic.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInCombat, Strings.UserInterface_Settings_NearbyPlayers_HideInCombat_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInInstance, Strings.UserInterface_Settings_NearbyPlayers_HideInInstance_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInInstance);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockPosition, Strings.UserInterface_Settings_NearbyPlayers_LockPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockPosition);
            if (SettingsLogic.Configuration.NearbyPlayers.LockPosition)
            {
                ImGui.Indent();
                Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_SpecifiedPosition, Strings.UserInterface_Settings_NearbyPlayers_SpecifiedPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.SetWindowPosition);

                if (SettingsLogic.Configuration.NearbyPlayers.SetWindowPosition)
                {
                    ImGui.Indent();
                    Slider.Draw(
                        Strings.UserInterface_Settings_NearbyPlayers_WindowPositionX,
                        Strings.UserInterface_Settings_NearbyPlayers_WindowPositionX_Description,
                        ref SettingsLogic.Configuration.NearbyPlayers.WindowPositionX,
                        0 - 200,
                        (int)ImGui.GetIO().DisplaySize.X + 200
                        );
                    Slider.Draw(
                        Strings.UserInterface_Settings_NearbyPlayers_WindowPositionY,
                        Strings.UserInterface_Settings_NearbyPlayers_WindowPositionY_Description,
                        ref SettingsLogic.Configuration.NearbyPlayers.WindowPositionY,
                        0 - 200,
                        (int)ImGui.GetIO().DisplaySize.Y + 200
                        );
                    ImGui.Unindent();
                }

                ImGui.Unindent();
            }
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockSize, Strings.UserInterface_Settings_NearbyPlayers_LockSize_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockSize);
        }

        /// <summary>
        ///     Draws the appearance options of the nearby players tab.
        /// </summary>
        /// <param name="_"></param>
        private static void DrawAppearanceOptions(SettingsLogic _)
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_MinimalMode, Strings.UserInterface_Settings_NearbyPlayers_MinimalMode_Description,
    ref SettingsLogic.Configuration.NearbyPlayers.MinimalMode);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations, Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations_Description,
            ref SettingsLogic.Configuration.NearbyPlayers.UseJobAbbreviations);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown, Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown_Description, ref SettingsLogic.Configuration.NearbyPlayers.PrioritizeKnown);
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
        }
    }
}
