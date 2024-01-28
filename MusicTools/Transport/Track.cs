using System.Runtime.InteropServices;

namespace MusicTools;

public class Track
{
    private readonly List<Segment> segments = [];
    private readonly MidiDevice device;
    private readonly int channel;

    private readonly NoteOffQueue noteOffQueue = new();

    public Track(MidiDevice device, int channel)
    {
        this.device = device;
        this.channel = channel;
    }

    public void Add(Segment segment)
    {
        segments.Add(segment);
    }

    public void AdvanceTo(Position position)
    {
        if (segments.Count == 0)
            return;

        var events = GetEvents(position);

        for (int i = 0; i < events.Length; i++)
        {
            var evnt = events[i];
            //device.NoteOn(channel, evnt.Pitch.ToMidiNote(), evnt.Velocity);
            noteOffQueue.Push(position, evnt);
        }

        var noteOff = noteOffQueue.Pop(position);
        while (noteOff != null)
        {
            //device.NoteOff(channel, noteOff.Value.ToMidiNote());
            noteOff = noteOffQueue.Pop(position);
        }
    }

    public void AllNotesOff()
    {
        device.AllNotesOff(channel);
    }

    private readonly Event[] events = new Event[100];

    private ReadOnlySpan<Event> GetEvents(Position position)
    {
        //for(int i =0; i < segments.Count; i++)
        //    events.AddRange(segments[i].EventsAt(position));

        var x = segments[0].EventsAt(position);        
        for(int i = 0; i < x.Length; i++)
            events[i] = x[i];

        return events.AsSpan()[..x.Length];
    }
}


