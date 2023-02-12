using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Common;

namespace Wholist.UserInterface.Windows.Settings.Components
{
    internal static class Checkbox
    {
        /// <summary>
        ///     Draws a checkbox.
        /// </summary>
        /// <param name="label">The label of the checkbox.</param>
        /// <param name="hint">The hint of the checkbox.</param>
        /// <param name="value">The value of the checkbox.</param>
        /// <returns>Whether the checkbox was updated.</returns>
        internal static bool Draw(string label, string hint, ref bool value)
        {
            var checkbox = ImGui.Checkbox(label, ref value);
            if (checkbox)
            {
                Services.Configuration.Save();
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
