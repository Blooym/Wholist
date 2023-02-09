using Dalamud.Utility;
using ImGuiNET;
using Sirensong.UserInterface;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal sealed class DonationTab
    {
        public static void Draw(SettingsLogic _)
        {
            SiGui.Heading(Strings.UserInterface_Settings_Donate_Heading);
            SiGui.TextWrapped(Strings.UserInterface_Settings_Donate_Description);

            if (ImGui.Button(Strings.UserInterface_Settings_Donate_Kofi))
            {
                Util.OpenLink(Constants.Links.KoFi);
            }
            SiGui.TooltipLast(Constants.Links.KoFi);
            ImGui.SameLine();

            if (ImGui.Button(Strings.UserInterface_Settings_Donate_GitHubSponsors))
            {
                Util.OpenLink(Constants.Links.GitHubSponsors);
            }
            SiGui.TooltipLast(Constants.Links.GitHubSponsors);
        }
    }
}