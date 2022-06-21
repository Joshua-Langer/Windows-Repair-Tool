using RepairTool.Core;
using RepairTool.Repairs.Activities.OS;
using RepairTool.Repairs.Activities.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairTool.Repairs.Activities.Malware;

namespace RepairTool.Repairs
{
    public static class UserMenu
    {
        public static void Start()
        {
            Console.Clear();
            var serviceOrderNumber = "";

            Console.WriteLine("Please enter your service order number");
            Console.WriteLine("");
            serviceOrderNumber = Console.ReadLine();

            var thisRepairLog = EnvironmentVars.LOGFILE + serviceOrderNumber + ".log";
            var rawRepairDir = EnvironmentVars.RAWLOGDIR + "\\" + serviceOrderNumber + "\\";

            File.Create(thisRepairLog);

            EnvironmentVars.LOGFILE = thisRepairLog;
            EnvironmentVars.RAWLOGDIR = rawRepairDir;
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.Title = "Windows Repair Tool - " + EnvironmentVars.APPVERSION + " running on " + Systems.WindowsVersionDetection();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("           Windows Repair Tool " + EnvironmentVars.APPVERSION);
            Console.WriteLine("Please enter a selection for your phase of repair");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("           1. Windows OS Repairs");
            Console.WriteLine("           2. Malware Repairs");
            Console.WriteLine("           3. Initial Setup");
            Console.WriteLine("           0. Exit");
            var choice = ReadInt("Enter your phase: ");
            RepairLauncher(choice);
        }

        private static void RepairLauncher(int choice)
        {
            switch (choice)
            {
                case 1:
                    SystemRepair.BeginRepair();
                    break;
                case 2:
                    MalwareScans.RunScans();
                    break;
                case 3:
                    SystemPrep.BeginPrep();
                    break;
                case 0:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.SYSTEMLOGS))
                    {
                        Logger.LogInfo("Application is closing cleanly..." + EnvironmentVars.NORMALEXITCODE, w);
                    }
                    Environment.Exit(EnvironmentVars.NORMALEXITCODE);
                    break;
            }
        }

        private static int ReadInt(string text)
        {
            Console.WriteLine(text);
            var line = Console.ReadLine();
            return int.Parse(line);
        }
    }
}
