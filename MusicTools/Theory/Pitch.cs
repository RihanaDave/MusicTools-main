
namespace MusicTools;

public readonly record struct Pitch
{
    public PitchClass Note { get; init; }
    public int Octave { get; init; }

    public Pitch(PitchClass note, int octave)
    {
        Note = note;
        Octave = octave;
    }

    public Pitch(ReadOnlySpan<char> pitch)
    {
        Note = (PitchClass)Enum.Parse(typeof(PitchClass), pitch[..1]);
        if (pitch.Length == 2)
            Octave = int.Parse(pitch[1..]);
        else
        {
            Octave = int.Parse(pitch[2..]);
            
            char acc = pitch[1];
            if (acc == '#')
            {
                if (Note == PitchClass.B)
                {
                    Octave++;
                    Note = PitchClass.C;
                }
                else
                    Note++;
            }
            if(acc == 'b')
            {
                if (Note == PitchClass.C)
                {
                    Octave--;
                    Note = PitchClass.B;
                }
                else
                Note--;
            }            
        }
    }

    public static Pitch operator +(Pitch a, Interval b)
    {
        var semitones = (int)a.Note + b.Semitones;
        var octave = a.Octave + (semitones / 12);
        var note = (PitchClass)(semitones % 12);
        return new Pitch(note, octave);
    }

    public static Pitch operator -(Pitch a, Interval b)
    {
        var semitones = (int)a.Note - b.Semitones;
        var octave = a.Octave + (semitones / 12);
        var note = (PitchClass)(semitones % 12);
        return new Pitch(note, octave);
    }

    public byte ToMidiNote() => (byte) (((Octave + 1) * 12) + (int)Note);

    public override readonly string ToString()
    {
        return $"{Note}{Octave}";
    }
}