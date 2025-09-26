using System.Collections.Generic;
using UnityEngine.Events;

namespace OMGG.Chronicle {

    using OMGG.Chronicle.Network;

    public class ChronicleManager {

        #region Variables

        public event UnityAction<ChronicleEntry> OnChronicleAdded;

        private readonly List<ChronicleEntry> _History = new();

        private IChronicleNetworkAdapter _NetworkAdapter;

        #endregion

        #region Setters

        public void SetNetworkAdapter(IChronicleNetworkAdapter adapter)
        {
            _NetworkAdapter = adapter;
        }

        public void AddEvent(ChronicleEntry entry)
        {
            _History.Add(entry);

            OnChronicleAdded?.Invoke(entry);

            _NetworkAdapter?.BroadcastChronicle(entry);
        }

        #endregion

        #region Getters

        public IReadOnlyList<ChronicleEntry> GetHistory() => _History;

        #endregion
    }
}
