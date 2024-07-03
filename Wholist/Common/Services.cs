using System.Collections.Generic;
using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Lumina.Excel.GeneratedSheets;
using Sirensong;
using Sirensong.Cache;
using Sirensong.IoC;
using Wholist.Configuration;
using Wholist.Resources.Localization;
using Wholist.UserInterface;

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
        [PluginService] internal static IDalamudPluginInterface PluginInterface { get; set; } = null!;
        [PluginService] internal static IClientState ClientState { get; set; } = null!;
        [PluginService] internal static IObjectTable ObjectTable { get; set; } = null!;
        [PluginService] internal static ICommandManager Commands { get; set; } = null!;
        [PluginService] internal static ITargetManager TargetManager { get; set; } = null!;
        [PluginService] internal static ICondition Condition { get; set; } = null!;
        [PluginService] internal static IPartyList PartyList { get; set; } = null!;
        [PluginService] internal static IPluginLog PluginLog { get; set; } = null!;

        [SirenService] internal static LuminaCacheService<World> WorldCache { get; set; } = null!;
        [SirenService] internal static LuminaCacheService<ClassJob> ClassJobCache { get; set; } = null!;

        // Temporary Caches until rewrite
        internal static Dictionary<uint, string> WorldNames { get; } = new();
        internal static Dictionary<uint, string> ClassJobNames { get; } = new();
        internal static Dictionary<uint, string> ClassJobAbbreviations { get; } = new();

        // Plugin services
        internal static WindowManager WindowManager { get; private set; } = null!;
        // internal static XivCommonBase XivCommon { get; private set; } = null!;
        internal static PluginConfiguration Configuration { get; private set; } = null!;

        /// <summary>
        ///     Initializes the service class.
        /// </summary>
        internal static void Initialize(IDalamudPluginInterface pluginInterface)
        {
            SirenCore.InjectServices<Services>();
            pluginInterface.Create<Services>();
            BetterLog.Debug("Initializing services.");

            // XivCommon = new XivCommonBase(pluginInterface);
            Configuration = PluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            ServiceContainer.GetOrCreateService<LocalizationManager>();
            WindowManager = ServiceContainer.GetOrCreateService<WindowManager>();
            ServiceContainer.CreateService<CommandHandling.CommandManager>();
        }

        /// <summary>
        ///     Disposes of the service class.
        /// </summary>
        internal static void Dispose() => ServiceContainer.Dispose();// XivCommon.Dispose();
    }
}
