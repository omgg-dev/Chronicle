using System.Collections.Generic;
using UnityEngine;

namespace OMGG.Chronicle.DemoGame {

    public class DemoPayloadCache : MonoBehaviour {

        private Dictionary<string, string> _Payloads = new();

        public void RegisterPayload(string entryId, string json)
        {
            _Payloads[entryId] = json;
        }

        public string RequestPayload(string entryId)
        {
            if (_Payloads.TryGetValue(entryId, out var json))
                return json;
            // If the payload is not found, simulate a server fetch here.
            Debug.LogWarning($"Payload for {entryId} not found, simulating server fetch...");

            return "{}";
        }
    }
}
