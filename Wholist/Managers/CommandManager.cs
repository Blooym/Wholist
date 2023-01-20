using System;
using Dalamud.Game.Command;
using Dalamud.Logging;
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
        ///     Initializes a new instance of <see cref="CommandManager" />
        /// </summary>
        internal CommandManager() => Services.Commands.AddHandler(WhoCommand, new CommandInfo(this.OnWhoCommand) { HelpMessage = LStrings.Commands.WhoHelp });

        /// <summary>
        ///     Dispose of the <see cref="CommandManager" />
        /// </summary>
        public void Dispose() => Services.Commands.RemoveHandler(WhoCommand);

        /// <summary>
        ///     /Who command handler.
        /// </summary>
        private void OnWhoCommand(string command, string args)
        {
            if (!Services.ClientState.IsLoggedIn)
            {
                PluginLog.Information(LStrings.WholistWindow.MustBeLoggedIn);
                return;
            }
            Services.WindowManager.WindowSystem.GetWindow<WholistWindow>()?.Toggle();
        }
    }
}
