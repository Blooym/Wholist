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
            SiGui.Heading(Strings.UserInterface_Settings_Display_Heading);
            DrawDisplayOptions(logic);

            SiGui.Heading(Strings.UserInterface_Settings_Colours_Heading);
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
        private static void DrawDisplayOptions(SettingsLogic _)
        {
            TextWithDescription.Draw(Strings.UserInterface_Settings_NearbyPlayers_ShownRows, Strings.UserInterface_Settings_NearbyPlayers_ShownRows_Description);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations, Strings.UserInterface_Settings_NearbyPlayers_JobAbbreviations_Description, ref SettingsLogic.Configuration.NearbyPlayers.UseJobAbbreviations);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_ShowSearchbar, Strings.UserInterface_Settings_NearbyPlayers_ShowSearchbar_Description, ref SettingsLogic.Configuration.NearbyPlayers.ShowSearchBar);
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
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Paladin, ref NearbyPlayersLogic.Configuration.Colours.Job.Paladin))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Warrior, ref NearbyPlayersLogic.Configuration.Colours.Job.Warrior))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_DarkKnight, ref NearbyPlayersLogic.Configuration.Colours.Job.DarkKnight))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Gunbreaker, ref NearbyPlayersLogic.Configuration.Colours.Job.Gunbreaker))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Healers
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_WhiteMage, ref NearbyPlayersLogic.Configuration.Colours.Job.WhiteMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Scholar, ref NearbyPlayersLogic.Configuration.Colours.Job.Scholar))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Astrologian, ref NearbyPlayersLogic.Configuration.Colours.Job.Astrologian))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Sage, ref NearbyPlayersLogic.Configuration.Colours.Job.Sage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Melee DPS
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Monk, ref NearbyPlayersLogic.Configuration.Colours.Job.Monk))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Dragoon, ref NearbyPlayersLogic.Configuration.Colours.Job.Dragoon))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Ninja, ref NearbyPlayersLogic.Configuration.Colours.Job.Ninja))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Samurai, ref NearbyPlayersLogic.Configuration.Colours.Job.Samurai))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Reaper, ref NearbyPlayersLogic.Configuration.Colours.Job.Reaper))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Viper, ref NearbyPlayersLogic.Configuration.Colours.Job.Viper))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            /// Ranged DPS
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Bard, ref NearbyPlayersLogic.Configuration.Colours.Job.Bard))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Machinist, ref NearbyPlayersLogic.Configuration.Colours.Job.Machinist))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Dancer, ref NearbyPlayersLogic.Configuration.Colours.Job.Dancer))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Casters
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_BlackMage, ref NearbyPlayersLogic.Configuration.Colours.Job.BlackMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Summoner, ref NearbyPlayersLogic.Configuration.Colours.Job.Summoner))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_RedMage, ref NearbyPlayersLogic.Configuration.Colours.Job.RedMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Pictomancer, ref NearbyPlayersLogic.Configuration.Colours.Job.Pictomancer))
            {
                NearbyPlayersLogic.Configuration.Save();
            }
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_BlueMage, ref NearbyPlayersLogic.Configuration.Colours.Job.BlueMage))
            {
                NearbyPlayersLogic.Configuration.Save();
            }

            // Other
            if (ColourEdit.Draw(Strings.UserInterface_Settings_NearbyPlayers_Job_Other, ref NearbyPlayersLogic.Configuration.Colours.Job.Other))
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
