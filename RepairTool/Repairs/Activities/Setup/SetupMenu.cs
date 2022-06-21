using RepairTool.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool.Repairs.Activities.Setup
{
    public static class SetupMenu
    {
        public static void Menu()
        {
            Console.Clear();
            Console.Title = "Setup and Provision Tool - " + EnvironmentVars.APPVERSION;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(EnvironmentVars.COMPANYNAME + "            Setup and Provision Tool " + EnvironmentVars.APPVERSION);
            Console.WriteLine("Please enter a selection for your phase of repair");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("           1. Managed Services Customer");
            Console.WriteLine("           2. Bench Customer");
            Console.WriteLine("           0. Exit");
            var choice = ReadInt("Enter your phase: ");
            RepairLauncher(choice);
        }

        private static void RepairLauncher(int choice)
        {
            switch (choice)
            {
                case 1:
                    ManagedCustomer.BeginSetup();
                    break;
                case 2:
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

