using RepairTool.Admin;
using RepairTool.Repairs;
using System;
using System.Diagnostics;
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
            CheckSystem();
        }

        private static void CheckSystem()
        {
            
        }

        private static void ResetVarsForDirs()
        {
            EnvironmentVars.LOGDIR = EnvironmentVars.BINDIR + "\\logs\\";
            EnvironmentVars.RESDIR = EnvironmentVars.BINDIR + "\\resources\\";
            EnvironmentVars.CONFDIR = EnvironmentVars.BINDIR + "\\configurations\\";
            EnvironmentVars.CONFFILE = EnvironmentVars.CONFDIR + "companyconfiguration.ini";
            EnvironmentVars.WINREP = EnvironmentVars.RESDIR + "WindowsRepair\\";
            EnvironmentVars.WINMAL = EnvironmentVars.RESDIR + "MalwareScans\\";
            EnvironmentVars.INITSETUP = EnvironmentVars.RESDIR + "InitialSetup\\";
            EnvironmentVars.GLOBALREP = EnvironmentVars.RESDIR + "GlobalRepairs\\";
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
                if (NetworkCheck.CurrentIPAddress() == EnvironmentVars.IPADDR)
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
