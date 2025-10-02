namespace OMGG.Chronicle.DemoGame {

    public enum EventTypeDemo : byte {
        CityFounded,
        UnitKilled
    }

#if CSHARP_10_OR_GREATER

    // Modern Version if C#10 or greater is used
    // Discriminated Union Pattern: https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions
    public readonly record struct DemoEvent(EventTypeDemo type) : IEventType;

#else

    // Fallback for older C# versions
    public readonly struct DemoEvent : IEventType {

        public EventTypeDemo Type { get; }

        public DemoEvent(EventTypeDemo type)
        {
            Type = type;
        }

        public override string ToString() => Type.ToString();
    }

#endif
}
