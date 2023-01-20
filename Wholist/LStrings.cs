using CheapLoc;

namespace Wholist
{
    /// <summary>
    ///     A collection of translatable strings for the plugin.
    /// </summary>
    internal static class LStrings
    {
        internal sealed class Commands
        {
            internal static string WhoHelp => Loc.Localize("Commands.Wholist.Help", "Opens the Wholist window.");
        }

        internal sealed class WholistWindow
        {
            internal static string WindowName => Loc.Localize("Window.Wholist", "Wholist");
            internal static string CantUseInPvP => Loc.Localize("Window.Wholist.CantUseInPvP", "You can't use the wholist whilst participating in PvP.");
            internal static string Name => Loc.Localize("Window.Wholist.Name", "Name");
            internal static string Company => Loc.Localize("Window.Wholist.Company", "Company");
            internal static string Level => Loc.Localize("Window.Wholist.Level", "Level");
            internal static string Class => Loc.Localize("Window.Wholist.Class", "Class");
            internal static string Total(int size) => string.Format(Loc.Localize("Window.Wholist.Total", "Total: {0}"), size);
            internal static string ActionsFor(string name) => string.Format(Loc.Localize("Window.Wholist.ActionsFor", "Actions for {0}"), name);
            internal static string Target => Loc.Localize("Window.Wholist.Target", "Target");
            internal static string Examine => Loc.Localize("Window.Wholist.Examine", "Examine");
            internal static string ViewAdventurerPlate => Loc.Localize("Window.Wholist.ViewAdventurerPlate", "View Adventurer Plate");
            internal static string Tell => Loc.Localize("Window.Wholist.Tell", "Tell");
            internal static string NoPlayersFound => Loc.Localize("Window.Wholist.NoPlayersFound", "No players found.");
            internal static string SearchFor => Loc.Localize("Window.Wholist.SearchFor", "Search for...");
            internal static string SuspectedBot => Loc.Localize("Window.Wholist.SuspectedBot", "This player could be a bot account (or a very new player matching bot behaviours).");
            internal static string HideSuspectedBots => Loc.Localize("Window.Wholist.HideSuspectedBots", "Hide suspected bots");
            internal static string HideAfkPlayers => Loc.Localize("Window.Wholist.HideAfkPlayers", "Hide AFK players");
            internal static string PlayerIsAFK => Loc.Localize("Window.Wholist.PlayerIsAFK", "Player is AFK.");
            internal static string PlayerIsBusy => Loc.Localize("Window.Wholist.PlayerIsBusy", "Player is busy.");
            internal static string MustBeLoggedIn => Loc.Localize("Window.Wholist.MustBeLoggedIn", "You must be logged in to a character to use this.");
            internal static string SendMessage => Loc.Localize("Window.Wholist.SendMessage", "Send Message");
            internal static string ExportLocalization => Loc.Localize("Window.Wholist.ExportLocalization", "Export Localization");
            internal static string ErrorSendingTell(string err) => string.Format(Loc.Localize("Window.Wholist.ErrorSendingTell", "Error sending tell: {0}"), err);
        }
    }
}
