using System.IO;

namespace FPSSetup;

class Program
{
    [STAThread]
    public static void log(string text)
    {
        Console.WriteLine(text);
    }
    public static void logw(string text)
    {
        Console.Write(text);
    }
    public static bool isInt(string text)
    {
        var test = int.TryParse(text, out int number);
        if (test)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static string FPSValuesSetup()
    {
        List<string> FPSCapValues = new List<string>();
        int used = 0;
        log("Okay, now we need to setup the FPS values.");
        log("Please enter the FPS values you want to use.");
        log("Enter 0 to stop adding FPS values.");
        while (true)
        {
            logw("FPS Value: ");
            string? FPSValue = Console.ReadLine();
            string FinalString = "FPSCapValues=[";
            if (!isInt(FPSValue))
            {
                log("Invalid number.");
                continue;
            }

            if (FPSValue == "0")
            {
                if (used == 0)
                {
                    log("You need to add at least one FPS value.");
                    continue;
                }
                FPSCapValues[FPSCapValues.Count - 1] = FPSCapValues[FPSCapValues.Count - 1].Replace(", ", "");
                foreach (string ball in FPSCapValues)
                {
                    FinalString += ball;
                }
                FinalString += "]";
                return FinalString;
            }
            else
            {
                FPSCapValues.Add(FPSValue + ".000000, ");
                used += 1;
            }
        }
    }
    static void Main(string[] args)
    {
        string? UnlockClient;
        string? UnlockStudio;
        string? FPSCap;
        string? CheckForUpdates;
        string? NonBlockingErrors;
        string? SilentErrors;
        string? QuickStart;
        int? UnlockMethod; // 0 = Hybrid, 1 = Memory Write, 2 = Flags File

        log("Roblox FPS Unlocker settings setup by | termizzle (@terminite)");
        log("Please answer the following questions to setup your FPS Unlocker settings.");
        logw("Unlock FPS for client? (true/false): ");
        UnlockClient = Console.ReadLine();
        logw("Unlock FPS for studio? (true/false): ");
        UnlockStudio = Console.ReadLine();
        string FPSValues = FPSValuesSetup();
        logw("FPS Cap (default FPS when launching, 0 for no cap): ");
        FPSCap = Console.ReadLine() + ".000000";
        logw("Check for updates? (true/false): ");
        CheckForUpdates = Console.ReadLine();
        logw("Non-blocking errors? (true/false): ");
        NonBlockingErrors = Console.ReadLine();
        logw("Silent errors? (true/false): ");
        SilentErrors = Console.ReadLine();
        logw("Quick start? (true/false): ");
        QuickStart = Console.ReadLine();
        logw("Unlock method (0 = Hybrid, 1 = Memory Write, 2 = Flags File): ");
        if (!isInt(Console.ReadLine()))
        {
            log("Invalid number. Setting it to 1 anyways.");
            UnlockMethod = 1;
        } else
        {
            UnlockMethod = Convert.ToInt32(Console.ReadLine());
        }

        // create the settings file
        string SettingsFile = "UnlockClient=" + UnlockClient + "\nUnlockStudio=" + UnlockStudio + "\n" + FPSValues + "\nFPSCapSelection=0" + "\nFPSCap=" + FPSCap + "\nCheckForUpdates=" + CheckForUpdates + "\nNonBlockingErrors=" + NonBlockingErrors + "\nSilentErrors=" + SilentErrors + "\nQuickStart=" + QuickStart + "\nUnlockMethod=" + UnlockMethod;
        File.WriteAllText("settings", SettingsFile);
        log("Settings file created.");
        log("Press any key to exit.");
        Console.ReadKey();
        Environment.Exit(0);
    }   
}
