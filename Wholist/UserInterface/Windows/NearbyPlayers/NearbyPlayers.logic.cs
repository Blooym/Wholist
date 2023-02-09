using System;
using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.Colors;
using Dalamud.Utility;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Common.Math;
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
        /// <inheritdoc cref="Configuration" />
        internal static PluginConfiguration Configuration => Services.Configuration;

        /// <inheritdoc cref="IsPvPExcludingDen" />
        internal static bool IsPvPExcludingDen => Services.ClientState.IsPvPExcludingDen;

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
        /// Whether or not the window should be closed when the escape key is pressed.
        /// </summary>
        internal static bool DisableEscClose => Configuration.NearbyPlayers.LockPosition;

        /// <summary>
        /// The search text to apply to the object table.
        /// </summary>
        internal string SearchText = string.Empty;

        /// <summary>
        /// Pulls a list of <see cref="PlayerCharacter" /> from the object table any applies the filter & configuration.
        /// </summary>
        /// <returns></returns>
        internal List<PlayerInfoSlim> GetNearbyPlayers() => Services.NearbyPlayerManager.GetNearbyPlayers()
                .Where(o => !Configuration.NearbyPlayers.FilterAfk || o.OnlineStatusId != (uint)OnlineStatusType.AFK)
                .Where(o => this.SearchText.IsNullOrWhitespace() || o.Name.ToString().Contains(this.SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

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

        /// <summary>
        /// Gets the colour for the given player.
        /// </summary>
        /// <param name="playerInfo"></param>
        /// <returns>The colour for the player.</returns>
        internal static Vector4 GetColourForPlayer(PlayerInfoSlim playerInfo)
        {
            if (playerInfo.IsInParty)
            {
                return Configuration.Colours.Party;
            }
            else if (playerInfo.IsFriend)
            {
                return Configuration.Colours.Friend;
            }
            else if (playerInfo.IsInFreeCompany)
            {
                return Configuration.Colours.FreeCompany;
            }
            else
            {
                return ImGuiColors.DalamudWhite;
            }
        }
    }
}
