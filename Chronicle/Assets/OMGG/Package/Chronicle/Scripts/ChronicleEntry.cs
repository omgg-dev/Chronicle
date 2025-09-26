using System;

namespace OMGG.Chronicle {

    [Serializable]
    public partial class ChronicleEntry {

        public string Id = Guid.NewGuid().ToString();

        public DateTime TImestampUtc = DateTime.UtcNow;

        public IEventType  EventType;
        public ChroniclePriority Priority = ChroniclePriority.Normal;
    }
}
