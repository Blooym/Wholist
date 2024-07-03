using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Configuration;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.NearbyPlayers;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class AppearanceTab
    {
        internal static void Draw(SettingsLogic logic)
        {
            SiGui.TextWrapped("Modify the appearance of the nearby players list.");
            ImGui.Dummy(Spacing.SectionSpacing);

            // Appearance options.
            SiGui.Heading(Strings.UserInterface_Settings_NearbyPlayers_Appearance);
            DrawAppearanceOptions(logic);

            SiGui.Heading("Colours");
            SiGui.TextDisabled(Strings.UserInterface_Settings_Colours_NameColours);
            DrawNameColours(logic);
            ImGui.Dummy(Spacing.SectionSpacing);
            SiGui.TextDisabled(Strings.UserInterface_Settings_Colours_JobColours);
            var isPerJobColours = NearbyPlayersLogic.Configuration.Colours.JobColMode == PluginConfiguration.ColourConfiguration.JobColourMode.Job;
            if (Checkbox.Draw(Strings.UserInterface_Settings_Colours_UsePerJobColours, Strings.UserInterface_Settings_Colours_UsePerJobColours_Description, ref isPerJobColours))
            {
                NearbyPlayersLogic.Configuration.Colours.JobColMode = isPerJobColours ? PluginConfiguration.ColourConfiguration.JobColourMode.Job : PluginConfiguration.ColourConfiguration.JobColourMode.Role;
                NearbyPlayersLogic.Configuration.Save();
            }
            if (isPerJobColours)
            {
                DrawJobColours(logic);
            }
            else
            {
                DrawRoleColours(logic);
            }
            ImGui.Dummy(Spacing.SectionSpacing);
            DrawOtherColourOptions(logic);
        }
        private static void DrawAppearanceOptions(SettingsLogic _)
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown, Strings.UserInterface_Settings_NearbyPlayers_PrioritizeKnown_Description, ref SettingsLogic.Configuration.NearbyPlayers.PrioritizeKnown);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations, Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations_Description, ref SettingsLogic.Configuration.NearbyPlayers.UseJobAbbreviations);
            Checkbox.Draw("Show Searchbar", "Show the searchbar from the nearby players list.", ref SettingsLogic.Configuration.NearbyPlayers.ShowSearchBar);
        }
        private static void DrawNameColours(SettingsLogic _)
        {
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Default, ref NearbyPlayersLogic.Configuration.Colours.Name.Default))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Friend, ref NearbyPlayersLogic.Configuration.Colours.Name.Friend))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Party, ref NearbyPlayersLogic.Configuration.Colours.Name.Party))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
        }

        private static void DrawRoleColours(SettingsLogic _)
        {
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Healer, ref NearbyPlayersLogic.Configuration.Colours.Role.Healer))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Tank, ref NearbyPlayersLogic.Configuration.Colours.Role.Tank))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_MeleeDPS, ref NearbyPlayersLogic.Configuration.Colours.Role.MeleeDps))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_RangedDPS, ref NearbyPlayersLogic.Configuration.Colours.Role.RangedDps))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_Colours_Other, ref NearbyPlayersLogic.Configuration.Colours.Role.Other))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
        }
        private static void DrawJobColours(SettingsLogic _)
        {
            // Tanks
            if (ColourEdit.Draw("Paladin", ref NearbyPlayersLogic.Configuration.Colours.Job.Paladin))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Warrior", ref NearbyPlayersLogic.Configuration.Colours.Job.Warrior))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Dark Knight", ref NearbyPlayersLogic.Configuration.Colours.Job.DarkKnight))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Gunbreaker", ref NearbyPlayersLogic.Configuration.Colours.Job.Gunbreaker))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Healers
            if (ColourEdit.Draw("White Mage", ref NearbyPlayersLogic.Configuration.Colours.Job.WhiteMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Scholar", ref NearbyPlayersLogic.Configuration.Colours.Job.Scholar))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Astrologian", ref NearbyPlayersLogic.Configuration.Colours.Job.Astrologian))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Sage", ref NearbyPlayersLogic.Configuration.Colours.Job.Sage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Melee DPS
            if (ColourEdit.Draw("Monk", ref NearbyPlayersLogic.Configuration.Colours.Job.Monk))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Dragoon", ref NearbyPlayersLogic.Configuration.Colours.Job.Dragoon))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Ninja", ref NearbyPlayersLogic.Configuration.Colours.Job.Ninja))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Samurai", ref NearbyPlayersLogic.Configuration.Colours.Job.Samurai))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Reaper", ref NearbyPlayersLogic.Configuration.Colours.Job.Reaper))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Viper", ref NearbyPlayersLogic.Configuration.Colours.Job.Viper))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            /// Ranged DPS
            if (ColourEdit.Draw("Bard", ref NearbyPlayersLogic.Configuration.Colours.Job.Bard))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Machinist", ref NearbyPlayersLogic.Configuration.Colours.Job.Machinist))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Dancer", ref NearbyPlayersLogic.Configuration.Colours.Job.Dancer))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Casters
            if (ColourEdit.Draw("Black Mage", ref NearbyPlayersLogic.Configuration.Colours.Job.BlackMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Summoner", ref NearbyPlayersLogic.Configuration.Colours.Job.Summoner))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Red Mage", ref NearbyPlayersLogic.Configuration.Colours.Job.RedMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Pictomancer", ref NearbyPlayersLogic.Configuration.Colours.Job.Pictomancer))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw("Blue Mage", ref NearbyPlayersLogic.Configuration.Colours.Job.BlueMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Other
            if (ColourEdit.Draw("Other", ref NearbyPlayersLogic.Configuration.Colours.Job.Other))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
        }

        private static void DrawOtherColourOptions(SettingsLogic _)
        {
            if (ImGui.Button(Strings.UserInterface_Settings_Colours_ResetAll))
            {
                NearbyPlayersLogic.Configuration.Colours = new PluginConfiguration.ColourConfiguration();
                NearbyPlayersLogic.Configuration.Save();
            }
        }
    }
}
