using Dalamud.Game.Command;
using Sirensong.Game;
using Wholist.CommandHandling.Interfaces;
using Wholist.Common;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.NearbyPlayers;

namespace Wholist.CommandHandling.Commands
{
    internal sealed class WhoCommand : IDalamudCommand
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
                if (Services.ClientState.IsPvP)
                {
                    BetterLog.Information(Strings.Errors_NoUseInPvP);
                    GameChat.PrintError(Strings.Errors_NoUseInPvP);
                    return;
                }

                Services.WindowManager.WindowingSystem.GetWindow<NearbyPlayersWindow>()?.Toggle();
            }
        };
    }
}
