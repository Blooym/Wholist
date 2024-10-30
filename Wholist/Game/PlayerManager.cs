using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using Sirensong.Game.Enums;
using Wholist.Common;
using Wholist.DataStructures;

namespace Wholist.Game
{
    internal static class PlayerManager
    {
        internal static unsafe IOrderedEnumerable<IPlayerCharacter> GetNearbyPlayers()
            => Services.ObjectTable
                .Where(x => !Services.BlockedCharacterHandler.IsCharacterBlocked((BattleChara*)x.Address))
                .Where(x => x is IPlayerCharacter).Cast<IPlayerCharacter>()
                .Where(x => x.GameObjectId != Services.ClientState.LocalPlayer?.GameObjectId)
                .Where(x => x.GameObjectId > 240)
                .OrderBy(x => x.YalmDistanceX);

        /// <summary>
        ///     Gets nearby players from the <see cref="Dalamud.Game.ClientState.Objects.ObjectTable" /> that meet the given
        ///     criteria and turns them into <see cref="PlayerInfoSlim" />s.
        /// </summary>
        /// <param name="maxPlayers">The maximum number of players to return.</param>
        /// <param name="prioritizeKnownPlayers">Whether to prioritize known players.</param>
        /// <param name="filterAfk">Whether to filter out AFK players.</param>
        /// <returns></returns>
        internal static unsafe List<PlayerInfoSlim> GetNearbyPlayersSlim(int maxPlayers = 100, bool filterAfk = false, bool prioritizeKnownPlayers = false)
        {
            var nearbyPlayers = new List<PlayerInfoSlim>();

            // Get nearby players from the object table and order by them by distance to the local player
            // so that when the list is truncated, the closest players are kept.
            var players = GetNearbyPlayers();

            foreach (var player in players)
            {
                if (nearbyPlayers.Count >= maxPlayers)
                {
                    break;
                }

                if (player.GameObjectId == Services.ClientState.LocalPlayer?.GameObjectId)
                {
                    continue;
                }

                if (player.Level <= 3)
                {
                    continue;
                }

                if (filterAfk && player.OnlineStatus.Id == (byte)OnlineStatusType.Afk)
                {
                    continue;
                }

                nearbyPlayers.Add(new(player));
            }

            // Filter the list alphabetically and prioritize known players if necessary.
            // We sort alphabetically so the list doesn't jump around when players move.
            if (prioritizeKnownPlayers)
            {
                return nearbyPlayers.OrderByDescending(p => p.IsKnownPlayer).ThenBy(p => p.Name).ToList();
            }
            return nearbyPlayers.OrderBy(p => p.Name).ToList();
        }

        /// <summary>
        ///     Checks if the given <see cref="PlayerCharacter" /> is in the party from its ObjectId.
        /// </summary>
        /// <param name="playerCharacter">The <see cref="PlayerCharacter" /> to check.</param>
        /// <returns>True if the <see cref="PlayerCharacter" /> is in the party, otherwise false.</returns>
        internal static bool IsPlayerInParty(IPlayerCharacter playerCharacter)
        {
            foreach (var member in Services.PartyList)
            {
                if (member.ObjectId == playerCharacter.GameObjectId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Gets the colour for the given player.
        /// </summary>
        /// <param name="playerInfo"></param>
        /// <returns>The colour for the player.</returns>
        internal static Vector4 GetPlayerNameColour(PlayerInfoSlim playerInfo)
        {
            if (playerInfo.IsInParty)
            {
                return Services.Configuration.Colours.Name.Party;
            }
            if (playerInfo.IsFriend)
            {
                return Services.Configuration.Colours.Name.Friend;
            }
            return Services.Configuration.Colours.Name.Default;
        }
    }
}
