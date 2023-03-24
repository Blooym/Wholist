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
            /// <summary>
            ///     Represents different ways JobColours can be obtained.
            /// </summary>
            public enum JobColourMode
            {
                Role,
                Job,
            }

            public JobColourMode JobColMode = JobColourMode.Role;
            public NameColours Name = new();
            public RoleColours Role = new();
            public JobColours Job = new();

            /// <summary>
            ///     Represents the colours used for player names.
            /// </summary>
            internal sealed class NameColours
            {
                public Vector4 Default = new(1.0f, 1.0f, 1.0f, 1.0f);
                public Vector4 Friend = new(1.0f, 0.5f, 0.0f, 1.0f);
                public Vector4 Party = new(0.0f, 0.7f, 1.0f, 1.0f);
            }

            /// <summary>
            ///     Represents the colours used for player job roles.
            /// </summary>
            internal sealed class RoleColours
            {
                public Vector4 Healer = new(0f, 0.8f, 0.1333333f, 1f);
                public Vector4 Tank = new(0f, 0.6f, 1f, 1f);
                public Vector4 MeleeDps = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 RangedDps = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Other = new(0.5f, 0.5f, 0.5f, 1f);
            }

            /// <summary>
            ///     Represents the colours used for player jobs.
            /// </summary>
            internal sealed class JobColours
            {
                // Tanks
                public Vector4 Warrior = new(0f, 0.6f, 1f, 1f);
                public Vector4 Paladin = new(0f, 0.6f, 1f, 1f);
                public Vector4 DarkKnight = new(0f, 0.6f, 1f, 1f);
                public Vector4 Gunbreaker = new(0f, 0.6f, 1f, 1f);

                // Healers
                public Vector4 WhiteMage = new(0f, 0.8f, 0.1333333f, 1f);
                public Vector4 Scholar = new(0f, 0.8f, 0.1333333f, 1f);
                public Vector4 Astrologian = new(0f, 0.8f, 0.1333333f, 1f);
                public Vector4 Sage = new(0f, 0.8f, 0.1333333f, 1f);

                // Melee
                public Vector4 Monk = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Dragoon = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Ninja = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Samurai = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Reaper = new(0.7058824f, 0f, 0f, 1f);

                // Ranged
                public Vector4 Bard = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Machinist = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Dancer = new(0.7058824f, 0f, 0f, 1f);

                // Casters
                public Vector4 BlackMage = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 Summoner = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 RedMage = new(0.7058824f, 0f, 0f, 1f);
                public Vector4 BlueMage = new(0.7058824f, 0f, 0f, 1f);

                // Misc
                public Vector4 Other = new(0.5f, 0.5f, 0.5f, 1f);
            }
        }
    }
}
