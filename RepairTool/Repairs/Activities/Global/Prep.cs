using Microsoft.Win32;
using RepairTool.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace RepairTool.Repairs.Activities.Global
{
    public static class Prep
    {

        private static string installLog = EnvironmentVars.RAWLOGDIR + "installed-programs-before.log";
        public static void RunTasks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Setup();
            RKill();
            SystemState();
            FileList();
            GUID();
            MetroDump();
            NoSleep();
            ProcessKiller();
            RegBack();
            NetInstall();
            PrepComplete();
        }

        private static void Setup()
        {
            EnvironmentVars.WarningsDetected = false;
            var repairType = "Prep";
            var taskName = "Initial Restore Point";
            var runFile = EnvironmentVars.WINDIR + "system32\\WindowsPowerShell\\v1.0\\powershell.exe";
            var arguments = "enable-computerrestore -drive C:\\";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("Restore point may not have been made, be cautious...", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void RKill()
        {
            var repairType = "Prep";
            var taskName = "Rkill";
            var runFile = EnvironmentVars.GLOBALREP + "rkill\\solitaire.exe";
            var arguments = "-s -l " + EnvironmentVars.RAWLOGDIR + "rkill.log -w " + EnvironmentVars.GLOBALREP + "rkill\\rkill_process_whitelist.txt";
            var exitCode = 0;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("If this task takes more than 20 minutes, kill solitaire.exe with Task Manager", w);
            }
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("RKill failed to run or was killed.", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void SystemState()
        {
            var regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regKey))
            {
                foreach(string subkey in key.GetSubKeyNames())
                {
                    using (RegistryKey sub = key.OpenSubKey(subkey))
                    {
                        if (sub.GetValue("DisplayName") != null)
                        {
                            using (StreamWriter w = File.AppendText(installLog))
                            {
                                Logger.LogInfo(sub.GetValue("DisplayName").ToString(), w);
                            }
                        }
                        
                    }
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void FileList()
        {
            var runFile = EnvironmentVars.GLOBALREP + "log_tools\\everything\\everything.exe";
            var repairType = "Prep";
            var taskName = "Analyze File List";
            var arguments = "-create-filelist " + EnvironmentVars.RAWLOGDIR + "filelist-before.txt C:\\";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("System file list was not logged", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void GUID()
        {
            var runFile = EnvironmentVars.WMIC;
            var repairType = "Prep";
            var taskName = "GUID Dump";
            var arguments = "product get identifyingnumber,name,version /all > " + EnvironmentVars.RAWLOGDIR + "wmic_dump.log";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("GUID dump was not completed.", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void MetroDump()
        {
            var runFile = EnvironmentVars.WINDIR + "System32\\WindowsPowerShell\\v1.0\\powershell.exe";
            var repairType = "Prep";
            var taskName = "Metro App Dump";
            var arguments = "Get-AppxPackage -AllUsers | Select Name > " + EnvironmentVars.RAWLOGDIR + "metro_app_dump.log";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("Metro App Dump was not completed.", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void NoSleep()
        {
            var runFile = EnvironmentVars.GLOBALREP + "caffeine\\caffeine.exe";
            Console.Title = "Windows Repair Tool - Prep - DisableSleepandScreensaver " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Temporarily disable system sleep and screensaver...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-noicon";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                if (proc.HasExited)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("Caffeine was not run or manually closed, this system may go to sleep during the scans.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void ProcessKiller()
        {
            var runFile = EnvironmentVars.GLOBALREP + "processkiller\\processkiller.exe";
            var repairType = "Prep";
            var taskName = "ProcessKiller";
            var arguments = "/silent";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("ProcessKiller did not run correctly", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        private static void RegBack()
        {
            var runFile = EnvironmentVars.GLOBALREP + "backup_registry\\erunt.exe";
            var repairType = "Prep";
            var taskName = "Registry Backup";
            var arguments = EnvironmentVars.RAWLOGDIR + "registry_backup /noconfirmdelete /noprogresswindow";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("System Registry was not backed up.", w);
                }
            }
            EnvironmentVars.WarningsDetected = false;
        }

        // Check for .NET 3.5 minimum.
        private static void NetInstall()
        {
            Console.Title = "Windows Repair Tool - Prep - McAfee Stinger " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Checking for .NET 3.5 first...", w);
            }

            RegistryKey registryKey = Registry.LocalMachine;
            registryKey = registryKey.OpenSubKey(@"software\microsoft\net framework setup\ndp\v3.5");

            if (registryKey == null)
            {
                EnvironmentVars.WarningsDetected = false;
                InstallNetThreeFive();
            }
            else
            {
                EnvironmentVars.WarningsDetected = false;
                EnvironmentVars.netThreeFivePresent = true;
            }
        }

        private static void InstallNetThreeFive()
        {
            var runFile = EnvironmentVars.WINDIR + "system32\\dism.exe";
            var repairType = "Prep";
            var taskName = "Install .NET 3.5";
            var arguments = "/online /enable-feature /featurename:NetFX3 /source:C:sourcessxs /LimitAccess";
            var exitCode = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning(".NET 3.5 was not installed, skipping McAfee Stinger...", w);
                }
                EnvironmentVars.WarningsDetected = false;
                EnvironmentVars.netThreeFivePresent = false;
            }
            else
            {
                EnvironmentVars.netThreeFivePresent = true;
            }
        }

        private static void PrepComplete()
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Prep complete...", w);
            }
            EnvironmentVars.WarningsDetected = false;
            UserMenu.Menu();
        }
    }
}
