# homeintercom
Home intercom project based on Mumble protocol.

## Project Status

**Phase 1: Core Connectivity and Basic Audio** - IN PROGRESS

### Completed (as of current version):
- ✅ **Task 1.2**: Initial C# Project & Dependencies - Complete
  - .NET 8 console application created
  - MumbleSharp NuGet package installed (v2.0.1)
  - NAudio NuGet package installed (v2.2.1)
  - Concentus.Oggfile for Opus codec support installed (v1.0.6)

- ✅ **Task 1.3**: Mumble Connection Logic - Foundational Implementation
  - MumbleIntercomController class implemented
  - Connection architecture established
  - Note: Full MumbleConnection integration requires custom IMumbleProtocol implementation

- ✅ **Task 1.4**: Seamless Connection/Reconnection - Complete
  - Auto-reconnection logic implemented
  - Configurable reconnection delay (5 seconds default)
  - Infinite retry attempts by default

- ✅ **Task 1.6**: Audio Capture & Transmission (PTT POC) - Complete
  - NAudio configured for audio input (WaveInEvent)
  - Opus encoder initialized for voice encoding
  - StartTransmission() and StopTransmission() methods implemented
  - Audio data encoding pipeline established

- ✅ **Task 1.7**: Audio Playback - Complete
  - NAudio configured for audio output (WaveOutEvent with BufferedWaveProvider)
  - Opus decoder initialized
  - OnVoicePacketReceived() method implemented for packet handling and playback

### In Progress:
- ⏳ **Task 1.1**: Setup Client Environment
  - To be completed on actual Raspberry Pi Zero W hardware
  
- ⏳ **Task 1.3 & 1.5**: Full Mumble Protocol Integration
  - Current implementation provides foundational architecture
  - Requires custom IMumbleProtocol implementation for full server interaction
  - Auto-channel join ("All" channel) logic designed but requires server connectivity

## Architecture Overview

### Core Components

1. **MumbleIntercomController**: Main controller class handling all Mumble protocol operations
   - Connection management with auto-reconnection
   - Audio I/O initialization and management
   - Push-To-Talk (PTT) functionality
   - Opus audio encoding/decoding

2. **Program.cs**: Entry point and application host
   - Configuration management
   - Graceful shutdown handling
   - Command-line argument support

3. **Audio Pipeline**:
   - Input: WaveInEvent → PCM16 → Opus Encoder → Mumble transmission
   - Output: Mumble reception → Opus Decoder → PCM16 → WaveOutEvent playback

## Configuration

Default settings (can be overridden via command-line arguments):
- **Server Address**: 192.168.1.50
- **Server Port**: 64738
- **Username**: System hostname (e.g., "Pi-Kitchen")
- **Password**: intercompass
- **Target Channel**: All
- **Audio Device Index**: 0

### Command-line Usage:
```bash
dotnet run [serverAddress] [serverPort] [username]
```

Example:
```bash
dotnet run 192.168.1.50 64738 Pi-Kitchen
```

## Audio Device Configuration

**IMPORTANT NOTE ON AUDIO DEVICES:**

The application uses NAudio for audio input/output. On startup, it lists all available audio devices. The default device index is 0, but for USB audio devices on Raspberry Pi, you may need to change this.

To select a different audio device, modify the `audioDeviceIndex` parameter in Program.cs or extend the command-line argument parsing.

Example device output:
```
Available input devices: 2
  Device 0: Built-in Microphone
  Device 1: USB Audio Device
```

## Building and Running

### Prerequisites:
- .NET 8 SDK or runtime
- Linux (for Raspberry Pi deployment) or Windows (for development)

### Build:
```bash
cd HomeIntercom
dotnet build
```

### Run:
```bash
cd HomeIntercom
dotnet run
```

### Publish for Raspberry Pi:
```bash
cd HomeIntercom
dotnet publish -c Release -r linux-arm
```

## Next Steps (Phase 2)

Following the PROJECT PLAN.md:
- Implement custom IMumbleProtocol for full server integration
- Add Avalonia UI for touchscreen interface
- Implement user/channel tracking
- Add recipient selection UI
- Integrate PTT button with touchscreen

## Known Limitations

1. **MumbleSharp Integration**: The current version uses a foundational architecture. Full integration with MumbleSharp 2.0.1 requires implementing a custom IMumbleProtocol interface to handle all server events properly.

2. **Audio Hardware**: NAudio's WaveInEvent/WaveOutEvent are designed for Windows and may require platform-specific alternatives on Linux (such as ALSA-based alternatives).

3. **Server Connection**: Actual Mumble server connectivity testing requires a running Murmur server instance.

## Project Structure

```
homeintercom/
├── HomeIntercom/
│   ├── HomeIntercom.csproj          # Project file with NuGet references
│   ├── Program.cs                    # Application entry point
│   └── MumbleIntercomController.cs   # Main controller implementation
├── PROJECT PLAN.md                   # Detailed project plan with phases
├── Hardware.md                        # Hardware specifications
└── README.md                          # This file
```

## References

- [MumbleSharp on NuGet](https://www.nuget.org/packages/MumbleSharp/)
- [NAudio Documentation](https://github.com/naudio/NAudio)
- [Concentus (Opus Codec)](https://github.com/lostromb/concentus)
- [Mumble Protocol Documentation](https://mumble-protocol.readthedocs.io/)

## License

See LICENSE file for details.

