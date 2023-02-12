using System;
using System.Collections.Generic;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Plugin.Ipc;
using Wholist.Common;

namespace Wholist.IntegrationHandling
{
    /// <summary>
    ///     Handles inbound IPC calls.
    /// </summary>
    internal sealed class InboundIpcManager : IDisposable
    {

        #region Constant Values

        /// <summary>
        ///     The current API version.
        /// </summary>
        private const string ApiVersion = "1.0.0";

        #endregion

        #region Stored IPCs

        /// <summary>
        ///     All registered player context menus.
        /// </summary>
        /// <remarks>
        ///     Key - The IPC ID.
        ///     Value - The user-friendly name of the item.
        /// </remarks>
        private readonly Dictionary<string, string> registeredPlayerContextMenuIpcs = new();

        #endregion

        #region Internal Methods

        /// <summary>
        ///     Gets the registered player context menus.
        /// </summary>
        /// <remarks>
        ///     Key - The IPC ID.
        ///     Value - The user-friendly name of the menu, usually the plugin name.
        /// </remarks>
        /// <returns>The registered player context menus.</returns>
        internal IReadOnlyDictionary<string, string> GetPlayerContextItems() => this.registeredPlayerContextMenuIpcs;

        #endregion

        #region Constructor and Dispose

        /// <summary>
        ///     Creates a new instance of <see cref="InboundIpcManager" />
        /// </summary>
        private InboundIpcManager()
        {
            // API version
            this.ApiVersionGate = Services.PluginInterface.GetIpcProvider<string>(ApiVersionString);
            this.ApiVersionGate.RegisterFunc(this.GetApiVersion);

            // Context menus
            this.RegisterPlayerContextMenuGate = Services.PluginInterface.GetIpcProvider<string, string>(RegisterPlayerContextMenuString);
            this.RegisterPlayerContextMenuGate.RegisterFunc(this.RegisterPlayerContextMenu);

            this.UnregisterPlayerContextMenuGate = Services.PluginInterface.GetIpcProvider<string, object?>(UnregisterPlayerContextMenuString);
            this.UnregisterPlayerContextMenuGate.RegisterAction(this.UnregisterPlayerContextItem);

            this.InvokePlayerContextMenuGate = Services.PluginInterface.GetIpcProvider<string, PlayerCharacter, object?>(InvokePlayerContextMenuString);
            this.InvokePlayerContextMenuGate.RegisterAction(this.InvokePlayerContextMenu);

            // Available
            this.AvailableGate = Services.PluginInterface.GetIpcProvider<object?>(IpcAvailableString);
            this.AvailableGate.SendMessage();
        }

        /// <summary>
        ///     Disposes of the <see cref="InboundIpcManager" />.
        /// </summary>
        public void Dispose()
        {
            this.ApiVersionGate.UnregisterFunc();
            this.AvailableGate.UnregisterFunc();
            this.UnregisterPlayerContextMenuGate.UnregisterFunc();
            this.RegisterPlayerContextMenuGate.UnregisterFunc();

            this.registeredPlayerContextMenuIpcs.Clear();
        }

        #endregion

        #region IPC Methods

        /// <summary>
        ///     Gets the current API version.
        /// </summary>
        /// <returns>The current API version.</returns>
        private string GetApiVersion() => ApiVersion;

        /// <summary>
        ///     Registers a context menu.
        /// </summary>
        /// <param name="pluginName">The name of the plugin.</param>
        /// <returns>The IPC ID, which should be saved to handle invokes and unregisters.</returns>
        private string RegisterPlayerContextMenu(string pluginName)
        {
            var id = Guid.NewGuid().ToString();
            this.registeredPlayerContextMenuIpcs.Add(id, pluginName);
            return id;
        }

        /// <summary>
        ///     Unregisters a context menu.
        /// </summary>
        /// <param name="id">The IPC ID of the item to unregister.</param>
        /// <returns>True if the item was unregistered, false otherwise.</returns>
        private void UnregisterPlayerContextItem(string id) => this.registeredPlayerContextMenuIpcs.Remove(id);

        /// <summary>
        ///     Invokes a player context menu.
        /// </summary>
        /// <param name="id">The ID of the IPC to invoke.</param>
        /// <param name="playerCharacter">The player character to send to the plugin.</param>
        internal void InvokePlayerContextMenu(string id, PlayerCharacter playerCharacter) => this.InvokePlayerContextMenuGate.SendMessage(id, playerCharacter);

        #endregion

        #region CallGateProviders

        /// <summary>
        ///     The gate for the API version.
        /// </summary>
        private ICallGateProvider<string> ApiVersionGate { get; }

        /// <summary>
        ///     The gate to see if the plugin is available.
        /// </summary>
        private ICallGateProvider<object?> AvailableGate { get; }

        /// <summary>
        ///     The gate to register a player context menu.
        /// </summary>
        private ICallGateProvider<string, string> RegisterPlayerContextMenuGate { get; }

        /// <summary>
        ///     The gate to unregister a player context menu.
        /// </summary>
        private ICallGateProvider<string, object?> UnregisterPlayerContextMenuGate { get; }

        /// <summary>
        ///     The gate to invoke a player context menu.
        /// </summary>
        private ICallGateProvider<string, PlayerCharacter, object?> InvokePlayerContextMenuGate { get; }

        #endregion

        #region IPC String Constants

        /// <summary>
        ///     The IPC ID for when IPC is available.
        /// </summary>
        private const string IpcAvailableString = "Wholist.Available";

        /// <summary>
        ///     The IPC ID for the API version.
        /// </summary>
        private const string ApiVersionString = "Wholist.ApiVersion";

        /// <summary>
        ///     The IPC ID for registering a player context menu.
        /// </summary>
        private const string RegisterPlayerContextMenuString = "Wholist.RegisterPlayerContextMenu";

        /// <summary>
        ///     The IPC ID for unregistering a player context menu.
        /// </summary>
        private const string UnregisterPlayerContextMenuString = "Wholist.UnregisterPlayerContextMenu";

        /// <summary>
        ///     The IPC ID for invoking a player context menu.
        /// </summary>
        private const string InvokePlayerContextMenuString = "Wholist.InvokePlayerContextMenu";

        #endregion

    }
}
