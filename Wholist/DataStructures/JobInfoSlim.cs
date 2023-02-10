using System.Numerics;
using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;
using Sirensong.Extensions;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;
using Wholist.Common;

namespace Wholist.DataStructures
{
    /// <summary>
    /// Represents a pre-formatted version of a <see cref="ClassJob" /> with slimmed down information.
    /// </summary>
    internal readonly struct JobInfoSlim
    {
        /// <summary>
        /// Creates a new <see cref="JobInfoSlim" />.
        /// </summary>
        /// <param name="classJob">The <see cref="ClassJob" /> to create the <see cref="JobInfoSlim" /> from.</param>
        public JobInfoSlim(ClassJob classJob)
        {
            this.Name = classJob!.Name.ToDalamudString().ToString().ToTitleCase();
            this.RoleColour = classJob.GetJobRole() switch
            {
                ClassJobRole.Tank => Services.Configuration.Colours.Tank,
                ClassJobRole.Healer => Services.Configuration.Colours.Healer,
                ClassJobRole.MeleeDPS => Services.Configuration.Colours.MeleeDPS,
                ClassJobRole.RangedDPS => Services.Configuration.Colours.RangedDPS,
                ClassJobRole.Misc => Services.Configuration.Colours.Other,
                _ => Services.Configuration.Colours.Other,
            };
        }

        /// <summary>
        /// The name of the job.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The colour of the role.
        /// </summary>
        public readonly Vector4 RoleColour;
    }
}
