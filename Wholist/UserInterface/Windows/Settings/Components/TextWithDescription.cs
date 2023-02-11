using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;

namespace Wholist.UserInterface.Windows.Settings.Components
{
    internal static class TextWithDescription
    {
        internal static void Draw(string text, string description)
        {
            SiGui.Text(text);
            SiGui.TextDisabledWrapped(description);
            ImGui.Dummy(Spacing.SectionSpacing);
        }
    }
}
