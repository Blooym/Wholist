using System;
using ImGuiNET;
using Sirensong.UserInterface;
using Sirensong.UserInterface.Style;
using Wholist.Common;
using Wholist.IntegrationHandling.Interfaces;

namespace Wholist.IntegrationHandling.OutboundProviders
{
    /// <summary>
    ///     Base class for outbound IPC providers.
    /// </summary>
    internal abstract class OutboundIpcProviderBase : IOutboundIpcProvider
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="OutboundIpcProviderBase" /> class.
        /// </summary>
        internal OutboundIpcProviderBase()
        {
            if (this.Configuration.Enabled)
            {
                this.Load();
            }
        }

        /// <inheritdoc />
        public abstract IOutboundIpcConfiguration Configuration { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public abstract string Description { get; }

        /// <inheritdoc />
        public bool ForceDisabled { get; private set; }

        /// <inheritdoc />
        public void Load()
        {
            if (this.ForceDisabled)
            {
                BetterLog.Warning($"A load request was made for outbound IPC provider {this.Name}, but it is force disabled.");
                return;
            }

            try
            {
                this.LoadInternal();
                BetterLog.Information($"Loaded IPC provider {this.Name}");
            }
            catch (Exception e)
            {
                this.ForceDisabled = true;
                BetterLog.Error($"Error loading IPC provider {this.Name}, forcing disabled until next restart: {e}");
            }
        }

        /// <inheritdoc />
        public void Unload()
        {
            if (this.ForceDisabled)
            {
                BetterLog.Warning($"An unload request was made for outbound IPC provider {this.Name}, but it is force disabled.");
                return;
            }

            try
            {
                this.UnloadInternal();
                BetterLog.Information($"Unloaded IPC provider {this.Name}");
            }
            catch (Exception e)
            {
                this.ForceDisabled = true;
                BetterLog.Error($"Error unloading IPC provider {this.Name}, forcing disabled until next restart: {e}");
            }
        }

        /// <summary>
        ///     Draws the ImGui configuration elements for the IPC provider.
        /// </summary>
        /// <remarks>
        ///     This is called inside of a try-catch and is embedded inside of a window already. You only need to draw the actual
        ///     configuration elements (not including Enable/Disable).
        /// </remarks>
        protected abstract void DrawConfigurationInternal();

        /// <summary>
        ///     Draws the ImGui configuration elements for the IPC provider.
        /// </summary>
        public void DrawConfiguration()
        {
            if (this.ForceDisabled)
            {
                SiGui.TextWrappedColoured(Colours.Error,
                    $"{this.Name} encounted an error and has been forcefully disabled until the next plugin restart to prevent further problems. Please check Dalamud's log for more information.");

                return;
            }

            try
            {
                SiGui.Heading(this.Name);
                SiGui.TextWrapped(this.Description);
                ImGui.Separator();

                var enabled = this.Configuration.Enabled;
                if (SiGui.Checkbox("Enabled", ref enabled))
                {
                    this.Configuration.Enabled = enabled;
                    OutboundIpcProviderUtil.SaveConfiguration(this.Configuration);
                }

                this.DrawConfigurationInternal();
            }
            catch (Exception e)
            {
                SiGui.TextWrappedColoured(Colours.Error, $"Error drawing configuration for {this.Name}: {e}");
            }
        }

        /// <summary>
        ///     Loads the IPC provider and sets up any IPC calls.
        /// </summary>
        protected abstract void LoadInternal();

        /// <summary>
        ///     Unloads the IPC provider and dispose of any IPC calls.
        /// </summary>
        protected abstract void UnloadInternal();
    }
}
