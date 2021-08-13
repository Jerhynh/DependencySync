using DependencySync.CLIService;
using DependencySync.Models;

namespace DependencySync
{
    public class Program
    {
        public const string DSVersion = "1.0.0";

        public static void Main(string[] args)
        {

            Console.Title = "SentinelSec Dependency Sync";
            Console.WriteLine("Initializing Dependency Sync...");
            DSOperation SyncOperation = ArgumentParsing.ParseArgs(args);
        }

        
    }
}
