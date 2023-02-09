using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal sealed class DebugTab
    {
        public static void Draw(SettingsLogic _)
        {
            SiGui.Heading(Strings.UserInterface_Settings_Debug_Heading);
            SiGui.TextWrapped(Strings.UserInterface_Settings_Debug_Disclaimer);
            ImGui.Dummy(Spacing.CollapsibleHeaderSpacing);

            if (ImGui.Button(Strings.UserInterface_Settings_Debug_Copy))
            {
                Services.Clipboard.Copy(Constants.Build.DebugString);
            }
            ImGui.Dummy(Spacing.SectionSpacing);

            SiGui.Heading(Strings.UserInterface_Settings_Debug_Info);
            if (ImGui.BeginChild("DebugInformation"))
            {
                SiGui.TextWrapped(Constants.Build.DebugString);
            };

            ImGui.EndChild();
        }
    }
}