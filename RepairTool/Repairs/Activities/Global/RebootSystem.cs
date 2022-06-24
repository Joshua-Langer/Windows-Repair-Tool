using RepairTool.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool.Repairs.Activities.Global
{
    public static class RebootSystem
    {
        public static void SystemReboot()
        {
			var runFile = EnvironmentVars.WINDIR + "system32\\shutdown.exe";
			using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
			{
				Logger.LogInfo("Repairing Component Store", w);
			}
			// Prepare the process to run
			ProcessStartInfo start = new ProcessStartInfo();
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			// Enter in the command line arguments, everything you would enter after the executable name itself
			start.Arguments = "-r -t 60";

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
    }
}
