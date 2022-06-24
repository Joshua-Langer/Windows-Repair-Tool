using System;
using System.Diagnostics;
using System.IO;
using RepairTool.Core;

namespace RepairTool.Repairs.Activities.OS
{
    public static class Cleanup
    {
        public static void DiskCheck()
        {
            Console.Title = "Windows Repair Tool - Repair - Check Disk " + EnvironmentVars.APPVERSION;
            var runFile = EnvironmentVars.WINDIR + "system32\\chkdsk.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Repairing Component Store", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = EnvironmentVars.SYSDRIVE;

            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;
            int exitCode;
            var error = "";


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                System.Threading.Thread.Sleep(30000);
                var output = proc.StandardOutput.ReadToEnd();
                error = proc.StandardError.ReadToEnd();
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogError(error, w);
                }
                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

            if (error != "0")
            {
                FileSystemUtil();
            }
            
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("No errors found on " + EnvironmentVars.SYSDRIVE + ". Skipping full chkdsk at next reboot.", w);
            }
        }

        private static void FileSystemUtil()
        {
            var runFile = EnvironmentVars.WINDIR + "system32\\fsutil.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Scheduling run for chkdsk.", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "dirty set " + EnvironmentVars.SYSDRIVE;

            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;
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
            }
            
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Errors found on " + EnvironmentVars.SYSDRIVE + ". Scheduling full chkdsk at next reboot.", w);
            }
        }

        public static void CleanupMsi()
        {
            Console.Title = "Windows Repair Tool - Repair - MSI Cleanup " + EnvironmentVars.APPVERSION;
            var runFile = EnvironmentVars.WINREP + "msi_cleanup\\msizap.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleanup orphaned MSI files...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "G!";

            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;
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
            }

            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Done.", w);
            }
        }

        public static void RepairFileExtensions()
        {
            Console.Title = "Windows Repair Tool - Repair - File Extension Repair " + EnvironmentVars.APPVERSION;
            var runFile = EnvironmentVars.WINREP + "repair_file_extensions\\repair_file_extensions.bat";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Repairing File Extensions...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;
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
            }

            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Done, with exit code: " + exitCode, w);
            }
        }
    }
}