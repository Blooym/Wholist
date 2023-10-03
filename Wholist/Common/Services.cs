using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Sirensong;
using Sirensong.IoC;
using Wholist.Configuration;
using Wholist.Game;
using Wholist.IntegrationHandling;
using Wholist.Resources.Localization;
using Wholist.UserInterface;
using XivCommon;

namespace Wholist.Common
{
    /// <summary>
    ///     Provides access to necessary instances and services.
    /// </summary>
    internal sealed class Services
    {
        /// <inheritdoc cref="MiniServiceContainer" />
        private static readonly MiniServiceContainer ServiceContainer = new();

        // Dalamud services
        [PluginService] internal static DalamudPluginInterface PluginInterface { get; set; } = null!;
        [PluginService] internal static IClientState ClientState { get; set; } = null!;
        [PluginService] internal static IObjectTable ObjectTable { get; set; } = null!;
        [PluginService] internal static ICommandManager Commands { get; set; } = null!;
        [PluginService] internal static ITargetManager TargetManager { get; set; } = null!;
        [PluginService] internal static ICondition Condition { get; set; } = null!;
        [PluginService] internal static IPartyList PartyList { get; set; } = null!;
        [PluginService] internal static IPluginLog PluginLog { get; set; } = null!;

        // Plugin services
        internal static WindowManager WindowManager { get; private set; } = null!;
        internal static XivCommonBase XivCommon { get; private set; } = null!;
        internal static PlayerManager PlayerManager { get; private set; } = null!;
        internal static PluginConfiguration Configuration { get; private set; } = null!;
        internal static InboundIpcManager InboundIpcManager { get; private set; } = null!;

        /// <summary>
        ///     Initializes the service class.
        /// </summary>
        internal static void Initialize(DalamudPluginInterface pluginInterface)
        {
            SirenCore.InjectServices<Services>();
            pluginInterface.Create<Services>();
            BetterLog.Debug("Initializing services.");

            XivCommon = new XivCommonBase(pluginInterface);
            Configuration = PluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            ServiceContainer.GetOrCreateService<LocalizationManager>();
            WindowManager = ServiceContainer.GetOrCreateService<WindowManager>();
            ServiceContainer.CreateService<CommandHandling.CommandManager>();
            PlayerManager = ServiceContainer.GetOrCreateService<PlayerManager>();
            InboundIpcManager = ServiceContainer.GetOrCreateService<InboundIpcManager>();
        }

        /// <summary>
        ///     Disposes of the service class.
        /// </summary>
        internal static void Dispose()
        {
            ServiceContainer.Dispose();
            XivCommon.Dispose();
        }
    }
}
