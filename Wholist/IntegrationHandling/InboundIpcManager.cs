using System;
using System.Collections.Generic;
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
        private const string ApiVersion = "2.0.0";

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

        #region Fields

        private bool disposedValue;

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
        internal IReadOnlyDictionary<string, string> GetPlayerContextMenuItems() => this.registeredPlayerContextMenuIpcs;

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
            this.UnregisterPlayerContextMenuGate.RegisterAction(this.UnregisterPlayerContextMenu);

            this.InvokePlayerContextMenuGate = Services.PluginInterface.GetIpcProvider<string, nint, object?>(InvokePlayerContextMenuString);
            this.InvokePlayerContextMenuGate.RegisterAction(this.InvokePlayerContextMenu);

            // Disposed
            this.DisposedGate = Services.PluginInterface.GetIpcProvider<object?>(IpcDisposedString);

            // Available
            this.AvailableGate = Services.PluginInterface.GetIpcProvider<object?>(IpcAvailableString);
            this.AvailableGate.SendMessage();
        }

        /// <summary>
        ///     Disposes of the <see cref="InboundIpcManager" />.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                this.ApiVersionGate.UnregisterFunc();
                this.AvailableGate.UnregisterFunc();
                this.UnregisterPlayerContextMenuGate.UnregisterFunc();
                this.RegisterPlayerContextMenuGate.UnregisterFunc();

                foreach (var contextMenu in this.registeredPlayerContextMenuIpcs)
                {
                    this.UnregisterPlayerContextMenu(contextMenu.Key);
                }

                this.registeredPlayerContextMenuIpcs.Clear();
                this.DisposedGate.SendMessage();

                this.disposedValue = true;
            }
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
        /// <param name="menuName">The name of the menu.</param>
        /// <returns>The IPC ID, which should be saved to handle invokes and unregisters.</returns>
        private string RegisterPlayerContextMenu(string menuName)
        {
            var id = Guid.NewGuid().ToString();
            this.registeredPlayerContextMenuIpcs.Add(id, menuName);
            BetterLog.Verbose($"Registered player context menu with ID {id} and name {menuName}");
            return id;
        }

        /// <summary>
        ///     Unregisters a context menu.
        /// </summary>
        /// <param name="id">The IPC ID of the item to unregister.</param>
        /// <returns>True if the item was unregistered, false otherwise.</returns>
        private void UnregisterPlayerContextMenu(string id)
        {
            this.registeredPlayerContextMenuIpcs.Remove(id);
            BetterLog.Verbose($"Unregistered player context menu with ID {id}");
        }

        /// <summary>
        ///     Invokes a player context menu.
        /// </summary>
        /// <param name="id">The ID of the IPC to invoke.</param>
        /// <param name="address">The address of the character to send to the plugin.</param>
        internal void InvokePlayerContextMenu(string id, nint address) => this.InvokePlayerContextMenuGate.SendMessage(id, address);

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
        ///     The gate to see when the plugin is disposed.
        /// </summary>
        private ICallGateProvider<object?> DisposedGate { get; }

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
        private ICallGateProvider<string, nint, object?> InvokePlayerContextMenuGate { get; }

        #endregion

        #region IPC String Constants

        /// <summary>
        ///     The IPC ID for the API version.
        /// </summary>
        private const string ApiVersionString = "Wholist.ApiVersion";

        /// <summary>
        ///     The IPC ID for when IPC is available.
        /// </summary>
        private const string IpcAvailableString = "Wholist.Available";

        /// <summary>
        ///     The IPC ID for when IPC is disposed.
        /// </summary>
        private const string IpcDisposedString = "Wholist.Disposed";

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
