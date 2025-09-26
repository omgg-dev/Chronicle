using UnityEngine.UI;
using UnityEngine;

namespace OMGG.Chronicle.DemoGame {

    public class ChronicleDemoUI : MonoBehaviour {

        [Header("References")]

        public ChronicleManager Manager = new();

        public Transform LogContainer;

        public GameObject LogEntryPrefab;

        [Header("Buttons")]

        public Button BtnCityFounded;
        public Button BtnUnitKilled;

        private void Awake()
        {
            Manager.OnChronicleAdded += OnChronicleAdded;

            BtnCityFounded.onClick.AddListener(() => {
                var entry = ChronicleEntry.Create(
                    new DemoEvent(EventTypeDemo.CityFounded),
                    "City Founded",
                    "A new city has been founded!",
                    "city_icon"
                );

                Manager.AddEvent(entry);
            });

            BtnUnitKilled.onClick.AddListener(() => {
                var entry = ChronicleEntry.Create(
                    new DemoEvent(EventTypeDemo.UnitKilled),
                    "Unit Killed",
                    "A unit has been killed in battle.",
                    "unit_icon"
                );

                Manager.AddEvent(entry);
            });
        }

        private void OnChronicleAdded(ChronicleEntry entry)
        {
            var go = GameObject.Instantiate(LogEntryPrefab, LogContainer);
            var ui = go.GetComponent<ChronicleDemoEntryUI>();

            ui.Setup(entry);

            ui.OnRightClick.AddListener(OnRemove);
        }

        private void OnRemove(ChronicleEntry entry)
        {
            foreach (Transform child in LogContainer) {
                var ui = child.GetComponent<ChronicleDemoEntryUI>();

                if (ui != null && ui.Entry.Id == entry.Id) {
                    GameObject.Destroy(child.gameObject);

                    break;
                }
            }
        }
    }
}
