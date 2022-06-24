using RepairTool.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace RepairTool.Repairs.Activities.OS
{
    public static class InternetFixes
    {
        public static void WinsockReset()
        {
			Console.Title = "Windows Repair Tool - Repair - Winsock Reset " + EnvironmentVars.APPVERSION;
			var runFile = EnvironmentVars.WINDIR + "system32\\netsh.exe";
			using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
			{
				Logger.LogInfo("Resetting the Windows Socket Catalog", w);
			}
			// Prepare the process to run
			ProcessStartInfo start = new ProcessStartInfo();
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			// Enter in the command line arguments, everything you would enter after the executable name itself
			start.Arguments = "winsock reset";

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

			EnvironmentVars.RebootRequired = true;

			using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
			{
				Logger.LogInfo("Catalog reset, this system will need a reboot at the end of repairs.", w);
			}
		}

		public static void RepairDNS()
        {
			Console.Title = "Windows Repair Tool - Repair - DNS Repair " + EnvironmentVars.APPVERSION;
			var runFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
			using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
			{
				Logger.LogInfo("Resetting DNS on this system...", w);
			}
			// Prepare the process to run
			ProcessStartInfo start = new ProcessStartInfo();
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			// Enter in the command line arguments, everything you would enter after the executable name itself
			start.Arguments = "ipconfig /flushdns && ipconfig /registerdns";

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
				Logger.LogInfo("DNS has been reset", w);
			}
        }
    }
}
