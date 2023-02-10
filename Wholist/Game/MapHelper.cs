using System.Numerics;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace Wholist.Game
{
    internal static class MapHelper
    {
        /// <summary>
        /// Places a flag on the map at the specified position and opens the map.
        /// </summary>
        /// <param name="position">The position to place the flag.</param>
        /// <param name="title">The title of the flag.</param>
        /// <param name="type">The type of flag to place.</param>
        public static unsafe void FlagAndOpen(Vector3 position, string? title = null, MapType type = MapType.FlagMarker)
        {
            var agent = AgentMap.Instance();
            agent->SetFlagMapMarker(agent->CurrentTerritoryId, agent->CurrentMapId, position);
            agent->OpenMap(agent->CurrentMapId, agent->CurrentTerritoryId, title, type);
        }
    }
}
