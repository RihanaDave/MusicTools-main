
namespace MusicToolTests;

public class PositionTests
{
    [Fact]
    public void Ctor_String()
    {
        var actual = new Position("5:2:480");
        var expected = new Position(5, 2, 480);

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Bars()
    {
        Assert.Equal(0, (new Position(0)).Bar);
        Assert.Equal(0, (new Position((4 * 960 - 1))).Bar);
        Assert.Equal(1, (new Position(4 * 960)).Bar);
        Assert.Equal(1, (new Position(8 * 960 - 1)).Bar);
        Assert.Equal(2, (new Position(8 * 960)).Bar);
    }

    [Fact]
    public void Beats()
    {
        Assert.Equal(0, (new Position(0)).Beat);
        Assert.Equal(0, (new Position(959)).Beat);
        Assert.Equal(1, (new Position(960)).Beat);
        Assert.Equal(1, (new Position(1919)).Beat);
        Assert.Equal(2, (new Position(1920)).Beat);
        Assert.Equal(2, (new Position(2879)).Beat);
        Assert.Equal(3, (new Position(2880)).Beat);
        Assert.Equal(3, (new Position(3839)).Beat);
        Assert.Equal(0, (new Position(3840)).Beat);
    }

    [Fact]
    public void Increment()
    {
        var actual = new Position(0);
        var expected = new Position(1);

        Assert.Equal(expected, ++actual);
    }

    [Fact]
    public void Decrement()
    {
        var actual = new Position(1);
        var expected = new Position(0);

        Assert.Equal(expected, --actual);
    }

    [Fact]
    public void Addition()
    {
        var actual = new Position(1) + new Position(1);
        var expected = new Position(2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Subtraction()
    {
        var actual = new Position(2) - new Position(1);
        var expected = new Position(1);

        Assert.Equal(expected, actual);
    }
}