namespace Wholist.IntegrationHandling.Interfaces
{
    /// <summary>
    ///     The interface for an outbound IPC provider.
    /// </summary>
    internal interface IOutboundIpcProvider
    {
        /// <summary>
        ///     The name of the provider.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     The description of the provider.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Whether or not the provider has been force disabled.
        /// </summary>
        bool ForceDisabled { get; }

        /// <summary>
        ///     The configuration for the provider.
        /// </summary>
        IOutboundIpcConfiguration Configuration { get; }

        /// <summary>
        ///     Loads the outbound IPC provider.
        /// </summary>
        void Load();

        /// <summary>
        ///     Unloads the IPC provider.
        /// </summary>
        void Unload();
    }
}
