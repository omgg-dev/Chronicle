using UnityEngine.UI;
using UnityEngine;

namespace OMGG.Chronicle.DemoGame {

    public class ChronicleDemoUI : MonoBehaviour {

        [Header("References")]

        [SerializeField]
        private ChronicleManager _Manager = new();

        public DemoPayloadCache PayloadCache;

        public DemoActionResolver ActionResolver;

        [SerializeField]
        private ChronicleDemoEntryUI _EntryPrefab;

        [SerializeField]
        private Transform _EntryContainer;

        [Header("Buttons")]

        public Button FoundNewEgyptianCity;
        public Button FoundNewBabylonianCity;
        public Button BtnQuestCompleted;

        private void Awake()
        {
            LocalBroadcaster broadCaster = new();

            _Manager.SetBroadcaster(broadCaster);

            broadCaster.OnChronicleReceived += HandleEntryReceived;
            broadCaster.OnChronicleRemoved  += HandleEntryRemoved;

            FoundNewEgyptianCity.onClick.AddListener(() => AddCityFoundEntry("egyptian001", new Vector2(10, 20), 15));
            FoundNewBabylonianCity.onClick.AddListener(() => AddCityFoundEntry("babylonian001", new Vector2(0, 48), 5));
            BtnQuestCompleted.onClick.AddListener(() => AddQuestCompletedEntry());
        }

        private void HandleEntryReceived(ChronicleEntry entry)
        {
            PayloadCache.RegisterPayload(entry.Id, entry.PayloadJson);

            var ui = Instantiate(_EntryPrefab, _EntryContainer);

            ui.Bind(entry);

            ui.OnClicked += (e) => {
                ActionResolver.ExecuteAction(e);

                if (!string.IsNullOrEmpty(e.PayloadJson)) {
                    Debug.Log("[Payload] Already included: " + e.PayloadJson);
                } else {
                    var payload = PayloadCache.RequestPayload(e.Id);

                    Debug.Log("[Payload] Fetched lazily: " + payload);
                }
            };

            ui.OnRemoved += (e) => {
                HandleEntryRemoved(e);
            };
        }

        private void HandleEntryRemoved(string id)
        {
            foreach (Transform child in _EntryContainer) {
                var ui = child.GetComponent<ChronicleDemoEntryUI>();

                if (ui != null && ui.Entry.Id == id) {
                    GameObject.Destroy(child.gameObject);

                    break;
                }
            }
        }

        private void OnRemove(ChronicleEntry entry)
        {
            HandleEntryRemoved(entry.Id);
        }

        #region Buttons

        private void AddCityFoundEntry(string cityId, Vector2 coord, int population)
        {
            var entry = new ChronicleEntry() {
                TitleKey   = "New City Found!",
                MessageKey = "You have discovered a new city on the map.",
                IconKey    = cityId,
                ActionKey  = "FocusMap",
                ActionArgs = new string[] {
                    cityId
                },
                PayloadJson = $"{{ \"coords\": \"X:{coord.x},Y:{coord.y}\", \"population\": {population} }}"
            };

            _Manager.AddEvent(entry);
        }

        private void AddQuestCompletedEntry()
        {
            var entry = new ChronicleEntry()
            {
                TitleKey = "Quest Completed!",
                MessageKey = "You have successfully completed the quest.",
                IconKey = "quest",
                ActionKey = "OpenPopup",
                ActionArgs = new string[] {
                    "Congratulations on completing your quest!"
                }
            };

            _Manager.AddEvent(entry);
        }

        #endregion
    }
}
