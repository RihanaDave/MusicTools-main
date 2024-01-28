using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MusicTools;

public sealed partial class Transport
{
    private const int _ppqn = 960;

    private long _ticks = 0;
    private long _skips = 0;
    private long _lastTick = 0;
    private long _maxTicks = 0;
    private long _minTicks = long.MaxValue;

    private Task? _task;
    private CancellationTokenSource? _cancellation;

    public void Start()
    {       
        if (_task != null)
            throw new InvalidOperationException("Transport is already running.");

        if (_cancellation != null)
            throw new InvalidOperationException("Cancellation token is not null.");

        _cancellation = new();

        _task = Task.Factory.StartNew(() =>
        {
            var res = AvSetMmThreadCharacteristics("Pro Audio", out _);
            var f = Stopwatch.Frequency;

            var beatsPerSecond = Tempo / 60d;
            var pulsesPerSecond = beatsPerSecond * _ppqn;
            var ticksPerPulse = f / pulsesPerSecond;

            var musicalTime = new Position();
            
            var startTime = 0L;
            var nextTick = startTime + (long)ticksPerPulse;

            var sw = new Stopwatch();
            sw.Start();

            Tick(musicalTime);
            while (!_cancellation.Token.IsCancellationRequested)
            {
                var currentTime = sw.ElapsedTicks;

                if (currentTime >= nextTick)
                {
                    musicalTime++;
                    Tick(musicalTime);                    
                    nextTick += (long)ticksPerPulse;
                    
                    _ticks++;
                    var delta = currentTime - _lastTick;
                    _minTicks = Math.Min(_minTicks, delta);
                    _maxTicks = Math.Max(_maxTicks, delta);
                    _lastTick = currentTime;
                }
                else
                {
                    _skips++;
                }
            }

            for(int t=0; t<tracks.Count; t++)
                tracks[t].AllNotesOff();

        }, _cancellation.Token);            
    }

    public void Stop()    
    {
        if (_task != null)
        {
            if (_cancellation == null)
                throw new InvalidOperationException("Cancellation token is null.");
            
            _cancellation?.Cancel();
            _task?.Wait();
            _task?.Dispose();
            _task = null;
            _cancellation = null;
        }
    }

    private void Tick(Position position)
    {
        for(int t = 0; t < tracks.Count; t++)
        {
            var track = tracks[t];
            track.AdvanceTo(position);
        }
    }

    public double Tempo { get; set; } = 120d;

    private readonly List<Track> tracks = [];

    public void Add(Track track)
    {
        tracks.Add(track);
    }

    // Add p/invoke for AvSetMmThreadCharacteristics
    [DllImport("Avrt.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool AvSetMmThreadCharacteristics(string taskName, out int taskIndex);
}

