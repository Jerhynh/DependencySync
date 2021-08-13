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
            try
            {
                SyncWorker(SyncOperation);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occurred while performing an active sync job!");
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
        }

        private static void SyncWorker(DSOperation SyncOperation)
        {
            if (SyncOperation.Dependencies.Count() == 0) return;
            if (String.IsNullOrEmpty(SyncOperation.DeliveryPath)) return;

            foreach (var dependency in SyncOperation.Dependencies)
            {
                if (File.Exists(dependency))
                {
                    // format for destination path must include dependency filename.
                    File.Copy(dependency, Path.Combine(SyncOperation.DeliveryPath, Path.GetFileName(dependency)), true);
                    Console.WriteLine($"Synchronized {Path.GetFileName(dependency)} to {SyncOperation.DeliveryPath}");
                }
                else if (Directory.Exists(dependency))
                {
                    DirectoryCopy(dependency, SyncOperation.DeliveryPath, SyncOperation.CopySubDirectories);
                    Console.WriteLine($"Synchronized {dependency} to {SyncOperation.DeliveryPath}");
                }
                else
                {
                    Console.WriteLine($"Unable to sync dependency: {dependency}! No valid sync operation could be determined.");
                }
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
