using System;
using Wholist.Common;
using Wholist.Configuration;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings
{
    internal sealed class SettingsLogic
    {

        #region Methods

        /// <summary>
        ///     Gets the localized name of a configuration tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The localized name of the tab.</returns>
        internal static string ConfigTabName(ConfigurationTabs tab) => tab switch
        {
            ConfigurationTabs.NearbyPlayers => Strings.UserInterface_Settings_NearbyPlayers_Heading,
            ConfigurationTabs.Colours => Strings.UserInterface_Settings_Colours_Heading,
            ConfigurationTabs.Donation => Strings.UserInterface_Settings_Donate_Heading,
            _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, null),
        };

        #endregion

        #region Enums

        /// <summary>
        ///     The available sidebar tabs.
        /// </summary>
        internal enum ConfigurationTabs
        {
            NearbyPlayers,
            Colours,
            Donation,
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The currently selected sidebar tab.
        /// </summary>
        internal ConfigurationTabs SelectedTab = ConfigurationTabs.NearbyPlayers;

        /// <summary>
        ///     Gets the plugin configuration.
        /// </summary>
        internal static PluginConfiguration Configuration { get; } = Services.Configuration;

        #endregion

    }
}
