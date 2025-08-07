using Dalamud.Bindings.ImGui;
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
            var checkbox = SiGui.Checkbox(label, hint, ref value);
            if (checkbox)
            {
                Services.Configuration.Save();
            }
            ImGui.Dummy(Spacing.SectionSpacing);
            return checkbox;
        }
    }
}
