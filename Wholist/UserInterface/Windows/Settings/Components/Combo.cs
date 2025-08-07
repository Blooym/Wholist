using System;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Utility.Raii;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Common;

namespace Wholist.UserInterface.Windows.Settings.Components
{
    internal static class EnumCombo
    {
        /// <summary>
        ///     Draws a combo.
        /// </summary>
        /// <param name="label">The label of the combo.</param>
        /// <param name="hint">The hint for the combo1.</param>
        /// <param name="current">The current value of the combo.</param>
        /// <returns>Whether the checkbox was updated.</returns>
        internal static bool Draw<T>(string label, string hint, ref T current) where T : notnull, Enum
        {
            var changed = false;
            SiGui.TextWrapped(label);
            using (var combo = ImRaii.Combo($"##{label}", current.ToString()))
            {
                if (combo)
                {
                    foreach (T option in Enum.GetValues(typeof(T)))
                    {
                        if (ImGui.Selectable(option.ToString()))
                        {
                            current = option;
                            Services.Configuration.Save();
                            changed = true;
                        }
                    }
                }
            }
            SiGui.TextDisabledWrapped(hint);
            ImGui.Dummy(Spacing.SectionSpacing);
            return changed;
        }
    }
}
