using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Wholist.Base;
using Wholist.UI.Windows.Wholist;

namespace Wholist.Managers
{
    /// <summary>
    ///     Initializes and manages all windows and window-events for the plugin.
    /// </summary>
    internal sealed class WindowManager : IDisposable
    {
        /// <summary>
        ///     The windowing system service provided by Dalamud.
        /// </summary>
        public readonly WindowSystem WindowSystem = new(PluginConstants.PluginName);

        /// <summary>
        ///     All windows managed by the WindowManager.
        /// </summary>
        private readonly List<Window> windows = new()
        {
            new WholistWindow()
        };

        /// <summary>
        ///     Initializes the WindowManager and associated resources.
        /// </summary>
        internal WindowManager()
        {
            PluginLog.Debug("WindowManager(Constructor): Initializing...");

            foreach (var window in this.windows)
            {
                PluginLog.Debug($"WindowManager(Constructor): Registering window: {window.GetType().Name}");
                this.WindowSystem.AddWindow(window);
            }

            PluginService.PluginInterface.UiBuilder.Draw += this.OnDrawUI;
            PluginService.ClientState.Logout += this.OnLogout;

            PluginLog.Debug("WindowManager(Constructor): Successfully initialized.");
        }

        /// <summary>
        ///     Draws all windows for the draw event.
        /// </summary>
        private void OnDrawUI() => this.WindowSystem.Draw();

        /// <summary>
        ///    Handles the OnLogout event.
        /// </summary>
        public void OnLogout(object? e, EventArgs args)
        {
            foreach (var window in this.windows)
            {
                window.IsOpen = false;
            }
        }

        /// <summary>
        ///     Disposes of the WindowManager and associated resources.
        /// </summary>
        public void Dispose()
        {
            PluginService.PluginInterface.UiBuilder.Draw -= this.OnDrawUI;

            foreach (var window in this.windows.OfType<IDisposable>())
            {
                PluginLog.Debug($"WindowManager(Dispose): Disposing of {window.GetType().Name}...");
                window.Dispose();
            }

            this.WindowSystem.RemoveAllWindows();

            PluginLog.Debug("WindowManager(Dispose): Successfully disposed.");
        }
    }
}
