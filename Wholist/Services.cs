using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Wholist.Managers;
using XivCommon;

namespace Wholist
{
    /// <summary>
    ///     Provides access to necessary instances and services.
    /// </summary>
#pragma warning disable CS8618 // Injection is handled by the Dalamud Plugin Framework here.
    internal sealed class Services
    {
        [PluginService] internal static DalamudPluginInterface PluginInterface { get; private set; }
        [PluginService] internal static ClientState ClientState { get; private set; }
        [PluginService] internal static ObjectTable ObjectTable { get; private set; }
        [PluginService] internal static Dalamud.Game.Command.CommandManager Commands { get; private set; }

        internal static CommandManager CommandManager { get; private set; }
        internal static WindowManager WindowManager { get; private set; }
        internal static ResourceManager ResourceManager { get; private set; }
        internal static XivCommonBase XivCommon { get; private set; }
        internal static Configuration Configuration { get; private set; }

        /// <summary>
        ///     Initializes the service class.
        /// </summary>
        internal static void Initialize()
        {
            ResourceManager = new ResourceManager();
            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            WindowManager = new WindowManager();
            CommandManager = new CommandManager();
            XivCommon = new XivCommonBase();
        }

        /// <summary>
        ///     Disposes of the service class.
        /// </summary>
        internal static void Dispose()
        {
            ResourceManager?.Dispose();
            WindowManager?.Dispose();
            CommandManager?.Dispose();
            XivCommon?.Dispose();
        }
    }
}
