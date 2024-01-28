
namespace MusicTools;

public readonly record struct Duration(int Pulses)
{
    private const int _ppqn = 960;

    public const int PulsesPerQuarterNote = _ppqn;

    public int Bar => Pulses / (4 * _ppqn);

    public int Beat => (Pulses % (4 * _ppqn)) / _ppqn;

    public static Duration operator +(Duration a, Duration b) => new(a.Pulses + b.Pulses);

    public static Duration operator -(Duration a, Duration b) => new(a.Pulses - b.Pulses);

    public static Duration operator *(Duration a, int b) => new(a.Pulses * b);

    public static Duration operator /(Duration a, int b) => new(a.Pulses / b);

    public static Duration Dot(Duration a) => new(a.Pulses * 3 / 2);

    public Duration Dotted() => new(Pulses * 3 / 2);

    public Duration Triplet() => new(Pulses * 2 / 3);

    public static Duration WholeNote { get; } = new(PulsesPerQuarterNote * 4);
    public static Duration HalfNote { get; } = WholeNote / 2;
    public static Duration QuarterNote { get; } = WholeNote / 4;
    public static Duration SixteenthNote { get; } = WholeNote / 16;
    public static Duration ThirtySecondNote { get; } = WholeNote / 32;
    public static Duration SixtyFourthNote { get; } = WholeNote / 16;

    public static Duration DottedWholeNote { get; } = WholeNote.Dotted();
    public static Duration DottedHalfNote { get; } = HalfNote.Dotted();
    public static Duration DottedQuarterNote { get; } = QuarterNote.Dotted();
    public static Duration DottedSixteenthNote { get; } = SixteenthNote.Dotted();
    public static Duration DottedThirtySecondNote { get; } = ThirtySecondNote.Dotted();
    public static Duration DottedSixtyFourthNote { get; } = SixtyFourthNote.Dotted();

    public static Duration TripletWholeNote { get; } = WholeNote.Triplet();
    public static Duration TripletHalfNote { get; } = HalfNote.Triplet();
    public static Duration TripletQuarterNote { get; } = QuarterNote.Triplet();
    public static Duration TripletSixteenthNote { get; } = SixteenthNote.Triplet();        
    public static Duration TripletThirtySecondNote { get; } = ThirtySecondNote.Triplet();
    public static Duration TripletSixtyFourthNote { get; } = SixtyFourthNote.Triplet();
}
