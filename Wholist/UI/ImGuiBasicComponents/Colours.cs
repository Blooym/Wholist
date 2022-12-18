using System.Numerics;
using Dalamud.Interface.Colors;

namespace Wholist.UI.ImGuiBasicComponents
{
    public static class Colours
    {
        public static Vector4 GetColourForRole(byte classID) => classID switch
        {
            // 1 = Tank / Dark-ish Blue
            1 => ImGuiColors.TankBlue,
            // 2 = Melee DPS / Red
            2 => ImGuiColors.DPSRed,
            // 3 = Magic/Ranged DPS / Red
            3 => ImGuiColors.DPSRed,
            // 4 = Healer / Green
            4 => ImGuiColors.HealerGreen,
            // Fallback to White
            _ => ImGuiColors.DalamudGrey
        };
    }
}
