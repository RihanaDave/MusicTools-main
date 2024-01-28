using System.Runtime.InteropServices;

namespace MusicTools;

public partial class MidiDevice : IDisposable
{
    public MidiDevice(uint deviceID)
    {
        this.deviceID = deviceID;

        // call midiOutOpen
        midiOutOpen(out handle, deviceID, IntPtr.Zero, 0, 0);
    }
    
    private uint deviceID;
    private nint handle;
    private bool disposedValue;

    public void NoteOn(int channel, byte note, byte velocity) => Send(channel, MidiStatus.NoteOn, note, velocity);

    public void NoteOff(int channel, byte note) => Send(channel, MidiStatus.NoteOn, note, 0);

    public void AllNotesOff(int channel) => Send(MidiStatus.ControlChange, 0x7B, 0);

    public void Send(int channel, MidiStatus status, byte param1, byte param2)
    {
        int message = (int)status + channel - 1;
        message |= (param1) << 8;
        message |= (param2) << 16;

        midiOutShortMsg(handle, message);
    }

    public void Send(MidiStatus status, byte param1, byte param2)
    {
        int message = (int)status;
        message |= (param1) << 8;
        message |= (param2) << 16;

        midiOutShortMsg(handle, message);
    }

   
    [LibraryImport("winmm.dll")]
    private static partial MidiResult midiOutOpen(out IntPtr handle, uint deviceID, IntPtr proc, IntPtr instance, uint flags);


    [LibraryImport("winmm.dll")]
    private static partial MidiResult midiOutClose(nint handle);


    [LibraryImport("winmm.dll")]
    private static partial MidiResult midiOutShortMsg(nint handle, int message);

    #region IDisposable Support

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if(handle != 0)
            {
                midiOutClose(handle);
                handle = 0;
            }

            disposedValue = true;
        }
    }

    ~MidiDevice()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
