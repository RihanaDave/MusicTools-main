
namespace MusicToolTests;

public class PitchTests
{
    [Fact]
    public void Ctor_CSharp4()
    {
        var actual = new Pitch("C#4");
        var expected = new Pitch(PitchClass.CSharp, 4);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_CFlat4()
    {
        var actual = new Pitch("Cb4");
        var expected = new Pitch(PitchClass.B, 3);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_BSharp4()
    {
        var actual = new Pitch("B#4");
        var expected = new Pitch(PitchClass.C, 5);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Ctor_BFlat4()
    {
        var actual = new Pitch("Bb4");
        var expected = new Pitch(PitchClass.ASharp, 4);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToMidiNote_A0_21()
    {
        var actual = new Pitch("A0").ToMidiNote();
        var expected = 21;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToMidiNote_C4_60()
    {
        var actual = new Pitch("C4").ToMidiNote();
        var expected = 60;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToMidiNote_G9_127()
    {
        var actual = new Pitch("G9").ToMidiNote();
        var expected = 127;

        Assert.Equal(expected, actual);
    }
}