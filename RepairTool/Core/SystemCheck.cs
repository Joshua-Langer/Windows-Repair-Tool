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
            Console.WriteLine("Version: " + EnvironmentVars.APPVERSION);
            Console.WriteLine("");
            Console.WriteLine("");
            CheckSystem();
#if DEBUG
            EnvironmentVars.noConsoleWindow = false;
            EnvironmentVars.processWindowHide = ProcessWindowStyle.Normal;
#endif
        }

        private static void CheckSystem()
        {
            if (EnvironmentVars.IPADDR == NetworkCheck.CurrentIPAddress())
            {
                Console.WriteLine("Checking for Server Configuration...");
                System.Threading.Thread.Sleep(5000);
                SetVarsForServerDirs();
                System.Threading.Thread.Sleep(500);
                CheckConfFile();
            }
            else
            {
                Console.WriteLine("Checking for Server Configuration to start repairs...");
                System.Threading.Thread.Sleep(5000);
                SetVarsForRepairDirs();
                System.Threading.Thread.Sleep(500);
                CheckConfFile();
            }
        }

        private static void SetVarsForServerDirs()
        {
            EnvironmentVars.BINDIR = "C:\\wrtbin";
            EnvironmentVars.LOGDIR = EnvironmentVars.BINDIR + "\\logs\\";
            EnvironmentVars.RAWLOGDIR = EnvironmentVars.LOGDIR + "RawLogs\\";
            EnvironmentVars.RESDIR = EnvironmentVars.BINDIR + "\\resources\\";
            EnvironmentVars.CONFDIR = EnvironmentVars.BINDIR + "\\configurations\\";
            EnvironmentVars.CONFFILE = EnvironmentVars.CONFDIR + "companyconfiguration.ini";
            EnvironmentVars.WINREP = EnvironmentVars.RESDIR + "WindowsRepair\\";
            EnvironmentVars.WINMAL = EnvironmentVars.RESDIR + "MalwareScans\\";
            EnvironmentVars.INITSETUP = EnvironmentVars.RESDIR + "InitialSetup\\";
            EnvironmentVars.GLOBALREP = EnvironmentVars.RESDIR + "GlobalRepairs\\";
            EnvironmentVars.SYSTEMLOGS = EnvironmentVars.BINDIR + "\\SystemLogs\\wrtlogger.log";
        }

        public static void SetVarsForRepairDirs()
        {
            EnvironmentVars.BINDIR = "\\\\" + EnvironmentVars.IPADDR + "\\Repair"; // For Home testing change repairs to tools
            EnvironmentVars.LOGDIR = EnvironmentVars.BINDIR + "\\logs\\";
            EnvironmentVars.RAWLOGDIR = EnvironmentVars.LOGDIR + "RawLogs\\";
            EnvironmentVars.RESDIR = EnvironmentVars.BINDIR + "\\resources\\";
            EnvironmentVars.CONFDIR = EnvironmentVars.BINDIR + "\\configurations\\";
            EnvironmentVars.CONFFILE = EnvironmentVars.CONFDIR + "companyconfiguration.ini";
            EnvironmentVars.WINREP = EnvironmentVars.RESDIR + "WindowsRepair\\";
            EnvironmentVars.WINMAL = EnvironmentVars.RESDIR + "MalwareScans\\";
            EnvironmentVars.INITSETUP = EnvironmentVars.RESDIR + "InitialSetup\\";
            EnvironmentVars.GLOBALREP = EnvironmentVars.RESDIR + "GlobalRepairs\\";
            EnvironmentVars.SYSTEMLOGS = EnvironmentVars.BINDIR + "\\SystemLogs\\wrtlogger.log";
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
