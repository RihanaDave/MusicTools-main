using System.Collections.Immutable;

namespace MusicTools;

public readonly record struct Event(Position Position, int EventType, Pitch Pitch, byte Velocity, Duration Duration)
{
}

public abstract class Segment
{
    public abstract ReadOnlySpan<Event> EventsAt(Position time);
    public abstract Position Length { get; }
}

public static class MyExtensions
{
    public static int IndexOf<T>(this ReadOnlySpan<T> source, Func<T, bool> condition)
    {
        for (int i = 0; i < source.Length; i++)
            if (condition(source[i]))
                return i;

        return -1;
    }
}


public class Sequence : Segment
{
    private readonly List<ImmutableArray<Event>> _events = [];

    public override ReadOnlySpan<Event> EventsAt(Position time)
    {
        for(int i = 0; i < _events.Count; i++)
        {
            if (_events[i][0].Position == time)
                return _events[i].AsSpan();

            if (_events[i][0].Position > time)
                break;
        }   

        return [];
    }

    public override Position Length
    {
        // the length of the sequence is the position of th last event plus its duration
        get
        {
            if (_events.Count == 0)
                return new Position();

            var lastEvent = _events[^1];
            return lastEvent[^1].Position + lastEvent[^1].Duration;
        }
    }

    public void AddEvents(ReadOnlySpan<Event> events)
    {
        // Assuming the events are sorted by time, splice them up by equal Position, and add them to the dictionary.
        int i = 0;
        
        while(i < events.Length)
        {
            Position pos = events[i].Position;
            int j = events[i..].IndexOf(e => e.Position != pos);

            if (j != -1)
            {
                _events.Add(events[i..(i+j)].ToImmutableArray());
                i += j;
            }
            else
            {
                _events.Add(events[i..].ToImmutableArray());
                i = events.Length;
            }
        }        
    }   
}

public class Repeat(Segment segment, int count) : Segment
{
    private readonly Segment segment = segment;
    private readonly int count = count;

    public override ReadOnlySpan<Event> EventsAt(Position time)
    {
        throw new NotImplementedException();
    }

    public override Position Length => segment.Length * count;
}

public class Join(Segment[] segments) : Segment
{
    private readonly Segment[] segments = segments;

    public override ReadOnlySpan<Event> EventsAt(Position time)
    {
        throw new NotImplementedException();
    }

    public override Position Length
    {
        get
        {
            var length = new Position();
            foreach(var segment in segments)
                length += segment.Length;
            return length;
        }
    }
}