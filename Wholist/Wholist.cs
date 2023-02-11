using Dalamud.IoC;
using Dalamud.Plugin;
using Sirensong;
using Wholist.Common;

namespace Wholist
{
    internal sealed class Plugin : IDalamudPlugin
    {

        /// <summary>
        ///     The plugin's main entry point.
        /// </summary>
        public Plugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
        {
            SirenCore.Initialize(pluginInterface, this.Name);
            Services.Initialize(pluginInterface);
        }

        /// <summary>
        ///     The plugin's name.
        /// </summary>
        public string Name => Constants.PluginName;

        /// <summary>
        ///     Disposes of the plugin.
        /// </summary>
        public void Dispose()
        {
            SirenCore.Dispose();
            Services.Dispose();
        }
    }
}
