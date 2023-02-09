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
        public ConfigurationTabs SelectedTab = ConfigurationTabs.General;

        /// <summary>
        /// The available sidebar tabs.
        /// </summary>
        public enum ConfigurationTabs
        {
            General,
            Colours,
            Donation,
            Debug,
        }

        public static string ConfigTabName(ConfigurationTabs tab) => tab switch
        {
            ConfigurationTabs.General => Strings.UserInterface_Settings_General_Heading,
            ConfigurationTabs.Colours => tab.ToString(),
            ConfigurationTabs.Donation => Strings.UserInterface_Settings_Donate_Heading,
            ConfigurationTabs.Debug => Strings.UserInterface_Settings_Debug_Heading,
            _ => tab.ToString(),
        };
    }
}