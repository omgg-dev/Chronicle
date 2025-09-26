using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace OMGG.Chronicle.DemoGame {

    public class ChronicleDemoEntryUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        [Header("UI References")]
        public Image IconImage;

        public Text TitleText;
        public Text DescriptionText;

        public GameObject Tooltip;

        public ChronicleEntry Entry;

        public Action<ChronicleEntry> OnClicked;
        public Action<string> OnRemoved;

        public void Bind(ChronicleEntry entry)
        {
            Entry = entry;

            TitleText.text       = Entry.TitleKey;
            DescriptionText.text = Entry.MessageKey;

            if (!string.IsNullOrEmpty(Entry.IconKey)) {
                var sprite = Resources.Load<Sprite>(Entry.IconKey);

                if (sprite != null)
                    IconImage.sprite = sprite;
            }

            Tooltip.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) {
                OnClicked?.Invoke(Entry);
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                OnRemoved?.Invoke(Entry.Id);
            }
        }

        public void OnPointerEnter(PointerEventData _) => Tooltip.SetActive(true);

        public void OnPointerExit(PointerEventData _) => Tooltip.SetActive(false);
    }
}
