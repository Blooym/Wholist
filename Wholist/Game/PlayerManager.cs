using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Sirensong.Caching.Collections;
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
            AbsoluteExpiry = TimeSpan.FromSeconds(6), ExpireInterval = TimeSpan.FromSeconds(6),
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
        ///     Gets nearby players from the <see cref="Dalamud.Game.ClientState.Objects.ObjectTable" /> and turns them into
        ///     <see cref="PlayerInfoSlim" />s.
        /// </summary>
        /// <returns></returns>
        internal List<PlayerInfoSlim> GetNearbyPlayers(int maxPlayers = 100)
        {
            var nearbyPlayers = new List<PlayerInfoSlim>();
            foreach (var player in Services.ObjectTable.GetPlayerCharacters(false).Take(maxPlayers))
            {
                if (player.Level > 3)
                {
                    nearbyPlayers.Add(this.GetSlimInfo(player));
                }
            }
            return nearbyPlayers;
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
        ///     Checks if the given <see cref="PlayerCharacter" /> is on the friend-list.
        /// </summary>
        /// <param name="player">The <see cref="PlayerCharacter" /> to check.</param>
        /// <returns>True if the <see cref="PlayerCharacter" /> is on the friend-list, otherwise false.</returns>
        internal static bool IsPlayerFriend(PlayerCharacter player)
        {
            foreach (var friend in Services.XivCommon.Functions.FriendList.List)
            {
                if (friend.Name.TextValue == player.Name.TextValue && friend.HomeWorld == player.HomeWorld.Id)
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
        internal static Vector4 GetColourForPlayer(PlayerInfoSlim playerInfo)
        {
            if (playerInfo.IsInParty)
            {
                return Services.Configuration.Colours.Party;
            }
            if (playerInfo.IsFriend)
            {
                return Services.Configuration.Colours.Friend;
            }
            return Services.Configuration.Colours.Default;
        }

        #endregion

    }
}
