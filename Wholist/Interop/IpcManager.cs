using System;
using System.Collections.Generic;
using Dalamud.Plugin.Ipc;
using Wholist.Common;

namespace Wholist.Interop
{
    internal sealed class IpcManager
    {
        // -- MareSynchronos --
        // Active Pairs
        private readonly ICallGateSubscriber<List<nint>> mareActivePairsCallGateSubscriber = Services.PluginInterface.GetIpcSubscriber<List<nint>>("MareSynchronos.GetHandledAddresses");
        private readonly TimeSpan mareActivePairsCacheDuration = TimeSpan.FromSeconds(5);
        private DateTime mareActivePairsLastUpdateTime = DateTime.MinValue;
        private readonly HashSet<nint> mareActivePairs = [];
        public HashSet<nint> MareActivePairs
        {
            get
            {
                if (!this.MareActivePairsIpcAvailable)
                {
                    return [];
                }
                else if ((DateTime.UtcNow - this.mareActivePairsLastUpdateTime) > this.mareActivePairsCacheDuration)
                {
                    this.mareActivePairs.Clear();
                    foreach (var item in this.mareActivePairsCallGateSubscriber.InvokeFunc())
                    {
                        this.mareActivePairs.Add(item);
                    }
                    this.mareActivePairsLastUpdateTime = DateTime.UtcNow;
                }
                return this.mareActivePairs;
            }
        }
        public bool MareActivePairsIpcAvailable => this.mareActivePairsCallGateSubscriber.HasFunction;

        private IpcManager()
        {

        }
    }
}
