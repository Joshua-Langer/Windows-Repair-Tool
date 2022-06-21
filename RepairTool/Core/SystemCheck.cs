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
            CheckSystem();
        }

        private static void CheckSystem()
        {
            Console.WriteLine("Is this a repair?");
            Console.WriteLine("Enter 'y' or 'n'");
            var answer = Console.Read();
            if (answer != 'y')
            {
                EnvironmentVars.ApplicationOnServer = true;

                Console.WriteLine("Checking for configuration file on server... please wait...");
                System.Threading.Thread.Sleep(1500);
                CheckConfFile();
            }
            else
            {
                EnvironmentVars.IPADDR = InputServerAddress();
                EnvironmentVars.BINDIR = "\\\\" + EnvironmentVars.IPADDR;
                ResetVarsForDirs();
                Console.WriteLine("Checking for configuration file on server... please wait...");
                System.Threading.Thread.Sleep(1500);
                CheckConfFile();
            }
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

        private static string InputServerAddress()
        {
            Console.WriteLine("Input the server IP Address that you are connecting to: ");
            return Console.ReadLine();
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
                //Console.WriteLine(EnvironmentVars.IPADDR);
                //Console.WriteLine(NetworkCheck.CurrentIPAddress());
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
