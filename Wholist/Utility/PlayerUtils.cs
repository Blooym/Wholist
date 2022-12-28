using System;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Wholist.Base;

namespace Wholist.Utility
{
    /// <summary>
    ///     Utility class for player-related methods.
    /// </summary>
    internal static class PlayerUtils
    {
        /// <summary>
        ///     Checks if the local player is logged in.
        /// </summary>
        internal static bool PlayerLoggedIn => PluginService.ClientState.LocalPlayer != null;

        /// <summary>
        ///     Opens the given player's adventurer plate.
        /// </summary>
        /// <param name="address">The address.</param>
        internal static unsafe void OpenPlayerPlate(IntPtr address) => AgentCharaCard.Instance()->OpenCharaCard((FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject*)address);

        /// <summary>
        ///    Opens the given player's examine window.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        internal static void OpenPlayerExamine(Dalamud.Game.ClientState.Objects.Types.GameObject gameObject) => PluginService.XivCommon.Functions.Examine.OpenExamineWindow(gameObject);

        /// <summary>
        ///     Sets the local player's target to the given game object.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        internal static void SetPlayerTarget(Dalamud.Game.ClientState.Objects.Types.GameObject gameObject) => PluginService.TargetManager.SetTarget(gameObject);

        /// <summary>
        ///     Sanitizes text & sends a tell to the given player.
        /// </summary>
        /// <param name="playerObject">The player object.</param>
        /// <param name="message">The message.</param>
        internal static bool SendTell(Dalamud.Game.ClientState.Objects.SubKinds.PlayerCharacter playerCharacter, string message)
        {
            try
            {
                message = PluginService.XivCommon.Functions.Chat.SanitiseText(message).Trim();
                PluginService.XivCommon.Functions.Chat.SendMessage($"/tell {playerCharacter.Name}@{playerCharacter.HomeWorld.GameData?.Name} {message}");
                return true;
            }
            catch (Exception ex)
            {
                PluginService.ChatGui.PrintError($"[Wholist] Error sending tell: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        ///     Checks to see whether or not the given player is AFK.
        /// </summary>
        /// <param name="playerObject">The player object.</param>
        /// <returns>True if the player is AFK, false otherwise.</returns>
        internal static bool IsPlayerAFK(Dalamud.Game.ClientState.Objects.SubKinds.PlayerCharacter playerCharacter) => playerCharacter.OnlineStatus.Id == 17;

        /// <summary>
        ///     Checks to see whether or not the given player is busy.
        /// </summary>
        /// <param name="playerObject">The player object.</param>
        /// <returns>True if the player is busy, false otherwise.</returns>
        internal static bool IsPlayerBusy(Dalamud.Game.ClientState.Objects.SubKinds.PlayerCharacter playerCharacter) => playerCharacter.OnlineStatus.Id == 12;

        /// <summary>
        ///     Returns a boolean of whether the player is a potential bot.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if the player is a potential bot, false otherwise.</returns>
        internal static bool IsPlayerBot(Dalamud.Game.ClientState.Objects.SubKinds.PlayerCharacter player) => player.Level <= 4 && player.ClassJob.Id == 3;
    }
}
