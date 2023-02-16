using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Wholist.Common;
using Wholist.IntegrationHandling.Interfaces;
using Wholist.IntegrationHandling.OutboundProviders;

namespace Wholist.IntegrationHandling
{
    internal sealed class OutboundIpcManager : IDisposable
    {

        #region Methods

        /// <summary>
        ///     Scans the assembly for outbound IPC providers, creates new instances of them, and returns them.
        /// </summary>
        /// <remarks>
        ///     This will also load the providers if their configuration indicates to do so.
        /// </remarks>
        /// <returns>A <see cref="HashSet{T}" /> of <see cref="IOutboundIpcProvider" />.</returns>
        private static HashSet<IOutboundIpcProvider> LoadOutboundIpcProviders()
        {
            var loadedProviders = new HashSet<IOutboundIpcProvider>();

            // Scan the assembly for outbound IPC providers.
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(OutboundIpcProviderBase)) & !type.IsAbstract & !type.IsInterface)
                {
                    // Create a new instance of the provider.
                    var provider = (IOutboundIpcProvider?)Activator.CreateInstance(type, true);
                    if (provider == null)
                    {
                        BetterLog.Error($"Failed to create outbound IPC provider {type.Name} - skipping.");
                        continue;
                    }

                    loadedProviders.Add(provider);
                    BetterLog.Debug($"Created outbound IPC provider {provider.Name}.");
                }
            }

            return loadedProviders;
        }

        /// <summary>
        ///     Gets all loaded outbound IPC providers as an <see cref="ImmutableHashSet{T}" />.
        /// </summary>
        /// <returns></returns>
        internal ImmutableHashSet<IOutboundIpcProvider> GetOutboundIpcProviders() => this.outboundIpcProviders.ToImmutableHashSet();

        /// <summary>
        ///     Gets a specific outbound IPC provider.
        /// </summary>
        /// <typeparam name="T">The type of the outbound IPC provider to get.</typeparam>
        /// <returns>The outbound IPC provider, or null if it was not found.</returns>
        internal T? GetOutboundIpcProvider<T>() where T : class, IOutboundIpcProvider => this.outboundIpcProviders.FirstOrDefault(x => x is T) as T;

        #endregion

        #region Constructor and Dispose

        /// <summary>
        ///     Creates a new instance of <see cref="OutboundIpcManager" />
        /// </summary>
        private OutboundIpcManager() => this.outboundIpcProviders = LoadOutboundIpcProviders();

        /// <summary>
        ///     Disposes of the <see cref="OutboundIpcManager" />.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposedValue)
            {
                foreach (var provider in this.outboundIpcProviders)
                {
                    provider.Unload();
                }
                this.outboundIpcProviders.Clear();
                this.disposedValue = true;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        ///     All loaded outbound IPC providers.
        /// </summary>
        private readonly HashSet<IOutboundIpcProvider> outboundIpcProviders;

        private bool disposedValue;

        #endregion

    }
}
