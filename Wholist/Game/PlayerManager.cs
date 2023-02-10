using System;
using System.Collections.Generic;
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
        private bool disposedValue;

        /// <summary>
        /// The cache of nearby player information.
        /// </summary>
        private readonly CacheCollection<PlayerCharacter, PlayerInfoSlim> nearbyPlayersCache = new(new CacheOptions<PlayerCharacter, PlayerInfoSlim>()
        {
            AbsoluteExpiry = TimeSpan.FromSeconds(5),
            ExpireInterval = TimeSpan.FromSeconds(5),
        });

        /// <summary>
        /// Creates a new instance of the <see cref="PlayerManager" />.
        /// </summary>
        private PlayerManager()
        {

        }

        /// <summary>
        /// Disposes of the <see cref="PlayerManager" />.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                this.nearbyPlayersCache.Dispose();

                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Gets nearby players from the <see cref="ObjectTable" /> and turns them into <see cref="PlayerInfoSlim" />s.
        /// </summary>
        /// <returns></returns>
        public List<PlayerInfoSlim> GetNearbyPlayers()
        {
            var nearbyPlayers = new List<PlayerInfoSlim>();
            foreach (var player in Services.ObjectTable.GetPlayerCharacters(false))
            {
                if (player.Level > 3)
                {
                    nearbyPlayers.Add(this.GetSlimInfo(player));
                }
            }
            return nearbyPlayers;
        }

        /// <summary>
        /// Gets the <see cref="PlayerInfoSlim" /> for the given <see cref="PlayerCharacter" /> if it exists, otherwise creates a new one.
        /// </summary>
        /// <param name="player">The <see cref="PlayerCharacter" /> to get the <see cref="PlayerInfoSlim" /> for.</param>
        /// <returns>The <see cref="PlayerInfoSlim" /> for the given <see cref="PlayerCharacter" />.</returns>
        public PlayerInfoSlim GetSlimInfo(PlayerCharacter player) => this.nearbyPlayersCache.GetOrAdd(player, value => new PlayerInfoSlim(player));

        /// <summary>
        /// Checks if the given <see cref="PlayerCharacter" /> is in the party from its ObjectId.
        /// </summary>
        /// <param name="player">The <see cref="PlayerCharacter" /> to check.</param>
        /// <returns>True if the <see cref="PlayerCharacter" /> is in the party, otherwise false.</returns>
        public static bool IsPlayerInParty(PlayerCharacter playerCharacter)
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
        /// Checks if the given <see cref="PlayerCharacter" /> is on the friendlist.
        /// </summary>
        /// <param name="player">The <see cref="PlayerCharacter" /> to check.</param>
        /// <returns>True if the <see cref="PlayerCharacter" /> is on the friendlist, otherwise false.</returns>
        public static bool IsPlayerOnFriendlist(PlayerCharacter player)
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
        /// Gets the colour for the given player.
        /// </summary>
        /// <param name="playerInfo"></param>
        /// <returns>The colour for the player.</returns>
        internal static Vector4 GetColourForPlayer(PlayerInfoSlim playerInfo)
        {
            if (playerInfo.IsInParty)
            {
                return Services.Configuration.Colours.Party;
            }
            else if (playerInfo.IsFriend)
            {
                return Services.Configuration.Colours.Friend;
            }
            else
            {
                return Services.Configuration.Colours.Default;
            }
        }
    }
}
