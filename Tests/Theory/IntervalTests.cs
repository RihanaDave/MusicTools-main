
namespace MusicToolTests;

public class IntervalTests
{
    [Fact]
    public void Addition()
    {
        // create tests for all intervals adding to a perfect octave
        Assert.Equal(Interval.PerfectOctave, Interval.PerfectUnison + Interval.PerfectOctave);
        Assert.Equal(Interval.PerfectOctave, Interval.MinorSecond + Interval.MajorSeventh);
        Assert.Equal(Interval.PerfectOctave, Interval.MajorSecond + Interval.MinorSeventh);
        Assert.Equal(Interval.PerfectOctave, Interval.MinorThird + Interval.MajorSixth);
        Assert.Equal(Interval.PerfectOctave, Interval.MajorThird + Interval.MinorSixth);
        Assert.Equal(Interval.PerfectOctave, Interval.PerfectFourth + Interval.PerfectFifth);
        Assert.Equal(Interval.PerfectOctave, Interval.DiminishedFifth + Interval.DiminishedFifth);
        Assert.Equal(Interval.PerfectOctave, Interval.PerfectFifth + Interval.PerfectFourth);
        Assert.Equal(Interval.PerfectOctave, Interval.MinorSixth + Interval.MajorThird);
        Assert.Equal(Interval.PerfectOctave, Interval.MajorSixth + Interval.MinorThird);
        Assert.Equal(Interval.PerfectOctave, Interval.MinorSeventh + Interval.MajorSecond);
        Assert.Equal(Interval.PerfectOctave, Interval.MajorSeventh + Interval.MinorSecond);
        Assert.Equal(Interval.PerfectOctave, Interval.PerfectOctave + Interval.PerfectUnison);
    }
}