using Dalamud.Game.Command;
using Wholist.CommandHandling.Interfaces;
using Wholist.Common;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings;

namespace Wholist.CommandHandling.Commands
{
    internal sealed class WhoSettingsCommand : IDalamudCommand
    {
        /// <inheritdoc />
        public string Name { get; } = Constants.Commands.WhoSettingsCommand;

        /// <inheritdoc />
        public CommandInfo Command => new(this.OnExecute) { HelpMessage = Strings.Commands_WhoSettings_Help, ShowInHelp = true };

        /// <inheritdoc />
        public CommandInfo.HandlerDelegate OnExecute => (command, _) =>
        {
            if (command == Constants.Commands.WhoSettingsCommand)
            {
                Services.WindowManager.WindowingSystem.GetWindow<SettingsWindow>()?.Toggle();
            }
        };
    }
}
