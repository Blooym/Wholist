using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;
using Wholist.Common;

namespace Wholist.DataStructures
{
    /// <summary>
    /// Represents a pre-formatted version of a <see cref="PlayerCharacter" /> with slimmed down information.
    /// </summary>
    internal readonly struct WorldInfoSlim
    {
        /// <summary>
        /// The name of the world.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The id of the world.
        /// </summary>
        public readonly uint Id;

        /// <summary>
        /// Creates a new <see cref="WorldInfoSlim" />.
        /// </summary>
        /// <param name="world">The <see cref="World" /> to create the <see cref="WorldInfoSlim" /> from.</param>
        public WorldInfoSlim(World world)
        {
            this.Name = Services.WorldCache.GetRow(world.RowId)!.Name.ToDalamudString().ToString();
            this.Id = world.RowId;
        }
    }
}