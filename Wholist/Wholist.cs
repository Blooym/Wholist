using Dalamud.IoC;
using Dalamud.Plugin;
using Sirensong;

namespace Wholist
{
    internal sealed class Plugin : IDalamudPlugin
    {
        /// <summary>
        ///     The plugin's name.
        /// </summary>
        public string Name => "Wholist";

        /// <summary>
        ///     The plugin's main entry point.
        /// </summary>
        public Plugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
        {
            SirenCore.Initialize(pluginInterface, this.Name);
            Services.Initialize(pluginInterface);
        }

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
