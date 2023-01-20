using System;
using Dalamud.Configuration;
using Wholist.Base;

namespace Wholist
{
    /// <summary>
    ///     Provides access to and determines the Plugin configuration.
    /// </summary>
    [Serializable]
    public sealed class Configuration : IPluginConfiguration
    {
        /// <summary>
        ///     The current configuration version, incremented on breaking changes.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///   If the plugin should filter AFK players from the list.
        /// </summary>
        public bool FilterAfk { get; set; }

        /// <summary>
        ///     Saves the current configuration to disk.
        /// </summary>
        internal void Save() => Services.PluginInterface.SavePluginConfig(this);
    }
}