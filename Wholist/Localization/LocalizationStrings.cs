using CheapLoc;

namespace Wholist.Localization
{
    /// <summary>
    ///     A collection of translatable command help strings.
    /// </summary>
    internal sealed class TCommandHelp
    {
        public static string WholistHelp => Loc.Localize("Commands.Wholist.Help", "Opens the Wholist window.");
    }

    /// <summary>
    ///     A collection of translatable window strings.
    /// </summary>
    internal sealed class TWindowNames
    {
        public static string Wholist => Loc.Localize("Window.Wholist", "Wholist");
    }

    /// <summary>
    ///    A collection of translatable strings for the Wholist window.
    /// </summary>
    internal sealed class TWholistWindow
    {
        public static string CantUseInPvP => Loc.Localize("Window.Wholist.CantUseInPvP", "You can't use the wholist whilst participating in PvP.");
        public static string Name => Loc.Localize("Window.Wholist.Name", "Name");
        public static string Company => Loc.Localize("Window.Wholist.Company", "Company");
        public static string Level => Loc.Localize("Window.Wholist.Level", "Level");
        public static string Class => Loc.Localize("Window.Wholist.Class", "Class");
        public static string Total(int size) => string.Format(Loc.Localize("Window.Wholist.Total", "Total: {0}"), size);
        public static string ActionsFor(string name) => string.Format(Loc.Localize("Window.Wholist.ActionsFor", "Actions for {0}"), name);
        public static string Target => Loc.Localize("Window.Wholist.Target", "Target");
        public static string Examine => Loc.Localize("Window.Wholist.Examine", "Examine");
        public static string NoPlayersFound => Loc.Localize("Window.Wholist.NoPlayersFound", "No players found.");
        public static string SearchForPlayer => Loc.Localize("Window.Wholist.SearchForPlayer", "Search for player...");
        public static string SuspectedBot => Loc.Localize("Window.Wholist.SuspectedBot", "This player could be a bot account (or a very new player matching bot behaviours).");
    }
}
