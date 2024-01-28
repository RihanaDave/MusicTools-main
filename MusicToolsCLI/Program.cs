using MusicTools;


Console.WriteLine("Setup...");

var transport = new Transport();
var track1 = new Track(MidiDevices.GetMidiDevice("2- MIDISPORT 4x4 Out A"), 1);
var track2 = new Track(MidiDevices.GetMidiDevice("2- MIDISPORT 4x4 Out A"), 2);
transport.Add(track1);
transport.Add(track2);

var sequence1 = new Sequence();
var noteA = new Event(new Position(0, 0, 0), 0, new Pitch("C4"), 100, Duration.QuarterNote);
var noteB = new Event(new Position(0, 1, 0), 0, new Pitch("E4"), 100, Duration.QuarterNote);
var noteC = new Event(new Position(0, 2, 0), 0, new Pitch("G4"), 100, Duration.QuarterNote);
var noteD = new Event(new Position(0, 3, 0), 0, new Pitch("E4"), 100, Duration.QuarterNote);
sequence1.AddEvents(new[] { noteA, noteB, noteC, noteD });
track1.Add(sequence1);

var sequence2 = new Sequence();
var note2A = new Event(new Position(0, 0, 0), 0, new Pitch("C2"), 50, Duration.SixteenthNote);
var note2B = new Event(new Position(0, 0, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
var note2C = new Event(new Position(0, 1, 0), 0, new Pitch("G2"), 50, Duration.SixteenthNote);
var note2D = new Event(new Position(0, 1, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
var note2E = new Event(new Position(0, 2, 0), 0, new Pitch("C2"), 50, Duration.SixteenthNote);
var note2F = new Event(new Position(0, 2, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
var note2G = new Event(new Position(0, 3, 0), 0, new Pitch("G2"), 50, Duration.SixteenthNote);
var note2H = new Event(new Position(0, 3, 480), 0, new Pitch("E2"), 50, Duration.SixteenthNote);
sequence2.AddEvents(new[] { note2A, note2B, note2C, note2D, note2E, note2F, note2G, note2H });
track2.Add(sequence2);

Console.WriteLine("Starting...");
transport.Start();

Console.WriteLine("Waiting...");
Thread.Sleep(7000);

Console.WriteLine("Stopping...");
transport.Stop();

Console.WriteLine("Starting...");
transport.Start();

Console.WriteLine("Waiting...");
Thread.Sleep(7000);

Console.WriteLine("Stopping...");
transport.Stop();


Console.WriteLine("Done.");