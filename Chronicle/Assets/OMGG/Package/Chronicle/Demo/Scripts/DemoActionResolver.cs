using System;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;

namespace OMGG.Chronicle.DemoGame {

    public class DemoActionResolver : MonoBehaviour, IChronicleActionResolver {

        public void ExecuteAction(ChronicleEntry entry)
        {
            switch (entry.ActionKey) {
                case "FocusMap":
                    if (!string.IsNullOrEmpty(entry.PayloadJson)) {
                        try {
                            var json = JObject.Parse(entry.PayloadJson);

                            string coords  = json["coords"]?.ToString() ?? "Unknown";
                            int population = json["population"]?.ToObject<int>() ?? 0;

                            Debug.Log($"[Action] FocusMap on {entry.ActionArgs[0]} at {coords}, population: {population}");
                        } catch (Exception e) {
                            Debug.LogWarning("[Action] Failed to parse payload: " + e.Message);
                        }
                    }
                    else
                    {
                        Debug.Log($"[Action] FocusMap on {entry.ActionArgs[0]} (no payload available)");
                    }

                    break;

                case "OpenPopup":
                    string message = entry.ActionArgs.Length > 0 ? entry.ActionArgs[0] : "";

                    Debug.Log("[ACTION] Opening popup with message: " + message);

                    break;

                default:
                    Debug.LogWarning("[ACTION] Unknow action key: " + entry.ActionKey);

                    break;
            }
        }
    }
}
