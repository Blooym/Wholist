using System;
using System.Collections.Generic;
using Dalamud.Interface.Windowing;
using Sirensong;
using Sirensong.UserInterface.Windowing;
using Wholist.Common;
using Wholist.UserInterface.Windows.NearbyPlayers;
using Wholist.UserInterface.Windows.Settings;

namespace Wholist.UserInterface
{
    internal sealed class WindowManager : IDisposable
    {

        #region Event Handlers

        /// <summary>
        ///     Handle the plugin being logged in.
        /// </summary>
        private void OnLogin(object? sender, EventArgs e)
        {
            if (!Services.Configuration.NearbyPlayers.OpenOnLogin)
            {
                return;
            }

            if (this.WindowingSystem.TryGetWindow<NearbyPlayersWindow>(out var window))
            {
                window.IsOpen = true;
            }
        }

        /// <summary>
        ///     Handle the plugin being logged out.
        /// </summary>
        private void OnLogout(object? sender, EventArgs e)
        {
            if (this.WindowingSystem.TryGetWindow<NearbyPlayersWindow>(out var window))
            {
                window.IsOpen = false;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        ///     All windows to add to the windowing system, holds all references.
        /// </summary>
        private readonly Dictionary<Window, bool> windows = new() { { new NearbyPlayersWindow(), false }, { new SettingsWindow(), true } };

        /// <summary>
        ///     The windowing system.
        /// </summary>
        public WindowingSystem WindowingSystem { get; } = SirenCore.GetOrCreateService<WindowingSystem>();

        private bool disposedValue;

        #endregion

        #region Constructor and Dispose

        /// <summary>
        ///     Initializes a new instance of the <see cref="WindowManager" /> class.
        /// </summary>
        private WindowManager()
        {
            foreach (var (window, isSettings) in this.windows)
            {
                this.WindowingSystem.AddWindow(window, isSettings);
            }

            Services.ClientState.Login += this.OnLogin;
            Services.ClientState.Logout += this.OnLogout;

            if (Services.ClientState.IsLoggedIn)
            {
                this.OnLogin(null, EventArgs.Empty);
            }
        }


        /// <summary>
        ///     Disposes of the window manager.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                this.WindowingSystem.Dispose();

                Services.ClientState.Login -= this.OnLogin;
                Services.ClientState.Logout -= this.OnLogout;

                this.disposedValue = true;
            }
        }

        #endregion

    }
}
