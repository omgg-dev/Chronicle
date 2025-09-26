using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace OMGG.Chronicle.DemoGame {

    public class ChronicleDemoEntryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        public ChronicleEntry Entry;

        public Text  Title;
        public Text  Description;
        public Image IconImage;

        public GameObject Tooltip;

        public UnityEvent<ChronicleEntry> OnRightClick;

        public void Setup(ChronicleEntry entry)
        {
            Entry = entry;

            Title.text       = entry.DisplayTitle;
            Description.text = entry.DisplayText;

            if (!string.IsNullOrEmpty(entry.IconKey)) {
                var sprite = Resources.Load<Sprite>(entry.IconKey);

                if (sprite != null)
                    IconImage.sprite = sprite;
            }

            OnPointerExit(null);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.SetActive(false);
        }

        public void OnDelete()
        {
            if (OnRightClick != null)
                OnRightClick.Invoke(Entry);
        }
    }
}
