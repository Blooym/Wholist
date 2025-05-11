using System;
using System.Collections.Generic;
using System.Timers;
using Dalamud.Plugin.Ipc;
using Wholist.Common;

namespace Wholist.Interop
{
    internal sealed class IpcManager : IDisposable
    {
        private bool disposedValue;

        // MareSynchronos
        private readonly Timer mareUpdateActivePairsTimer = new(TimeSpan.FromSeconds(4));
        private readonly ICallGateSubscriber<List<nint>> mareActivePairCallGateSubscriber = Services.PluginInterface.GetIpcSubscriber<List<nint>>("MareSynchronos.GetHandledAddresses");
        public List<nint> MareActivePairs { get; private set; } = [];
        public bool MareActivePairsIpcAvailable => this.mareActivePairCallGateSubscriber.HasFunction;

        private IpcManager()
        {
            this.UpdateMareActivePairs(null, null);
            this.mareUpdateActivePairsTimer.Start();
            this.mareUpdateActivePairsTimer.Elapsed += this.UpdateMareActivePairs;
        }

        public void Dispose()
        {
            if (this.disposedValue)
            {
                return;
            }
            this.mareUpdateActivePairsTimer.Elapsed -= this.UpdateMareActivePairs;
            this.mareUpdateActivePairsTimer.Dispose();
            this.disposedValue = true;
        }

        private void UpdateMareActivePairs(object? sender, EventArgs? e)
        {
            ObjectDisposedException.ThrowIf(this.disposedValue, this);
            if (this.MareActivePairsIpcAvailable)
            {
                this.MareActivePairs = this.mareActivePairCallGateSubscriber.InvokeFunc();
            }
        }
    }
}
