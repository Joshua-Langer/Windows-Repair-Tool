﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairTool.Core;

namespace RepairTool.Repairs.Activities.Setup
{
    public static class Installer
    {
        public static void GoogleChrome()
        {
            var runFile = EnvironmentVars.INITSETUP + "GoogleChrome\\ChromeSetup.exe";
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Installing Chrome...", w);
            }
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "/silent /install";

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
                Logger.LogInfo("Chrome installed...", w);
            }
        }

        public static void InstallAdobe()
        {
            
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Adobe will be installed....", w);
            }
        }
    }
}
