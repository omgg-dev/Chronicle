namespace OMGG.Chronicle {

    public partial class ChronicleEntry {

        public string DisplayTitle;

        public string DisplayText;

        public string IconKey;

        public static ChronicleEntry Create(IEventType type, string title, string text, string iconKey)
        {
            return new ChronicleEntry() {
                EventType    = type,
                DisplayText  = text,
                DisplayTitle = title,
                IconKey      = iconKey
            };
        }
    }
}
