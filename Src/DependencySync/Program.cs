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

        private static void SyncWorker(DSOperation SyncOperation)
        {
            if (SyncOperation.Dependencies.Count() == 0) return;
            if (String.IsNullOrEmpty(SyncOperation.DeliveryPath)) return;

            foreach (var dependency in SyncOperation.Dependencies)
            {
                if (File.Exists(dependency))
                {
                    File.Copy(dependency, SyncOperation.DeliveryPath);
                }
                else if (Directory.Exists(dependency))
                {
                    DirectoryCopy(dependency, SyncOperation.DeliveryPath, SyncOperation.CopySubDirectories);
                }
                else
                {
                    Console.WriteLine("Unable to sync {}");
                }
                
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

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
