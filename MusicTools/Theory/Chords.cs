
namespace MusicTools;

public static class Chords
{
    public static Interval[] MajorTriad() => [Interval.PerfectUnison, Interval.MajorThird, Interval.PerfectFifth];

    public static Pitch[] Major(Pitch root) => [root, root + Interval.MajorThird, root + Interval.PerfectFifth];

    public static Pitch[] Minor(Pitch root) => [root, root + Interval.MinorThird, root + Interval.PerfectFifth];

    public static Pitch[] Diminished(Pitch root) => [root, root + Interval.MinorThird, root + Interval.DiminishedFifth];

    public static Pitch[] Augmented(Pitch root) => [root, root + Interval.MajorThird, root + Interval.AugmentedFifth];

    public static Pitch[] MajorSeventh(Pitch root) => [root, root + Interval.MajorThird, root + Interval.PerfectFifth, root + Interval.MajorSeventh];

    public static Pitch[] MinorSeventh(Pitch root) => [root, root + Interval.MinorThird, root + Interval.PerfectFifth, root + Interval.MinorSeventh]; 

    public static Pitch[] DominantSeventh(Pitch root) => [root, root + Interval.MajorThird, root + Interval.PerfectFifth, root + Interval.MinorSeventh];
}