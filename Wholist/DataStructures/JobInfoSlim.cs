using System.Numerics;
using Lumina.Excel.GeneratedSheets;
using Sirensong.Extensions;
using Sirensong.Game.Enums;
using Wholist.Common;

namespace Wholist.DataStructures
{
    /// <summary>
    ///     Represents a pre-formatted version of a <see cref="ClassJob" /> with slimmed down information.
    /// </summary>
    internal readonly struct JobInfoSlim
    {
        /// <summary>
        ///     Creates a new <see cref="JobInfoSlim" />.
        /// </summary>
        /// <param name="cj">The <see cref="ClassJob" /> to create the <see cref="JobInfoSlim" /> from.</param>
        internal JobInfoSlim(byte cj)
        {
            // Cache lookups
            var classJob = Services.ClassJobCache.GetRow(cj)!;
            if (!Services.ClassJobNames.TryGetValue(cj, out var name))
            {
                name = classJob.Name.RawString.ToTitleCase();
                Services.ClassJobNames.TryAdd(cj, name);
            }
            this.Name = name;
            if (!Services.ClassJobAbbreviations.TryGetValue(cj, out var abriv))
            {
                abriv = classJob.Abbreviation.RawString.ToUpperInvariant();
                Services.ClassJobAbbreviations.TryAdd(cj, abriv);
            }
            this.Abbreviation = abriv;

            this.RoleColour = classJob.GetJobRole() switch
            {
                ClassJobRole.Tank => Services.Configuration.Colours.Role.Tank,
                ClassJobRole.Healer => Services.Configuration.Colours.Role.Healer,
                ClassJobRole.MeleeDps => Services.Configuration.Colours.Role.MeleeDps,
                ClassJobRole.RangedDps => Services.Configuration.Colours.Role.RangedDps,
                ClassJobRole.Misc => Services.Configuration.Colours.Role.Other,
                _ => Services.Configuration.Colours.Role.Other,
            };

            this.JobColour = classJob.RowId switch
            {

                0 => Services.Configuration.Colours.Job.Other,
                1 => Services.Configuration.Colours.Job.Paladin,
                2 => Services.Configuration.Colours.Job.Monk,
                3 => Services.Configuration.Colours.Job.Warrior,
                4 => Services.Configuration.Colours.Job.Dragoon,
                5 => Services.Configuration.Colours.Job.Bard,
                6 => Services.Configuration.Colours.Job.WhiteMage,
                7 => Services.Configuration.Colours.Job.BlackMage,
                19 => Services.Configuration.Colours.Job.Paladin,
                20 => Services.Configuration.Colours.Job.Monk,
                21 => Services.Configuration.Colours.Job.Warrior,
                22 => Services.Configuration.Colours.Job.Dragoon,
                23 => Services.Configuration.Colours.Job.Bard,
                24 => Services.Configuration.Colours.Job.WhiteMage,
                25 => Services.Configuration.Colours.Job.BlackMage,
                26 => Services.Configuration.Colours.Job.Summoner,
                27 => Services.Configuration.Colours.Job.Summoner,
                28 => Services.Configuration.Colours.Job.Summoner,
                29 => Services.Configuration.Colours.Job.Ninja,
                30 => Services.Configuration.Colours.Job.Ninja,
                31 => Services.Configuration.Colours.Job.Machinist,
                32 => Services.Configuration.Colours.Job.DarkKnight,
                33 => Services.Configuration.Colours.Job.Astrologian,
                34 => Services.Configuration.Colours.Job.Samurai,
                35 => Services.Configuration.Colours.Job.RedMage,
                36 => Services.Configuration.Colours.Job.BlueMage,
                37 => Services.Configuration.Colours.Job.Gunbreaker,
                38 => Services.Configuration.Colours.Job.Dancer,
                39 => Services.Configuration.Colours.Job.Reaper,
                40 => Services.Configuration.Colours.Job.Sage,
                _ => Services.Configuration.Colours.Job.Other,
            };
        }

        /// <summary>
        ///     The name of the job.
        /// </summary>
        internal readonly string Name;

        /// <summary>
        ///     The abbreviation of the job.
        /// </summary>
        internal readonly string Abbreviation;

        /// <summary>
        ///     The colour of the role.
        /// </summary>
        internal readonly Vector4 RoleColour;

        /// <summary>
        ///     The colour of the job.
        /// </summary>
        internal readonly Vector4 JobColour;
    }
}
