using System.Diagnostics;
using System.IO;
using RepairTool.Core;

namespace RepairTool.Repairs.Activities.OS
{
    public static class Cleanup
    {
        public static void DiskCheck()
        {
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
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
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
                Logger.LogInfo("Repairing Component Store", w);
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
            }
            
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Errors found on " + EnvironmentVars.SYSDRIVE + ". Scheduling full chkdsk at next reboot.", w);
            }
        }

        public static void CleanupMsi()
        {
            var runFile = EnvironmentVars.WINREP + "msi_cleanup\\msizap.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Repairing Component Store", w);
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
            }

            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Done.", w);
            }
        }

        public static void RepairFileExtensions()
        {
            var runFile = EnvironmentVars.WINREP + "repair_file_extension\\repair_file_extensions.bat";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Repairing Component Store", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

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
            }

            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Done, with exit code: " + exitCode, w);
            }
        }
    }
}