using System;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.UI.Info;
using Wholist.Common;

namespace Wholist.Game
{
    public sealed unsafe class BlockedCharacterHandler
    {
        [Signature("48 83 EC 48 F6 81 ?? ?? ?? ?? ?? 75 ?? 33 C0 48 83 C4 48")]
        private readonly GetBlockResultTypeDelegate? getBlockResultType = null!;
        private delegate BlockResultType GetBlockResultTypeDelegate(InfoProxyBlacklist* thisPtr, ulong accountId, ulong contentId);

        private enum BlockResultType
        {
            NotBlocked = 1,
            BlockedByAccountId = 2,
            BlockedByContentId = 3,
        }

        public BlockedCharacterHandler() => Services.GameInteropProvider.InitializeFromAttributes(this);

        public bool IsCharacterBlocked(BattleChara* chara)
        {
            if (this.getBlockResultType == null)
            {
                throw new InvalidOperationException("GetBlockResultType signature wasn't found!");
            }

            var infoProxyBlacklist = InfoProxyBlacklist.Instance();
            return this.getBlockResultType(infoProxyBlacklist, chara->Character.AccountId, chara->Character.ContentId) != BlockResultType.NotBlocked;
        }
    }
}
