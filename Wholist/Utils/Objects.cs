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

        /// <summary>
        ///   Gets all Player Characters objects from the ObjectTable that match the bot filter.
        /// </summary>
        /// // if lvl 2 or 3 and the abbreviation is MRD
        public static List<PlayerCharacter> BotPlayerCharacters => PlayerCharacters.Where(o => o == (o.Level <= 3 && o.ClassJob.GameData?.Abbreviation.Equals("MRD") == false)).ToList();
    }
}
