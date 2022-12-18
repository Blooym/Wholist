using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Wholist.Base;

namespace Wholist.Utils
{
    public static class Objects
    {
        /// <summary>
        ///    Gets all Player Character objects from the ObjectTable.
        /// </summary>
        public static List<PlayerCharacter> PlayerCharacters => PluginService.ObjectTable.Where(o => o is PlayerCharacter).Cast<PlayerCharacter>().ToList();
    }
}
