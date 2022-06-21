using RepairTool.Admin;
using RepairTool.Repairs;
using System;
using System.IO;

namespace RepairTool.Core
{
    public static class SystemCheck
    {
        public static void Initialize()
        {
            Console.WriteLine("Windows Repair Tool");
            Console.WriteLine("Written by: Joshua Langer");
            Console.WriteLine("Built in 2022");
            Console.WriteLine(EnvironmentVars.APPVERSION);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Checking for configuration file on server... please wait...");
            EnvironmentVars.BINDIR = EnvironmentVars.ROOTDIR;
            System.Threading.Thread.Sleep(1500);
            CheckConfFile();
        }

        private static bool CheckForExistingConf()
        {
            return File.Exists(EnvironmentVars.CONFFILE);
        }

        private static void CheckConfFile()
        {
            if (!CheckForExistingConf())
            {
                Console.WriteLine("A configuration file has not been detected.");
                Console.WriteLine("Please make sure you are running this from a flash drive for initial setup...");
                Console.WriteLine("Follow on Screen for further steps...");
                System.Threading.Thread.Sleep(1500);
                Setup.RunSetup();
            }
            else
            {
                ConfReader.ConfigRead(EnvironmentVars.CONFFILE);
                Console.WriteLine("Configuration file read and applied...System Starting");
                System.Threading.Thread.Sleep(1500);
                if (NetworkCheck.Network())
                {
                    // Go to the Admin screen
                    AdminMaintenance.AdminMenu();
                }
                else
                {
                    // Otherwise go to the repair launcher
                    UserMenu.Start();
                }
            }
        }
    }
}
