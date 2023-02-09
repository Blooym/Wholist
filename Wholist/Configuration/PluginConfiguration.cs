using System;
using Dalamud.Configuration;
using Wholist.Common;

namespace Wholist.Configuration
{
    /// <summary>
    /// Provides access to and determines the Plugin configuration.
    /// </summary>
    [Serializable]
    internal sealed class PluginConfiguration : IPluginConfiguration
    {
        /// <summary>
        /// The current configuration version, incremented on breaking changes.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The configuration for the "Nearby Players" window.
        /// </summary>
        public NearbyPlayersConfiguration NearbyPlayers = new();

        /// <summary>
        /// Saves the current configuration to disk.
        /// </summary>
        internal void Save() => Services.PluginInterface.SavePluginConfig(this);

        /// <summary>
        /// The configuration for the "Nearby Players" window.
        /// </summary>
        internal sealed class NearbyPlayersConfiguration
        {
            /// <summary>
            ///   If the plugin should filter AFK players from the list.
            /// </summary>
            public bool FilterAfk;

            /// <summary>
            /// Automatically open the "Nearby Players" window on login.
            /// </summary>
            public bool OpenOnLogin;

            /// <summary>
            /// Locks the position of the "Nearby Players" window.
            /// </summary>
            public bool LockPosition;

            /// <summary>
            /// Whether to lock the size of the "Nearby Players" window.
            /// </summary>
            public bool LockSize;

            /// <summary>
            /// Whether to hide the "Nearby Players" window in combat.
            /// </summary>
            public bool HideInCombat;

            /// <summary>
            /// Whether to hide the "Nearby Players" window in instances.
            /// </summary>
            public bool HideInInstance;
        }
    }
}
