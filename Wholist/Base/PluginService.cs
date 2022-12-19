using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using Wholist.Managers;
using XivCommon;

namespace Wholist.Base
{
    /// <summary>
    ///     Provides access to necessary instances and services.
    /// </summary>
#pragma warning disable CS8618 // Injection is handled by the Dalamud Plugin Framework here.
    internal sealed class PluginService
    {
        [PluginService] internal static DalamudPluginInterface PluginInterface { get; private set; }
        [PluginService] internal static ClientState ClientState { get; private set; }
        [PluginService] internal static ObjectTable ObjectTable { get; private set; }
        [PluginService] internal static Dalamud.Game.Command.CommandManager Commands { get; private set; }
        [PluginService] internal static TargetManager TargetManager { get; private set; }

        internal static CommandManager CommandManager { get; private set; }
        internal static WindowManager WindowManager { get; private set; }
        internal static ResourceManager ResourceManager { get; private set; }
        internal static XivCommonBase Common { get; private set; }
        internal static Configuration Configuration { get; private set; }

        /// <summary>
        ///     Initializes the service class.
        /// </summary>
        public static void Initialize()
        {
            ResourceManager = new ResourceManager();
            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            WindowManager = new WindowManager();
            CommandManager = new CommandManager();
            Common = new XivCommonBase();

            PluginLog.Debug("PluginService(Initialize): Successfully initialized plugin services.");
        }

        /// <summary>
        ///     Disposes of the service class.
        /// </summary>
        internal static void Dispose()
        {
            ResourceManager?.Dispose();
            WindowManager?.Dispose();
            CommandManager?.Dispose();
            Common?.Dispose();

            PluginLog.Debug("PluginService(Initialize): Successfully disposed of plugin services.");
        }
    }
}
