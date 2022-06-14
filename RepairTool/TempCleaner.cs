using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool
{
    public class TempCleaner
    {
        private static bool b_CalledSingle = true;
        
        public static void RunTasks(bool runOnce)
        {
            b_CalledSingle = runOnce;
            SystemTempFileCleanup();
            ClearSSLCache();
            InternetExplorerClean();
            
            CleanRecycleBin();
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
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        // TODO: Test this
        private static void SystemTempFileCleanup()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - Delete System Temp Files " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleaning System Temp Files...", w);
            }

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = EnvironmentVars.STAGE1 + "systemp.bat";
            p.Start();

            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(output, w);
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        // TODO: Test
        private static void CleanRecycleBin()
        {
            var binPath = EnvironmentVars.SYSDRIVE + "$Recycle.Bin";
            Console.Title = "Windows Repair Tool - Temp Clean - Cleaning Recycle Bin " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Clean Recycle Bin...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "rmdir /s /q " + binPath;
            // Enter the executable to run, including the complete path
            start.FileName = "cmd";
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

        private static void CleanupComplete()
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Cleanup complete...", w);
            }
            if (b_CalledSingle)
                Menu.Start();
        }

        private static void EmptyProcFunction()
        {
            Console.Title = "Windows Repair Tool - Temp Clean - [JOB] " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Clean [JOB]...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "";
            // Enter the executable to run, including the complete path
            start.FileName = "";
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
        
    }
}
