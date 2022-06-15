using System;
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

        }
    }
}
