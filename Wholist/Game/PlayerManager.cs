using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Sirensong.Caching.Collections;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;
using Wholist.Common;
using Wholist.DataStructures;

namespace Wholist.Game
{
    internal sealed class PlayerManager : IDisposable
    {

        #region Fields

        /// <summary>
        ///     The cache of nearby player information.
        /// </summary>
        private readonly CacheCollection<PlayerCharacter, PlayerInfoSlim> nearbyPlayersCache = new(new CacheOptions<PlayerCharacter, PlayerInfoSlim>
        {
            AbsoluteExpiry = TimeSpan.FromSeconds(4),
            ExpireInterval = TimeSpan.FromSeconds(4),
        });

        private bool disposedValue;

        #endregion

        #region Constructor and Dispose

        /// <summary>
        ///     Creates a new instance of the <see />.
        /// </summary>
        private PlayerManager()
        {

        }

        /// <summary>
        ///     Disposes of the <see cref="PlayerManager" />.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                this.nearbyPlayersCache.Dispose();

                this.disposedValue = true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets nearby players from the <see cref="Dalamud.Game.ClientState.Objects.ObjectTable" /> that meet the given
        ///     criteria and turns them into <see cref="PlayerInfoSlim" />s.
        /// </summary>
        /// <param name="maxPlayers">The maximum number of players to return.</param>
        /// <param name="prioritizeKnownPlayers">Whether to prioritize known players.</param>
        /// <param name="filterAfk">Whether to filter out AFK players.</param>
        /// <returns></returns>
        internal List<PlayerInfoSlim> GetNearbyPlayers(int maxPlayers = 100, bool filterAfk = false, bool prioritizeKnownPlayers = false)
        {
            var nearbyPlayers = new List<PlayerInfoSlim>();

            // Get nearby players from the object table and order by them by distance to the local player
            // so that when the list is truncated, the closest players are kept.
            var nearbyPlayersFiltered = Services.ObjectTable.GetPlayerCharacters(false).OrderBy(p => p.YalmDistanceX);
            foreach (var player in nearbyPlayersFiltered)
            {
                if (nearbyPlayers.Count >= maxPlayers)
                {
                    break;
                }

                if (player.Level < 3)
                {
                    continue;
                }

                if (filterAfk && player.OnlineStatus.Id == (uint)OnlineStatusType.Afk)
                {
                    continue;
                }

                nearbyPlayers.Add(this.GetSlimInfo(player));
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
        ///     Clears the cache of nearby players.
        /// </summary>
        internal void ClearCache() => this.nearbyPlayersCache.Clear();

        /// <summary>
        ///     Gets the <see cref="PlayerInfoSlim" /> for the given <see cref="PlayerCharacter" /> if it exists, otherwise creates
        ///     a new one.
        /// </summary>
        /// <param name="player">The <see cref="PlayerCharacter" /> to get the <see cref="PlayerInfoSlim" /> for.</param>
        /// <returns>The <see cref="PlayerInfoSlim" /> for the given <see cref="PlayerCharacter" />.</returns>
        private PlayerInfoSlim GetSlimInfo(PlayerCharacter player) => this.nearbyPlayersCache.GetOrAdd(player, _ => new PlayerInfoSlim(player));

        /// <summary>
        ///     Checks if the given <see cref="PlayerCharacter" /> is in the party from its ObjectId.
        /// </summary>
        /// <param name="playerCharacter">The <see cref="PlayerCharacter" /> to check.</param>
        /// <returns>True if the <see cref="PlayerCharacter" /> is in the party, otherwise false.</returns>
        internal static bool IsPlayerInParty(PlayerCharacter playerCharacter)
        {
            foreach (var member in Services.PartyList)
            {
                if (member.ObjectId == playerCharacter.ObjectId)
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

        #endregion

    }
}
