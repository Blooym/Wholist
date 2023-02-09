using System;
using System.Collections.Generic;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Sirensong.Caching.Collections;
using Sirensong.Game.Extensions;
using Wholist.Common;
using Wholist.DataStructures;

namespace Wholist.ObjectManagement
{
    internal sealed class PlayerManager : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// The cache of nearby player information.
        /// </summary>
        private readonly CacheCollection<PlayerCharacter, PlayerInfoSlim> nearbyPlayersCache = new(new CacheOptions<PlayerCharacter, PlayerInfoSlim>()
        {
            AbsoluteExpiry = TimeSpan.FromSeconds(3),
            ExpireInterval = TimeSpan.FromSeconds(1),
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
    }
}