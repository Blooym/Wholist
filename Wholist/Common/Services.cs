using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.ClientState.Party;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Sirensong;
using Sirensong.IoC;
using Sirensong.UserInterface.Services;
using Wholist.Configuration;
using Wholist.Game;
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
        [PluginService] internal static ClientState ClientState { get; set; } = null!;
        [PluginService] internal static ObjectTable ObjectTable { get; set; } = null!;
        [PluginService] internal static CommandManager Commands { get; set; } = null!;
        [PluginService] internal static TargetManager TargetManager { get; set; } = null!;
        [PluginService] internal static Condition Condition { get; set; } = null!;
        [PluginService] internal static PartyList PartyList { get; set; } = null!;

        // Sirensong services
        [SirenService] internal static ClipboardService Clipboard { get; set; } = null!;

        // Plugin services
        internal static WindowManager WindowManager { get; private set; } = null!;
        internal static XivCommonBase XivCommon { get; private set; } = null!;
        internal static PlayerManager PlayerManager { get; private set; } = null!;
        internal static PluginConfiguration Configuration { get; private set; } = null!;

        /// <summary>
        ///     Initializes the service class.
        /// </summary>
        internal static void Initialize(DalamudPluginInterface pluginInterface)
        {
            BetterLog.Debug("Initializing services.");

            SirenCore.InjectServices<Services>();
            pluginInterface.Create<Services>();

            Configuration = PluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            XivCommon = new XivCommonBase();
            ServiceContainer.GetOrCreateService<LocalizationManager>();
            WindowManager = ServiceContainer.GetOrCreateService<WindowManager>();
            ServiceContainer.CreateService<CommandHandling.CommandManager>();
            PlayerManager = ServiceContainer.GetOrCreateService<PlayerManager>();
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
