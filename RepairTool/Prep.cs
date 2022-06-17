using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
            NetInstall();
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("Restore point may not have been made, be cautious.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
            start.Arguments = "-s -l " + EnvironmentVars.LOGDIR + "rawlogs\\xrkill.log -w " + EnvironmentVars.STAGE0 + "rkill\\rkill_process_whitelist.txt";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("RKill failed to run or was killed.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
            start.Arguments = "-save=[software]=" + EnvironmentVars.LOGDIR + "rawlogs\\installed-programs-before.txt";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("System Programs were not logged", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
            }
        }

        private static void FileList()
        {
            var runFile = EnvironmentVars.STAGE0 + "log_tools\\everything\\everything.exe";

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-create-filelist " + EnvironmentVars.LOGDIR + "rawlogs\\filelist-before.txt C:\\";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("System file list was not logged.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
            start.Arguments = "product get identifyingnumber,name,version /all > " + EnvironmentVars.LOGDIR + "rawlogs\\wmic_dump.log";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("GUID dump was not completed.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
            start.Arguments = "Get-AppxPackage -AllUsers | Select Name > " + EnvironmentVars.LOGDIR + "rawlogs\\metro_app_dump.log";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("Metro App Dump was not completed.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                if (proc.HasExited)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("Caffeine was not run or manually closed, this system may go to sleep during the scans.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("ProcessKiller did not run correctly.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("System Registry was not backed up.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
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
                InstallNetThreeFive();
            }
            else
            {
                Stinger();
            }
        }

        private static void InstallNetThreeFive()
        {
            var runFile = EnvironmentVars.WINDIR + "system32\\dism.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Installing .NET 3.5...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "/online /enable-feature /featurename:NetFX3 /source:C:sourcessxs /LimitAccess";

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
                System.Threading.Thread.Sleep(30000);
                var output = proc.StandardOutput.ReadToEnd();
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
                // Retrieve the app's exit code
                exitCode = proc.ExitCode;

                if (exitCode != 0)
                {
                    var netInstallFail = true;
                    TDKiller(netInstallFail);
                }
                else
                {
                    Stinger();
                }
            }
        }

        private static void Stinger()
        {
            var runFile = EnvironmentVars.STAGE0 + "\\mcafee_stinger\\stinger32.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Running McAfee Stinger...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "--GO --SILENT --PROGRAM --REPORTPATH=" + EnvironmentVars.LOGDIR + "rawlogs\\ --DELETE";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("Stinger failed to run correctly", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void TDKiller(bool stingerSkipped)
        {
            if (stingerSkipped)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    EnvironmentVars.WarningsDetected = true;
                    Logger.LogWarning("Stinger was skipped due to an issue with .NET 3.5 install.", w);
                    CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                }
            }

            var runFile = EnvironmentVars.STAGE0 + "tdss_killer\\TDSSKiller.exe";
            Console.Title = "Windows Repair Tool - Prep - TDSS Killer " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Running TDSS Killer...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-l " + EnvironmentVars.LOGDIR + "rawlogs\\ -silent -tdlfs -dcexact - accepteula -accepteulaksn";
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
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("TDSS Killer failed to run correctly.", w);
                        CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    }
                }
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
                Logger.LogInfo("Prep complete...", w);
            }
            TempCleaner.RunTasks(false);
        }
    }
}
