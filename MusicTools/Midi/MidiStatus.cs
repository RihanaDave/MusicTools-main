namespace MusicTools;

//create an enum of Midi message staus values
public enum MidiStatus
{
    NoteOff = 0x80,
    NoteOn = 0x90,
    KeyAfterTouch = 0xA0,
    ControlChange = 0xB0,
    ProgramChange = 0xC0,
    ChannelAfterTouch = 0xD0,
    PitchBend = 0xE0,
    SystemMessage = 0xF0
}
