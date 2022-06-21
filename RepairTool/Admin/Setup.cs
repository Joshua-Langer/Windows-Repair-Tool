using RepairTool.Core;
using System;
using System.IO;
using System.Net;

namespace RepairTool.Admin
{
    public static class Setup
    {
        private static string _setupLog = EnvironmentVars.SETUPLOG + Systems.CurrentDateTime() + ".log";

        public static void RunSetup()
        {
            Console.Title = "Repair Tool - Initial Setup " + EnvironmentVars.APPVERSION;
            CreateLogDirectory();
            CreateCompany();
            EnvironmentVars.IPADDR = GetServerAddress();
            CreateResourceDirectories();
            InstallResources();
            CreateConfiguration();
        }

        private static void CreateLogDirectory()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("**************************SETUP****************************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("System installing to C:\\wrtbin\\");
            ResetVarsForDirs();
            try
            {
                Console.WriteLine("Creating Bin Directory...");
                Directory.CreateDirectory(EnvironmentVars.BINDIR);
                System.Threading.Thread.Sleep(1500);
                Console.WriteLine("Creating Log Directory...");
                Directory.CreateDirectory(EnvironmentVars.LOGDIR);
                System.Threading.Thread.Sleep(1500);
                Console.WriteLine("Creating Config Directory...");
                Directory.CreateDirectory(EnvironmentVars.CONFDIR);
                System.Threading.Thread.Sleep(1500);
                Console.WriteLine("Creating Resources Directory...");
                Directory.CreateDirectory(EnvironmentVars.RESDIR);
                System.Threading.Thread.Sleep(1500);
            } catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(_setupLog))
                {
                    Logger.LogInfo(e + " Exit Code: " + EnvironmentVars.SETUPFAILEDTOCREATEMAINDIRS, w);
                }
            }
        }

        private static void ResetVarsForDirs()
        {
            EnvironmentVars.LOGDIR = EnvironmentVars.BINDIR +"\\logs\\";
            EnvironmentVars.RESDIR = EnvironmentVars.BINDIR + "\\resources\\";
            EnvironmentVars.CONFDIR = EnvironmentVars.BINDIR + "\\configurations\\";
            EnvironmentVars.CONFFILE = EnvironmentVars.CONFDIR + "companyconfiguration.ini";
            EnvironmentVars.WINREP = EnvironmentVars.RESDIR + "WindowsRepair\\";
            EnvironmentVars.WINMAL = EnvironmentVars.RESDIR + "MalwareScans\\";
            EnvironmentVars.INITSETUP = EnvironmentVars.RESDIR + "InitialSetup\\";
            EnvironmentVars.GLOBALREP = EnvironmentVars.RESDIR + "GlobalRepairs\\";
        }

        private static void CreateCompany()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("**************************SETUP****************************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Enter your company name: ");
            EnvironmentVars.COMPANYNAME = Console.ReadLine();
        }

        private static void CreateConfiguration()
        {
            try
            {
                CreateConf.NewConfiguration("Company", "Company Name", EnvironmentVars.COMPANYNAME);
                CreateConf.NewConfiguration("Application", "Version", EnvironmentVars.APPVERSION);
                CreateConf.NewConfiguration("System", "Bin Directory", EnvironmentVars.BINDIR);
                CreateConf.NewConfiguration("System", "Log Directory", EnvironmentVars.LOGDIR);
                CreateConf.NewConfiguration("System", "Resources Directory", EnvironmentVars.RESDIR);
                CreateConf.NewConfiguration("System", "Configuration Directory", EnvironmentVars.CONFDIR);
                CreateConf.NewConfiguration("Resource", "Windows Repair", EnvironmentVars.WINREP);
                CreateConf.NewConfiguration("Resource", "Malware Repair", EnvironmentVars.WINMAL);
                CreateConf.NewConfiguration("Resource", "Initial Setup", EnvironmentVars.INITSETUP);
                CreateConf.NewConfiguration("Resource", "Global Repair", EnvironmentVars.GLOBALREP);
                CreateConf.NewConfiguration("Network", "IP Address", EnvironmentVars.IPADDR);
                Console.WriteLine("Company Configuration file created");
            } catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(_setupLog))
                {
                    Logger.LogError("Failed to create configuration file..." + e.Message + " Exit Code: " + EnvironmentVars.SETUPFAILEDTOCREATECONF, w);
                }
            }
            
            System.Threading.Thread.Sleep(1500);
            Environment.Exit(EnvironmentVars.INITIALSETUPCOMPLETE);
        }

        private static void CreateResourceDirectories()
        {
            try
            {
                Console.WriteLine("Creating Resources Directories...");
                Directory.CreateDirectory(EnvironmentVars.WINREP);
                System.Threading.Thread.Sleep(1500);
                Directory.CreateDirectory(EnvironmentVars.WINMAL);
                System.Threading.Thread.Sleep(1500);
                Directory.CreateDirectory(EnvironmentVars.INITSETUP);
                System.Threading.Thread.Sleep(1500);
                Directory.CreateDirectory(EnvironmentVars.GLOBALREP);
                System.Threading.Thread.Sleep(1500);
            } catch(Exception e)
            {
                using (StreamWriter w = File.AppendText(_setupLog))
                {
                    Logger.LogInfo("Failed to install resource directories..." + e.Message + " Exit Code: " + EnvironmentVars.SETUPFAILEDTOCREATERESDIRS, w);
                }
            }
        }

        // TODO: this is broken, Issue #1
        private static void InstallResources()
        {
            var flashLocation = Directory.GetCurrentDirectory() + "\\resources\\";
            string[] flashFiles = Directory.GetFiles(flashLocation);
            try
            {
                foreach (string flashFile in flashFiles)
                {
                    File.Copy(flashFile, flashLocation, true);
                }
                Console.WriteLine("Setup is now completed. Share the folder on your server where everything is stored.");
                Console.WriteLine("Please load the application from a machine that needs to have repairs on it to ");
                Console.WriteLine("begin repairs...");
                Console.WriteLine("Running this again from this server will place you in the Admin Menu.");
            } catch(Exception e)
            {
                using (StreamWriter w = File.AppendText(_setupLog))
                {
                    Logger.LogInfo("Setup Failed to install resources..." + e.Message + " Exit Code: " + EnvironmentVars.SETUPFAILEDTOINSTALLRESOURCE, w);
                }
            }
        }

        private static string GetServerAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            try
            {
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            } catch (Exception e)
            {
                using (StreamWriter w = File.AppendText(_setupLog))
                {
                    Logger.LogInfo("Setup Failed to register an IP Address..." + e.Message + " Exit Code: " + EnvironmentVars.SETUPFAILEDTOGETIPADDR, w);
                }
            }
            throw new Exception("Setup Failed to register an IP Address..." + " Exit Code: " + EnvironmentVars.SETUPFAILEDTOGETIPADDR);
        }
    }
}
