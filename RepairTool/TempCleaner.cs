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
        public static void RunTasks()
        {
            ClearSSLCache();
            InternetExplorerClean();
            TempFileCleanup();

            Menu.Start();
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

        private static void TempFileCleanup()
        {
            var appdataPath = Environment.GetEnvironmentVariable("%USERPROFILE%");
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(appdataPath, w);
            }
            System.Threading.Thread.Sleep(15000);
        }
    }
}
