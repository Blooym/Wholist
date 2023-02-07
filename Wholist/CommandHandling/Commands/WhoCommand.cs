using Dalamud.Game.Command;
using Wholist.CommandHandling.Interfaces;
using Wholist.Common;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.WhoWindow;

namespace Wholist.CommandHandling.Commands
{
    internal sealed class WhoCommand : ICommand
    {
        /// <inheritdoc />
        public string Name { get; } = Constants.Commands.WhoCommand;

        /// <inheritdoc />
        public CommandInfo Command => new(this.OnExecute)
        {
            HelpMessage = Strings.Commands_Who_Help,
            ShowInHelp = true,
        };

        /// <inheritdoc />
        public CommandInfo.HandlerDelegate OnExecute => (command, arguments) =>
        {
            if (command == Constants.Commands.WhoCommand)
            {
                Services.WindowManager.WindowingSystem.GetWindow<WhoWindow>()?.Toggle();
            }
        };
    }
}