using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using CheapLoc;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Interface.ImGuiFileDialog;
using Wholist.Base;

namespace Wholist.UI.Windows.Wholist
{
    public sealed class WholistPresenter : IDisposable
    {
        /// <summary>
        ///     The timer used to update the list of players from the ObjectTable.
        ///     Note: This only updates the memory addresses, so if an object gets replaced at the same address, it will be drawn immediately.
        /// </summary>
        public Timer UpdateTimer { get; } = new(1500);

        /// <summary>
        ///     The list of player characters from the last update.
        /// </summary>
        public IEnumerable<PlayerCharacter> PlayerCharacters { get; private set; } = Enumerable.Empty<PlayerCharacter>();

        /// <summary>
        ///     Constructor.
        /// </summary>
        public WholistPresenter() => this.UpdateTimer.Elapsed += this.UpdateTimerOnElapsed;

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
        private static IEnumerable<PlayerCharacter> GetPlayerCharacters() => PluginService.ObjectTable.Where(o => o is PlayerCharacter).Cast<PlayerCharacter>();

        /// <summary>
        ///     Updates the list of players.
        /// </summary>
        public void UpdatePlayerList() => this.PlayerCharacters = GetPlayerCharacters();

        /// <summary>
        ///     When the timer elapses, update the list of players.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void UpdateTimerOnElapsed(object? sender, ElapsedEventArgs e) => this.UpdatePlayerList();

        /// <summary>
        ///    Returns a boolean of whether or not the player is AFK.
        /// </summary>
        public static bool IsPlayerAfk(PlayerCharacter player) => player.OnlineStatus.Id == 17;

        /// <summary>
        ///    Returns a boolean of whether the player is a bot or not.
        /// </summary>
        public static bool IsPlayerBot(PlayerCharacter player) => player.Level <= 3 || player.ClassJob.Id == 3;

        /// <summary>
        ///    Returns a boolean of whether the player is on busy mode or not.
        /// </summary>
        public static bool IsPlayerBusy(PlayerCharacter player) => player.OnlineStatus.Id == 12;

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
        public static void OnDirectoryPicked(bool cancelled, string path)
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
