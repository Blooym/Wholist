using System;
using Dalamud.Game.Command;
using Dalamud.Logging;
using Wholist.Base;
using Wholist.Localization;
using Wholist.UI.Windows.Wholist;

namespace Wholist.Managers
{
    /// <summary>
    ///     Initializes and manages all commands and command-events for the plugin.
    /// </summary>
    internal sealed class CommandManager : IDisposable
    {
        private const string WhoCommand = "/who";

        /// <summary>
        ///     Initializes the CommandManager and its resources.
        /// </summary>
        internal CommandManager()
        {
            PluginLog.Debug("CommandManager(Constructor): Initializing...");

            PluginService.Commands.AddHandler(WhoCommand, new CommandInfo(this.OnCommand) { HelpMessage = TCommandHelp.WholistHelp });

            PluginLog.Debug("CommandManager(Constructor): Initialization complete.");
        }

        /// <summary>
        ///     Dispose of the PluginCommandManager and its resources.
        /// </summary>
        public void Dispose()
        {
            PluginService.Commands.RemoveHandler(WhoCommand);

            PluginLog.Debug("CommandManager(Dispose): Successfully disposed.");
        }

        /// <summary>
        ///     Event handler for when a command is issued by the user.
        /// </summary>
        private void OnCommand(string command, string args)
        {
            var windowSystem = PluginService.WindowManager.WindowSystem;
            switch (command)
            {
                case WhoCommand:
                    if (windowSystem.GetWindow(TWindowNames.Wholist) is WholistWindow settingsWindow)
                    {
                        settingsWindow.IsOpen = !settingsWindow.IsOpen;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
