using Wholist.Common;
using Wholist.Configuration;
using Wholist.Resources.Localization;

namespace Wholist.UserInterface.Windows.Settings
{
    internal sealed class SettingsLogic
    {
        /// <summary>
        /// Gets the plugin configuration.
        /// </summary>
        public static PluginConfiguration Configuration { get; } = Services.Configuration;

        /// <summary>
        /// The currently selected sidebar tab.
        /// </summary>
        public ConfigurationTabs SelectedTab = ConfigurationTabs.NearbyPlayers;

        /// <summary>
        /// The available sidebar tabs.
        /// </summary>
        public enum ConfigurationTabs
        {
            NearbyPlayers,
            Colours,
            Donation,
            Debug,
        }

        /// <summary>
        /// Gets the localized name of a configuration tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <returns>The localized name of the tab.</returns>
        public static string ConfigTabName(ConfigurationTabs tab) => tab switch
        {
            ConfigurationTabs.NearbyPlayers => Strings.UserInterface_Settings_NearbyPlayers_Heading,
            ConfigurationTabs.Colours => Strings.UserInterface_Settings_Colours_Heading,
            ConfigurationTabs.Donation => Strings.UserInterface_Settings_Donate_Heading,
            ConfigurationTabs.Debug => Strings.UserInterface_Settings_Debug_Heading,
            _ => tab.ToString(),
        };
    }
}
