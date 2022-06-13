using System;
using System.IO;
using System.Net.NetworkInformation;

namespace RepairTool
{
    public static class Initializer
    {
        public static void Initialize()
        {
            StartScreen();
            CreateDirectories();
            CheckConfFile();
            CheckForLocalServer();
            ResourceDownloader.CopyTools();
            //DetectInternet();
            //ResourceDownloader.DownloadTools();
        }

        private static void StartScreen()
        {
            Console.WriteLine("Windows System Repair Tool");
            Console.WriteLine("Written by: Joshua Langer");
            Console.WriteLine("Built in 2022");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Checking for existing directories and configurations... Please wait");
            System.Threading.Thread.Sleep(1500);
        }

        private static bool CheckForBinDir()
        {
            return Directory.Exists(EnvironmentVars.BINDIR);
        }

        private static bool CheckForLogDir()
        {
            return Directory.Exists(EnvironmentVars.LOGDIR);
        }

        private static bool CheckForResDir()
        {
            return Directory.Exists(EnvironmentVars.RESDIR);
        }

        private static bool CheckForExistingConf()
        {
            return File.Exists(EnvironmentVars.CONFFILE);
        }
        
        private static void CreateDirectories()
        {
            if (CheckForBinDir() && CheckForLogDir() && CheckForResDir())
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo("Application has been ran before, directories are present.", w);
                    System.Threading.Thread.Sleep(1500);
                }
            }
            
            if (!CheckForBinDir())
            {
                Directory.CreateDirectory(EnvironmentVars.BINDIR);
                Console.WriteLine("");
                Console.WriteLine("techbin directory created");
                System.Threading.Thread.Sleep(1500);
            }

            if (!CheckForLogDir())
            {
                Directory.CreateDirectory(EnvironmentVars.LOGDIR);
                Console.WriteLine("log directory created");
                System.Threading.Thread.Sleep(1500);
            }

            if (!CheckForResDir())
            {
                Directory.CreateDirectory(EnvironmentVars.RESDIR);
                Console.WriteLine("resources directory created");
                System.Threading.Thread.Sleep(1500);
            }
        }

        private static void CheckConfFile()
        {
            if (!CheckForExistingConf())
            {
                CreateConf.NewConfiguration("Booleans", "Errors Detected", EnvironmentVars.ErrorsDetected.ToString());
                CreateConf.NewConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                CreateConf.NewConfiguration("Booleans", "TargetMetro", EnvironmentVars.TargetMetro.ToString());
                CreateConf.NewConfiguration("Booleans", "Help", EnvironmentVars.Help.ToString());
                CreateConf.NewConfiguration("Booleans", "Skip Check Update", EnvironmentVars.SkipCheckUpdate .ToString());
                CreateConf.NewConfiguration("Storage", "Free Space After", EnvironmentVars.FreeSpaceAfter.ToString());
                CreateConf.NewConfiguration("Storage", "Free Space Before", EnvironmentVars.FreeSpaceBefore.ToString());
                CreateConf.NewConfiguration("Storage", "Free Space Saved", EnvironmentVars.FreeSpaceSaved.ToString());
                CreateConf.NewConfiguration("Work State", "Initialize", EnvironmentVars.InitializeCompleted.ToString());
                Console.WriteLine("conf.ini file created");
                System.Threading.Thread.Sleep(1500);
            }
            else
            {
                ConfReader.ConfigRead(EnvironmentVars.CONFFILE);
                Console.WriteLine("conf.ini file read and applied");
                System.Threading.Thread.Sleep(1500);
            }
        }

        private static void DetectInternet()
        {
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(EnvironmentVars.HOST, 3000);
                if (reply != null && reply.Status == IPStatus.Success)
                {
                    EnvironmentVars.InternetStatus = NetworkStatus.ONLINE;
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Network status is " + EnvironmentVars.InternetStatus + ", systems will update as needed...", w);
                        System.Threading.Thread.Sleep(1500);
                        ResourceDownloader.CopyTools();
                    }

                }
            }
            catch
            {
                EnvironmentVars.InternetStatus = NetworkStatus.OFFLINE;
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("Network status is: " + EnvironmentVars.InternetStatus + ", some systems will not work in offline mode.", w);
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }

        private static void CheckForLocalServer()
        {
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(EnvironmentVars.NETWORKTEST, 3000);
                
                if (reply != null && reply.Status == IPStatus.Success)
                {
                    EnvironmentVars.NetworkStatus = NetworkStatus.ONLINE;
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo(
                            "Network status is " + EnvironmentVars.NetworkStatus +
                            ", systems can be copied from local network...", w);
                        System.Threading.Thread.Sleep(1500);
                    }
                }
            }
            catch
            {
                EnvironmentVars.NetworkStatus = NetworkStatus.OFFLINE;
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("Network status is: " + EnvironmentVars.NetworkStatus + ", some systems will not work in offline mode.", w);
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }
    }
}