namespace MusicTools;

public readonly record struct Position
{    
    public Position()
    { }

    private const int _ppqn = 960;

    public Position(int pulse) => Pulse = pulse;

    public Position(int bar, int beat, int pulse) => Pulse = (bar * 4 + beat) * _ppqn + pulse;

    public Position(ReadOnlySpan<char> position)
    {
        var parts = new Range[5];
        var len = position.Split(parts, ':');
        if(len != 3)
            throw new ArgumentException("Position must be in the format 'bar:beat:pulse'");

        Pulse = (int.Parse(position[parts[0]]) * 4 + int.Parse(position[parts[1]])) * _ppqn + int.Parse(position[parts[2]]);
    }

    public int Pulse { get; init; } = 0;

    public int Bar => Pulse / (4 * _ppqn);

    public int Beat => (Pulse % (4 * _ppqn)) / _ppqn;

    public static Position operator ++(Position musicalTime) => new(musicalTime.Pulse + 1);

    public static Position operator --(Position musicalTime) => new(musicalTime.Pulse - 1);

    public static Position operator +(Position a, Position b) => new(a.Pulse + b.Pulse);

    public static Position operator -(Position a, Position b) => new(a.Pulse - b.Pulse);

    public static Position operator *(Position a, int b) => new(a.Pulse * b);

    public static Position operator /(Position a, int b) => new(a.Pulse / b);

    public static bool operator <(Position a, Position b) => a.Pulse < b.Pulse;

    public static bool operator >(Position a, Position b) => a.Pulse > b.Pulse;

    public static bool operator <=(Position a, Position b) => a.Pulse <= b.Pulse;

    public static bool operator >=(Position a, Position b) => a.Pulse >= b.Pulse;
    
    public static Position operator +(Position a, Duration b) => new(a.Pulse + b.Pulses);

    public static Position operator -(Position a, Duration b) => new(a.Pulse - b.Pulses);

    public override string ToString() => $"{Bar}:{Beat}:{Pulse}";
}

