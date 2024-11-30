using System;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings
{
    internal sealed class SettingsLogic
    {
        /// <summary>
        ///     Gets the localized name of a configuration tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The localized name of the tab.</returns>
        internal static string ConfigTabName(ConfigurationTabs tab) => tab switch
        {
            ConfigurationTabs.Window => Strings.UserInterface_Settings_NearbyPlayers_Window,
            ConfigurationTabs.Appearance => Strings.UserInterface_Settings_NearbyPlayers_Appearance,
            ConfigurationTabs.Behaviour => Strings.UserInterface_Settings_NearbyPlayers_Behaviour,
            _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, null),
        };

        /// <summary>
        ///     The available sidebar tabs.
        /// </summary>
        internal enum ConfigurationTabs
        {
            Window,
            Appearance,
            Behaviour
        }

        /// <summary>
        ///     The currently selected sidebar tab.
        /// </summary>
        internal ConfigurationTabs SelectedTab = ConfigurationTabs.Appearance;
    }
}
