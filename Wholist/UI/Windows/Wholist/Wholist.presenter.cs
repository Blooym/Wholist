using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CheapLoc;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.ImGuiFileDialog;
using Dalamud.Interface.Internal.Notifications;
using Dalamud.Utility;
using Lumina.Excel.GeneratedSheets;
using Sirensong;
using Sirensong.Caching;
using Sirensong.Game.Enums;
using Sirensong.Game.Extensions;
using Sirensong.Game.UI;
using Sirensong.UserInterface;

namespace Wholist.UI.Windows.Wholist
{
    internal sealed class WholistPresenter
    {
        /// <inheritdoc cref="Configuration" />
        internal static Configuration Configuration => Services.Configuration;

        /// <inheritdoc cref="ClientState" />
        internal static ClientState ClientState => Services.ClientState;

        /// <inheritdoc cref="LuminaCacheService{T}" />
        internal static LuminaCacheService<World> WorldCache => SirenCore.GetOrCreateService<LuminaCacheService<World>>();

        /// <inheritdoc cref="LuminaCacheService{T}" />
        internal static LuminaCacheService<ClassJob> ClassJobCache => SirenCore.GetOrCreateService<LuminaCacheService<ClassJob>>();

        /// <summary>
        ///     Pulls a list of <see cref="PlayerCharacter" /> from the object table any applies the filter & configuration.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal static IEnumerable<PlayerCharacter> GetOTPlayers(string filter)
            => Services.ObjectTable.GetPlayerCharacters(false)
                .Where(o => !Configuration.FilterAfk || !o.HasOnlineStatus(OnlineStatuses.AFK))
                .Where(o => o.Level >= 4 && o.ClassJob.Id != 3)
                .Where(o => filter.IsNullOrWhitespace() || o.Name.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)
                    || ClassJobCache.GetRow(o.ClassJob.Id)?.Name.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase) == true);

        /// <summary>
        ///     All stored tell messages. Key is ContentID, value is message.
        /// </summary>
        private readonly Dictionary<uint, string> tellMessages = new();

        /// <summary>
        ///     The maximum length of a tell message.
        /// </summary>
        internal const int MaxMsgLength = 400;

        /// <summary>
        ///     Whether or not the given message is valid.
        /// </summary>
        /// <param name="message">The message.</param>s
        /// <returns>True if valid, false otherwise.</returns>
        internal static bool IsMessageValid(string message) => string.IsNullOrWhiteSpace(message) && message.Length <= MaxMsgLength;

        /// <summary>
        ///     Adds a message to the tell message dictionary.
        /// </summary>
        /// <param name="targetId">The target id.</param>
        /// <param name="message">The message.</param>
        internal void SetTell(uint targetId, string message) => this.tellMessages[targetId] = message;

        /// <summary>
        ///     Gets a message from the tell message dictionary.
        /// </summary>
        /// <param name="targetId">The target id.</param>
        /// <returns>The message.</returns>
        internal string GetTell(uint targetId) => this.tellMessages.TryGetValue(targetId, out var message) ? message : string.Empty;

        /// <summary>
        ///     Removes a message from the tell message dictionary.
        /// </summary>
        /// <param name="targetId">The target id.</param>
        internal void RemoveTell(uint targetId) => this.tellMessages.Remove(targetId);

        /// <summary>
        ///     Clears all messages from the tell message dictionary.
        /// </summary>
        internal void ClearTells() => this.tellMessages.Clear();

        /// <summary>
        ///     Sanitizes text & sends a tell to the given player.
        /// </summary>
        /// <param name="playerObject">The player object.</param>
        /// <param name="message">The message.</param>
        internal static bool SendTell(PlayerCharacter playerCharacter, string message)
        {
            try
            {
                message = Services.XivCommon.Functions.Chat.SanitiseText(message).Trim();
                Services.XivCommon.Functions.Chat.SendMessage($"/tell {playerCharacter.Name}@{playerCharacter.HomeWorld.GameData?.Name} {message}");
                return true;
            }
            catch (Exception ex)
            {
                Toasts.ShowErrorToast(LStrings.WholistWindow.ErrorSendingTell(ex.Message));
                return false;
            }
        }

#if DEBUG
        /// <summary>
        ///     The dialog manager for the settings window.
        /// </summary>
        internal FileDialogManager DialogManager = new();

        /// <summary>
        ///     The callback for when the user selects an export directory.
        /// </summary>
        /// <param name="cancelled">Whether the user cancelled the dialog.</param>
        /// <param name="path">The path the user selected.</param>
        internal static void OnDirectoryPicked(bool cancelled, string path)
        {
            if (!cancelled)
            {
                return;
            }

            var directory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(path);
            Loc.ExportLocalizable();
            File.Copy(Path.Combine(path, "Wholist_Localizable.json"), Path.Combine(path, "en.json"), true);
            Directory.SetCurrentDirectory(directory);
            SiUI.ShowToast("Localization exported successfully.", NotificationType.Success);
        }
#endif
    }
}
