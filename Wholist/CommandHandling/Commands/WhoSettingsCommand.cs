using Dalamud.Game.Command;
using Wholist.CommandHandling.Interfaces;
using Wholist.Common;
using Wholist.Resources.Localization;

namespace Wholist.CommandHandling.Commands
{
    internal sealed class WhoSettingsCommand : IDalamudCommand
    {
        /// <inheritdoc />
        public string Name { get; } = "/whosettings";

        /// <inheritdoc />
        public CommandInfo Command => new(this.OnExecute) { HelpMessage = Strings.Commands_WhoSettings_Help, ShowInHelp = true };

        /// <inheritdoc />
        public IReadOnlyCommandInfo.HandlerDelegate OnExecute => (command, _) =>
        {
            if (command != this.Name)
            {
                return;
            }
            Services.WindowManager.ToggleConfigWindow();

        };
    }
}
