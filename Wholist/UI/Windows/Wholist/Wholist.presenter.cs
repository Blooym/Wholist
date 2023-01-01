using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using CheapLoc;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.ImGuiFileDialog;
using Wholist.Base;

namespace Wholist.UI.Windows.Wholist
{
    internal sealed class WholistPresenter : IDisposable
    {
        /// <summary>
        ///    The service instance of <see cref="Configuration" />.
        /// </summary>
        internal static Configuration Configuration => PluginService.Configuration;

        /// <summary>
        ///    The service instance of <see cref="ClientState" />.
        /// </summary>
        internal static ClientState ClientState => PluginService.ClientState;

        /// <summary>
        ///     A dictionary of messages that have been typed to certain players, alongside the date of the last time an entry was modified.
        /// </summary>
        private readonly Dictionary<uint, string> tellMessages = new();

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
        ///     Remove all messages from the tell message dictionary.
        /// </summary>
        internal void RemoveAllTells() => this.tellMessages.Clear();

        /// <summary>
        ///     The timer used to update the list of players from the ObjectTable.
        ///     Note: This only updates the memory addresses, so if an object gets replaced at the same address, it will be drawn immediately.
        /// </summary>
        internal Timer UpdateTimer { get; } = new(1500);

        /// <summary>
        ///     The list of player characters from the last update.
        /// </summary>
        internal IEnumerable<PlayerCharacter> PlayerCharacters { get; private set; } = Enumerable.Empty<PlayerCharacter>();

        /// <summary>
        ///     Constructor.
        /// </summary>
        internal WholistPresenter() => this.UpdateTimer.Elapsed += this.UpdateTimerOnElapsed;

        /// <summary>
        ///     Disposes of the presenter and its resources.
        /// </summary>
        public void Dispose()
        {
            this.UpdateTimer.Elapsed -= this.UpdateTimerOnElapsed;

            this.UpdateTimer.Stop();
            this.UpdateTimer.Dispose();
        }

        /// <summary>
        ///     Pulls the list of players from the ObjectTable.
        /// </summary>
        /// <returns>The list of players.</returns>
        private static IEnumerable<PlayerCharacter> GetPlayerCharacters() => PluginService.ObjectTable
            .Where(o => o is PlayerCharacter)
            .Cast<PlayerCharacter>()
            .Where(o => o.Level > 0 && o != ClientState.LocalPlayer);

        /// <summary>
        ///     Updates the list of players.
        /// </summary>
        internal void UpdatePlayerList() => this.PlayerCharacters = GetPlayerCharacters();

        /// <summary>
        ///     When the timer elapses, update the list of players.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void UpdateTimerOnElapsed(object? sender, ElapsedEventArgs e) => this.UpdatePlayerList();

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
            PluginService.PluginInterface.UiBuilder.AddNotification("Localization exported successfully.", PluginConstants.PluginName, Dalamud.Interface.Internal.Notifications.NotificationType.Success);
        }
#endif
    }
}
