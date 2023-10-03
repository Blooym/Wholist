using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Plugin.Services;
using Dalamud.Utility;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ImGuiNET;
using Sirensong.Game.Enums;
using Sirensong.Game.Helpers;
using Wholist.Common;
using Wholist.Configuration;
using Wholist.DataStructures;
using Wholist.Game;
using Wholist.IntegrationHandling;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.NearbyPlayers
{
    internal sealed class NearbyPlayersLogic
    {

        #region Fields

        /// <summary>
        ///     The search text to apply to the object table.
        /// </summary>
        internal string SearchText = string.Empty;

        #endregion

        #region Methods

        /// <inheritdoc cref="MapHelper.FlagAndOpenCurrentMap(Vector3, string?, MapType)" />
        internal static void FlagAndOpen(Vector3 position, string? title = null, MapType mapType = MapType.FlagMarker) => MapHelper.FlagAndOpenCurrentMap(position, title, mapType);

        /// <inheritdoc cref="InboundIpcManager.GetPlayerContextMenuItems" />
        internal static IReadOnlyDictionary<string, string> GetExternContextMenuItems() => Services.InboundIpcManager.GetPlayerContextMenuItems();

        /// <inheritdoc cref="InboundIpcManager.InvokePlayerContextMenu" />
        internal static void InvokeExternPlayerContextMenu(string id, PlayerCharacter playerCharacter) => Services.InboundIpcManager.InvokePlayerContextMenu(id, playerCharacter);

        /// <inheritdoc cref="PlayerManager.GetNearbyPlayers" />
        internal List<PlayerInfoSlim> GetNearbyPlayers()
        {
            var players = new List<PlayerInfoSlim>();
            var nearbyPlayers = Services.PlayerManager.GetNearbyPlayers(
                Configuration.NearbyPlayers.MaxPlayersToShow,
                Configuration.NearbyPlayers.FilterAfk,
                Configuration.NearbyPlayers.PrioritizeKnown);

            foreach (var player in nearbyPlayers)
            {
                if (!this.SearchText.IsNullOrWhitespace() && !player.Name.Contains(this.SearchText, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                players.Add(player);
            }
            return players;
        }

        /// <summary>
        ///     Searches for the player on the lodestone.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="homeworld">The name of the player's homeworld.</param>
        internal static void SearchPlayerOnLodestone(string name, string homeworld) => Util.OpenLink($"https://eu.finalfantasyxiv.com/lodestone/character/?q={name}&worldname={homeworld}");

        /// <summary>
        ///     Applies the current flag configuration to the window.
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
        ///     Sets the chat target to the given player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="homeworldName">The homeworld name of the player.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal static void SetChatTellTarget(string name, string homeworldName)
        {
            Services.XivCommon.Functions.Chat.SendMessage($"/tell {name}@{homeworldName}");
            ChatHelper.Print(Strings.UserInterface_NearbyPlayers_SetChatTarget.Format($"{name}@{homeworldName}"));
            UIModule.PlaySound((int)SoundEffect.Se16, 0, 0, 0);
        }

        /// <summary>
        ///     Gets the job name of the given player based on the current configuration.
        /// </summary>
        /// <param name="job">The job of the player.</param>
        /// <returns>The job name.</returns>
        internal static string GetJobName(JobInfoSlim job) => Configuration.NearbyPlayers.UseJobAbbreviations ? job.Abbreviation : job.Name;

        /// <summary>
        ///     Gets the job colour of the given player based on the current configuration.
        /// </summary>
        /// <param name="job">The job of the player.</param>
        /// <returns>The job colour.</returns>
        /// <exception cref="NotImplementedException">When the colour mode is not implemented or unknown.</exception>
        internal static Vector4 GetJobColour(JobInfoSlim job) => Configuration.Colours.JobColMode switch
        {
            PluginConfiguration.ColourConfiguration.JobColourMode.Job => job.JobColour,
            PluginConfiguration.ColourConfiguration.JobColourMode.Role => job.RoleColour,
            _ => throw new NotImplementedException()
        };

        #endregion

        #region Properties

        /// <inheritdoc cref="PluginConfiguration" />
        internal static PluginConfiguration Configuration => Services.Configuration;

        /// <inheritdoc cref="Dalamud.Game.ClientState.Conditions.ICondition" />
        internal static ICondition Condition => Services.Condition;

        /// <inheritdoc cref="ClientState.IsPvP" />
        internal static bool IsPvP => Services.ClientState.IsPvP;

        /// <summary>
        ///     Whether or not the window should be closed when the escape key is pressed.
        /// </summary>
        internal static bool ShouldDisableEscClose => Configuration.NearbyPlayers.LockPosition;

        #endregion

    }
}
