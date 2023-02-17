using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class DebugTab
    {
        /// <summary>
        ///     Draws the debug tab of the settings window.
        /// </summary>
        /// <param name="logic"></param>
        internal static void Draw(SettingsLogic logic)
        {
            // Disclaimer.
            SiGui.TextWrapped(Strings.UserInterface_Settings_Debug_Disclaimer);
            ImGui.Dummy(Spacing.SectionSpacing);

            // Copy button.
            if (ImGui.Button(Strings.UserInterface_Settings_Debug_Copy))
            {
                ImGui.SetClipboardText(Constants.Build.DebugString);
            }
            ImGui.Dummy(Spacing.SectionSpacing);

            // Debug info preview.
            SiGui.Heading(Strings.UserInterface_Settings_Debug_Info);
            DrawDebugPreview(logic);
        }

        /// <summary>
        ///     Draws the debug information preview.
        /// </summary>
        /// <param name="_"></param>
        private static void DrawDebugPreview(SettingsLogic _)
        {
            if (ImGui.BeginChild("DebugInformation"))
            {
                SiGui.TextWrapped(Constants.Build.DebugString);
            }

            ImGui.EndChild();
        }
    }
}
