using Dalamud.Game.Command;

namespace Wholist.CommandHandling.Interfaces
{
    /// <summary>
    ///     Represents a command.
    /// </summary>
    internal interface IDalamudCommand
    {
        /// <summary>
        ///     The name of the command (including the /)
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The command info.
        /// </summary>
        public CommandInfo Command { get; }

        /// <summary>
        ///     The command's execution handler.
        /// </summary>
        public IReadOnlyCommandInfo.HandlerDelegate OnExecute { get; }
    }
}
