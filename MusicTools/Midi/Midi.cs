using System.Runtime.InteropServices;

namespace MusicTools;

[StructLayout(LayoutKind.Sequential)]
public struct MidiOutCaps
{
    public ushort wMid;
    public ushort wPid;
    public uint vDriverVersion;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string szPname;
    public ushort wTechnology;
    public ushort wVoices;
    public ushort wNotes;
    public ushort wChannelMask;
    public uint dwSupport;
}

public delegate void MidiOutProc(int handle, int msg, int instance, int param1, int param2);

public enum MidiResult
{
    MMSYSERR_NOERROR = 0,
    MMSYSERR_ERROR = 1,
    MMSYSERR_BADDEVICEID = 2,
    MMSYSERR_NOTENABLED = 3,
    MMSYSERR_ALLOCATED = 4,
    MMSYSERR_INVALHANDLE = 5,
    MMSYSERR_NODRIVER = 6,
    MMSYSERR_NOMEM = 7,
    MMSYSERR_NOTSUPPORTED = 8,
    MMSYSERR_BADERRNUM = 9,
    MMSYSERR_INVALFLAG = 10,
    MMSYSERR_INVALPARAM = 11,
    MMSYSERR_HANDLEBUSY = 12,
    MMSYSERR_INVALIDALIAS = 13,
    MMSYSERR_BADDB = 14,
    MMSYSERR_KEYNOTFOUND = 15,
    MMSYSERR_READERROR = 16,
    MMSYSERR_WRITEERROR = 17,
    MMSYSERR_DELETEERROR = 18,
    MMSYSERR_VALNOTFOUND = 19,
    MMSYSERR_NODRIVERCB = 20,
    WAVERR_BADFORMAT = 32,
    WAVERR_STILLPLAYING = 33,
    WAVERR_UNPREPARED = 34,
    MIDIERR_UNPREPARED = 64,
    MIDIERR_STILLPLAYING = 65,
    MIDIERR_NOMAP = 66,
    MIDIERR_NOTREADY = 67,
    MIDIERR_NODEVICE = 68,
    MIDIERR_INVALIDSETUP = 69,
    MIDIERR_BADOPENMODE = 70,
    MIDIERR_DONT_CONTINUE = 71,
    MIDIERR_LASTERROR = 71
}
