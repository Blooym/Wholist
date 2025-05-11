using System.Collections.Generic;
using Dalamud.Plugin.Ipc;
using Wholist.Common;

namespace Wholist.Interop
{
    internal sealed class IpcManager
    {
        // Mare
        private const string MARE_ACTIVEPAIR_IPC_NAME = "MareSynchronos.GetHandledAddresses";
        public ICallGateSubscriber<List<nint>> MareActivePairCallGateSubscriber = Services.PluginInterface.GetIpcSubscriber<List<nint>>(MARE_ACTIVEPAIR_IPC_NAME);
        public bool MareActivePairCallGateAvailable => this.MareActivePairCallGateSubscriber.HasFunction;
    }
}
