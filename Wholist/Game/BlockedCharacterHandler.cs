using System;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.UI.Info;
using Wholist.Common;

namespace Wholist.Game
{
    public static unsafe class BlockedCharacterHandler
    {
        /// <summary>
        ///     Whether a character or service account has been blocked by the local player.
        /// </summary>
        /// <remarks>
        ///     This intentionally always returns false if the local player is in a duty.
        /// </remarks>
        public static bool IsCharacterBlocked(BattleChara* chara)
        {
            try
            {
                return InfoProxyBlacklist.Instance()->GetBlockResultType(chara->Character.AccountId, chara->Character.ContentId) != InfoProxyBlacklist.BlockResultType.NotBlocked;
            }
            catch (Exception e)
            {
                Services.PluginLog.Verbose($"Failed to fetch InfoProxyBlacklist GetBlockResultType: {e}");
                return false;
            }
        }
    }
}
