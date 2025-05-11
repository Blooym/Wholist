using System.Collections.Generic;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Sirensong;
using Sirensong.IoC;
using Wholist.Configuration;
using Wholist.DTRHandling;
using Wholist.Interop;
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
        [PluginService] internal static ICondition Condition { get; set; } = null!;
        [PluginService] internal static IPartyList PartyList { get; set; } = null!;
        [PluginService] internal static IPluginLog PluginLog { get; set; } = null!;
        [PluginService] internal static IGameInteropProvider GameInteropProvider { get; set; } = null!;
        [PluginService] internal static IDataManager DataManager { get; set; } = null!;
        [PluginService] internal static IDtrBar DtrBar { get; set; } = null!;
        [PluginService] internal static IFramework Framework { get; set; } = null!;

        internal static ExcelSheet<World> WorldSheet { get; set; } = null!;
        internal static ExcelSheet<ClassJob> ClassJobSheet { get; set; } = null!;

        // Temporary Caches until rewrite
        internal static Dictionary<uint, string> WorldNames { get; } = [];
        internal static Dictionary<uint, string> ClassJobNames { get; } = [];
        internal static Dictionary<uint, string> ClassJobAbbreviations { get; } = [];

        // Plugin services
        internal static WindowManager WindowManager { get; private set; } = null!;
        internal static PluginConfiguration Configuration { get; private set; } = null!;
        internal static DTRManager DtrManager { get; private set; } = null!;
        internal static IpcManager IpcManager { get; private set; } = null!;

        /// <summary>
        ///     Initializes the service class.
        /// </summary>
        internal static void Initialize(IDalamudPluginInterface pluginInterface)
        {
            SirenCore.InjectServices<Services>();
            pluginInterface.Create<Services>();
            Configuration = PluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            ServiceContainer.GetOrCreateService<LocalizationManager>();
            IpcManager = ServiceContainer.GetOrCreateService<IpcManager>();
            WindowManager = ServiceContainer.GetOrCreateService<WindowManager>();
            DtrManager = ServiceContainer.GetOrCreateService<DTRManager>();
            ServiceContainer.CreateService<CommandHandling.CommandManager>();
            WorldSheet = DataManager.GetExcelSheet<World>();
            ClassJobSheet = DataManager.GetExcelSheet<ClassJob>();
        }

        /// <summary>
        ///     Disposes of the service class.
        /// </summary>
        internal static void Dispose() => ServiceContainer.Dispose();
    }
}
