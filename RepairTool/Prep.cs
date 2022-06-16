﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool
{
    public static class Prep
    {
        public static void RunTasks()
        {
            Console.Clear();
            Setup();
            RKill();
            SystemState();
            FileList();
            GUID();
            MetroDump();
            NoSleep();
            ProcessKiller();
            RegBack();
            PrepComplete();
        }

        private static void Setup()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Prep Beginning " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Begining prep stage...", w);
            }
            System.Threading.Thread.Sleep(3000);
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Creating a restore point...", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.Arguments = "enable-computerrestore -drive C:\\";
            start.FileName = EnvironmentVars.WINDIR + "System32\\WindowsPowerShell\\v1.0\\powershell.exe";
            start.CreateNoWindow = true;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.StandardOutputEncoding = Encoding.UTF8;
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                var output = proc.StandardOutput.ReadToEnd();
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }


            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void RKill()
        {
            var runFile = EnvironmentVars.STAGE0 + "rkill\\solitaire.exe";
            Console.Title = "Windows Repair Tool - Prep - RKill " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Running RKill...", w);
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("If this task takes more than 20 minutes, kill solitaire.exe with Task Manager", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-s -l " + EnvironmentVars.LOGDIR + "tron_rkill.log -w " + EnvironmentVars.STAGE0 + "rkill\\rkill_process_whitelist.txt";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void SystemState()
        {
            string runFile;

            if (Environment.Is64BitOperatingSystem)
                runFile = EnvironmentVars.STAGE0 + "log_tools\\siv\\siv32x.exe";
            else
                runFile = EnvironmentVars.STAGE0 + "log_tools\\siv\\siv64x.exe";

            Console.Title = "Windows Repair Tool - Prep - Analyze System State " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Generating pre-run system profile...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-save=[software]=" + EnvironmentVars.LOGDIR + "installed-programs-before.txt";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
        }

        private static void FileList()
        {
            var runFile = EnvironmentVars.STAGE0 + "log_tools\\everything\\everything.exe";

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-create-filelist " + EnvironmentVars.LOGDIR + "filelist-before.txt C:\\";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void GUID()
        {
            var runFile = EnvironmentVars.WMIC;
            Console.Title = "Windows Repair Tool - Prep - GUID Dump " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Dumping GUID list to logs...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "product get identifyingnumber,name,version /all > " + EnvironmentVars.LOGDIR + "wmic_dump.log";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void MetroDump()
        {
            var runFile = EnvironmentVars.WINDIR + "System32\\WindowsPowerShell\\v1.0\\powershell.exe";
            Console.Title = "Windows Repair Tool - Prep - Metro app dump " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Dumping Metro app list to log...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "Get-AppxPackage -AllUsers | Select Name > " + EnvironmentVars.LOGDIR + "metro_app_dump.log";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void NoSleep()
        {
            var runFile = EnvironmentVars.STAGE0 + "caffeine\\caffeine.exe";
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
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void ProcessKiller()
        {
            var runFile = EnvironmentVars.STAGE0 + "\\processkiller\\processkiller.exe";
            Console.Title = "Windows Repair Tool - Prep - ProcessKiller " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Process Killer running...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "/silent";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void RegBack()
        {
            var runFile = EnvironmentVars.STAGE0 + "backup_registry\\erunt.exe";
            Console.Title = "Windows Repair Tool - Prep - Registry Backup " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Backing up the registry...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = EnvironmentVars.LOGDIR + "registry_backup /noconfirmdelete /noprogresswindow";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }



        private static void Blank()
        {
            var runFile = EnvironmentVars.STAGE0 + "";
            Console.Title = "Windows Repair Tool - Prep - [Job] " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("[Job]...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "";
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void PrepComplete()
        {
            CreateConf.UpdateConfiguration("Work State", "Prep", "true");
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleanup complete...", w);
            }
            TempCleaner.RunTasks(false);
        }
    }
}
