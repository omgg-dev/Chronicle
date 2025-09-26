using System;

namespace OMGG.Chronicle.DemoGame {

    public class LocalBroadcaster : IChronicleBroadcaster {

        public event Action<ChronicleEntry> OnChronicleReceived;
        public event Action<string>         OnChronicleRemoved;

        public void BroadcastChronicle(ChronicleEntry entry)
        {
            // Local broadcast, so we just invoke the event directly.
            // Add an RPC function or necessary network code here to send the entry to other players.
            OnChronicleReceived?.Invoke(entry);
        }

        public void BroadcastChronicleRemoval(string id)
        {
            OnChronicleRemoved?.Invoke(id);
        }
    }
}
