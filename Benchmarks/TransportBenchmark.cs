using BenchmarkDotNet.Attributes;
using MusicTools;

namespace Benchmarks;

[MemoryDiagnoser]
public class TransportBenchmark
{
    private Transport? transport;
    private Sequence? segment1;
    private Sequence? segment2;

    [Params(0, 2, 50)]
    public int _tracks;

    [Params(0, 1, 2)]
    public int _segments;

    [GlobalSetup]
    public void GlobalSetup()
    {
        transport = new Transport
        {
            Tempo = 480
        };

        if (_tracks == 0)
            return;

        var tracks = new Track[_tracks];

        tracks[0] = new Track(MidiDevices.GetMidiDevice("2- MIDISPORT 4x4 Out A"), 1);
        tracks[1] = new Track(MidiDevices.GetMidiDevice("2- MIDISPORT 4x4 Out A"), 2);
        
        if(_tracks > 2)
            for(int i = 2; i < _tracks; i++)
                tracks[i] = new Track(MidiDevices.GetMidiDevice("2- MIDISPORT 4x4 Out A"), 3);

        foreach (var track in tracks)
            transport.Add(track);
        
        if (_segments > 0)
        {
            segment1 = new Sequence();
            var noteA = new Event(new Position(0, 0, 0), 0, new Pitch("C4"), 100, Duration.QuarterNote);
            var noteB = new Event(new Position(0, 1, 0), 0, new Pitch("E4"), 100, Duration.QuarterNote);
            var noteC = new Event(new Position(0, 2, 0), 0, new Pitch("G4"), 100, Duration.QuarterNote);
            var noteD = new Event(new Position(0, 3, 0), 0, new Pitch("E4"), 100, Duration.QuarterNote);
            segment1.AddEvents(new[] { noteA, noteB, noteC, noteD });
            tracks[0].Add(segment1);
        }

        if (_segments > 1)
        {
            segment2 = new Sequence();
            var note2A = new Event(new Position(0, 0, 0), 0, new Pitch("C2"), 50, Duration.SixteenthNote);
            var note2B = new Event(new Position(0, 0, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
            var note2C = new Event(new Position(0, 1, 0), 0, new Pitch("G2"), 50, Duration.SixteenthNote);
            var note2D = new Event(new Position(0, 1, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
            var note2E = new Event(new Position(0, 2, 0), 0, new Pitch("C2"), 50, Duration.SixteenthNote);
            var note2F = new Event(new Position(0, 2, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
            var note2G = new Event(new Position(0, 3, 0), 0, new Pitch("G2"), 50, Duration.SixteenthNote);
            var note2H = new Event(new Position(0, 3, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
            segment2.AddEvents(new[] { note2A, note2B, note2C, note2D, note2E, note2F, note2G, note2H });
            tracks[1].Add(segment2);
        }
    }

    [Benchmark]
    public void ShortLoop()
    {       
        transport?.Start();
        Thread.Sleep(1000);
        transport?.Stop();
    }
}
