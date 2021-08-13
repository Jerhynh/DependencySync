namespace DependencySync.CLIService
{
    class CLIResponses
    {
        public static void DisplayHelp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" ______");
            Console.WriteLine("| |__| |       SentinelSec");
            Console.WriteLine("|  ()  |     Dependency Sync");
            Console.WriteLine($"|______|          {Program.DSVersion}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("About Dependency Sync:");
            Console.WriteLine("Dependency Sync is a tool to make synchronizing dependencies between multiple projects fast and easy.");
            Console.WriteLine("For best usage: Pair Dependency Sync with a .bat or .sh script to further automate synchronization.");
            Console.WriteLine("Avaliable CLI Flags:");
            Console.WriteLine("-DependencyPath: Specifies a dependency (file/folder) to be moved to the delivery path.");
            Console.WriteLine("-DeliveryPath: Specifies a delivery location (folder) for dependencies to be moved to.");
            Console.WriteLine("-usage: Displays the default message that shows when no command line arguments are passed.");
            Console.WriteLine("-help: Displays this message.");
            Console.WriteLine(String.Empty);
            Console.WriteLine("Examples:");
            Console.WriteLine("Note: Multiple dependencies can be specified at once.");
            Console.WriteLine(String.Empty);
            Console.WriteLine("DependencySync.exe -DependencyPath=\"C:\\files\\FolderOfDependencies\" -DependencyPath=\"C:\\files\\Dependency1.dll\" -DeliveryPath=\"C:\\\"");
            Console.WriteLine(String.Empty);
            Console.WriteLine("DependencySync.exe -usage");
        }

        public static void DisplayUsage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" ______");
            Console.WriteLine("| |__| |       SentinelSec");
            Console.WriteLine("|  ()  |     Dependency Sync");
            Console.WriteLine($"|______|          {Program.DSVersion}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to SentinelSec Dependency Sync!");
            Console.WriteLine("Please use the -help CLI flag to see all avaliable functions.");
            Console.WriteLine("Example: DependencySync.exe -help");
        }
    }
}
