using System;
using System.IO;
using System.Reflection;
using CheapLoc;

namespace Wholist.Managers
{
    /// <summary>
    ///     Manages resources and localization for the plugin.
    /// </summary>
    internal sealed class ResourceManager : IDisposable
    {
        /// <summary>
        ///     Initializes the ResourceManager and loads localization.
        /// </summary>
        internal ResourceManager()
        {
            this.Setup(Services.PluginInterface.UiLanguage);
            Services.PluginInterface.LanguageChanged += this.Setup;
        }

        /// <summary>
        //      Disposes of the ResourceManager and associated resources.
        /// </summary>
        public void Dispose() => Services.PluginInterface.LanguageChanged -= this.Setup;

        /// <summary>
        ///     Sets up the plugin's localization.
        /// </summary>
        /// <param name="language">The ISO language code.</param>
        private void Setup(string language)
        {
            try
            {
                using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Wholist.Resources.Localization.{language}.json");

                if (resource == null)
                {
                    throw new FileNotFoundException($"Could not find resource file for language {language}.");
                }

                using var reader = new StreamReader(resource);
                Loc.Setup(reader.ReadToEnd());
            }
            catch (Exception)
            {
                Loc.SetupWithFallbacks();
            }
        }
    }
}
