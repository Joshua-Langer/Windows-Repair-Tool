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
            CheckConfFile();
            SystemInfo();
            Menu.Start();
        }

        private static void StartScreen()
        {
            Console.WriteLine("Windows System Repair Tool");
            Console.WriteLine("Written by: Joshua Langer");
            Console.WriteLine("Built in 2022");
            Console.WriteLine(EnvironmentVars.APPVERSION);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Checking for updates... Please wait");
            System.Threading.Thread.Sleep(1500);
        }

        private static bool CheckForExistingConf()
        {
            return File.Exists(EnvironmentVars.CONFFILE);
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

        private static void SystemInfo()
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(Systems.WindowsVersionDetection(), w);
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(Systems.SystemLanguage(), w);
            }
        }
    }
}