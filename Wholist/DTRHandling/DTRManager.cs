using System;
using System.Linq;
using System.Timers;
using Dalamud.Game.Gui.Dtr;
using Wholist.Common;
using Wholist.Game;
using Wholist.Resources.Localization;

namespace Wholist.DTRHandling
{
    public sealed class DTRManager : IDisposable
    {
        private const string NEARBY_PLAYERS_DTR_ICON_PAYLOAD = "\uE033";
        private readonly IDtrBarEntry nearbyPlayersDtrEntry;
        private readonly Timer nearbyPlayersDtrUpdateTimer = new(TimeSpan.FromSeconds(2));

        public DTRManager()
        {
            var nearbyPlayerCount = PlayerManager.GetNearbyPlayers().Count();
            this.nearbyPlayersDtrEntry = Services.DtrBar.Get("Nearby Player Count", $"{NEARBY_PLAYERS_DTR_ICON_PAYLOAD} {nearbyPlayerCount}");
            this.nearbyPlayersDtrEntry.Tooltip = string.Format(Strings.DTR_NearbyPlayers_Tooltip, nearbyPlayerCount);
            this.nearbyPlayersDtrEntry.OnClick += DtrOnClick;
            this.nearbyPlayersDtrUpdateTimer.Elapsed += this.OnNearbyPlayerUpdaterTimeElapsed;
            this.nearbyPlayersDtrUpdateTimer.Start();
        }

        public void Dispose()
        {
            this.nearbyPlayersDtrUpdateTimer.Elapsed -= this.OnNearbyPlayerUpdaterTimeElapsed;
            this.nearbyPlayersDtrUpdateTimer.Stop();
            this.nearbyPlayersDtrUpdateTimer.Dispose();
            this.nearbyPlayersDtrEntry.OnClick -= DtrOnClick;
            this.nearbyPlayersDtrEntry.Remove();
        }

        private static void DtrOnClick() => Services.WindowManager.ToggleMainWindow();

        private void OnNearbyPlayerUpdaterTimeElapsed(object? sender, ElapsedEventArgs e)
        {
            if (this.nearbyPlayersDtrEntry.UserHidden)
            {
                return;
            }

            Services.Framework.RunOnFrameworkThread(() =>
           {
               var nearbyPlayerCount = PlayerManager.GetNearbyPlayers().Count();
               this.nearbyPlayersDtrEntry.Text = $"{NEARBY_PLAYERS_DTR_ICON_PAYLOAD} {nearbyPlayerCount}";
               this.nearbyPlayersDtrEntry.Tooltip = string.Format(Strings.DTR_NearbyPlayers_Tooltip, nearbyPlayerCount);
           });
        }

    }
}
