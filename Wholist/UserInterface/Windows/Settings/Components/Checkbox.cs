using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;

namespace Wholist.UserInterface.Windows.Settings.Components
{
    internal static class Checkbox
    {
        public static bool Draw(string label, string hint, ref bool value)
        {
            var checkbox = ImGui.Checkbox(label, ref value);
            if (checkbox)
            {
                SettingsLogic.Configuration.Save();
            }

            if (!string.IsNullOrEmpty(hint))
            {
                SiGui.TextDisabledWrapped(hint);
            }
            ImGui.Dummy(Spacing.SectionSpacing);

            return checkbox;
        }
    }
}