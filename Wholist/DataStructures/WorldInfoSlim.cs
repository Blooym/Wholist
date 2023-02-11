using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;

namespace Wholist.DataStructures
{
    /// <summary>
    ///     Represents a pre-formatted version of a <see cref="PlayerCharacter" /> with slimmed down information.
    /// </summary>
    internal readonly struct WorldInfoSlim
    {
        /// <summary>
        ///     Creates a new <see cref="WorldInfoSlim" />.
        /// </summary>
        /// <param name="world">The <see cref="World" /> to create the <see cref="WorldInfoSlim" /> from.</param>
        internal WorldInfoSlim(World world)
        {
            this.Name = world.Name.ToDalamudString().ToString();
            this.Id = world.RowId;
        }

        /// <summary>
        ///     The name of the world.
        /// </summary>
        internal readonly string Name;

        /// <summary>
        ///     The id of the world.
        /// </summary>
        internal readonly uint Id;
    }
}
