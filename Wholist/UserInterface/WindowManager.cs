using System;
using System.Collections.Generic;
using Dalamud.Interface.Windowing;
using Sirensong;
using Sirensong.UserInterface.Windowing;
using Wholist.UserInterface.Windows.WhoWindow;

namespace Wholist.UserInterface
{
    internal sealed class WindowManager : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// The windowing system.
        /// </summary>
        public WindowingSystem WindowingSystem { get; } = SirenCore.GetOrCreateService<WindowingSystem>();

        /// <summary>
        /// All windows to add to the windowing system.
        /// </summary>
        private readonly Dictionary<Window, bool> windows = new()
        {
            { new WhoWindow(), true },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowManager" /> class.
        /// </summary>
        private WindowManager()
        {
            foreach (var (window, isSettings) in this.windows)
            {
                this.WindowingSystem.AddWindow(window, isSettings);
            }
        }

        /// <summary>
        /// Disposes of the window manager.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                this.WindowingSystem.Dispose();
                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Toggles the guide viewer window visibility.
        /// </summary>
        public void ToggleGuideViewerWindow()
        {
            if (this.WindowingSystem.TryGetWindow<WhoWindow>(out var window))
            {
                window.Toggle();
            }
        }
    }
}