using BenchmarkDotNet.Attributes;
using MusicTools;
using System.Runtime.InteropServices;

namespace Benchmarks;

[MemoryDiagnoser]
public class SequenceBenchmark
{
    private Sequence? _sequence;

    [Params(1, 100)]
    public int positions;

    [Params(1, 100)]
    public int notes;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var _sequence = new Sequence();

        if (positions == 0)
            return;

        for(int i = 0; i < positions; i++)
        {
            List<Event> events = [];
            for(int n = 0; n < notes; n++)
                events.Add(new Event(new Position(i+1, 0, 0), 0, new Pitch("C4"), 100, Duration.QuarterNote));
            
            _sequence.AddEvents(CollectionsMarshal.AsSpan(events));
        }
    }

    [Benchmark]
    public void GetEvents_First()
    {
        _sequence?.EventsAt(new Position(1, 0, 0));
    }

    [Benchmark]
    public void GetEvents_Last()
    {
        _sequence?.EventsAt(new Position(positions, 0, 0));
    }

    [Benchmark]
    public void GetEvents_PastEnd()
    {
        _sequence?.EventsAt(new Position(positions +1, 0, 0));
    }



}
