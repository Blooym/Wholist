using System.IO;
using Newtonsoft.Json;
using Wholist.Common;
using Wholist.IntegrationHandling.Interfaces;

namespace Wholist.IntegrationHandling.OutboundProviders
{
    /// <summary>
    ///     Utility class for outbound IPC providers.
    /// </summary>
    internal static class OutboundIpcProviderUtil
    {
        /// <summary>
        ///     Saves the outbound ipc provider configuration to disk.
        /// </summary>
        /// <param name="configuration">The configuration to save.</param>
        internal static void SaveConfiguration(IOutboundIpcConfiguration configuration)
        {
            if (Directory.Exists(Constants.Directory.Integrations))
            {
                Directory.CreateDirectory(Constants.Directory.Integrations);
            }

            var configJson = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(Path.Combine(Constants.Directory.Integrations, $"{configuration.GetType().Name}.json"), configJson);
        }

        /// <summary>
        ///     Loads the outbound ipc provider configuration from disk, or returns a new instance if the configuration does not
        ///     exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal static T LoadConfiguration<T>() where T : IOutboundIpcConfiguration, new()
        {
            if (!Directory.Exists(Constants.Directory.Integrations))
            {
                Directory.CreateDirectory(Constants.Directory.Integrations);
            }

            var configPath = Path.Combine(Constants.Directory.Integrations, $"{typeof(T).Name}.json");

            if (!File.Exists(configPath))
            {
                return new T();
            }

            var configJson = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<T>(configJson) ?? new T();
        }
    }
}
