using System;
using Sirensong;
using Sirensong.UserInterface.Windowing;
using Wholist.UI.Windows.Wholist;

namespace Wholist.Managers
{
    /// <summary>
    ///     Initializes and manages all windows and window-events for the plugin.
    /// </summary>
    internal sealed class WindowManager : IDisposable
    {
        /// <summary>
        ///     The windowing system.
        /// </summary>
        internal readonly WindowingSystem WindowSystem = SirenCore.GetOrCreateService<WindowingSystem>();

        /// <summary>
        ///     Initializes a new instance of <see cref="WindowManager" />
        /// </summary>
        internal WindowManager()
        {
            this.WindowSystem.AddWindow(new WholistWindow(), true);
            Services.ClientState.Logout += this.OnLogout;
        }

        /// <summary>
        ///    Handles the OnLogout event.
        /// </summary>
        private void OnLogout(object? e, EventArgs args)
        {
            foreach (var window in this.WindowSystem.Windows)
            {
                window.IsOpen = false;
            }
        }

        /// <summary>
        ///     Disposes of the <see cref="WindowManager" />.
        /// </summary>
        public void Dispose() => this.WindowSystem.Dispose();
    }
}
