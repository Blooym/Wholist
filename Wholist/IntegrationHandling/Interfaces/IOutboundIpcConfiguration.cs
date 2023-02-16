namespace Wholist.IntegrationHandling.Interfaces
{
    /// <summary>
    ///     The configuration for an outbound IPC provider.
    /// </summary>
    public interface IOutboundIpcConfiguration
    {
        /// <summary>
        ///     The version of the configuration.
        /// </summary>
        uint Version { get; set; }

        /// <summary>
        ///     Whether or not the IPC provider is enabled.
        /// </summary>
        bool Enabled { get; set; }
    }
}
