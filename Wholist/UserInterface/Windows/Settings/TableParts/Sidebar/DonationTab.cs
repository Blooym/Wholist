using Dalamud.Utility;
using ImGuiNET;
using Sirensong.UserInterface;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class DonationTab
    {
        /// <summary>
        ///     Draws the donation tab of the settings window.
        /// </summary>
        /// <param name="_"></param>
        internal static void Draw(SettingsLogic _)
        {
            // Description.
            SiGui.TextWrapped(Strings.UserInterface_Settings_Donate_Description);

            // KoFi button.
            if (ImGui.Button(Strings.UserInterface_Settings_Donate_Kofi))
            {
                Util.OpenLink(Constants.Links.KoFi);
            }
            SiGui.AddTooltip(Constants.Links.KoFi);
            ImGui.SameLine();

            // GitHub sponsors button.
            if (ImGui.Button(Strings.UserInterface_Settings_Donate_GitHubSponsors))
            {
                Util.OpenLink(Constants.Links.GitHubSponsors);
            }
            SiGui.AddTooltip(Constants.Links.GitHubSponsors);
        }
    }
}
