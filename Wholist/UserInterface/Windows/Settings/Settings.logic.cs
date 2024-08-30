using System;
using Wholist.Common;
using Wholist.Configuration;

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
            ConfigurationTabs.Window => "Window",
            ConfigurationTabs.Appearance => "Appearance",
            ConfigurationTabs.Behaviour => "Behaviour",
            _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, null),
        };

        #endregion

        #region Enums

        /// <summary>
        ///     The available sidebar tabs.
        /// </summary>
        internal enum ConfigurationTabs
        {
            Window,
            Appearance,
            Behaviour
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The currently selected sidebar tab.
        /// </summary>
        internal ConfigurationTabs SelectedTab = ConfigurationTabs.Appearance;

        /// <summary>
        ///     Gets the plugin configuration.
        /// </summary>
        internal static PluginConfiguration Configuration { get; } = Services.Configuration;

        #endregion

    }
}
