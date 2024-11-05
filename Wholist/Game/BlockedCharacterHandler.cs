using System;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.UI.Info;

namespace Wholist.Game
{
    public sealed unsafe class BlockedCharacterHandler
    {
        /// <summary>
        ///     Whether a character or service account has been blocked by the local player.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public bool IsCharacterBlocked(BattleChara* chara)
        {
            var infoProxyBlacklist = InfoProxyBlacklist.Instance();
            return InfoProxyBlacklist.Instance()->GetBlockResultType(chara->Character.AccountId, chara->Character.ContentId) != InfoProxyBlacklist.BlockResultType.NotBlocked;
        }
    }
}
