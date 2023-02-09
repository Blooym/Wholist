using System;
using System.Collections.Generic;
using Dalamud.Utility;
using FFXIVClientStructs.FFXIV.Client.UI;
using ImGuiNET;
using Sirensong.Game;
using Sirensong.Game.Enums;
using Wholist.Common;
using Wholist.Configuration;
using Wholist.DataStructures;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.NearbyPlayers
{
    internal sealed class NearbyPlayersLogic
    {
        /// <summary>
        /// The search text to apply to the object table.
        /// </summary>
        internal string SearchText = string.Empty;

        /// <inheritdoc cref="Configuration" />
        internal static PluginConfiguration Configuration => Services.Configuration;

        /// <inheritdoc cref="IsPvPExcludingDen" />
        internal static bool IsPvPExcludingDen => Services.ClientState.IsPvPExcludingDen;

        /// <summary>
        /// Whether or not the window should be closed when the escape key is pressed.
        /// </summary>
        internal static bool DisableEscClose => Configuration.NearbyPlayers.LockPosition;

        /// <summary>
        /// Applies the current flag configuration to the window.
        /// </summary>
        /// <param name="currentFlags">The current flags.</param>
        /// <returns>The adjusted flags.</returns>
        internal static ImGuiWindowFlags ApplyFlagConfiguration(ImGuiWindowFlags currentFlags)
        {
            if (Configuration.NearbyPlayers.LockPosition)
            {
                currentFlags |= ImGuiWindowFlags.NoMove;
            }
            else
            {
                currentFlags &= ~ImGuiWindowFlags.NoMove;
            }

            if (Configuration.NearbyPlayers.LockSize)
            {
                currentFlags |= ImGuiWindowFlags.NoResize;
            }
            else
            {
                currentFlags &= ~ImGuiWindowFlags.NoResize;
            }

            return currentFlags;
        }

        /// <summary>
        /// Pulls <see cref="PlayerCharacter" />s from the object table any applies the filter & configuration.
        /// </summary>
        /// <returns></returns>
        internal List<PlayerInfoSlim> GetNearbyPlayers()
        {
            var players = new List<PlayerInfoSlim>();
            foreach (var player in Services.NearbyPlayerManager.GetNearbyPlayers())
            {
                if (players.Count >= Configuration.NearbyPlayers.MaxPlayersToShow)
                {
                    break;
                }

                if (Configuration.NearbyPlayers.FilterAfk && player.OnlineStatusId == (uint)OnlineStatusType.AFK)
                {
                    continue;
                }

                if (!this.SearchText.IsNullOrWhitespace() && !player.Name.ToString().Contains(this.SearchText, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                players.Add(player);
            }
            return players;
        }

        /// <summary>
        /// Sets the chat target to the given player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="homeworldName">The homeworld name of the player.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal static unsafe void SetChatTellTarget(string name, string homeworldName)
        {
            Services.XivCommon.Functions.Chat.SendMessage($"/tell {name}@{homeworldName} ");
            GameChat.Print(Strings.UserInterface_NearbyPlayers_SetChatTarget.Format($"{name}@{homeworldName}"));
            UIModule.PlaySound((int)SoundEffect.Se16, 0, 0, 0);
        }
    }
}
