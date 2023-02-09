using System.Numerics;
using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;
using Sirensong.Extensions;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;

namespace Wholist.DataStructures
{
    /// <summary>
    /// Represents a pre-formatted version of a <see cref="ClassJob" /> with slimmed down information.
    /// </summary>
    internal readonly struct ClassInfoSlim
    {
        /// <summary>
        /// The name of the class.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The role colour of the class.
        /// </summary>
        public readonly Vector4 RoleColour;

        /// <summary>
        /// Creates a new <see cref="ClassInfoSlim" />.
        /// </summary>
        /// <param name="classJob">The <see cref="ClassJob" /> to create the <see cref="ClassInfoSlim" /> from.</param>
        public ClassInfoSlim(ClassJob classJob)
        {
            this.Name = classJob!.Name.ToDalamudString().ToString().ToTitleCase();
            this.RoleColour = classJob.GetJobRole().GetColourForRole();
        }
    }
}