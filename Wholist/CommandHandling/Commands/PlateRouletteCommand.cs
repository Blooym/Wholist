using System;
using System.Linq;
using Dalamud.Game.Command;
using Sirensong.Extensions;
using Sirensong.Game.Helpers;
using Wholist.CommandHandling.Interfaces;
using Wholist.Common;
using Wholist.Game;
using Wholist.Resources.Localization;


namespace Wholist.CommandHandling.Commands
{
    internal sealed class RandomPlateCommand : IDalamudCommand
    {

        /// <inheritdoc />
        public string Name { get; } = "/randplate";

        /// <inheritdoc />
        public CommandInfo Command => new(this.OnExecute) { HelpMessage = "Open a random nearby player adventure plate", ShowInHelp = true };

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

            var players = PlayerManager.GetNearbyPlayers()
                .Where(x => x.GameObjectId != Services.ClientState.LocalPlayer?.GameObjectId);
            if (!players.Any())
            {
                ToastHelper.ShowErrorToast("Cannot show random adventurer plate. There are no players nearby.");
                return;
            }
            var r = new Random();
            players.ElementAt(r.Next(players.Count())).OpenCharaCard();
        };
    }
}
