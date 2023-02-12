using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Configuration;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.NearbyPlayers;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class ColoursTab
    {
        /// <summary>
        ///     Draws the colours tab of the settings window.
        /// </summary>
        /// <param name="_"></param>
        internal static void Draw(SettingsLogic _)
        {
            // Colour notice.
            SiGui.Heading(Strings.UserInterface_Settings_Colours_UpdateTimeNotice_Title);
            SiGui.TextWrapped(Strings.UserInterface_Settings_Colours_UpdateTimeNotice_Text);
            ImGui.Dummy(Spacing.SectionSpacing);

            // Name colour settings.
            SiGui.Heading(Strings.UserInterface_Settings_Colours_NameColours);
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Default, ref NearbyPlayersLogic.Configuration.Colours.Default))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Friend, ref NearbyPlayersLogic.Configuration.Colours.Friend))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Party, ref NearbyPlayersLogic.Configuration.Colours.Party))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            ImGui.Dummy(Spacing.SectionSpacing);

            // Job colours.
            SiGui.Heading(Strings.UserInterface_Settings_Colours_RoleColours);
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_MeleeDPS, ref NearbyPlayersLogic.Configuration.Colours.MeleeDPS))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_RangedDPS, ref NearbyPlayersLogic.Configuration.Colours.RangedDPS))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Healer, ref NearbyPlayersLogic.Configuration.Colours.Healer))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Tank, ref NearbyPlayersLogic.Configuration.Colours.Tank))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Other, ref NearbyPlayersLogic.Configuration.Colours.Other))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            ImGui.Dummy(Spacing.SectionSpacing);

            // Other options.
            SiGui.Heading(Strings.UserInterface_Settings_Colours_OtherOptions);
            if (ImGui.Button(Strings.UserInterface_Settings_Colours_ResetAll))
            {
                NearbyPlayersLogic.Configuration.Colours = new PluginConfiguration.ColourConfiguration();
                NearbyPlayersLogic.Configuration.Save();
            }
        }
    }
}
