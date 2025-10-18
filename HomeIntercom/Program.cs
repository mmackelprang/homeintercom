using System;
using System.Threading;
using System.Threading.Tasks;
using HomeIntercom;

/// <summary>
/// Entry point for the Mumble-based Home Intercom application.
/// This is a headless console application designed to run on a Raspberry Pi Zero W.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("   Mumble Home Intercom - Raspberry Pi   ");
        Console.WriteLine("===========================================");
        Console.WriteLine();
        
        // Configuration - In production, these should come from a config file or environment variables
        string serverAddress = "192.168.1.50"; // Mumble server IP
        int serverPort = 64738;                // Default Mumble port
        string? username = null;               // Will use hostname if null
        string password = "intercompass";      // Authentication password
        string targetChannel = "All";          // Channel to auto-join
        int audioDeviceIndex = 0;              // Audio device index (change if needed)
        
        // Check for command-line arguments to override defaults
        if (args.Length > 0)
        {
            serverAddress = args[0];
            Console.WriteLine($"Using server address from command line: {serverAddress}");
        }
        
        if (args.Length > 1)
        {
            if (int.TryParse(args[1], out int port))
            {
                serverPort = port;
                Console.WriteLine($"Using server port from command line: {serverPort}");
            }
        }
        
        if (args.Length > 2)
        {
            username = args[2];
            Console.WriteLine($"Using username from command line: {username}");
        }
        
        Console.WriteLine();
        Console.WriteLine("Creating intercom controller...");
        
        // Create the intercom controller
        var controller = new MumbleIntercomController(
            serverAddress: serverAddress,
            serverPort: serverPort,
            username: username,
            password: password,
            targetChannelName: targetChannel,
            audioDeviceIndex: audioDeviceIndex
        );
        
        Console.WriteLine();
        Console.WriteLine("IMPORTANT NOTES:");
        Console.WriteLine("----------------");
        Console.WriteLine("1. Audio Device Selection:");
        Console.WriteLine("   - The default audio device index is 0");
        Console.WriteLine("   - For USB audio devices, you may need to change this");
        Console.WriteLine("   - The controller will list available devices on startup");
        Console.WriteLine("   - Set audioDeviceIndex parameter to select a different device");
        Console.WriteLine();
        Console.WriteLine("2. Push-To-Talk (PTT) Testing:");
        Console.WriteLine("   - This initial version demonstrates audio transmission");
        Console.WriteLine("   - Call controller.StartTransmission() to begin transmitting");
        Console.WriteLine("   - Call controller.StopTransmission() to stop transmitting");
        Console.WriteLine("   - In Phase 2, this will be connected to the touchscreen UI");
        Console.WriteLine();
        Console.WriteLine("3. Auto-Reconnection:");
        Console.WriteLine("   - The client will automatically reconnect if connection is lost");
        Console.WriteLine("   - No manual intervention required");
        Console.WriteLine();
        Console.WriteLine("Press Ctrl+C to stop the application");
        Console.WriteLine("===========================================");
        Console.WriteLine();
        
        // Set up cancellation token for graceful shutdown
        var cts = new CancellationTokenSource();
        
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            Console.WriteLine();
            Console.WriteLine("Shutdown requested...");
            cts.Cancel();
        };
        
        try
        {
            // Start the controller (this will run indefinitely with auto-reconnection)
            await controller.StartAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Application cancelled by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unhandled error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            // Clean up
            controller.Stop();
            Console.WriteLine("Application stopped.");
        }
        
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

