using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencySync.CLIService;
using DependencySync.Models;

namespace DependencySync.CLIService
{
    class ArgumentParsing
    {
        public static DSOperation ParseArgs(string[] args)
        {
            DSOperation operation = new();
            if (args.Length == 0)
            {
                // No argument was provided,display usage message.
                CLIResponses.DisplayUsage();
                Environment.Exit(0);
            }
            else if (args.Length == 1 && args[0] == "-help")
            {
                CLIResponses.DisplayHelp();
                Environment.Exit(0);
            }
            else
            {
                // Parse avaliable arguments and return a object to be passed to the SyncWorker.
                foreach (string arg in args)
                {
                    // Obtain Dependency paths
                    if (arg.ToLower().Contains("dependencypath"))
                    {
                        string dependencyPath = "";
                        try
                        {
                            dependencyPath = arg.Split('=')[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine($"Unable to parse dependency path for CLI argument: {arg}");
                        }
                        if (File.Exists(dependencyPath) || Directory.Exists(dependencyPath))
                        {
                            Console.WriteLine($"Found Dependency Path: {dependencyPath}");
                            operation.Dependencies.Add(dependencyPath);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Error resolving path to dependency: {dependencyPath}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    // Obtain Dependency Delivery Path (Must exit upon failure as path is crucial to operation.)
                    else if (arg.ToLower().Contains("deliverypath"))
                    {
                        string deliveryPath = string.Empty;
                        try
                        {
                            deliveryPath = arg.Split('=')[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Unable to parse dependency path for CLI argument: {arg}");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Exiting...");
                            Environment.Exit(1);
                        }
                        if (File.Exists(deliveryPath) || Directory.Exists(deliveryPath))
                        {
                            Console.WriteLine($"Found Delivery Path: {deliveryPath}");
                            operation.DeliveryPath = deliveryPath;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"An error occurred while resolving the Delivery Path: {deliveryPath}");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Exiting...");
                            Environment.Exit(1);
                        }
                    }
                }
            }
            return operation;
        }
    }
}
