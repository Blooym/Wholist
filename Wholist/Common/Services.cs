using System;
using System.Collections.Generic;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Sirensong;
using Wholist.CommandHandling;
using Wholist.Configuration;
using Wholist.Resources.Localization;
using Wholist.UserInterface;
using XivCommon;

namespace Wholist.Common
{
    /// <summary>
    /// Provides access to necessary instances and services.
    /// </summary>
    internal sealed class Services
    {
        [PluginService] internal static DalamudPluginInterface PluginInterface { get; private set; } = null!;
        [PluginService] internal static ClientState ClientState { get; private set; } = null!;
        [PluginService] internal static ObjectTable ObjectTable { get; private set; } = null!;
        [PluginService] internal static Dalamud.Game.Command.CommandManager Commands { get; private set; } = null!;

        internal static WindowManager WindowManager { get; private set; } = null!;
        internal static XivCommonBase XivCommon { get; private set; } = null!;
        internal static PluginConfiguration PluginConfiguration { get; private set; } = null!;

        // Additional services
        private static readonly List<object> ServiceContainer = new();

        /// <summary>
        /// Initializes the service class.
        /// </summary>
        internal static void Initialize(DalamudPluginInterface pluginInterface)
        {
            BetterLog.Debug("Initializing services.");

            SirenCore.InjectServices<Services>();
            pluginInterface.Create<Services>();

            PluginConfiguration = PluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            XivCommon = new XivCommonBase();
            GetOrCreateService<LocalizationManager>();
            WindowManager = GetOrCreateService<WindowManager>();
            GetOrCreateService<CommandManager>();
        }

        /// <summary>
        /// Disposes of the service class.
        /// </summary>
        internal static void Dispose()
        {
            // Unregister all services
            foreach (var service in ServiceContainer.ToArray())
            {
                if (service is IDisposable disposable)
                {
                    disposable.Dispose();
                    BetterLog.Debug($"Disposed of service: {service.GetType().Name}");
                }


                BetterLog.Debug($"Unregistered service: {service.GetType().Name}");
                ServiceContainer.Remove(service);
            }

            XivCommon.Dispose();
        }

        /// <summary>
        /// Gets a service from the service container if it exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The service if found, otherwise <see langword="null"/>.</returns>
        internal static T? GetService<T>() where T : class => ServiceContainer.Find(x => x is T) as T;

        /// <summary>
        /// Creates the service if it does not exist, returns the service either way.
        /// </summary>
        /// <remarks>
        /// If you do not dispose of the service yourself, it will be disposed of when the plugin is unloaded.
        /// </remarks>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service that was created or found.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the service could not be created.</exception>
        internal static T GetOrCreateService<T>() where T : class
        {
            var existingService = GetService<T>();
            if (existingService is not null)
            {
                return existingService;
            }

            BetterLog.Debug($"Creating service: {typeof(T).FullName}");

            if (Activator.CreateInstance(typeof(T), true) is not T service)
            {
                throw new InvalidOperationException($"Could not create service of type {typeof(T).FullName}.");
            }

            ServiceContainer.Add(service);
            BetterLog.Debug($"Service created: {service.GetType().Name}");
            return service;
        }

        /// <summary>
        /// Removes a service from the service container if it exists and disposes of it if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>True if removal was successful, otherwise false.</returns>
        internal static bool RemoveService<T>() where T : class
        {
            var service = ServiceContainer.Find(x => x is T);
            if (service is not null)
            {
                ServiceContainer.Remove(service);

                if (service is IDisposable disposable)
                {
                    BetterLog.Debug($"Disposing service: {service.GetType().Name}");
                    disposable.Dispose();
                }

                BetterLog.Debug($"Unregistered service: {service.GetType().Name}");
                return true;
            }
            return false;
        }
    }
}