using RepairTool.Admin.Activities;
using RepairTool.Core;
using System;
using System.IO;

namespace RepairTool.Admin
{
    public static class AdminMaintenance
    {
        // TODO: Issue #4 here, working in this menu, get a crash.
        // Line 61 is the crash
        public static void AdminMenu()
        {
            Console.Clear();
            Console.Title = "Windows Repair Tool - Admin Screen" + EnvironmentVars.APPVERSION;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("           Windows Repair Tool - Admin Screen " + EnvironmentVars.APPVERSION);
            Console.WriteLine("Please enter a selection for maintenance");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("           1. System Update - NYI");
            Console.WriteLine("           2. Change Directories - NYI");
            Console.WriteLine("           3. Update Company Name");
            Console.WriteLine("           4. Archive Logs");
            Console.WriteLine("           0. Exit");
            var choice = ReadInt("Enter your phase: ");
            AdminLauncher(choice);
        }

        private static void AdminLauncher(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Updates are not yet implemented");
                    System.Threading.Thread.Sleep(3000);
                    AdminMenu();
                    break;
                case 2:
                    Console.WriteLine("Updating your working directories is not yet implemented");
                    System.Threading.Thread.Sleep(3000);
                    AdminMenu();
                    break;
                case 3:
                    UpdateCompanyName.NewName();
                    break;
                case 4:
                    ArchiveLogs.ZipCurrentLogs();
                    break;
                case 0:
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
