using Dalamud.Game.Command;
using Sirensong.Game.Helpers;
using Wholist.CommandHandling.Interfaces;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.CommandHandling.Commands
{
    internal sealed class WhoCommand : IDalamudCommand
    {
        /// <inheritdoc />
        public string Name { get; } = "/who";

        /// <inheritdoc />
        public CommandInfo Command => new(this.OnExecute) { HelpMessage = Strings.Commands_Who_Help, ShowInHelp = true };

        /// <inheritdoc />
        public IReadOnlyCommandInfo.HandlerDelegate OnExecute => (command, _) =>
        {
            if (command != this.Name)
            {
                return;
            }

            if (Services.ClientState.IsPvP)
            {
                ChatHelper.PrintError(Strings.Errors_NoUseInPvP);
                return;
            }

            Services.WindowManager.ToggleMainWindow();
        };
    }
}
