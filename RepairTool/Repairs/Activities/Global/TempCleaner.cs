using RepairTool.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RepairTool.Repairs.Activities.Global
{
    public class TempCleaner
    {
        private static bool b_CalledSingle = true;
        
        public static void RunTasks(bool runOnce)
        {
            b_CalledSingle = runOnce;
            ClearSSLCache();
            InternetExplorerClean();
            SystemTempFileCleanup();
            CleanRecycleBin();
            RunCCleaner();
            USBDeviceCleanup();
            ClearUpdateCache();
            ResetBranchCache();
            DefaultWindowsDiskCleanup();
            CleanupComplete();
        }

        private static void ClearSSLCache()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Clear SSL Cache " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Clear SSL Certificate Cache...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-URLcache * delete";
            // Enter the executable to run, including the complete path
            start.FileName = "certutil";
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
                        Logger.LogWarning("SSL Cache was not cleared.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void InternetExplorerClean()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - IE Cleanup " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Clean Internet Explorer...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "inetcpl.cpl,ClearMyTracksByProcess 4351";
            // Enter the executable to run, including the complete path
            start.FileName = "rundll32.exe";
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
                        Logger.LogWarning("Internet Explorer data was not cleaned.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void SystemTempFileCleanup()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Delete System Temp Files " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleaning System Temp Files...", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.FileName = EnvironmentVars.GLOBALREP + "\\tempfilecleanup\\systemp.bat";
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
                        Logger.LogWarning("System Temp files were not cleaned.", w);
                    }
                }
            }

            
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void CleanRecycleBin()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Empty Recycle Bin " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleaning hidden recycle bins...", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.FileName = EnvironmentVars.GLOBALREP + "\\tempfilecleanup\\rbinclean.bat";
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
                        Logger.LogWarning("Hidden recycle bin folder was not cleaned.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void RunCCleaner()
        {
            string runPath;

            if (Environment.Is64BitOperatingSystem)
                runPath = EnvironmentVars.GLOBALREP + "ccleaner\\ccleaner64.exe";
            else
                runPath = EnvironmentVars.GLOBALREP + "ccleaner\\ccleaner.exe";


            Console.Title = "Windows Repair Tool - Temp Clean - CCleaner " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("CCleaner Job Started...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "/auto";
            // Enter the executable to run, including the complete path
            start.FileName = runPath;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Hardcoded delay for 30 seconds
                System.Threading.Thread.Sleep(30000);

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("CCleaner failed to run correctly or was killed.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void USBDeviceCleanup()
        {
            string runPath;

            if (Environment.Is64BitOperatingSystem)
                runPath = EnvironmentVars.GLOBALREP + "usb_cleanup\\DriveCleanup x64.exe";
            else
                runPath = EnvironmentVars.GLOBALREP + "usb_cleanup\\DriveCleanup x86.exe";

            Console.Title = "Windows Repair Tool - Temp Clean - USB Cleanup " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Clean up USB Devices...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-n";
            // Enter the executable to run, including the complete path
            start.FileName = runPath;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Hardcoded delay for 30 seconds
                System.Threading.Thread.Sleep(30000);

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
                if (exitCode != 0)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                        Logger.LogWarning("USB Device data was not cleaned.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        public static void ClearUpdateCache()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Clear Update Cache " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleaning Update Files...", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.FileName = EnvironmentVars.GLOBALREP + "\\tempfilecleanup\\updatecache.bat";
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
                        Logger.LogWarning("Update Cache was not cleaned out.", w);
                    }
                }
            }


            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        public static void ResetBranchCache()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Reset Branch Cache " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Resetting the Branch Cache...", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.FileName = EnvironmentVars.GLOBALREP + "\\tempfilecleanup\\branchcache.bat";
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
                        Logger.LogWarning("Branch Cache was not cleaned.", w);
                    }
                }
            }


            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void DefaultWindowsDiskCleanup()
        {
            var runFile = EnvironmentVars.WINDIR + "system32\\cleanmgr.exe";
            Console.Title = "Windows Repair Tool - Temp Clean - Windows Disk Cleanup " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Windows Disk Cleanup...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "/sagerun:100";
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
                        Logger.LogWarning("Windows disk cleanup did not run correctly.", w);
                    }
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void CleanupComplete()
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleanup complete...", w);
            }
        }
    }
}
