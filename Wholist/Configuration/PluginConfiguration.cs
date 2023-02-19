using System;
using System.Numerics;
using Dalamud.Configuration;
using Wholist.Common;

namespace Wholist.Configuration
{
    /// <summary>
    ///     Provides access to and determines the Plugin configuration.
    /// </summary>
    [Serializable]
    internal sealed class PluginConfiguration : IPluginConfiguration
    {
        /// <summary>
        ///     The configuration for colours used in the plugin.
        /// </summary>
        public ColourConfiguration Colours = new();

        /// <summary>
        ///     The configuration for the "Nearby Players" window.
        /// </summary>
        public NearbyPlayersConfiguration NearbyPlayers = new();

        /// <summary>
        ///     The current configuration version, incremented on breaking changes.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     Saves the current configuration to disk.
        /// </summary>
        internal void Save() => Services.PluginInterface.SavePluginConfig(this);

        /// <summary>
        ///     The configuration for the "Nearby Players" window.
        /// </summary>
        internal sealed class NearbyPlayersConfiguration
        {
            /// <summary>
            ///     If the plugin should filter AFK players from the list.
            /// </summary>
            public bool FilterAfk;

            /// <summary>
            ///     Whether to hide the "Nearby Players" window in combat.
            /// </summary>
            public bool HideInCombat;

            /// <summary>
            ///     Whether to hide the "Nearby Players" window in instances.
            /// </summary>
            public bool HideInInstance;

            /// <summary>
            ///     Locks the position of the "Nearby Players" window.
            /// </summary>
            public bool LockPosition;

            /// <summary>
            ///     Whether to lock the size of the "Nearby Players" window.
            /// </summary>
            public bool LockSize;

            /// <summary>
            ///     The maximum number of players to show in the "Nearby Players" list.
            /// </summary>
            public int MaxPlayersToShow = 60;

            /// <summary>
            ///     Automatically open the "Nearby Players" window on login.
            /// </summary>
            public bool OpenOnLogin;

            /// <summary>
            ///     Whether or not the "Nearby Players" list should prioritize known players.
            /// </summary>
            public bool PrioritizeKnown;

            /// <summary>
            ///     Whether or not the "Nearby Players" list should use class abbreviations instead of full names.
            /// </summary>
            public bool UseJobAbbreviations;
        }

        /// <summary>
        ///     The configuration for colours used in the plugin.
        /// </summary>
        internal sealed class ColourConfiguration
        {
            public Vector4 Default = new(1.0f, 1.0f, 1.0f, 1.0f);
            public Vector4 Friend = new(1.0f, 0.5f, 0.0f, 1.0f);
            public Vector4 Healer = new(0f, 0.8f, 0.1333333f, 1f);
            public Vector4 MeleeDps = new(0.7058824f, 0f, 0f, 1f);
            public Vector4 Other = new(0.5f, 0.5f, 0.5f, 1f);
            public Vector4 Party = new(0.0f, 0.7f, 1.0f, 1.0f);
            public Vector4 RangedDps = new(0.7058824f, 0f, 0f, 1f);
            public Vector4 Tank = new(0f, 0.6f, 1f, 1f);
        }
    }
}
