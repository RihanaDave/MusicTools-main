using System.Runtime.InteropServices;

namespace MusicTools;

public static partial class MidiDevices
{
    private static MidiOutCaps[] _availableDevices = [];
    
    private static readonly Dictionary<string, MidiDevice> _activeDevices = [];

    public static MidiDevice GetMidiDevice(string name)
    {
        if(_activeDevices.TryGetValue(name, out var device))
            return device;

        if(_availableDevices.Length == 0)
            Refresh();

        for(uint i = 0; i < _availableDevices.Length; i++)
        {
            if (_availableDevices[i].szPname == name)
            {
                var newDevice = new MidiDevice(i);
                _activeDevices[name] = newDevice;
                return newDevice;
            }
        }

        throw new ArgumentException($"No MIDI device with name '{name}' found.");
    }
        
    private static void Refresh()
    {
        var numDevices = GetNumberOfDevices();
        _availableDevices = new MidiOutCaps[numDevices];
        for (uint i = 0; i < numDevices; i++)
            midiOutGetDevCaps(i, ref _availableDevices[i], (uint)Marshal.SizeOf(_availableDevices[i]));
    }
    
    // MidiOutGetNumDevs
    [LibraryImport("winmm.dll", EntryPoint = "midiOutGetNumDevs")]
    private static partial int GetNumberOfDevices();

    // MidiOutGetDevCaps
    [DllImport("winmm.dll")]
    private static extern MidiResult midiOutGetDevCaps(uint uDeviceID, ref MidiOutCaps lpMidiOutCaps, uint cbMidiOutCaps);
}
