using System;
using System.Diagnostics;
using System.IO;

namespace RepairTool.Core
{
    public static class ProcessRunner
    {
        private const string CONSOLETITLE = "Windows Repair Tool - ";
        
        /// <summary>
        /// Task runner to run a task without output logging. Overloads.
        /// </summary>
        /// <param name="repairType"></param>
        /// <param name="taskName"></param>
        /// <param name="runFile"></param>
        /// <param name="arguments"></param>
        public static void TaskRunner(string repairType, string taskName, string runFile, string arguments)
        {
            Console.Title = CONSOLETITLE + repairType + " - " + taskName + " " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(repairType + " " + taskName + " is running", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.Arguments = arguments;
            start.FileName = runFile;
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;

            using (Process process = Process.Start(start))
            {
                process.WaitForExit();
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(taskName + " has completed...", w);
            }
        }

        /// <summary>
        /// Task runner to run a task with output logging but no error logging. Overloads.
        /// </summary>
        /// <param name="repairType"></param>
        /// <param name="taskName"></param>
        /// <param name="runFile"></param>
        /// <param name="arguments"></param>
        /// <param name="exitCodeToAvoid"></param>
        public static void TaskRunner(string repairType, string taskName, string runFile, string arguments,
            int exitCodeToAvoid)
        {
            EnvironmentVars.WarningsDetected = false;
            Console.Title = CONSOLETITLE + repairType + " - " + taskName + " " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(repairType + " " + taskName + " is running", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.Arguments = arguments;
            start.FileName = runFile;
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;
            int exitCode;

            using (Process process = Process.Start(start))
            {
                process.WaitForExit();
                System.Threading.Thread.Sleep(15000);
                var output = process.StandardOutput.ReadToEnd();
                exitCode = process.ExitCode;
                if (exitCode == exitCodeToAvoid)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                    }
                }
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
                
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(taskName + " has completed...", w);
            }
        }

        /// <summary>
        /// Task runner to run a task with output logging and error logging.
        /// Uses errorCodeToAvoid to check against errors in a called process.
        /// </summary>
        /// <param name="repairType"></param>
        /// <param name="taskName"></param>
        /// <param name="runFile"></param>
        /// <param name="arguments"></param>
        /// <param name="exitCodeToAvoid"></param>
        /// <param name="errorCodeToAvoid"></param>
        public static void TaskRunner(string repairType, string taskName, string runFile, string arguments,
            int exitCodeToAvoid, int errorCodeToAvoid)
        {
            EnvironmentVars.ErrorsDetected = false; // Set to false in case an error was detected in a previous method run
            Console.Title = CONSOLETITLE + repairType + " - " + taskName + " " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(repairType + " " + taskName + " is running", w);
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.Arguments = arguments;
            start.FileName = runFile;
            start.WindowStyle = EnvironmentVars.processWindowHide;
            start.CreateNoWindow = EnvironmentVars.noConsoleWindow;
            int exitCode;

            var error = "";
            
            using (Process process = Process.Start(start))
            {
                process.WaitForExit();
                System.Threading.Thread.Sleep(15000);
                var output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                exitCode = process.ExitCode;
                if (exitCode == exitCodeToAvoid)
                {
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        EnvironmentVars.WarningsDetected = true;
                    }
                }
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(error, w);
                }
            }

            if (error != errorCodeToAvoid.ToString())
            {
                EnvironmentVars.ErrorsDetected = true; // Set to true in case we need to run additional methods.
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogError(taskName + " has completed with errors...", w);
                }
            }
            
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(taskName + " has completed without errors...", w);
            }
        }
    }
}