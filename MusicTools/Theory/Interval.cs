
namespace MusicTools;

public readonly record struct Interval(int Semitones)
{
    public static Interval operator + (Interval a, Interval b) => new(a.Semitones + b.Semitones);

    public override readonly string ToString() => $"{Semitones} semitones";

    // Convert these to use getters.
    public static Interval PerfectUnison { get => new(0); }
    public static Interval MinorSecond { get => new(1); }
    public static Interval MajorSecond { get => new(2); }
    public static Interval MinorThird { get => new(3); }
    public static Interval MajorThird { get => new(4); }
    public static Interval PerfectFourth { get => new(5); }
    public static Interval DiminishedFifth { get => new(6); }
    public static Interval PerfectFifth { get => new(7); }       
    public static Interval AugmentedFifth { get => new(8); }
    public static Interval MinorSixth { get => new(8); }
    public static Interval MajorSixth { get => new(9); }
    public static Interval MinorSeventh { get => new(10); }
    public static Interval MajorSeventh { get => new(11); }
    public static Interval PerfectOctave { get => new(12); }
}
