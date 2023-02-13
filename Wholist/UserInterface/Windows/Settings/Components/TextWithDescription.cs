using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;

namespace Wholist.UserInterface.Windows.Settings.Components
{
    internal static class TextWithDescription
    {
        /// <summary>
        ///     Draws a text with a description below it.
        /// </summary>
        /// <param name="text">The text to draw.</param>
        /// <param name="description">The description to draw.</param>
        internal static void Draw(string text, string description)
        {
            SiGui.TextWithDescription(text, description);
            ImGui.Dummy(Spacing.SectionSpacing);
        }
    }
}
