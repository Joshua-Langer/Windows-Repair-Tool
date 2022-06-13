using System.Diagnostics;
using System.IO;
using System.Net;

namespace RepairTool
{
    public static class ResourceDownloader
    {
        public static void DownloadTools()
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Downloading the tools... please wait...", w);
            }
            
            
            using (WebClient webClient = new WebClient())
            {
                Directory.CreateDirectory(EnvironmentVars.RESDIR + "prep\\");
                webClient.DownloadFile(
                    "https://www.malwarebytes.com/mwb-download/thankyou",
                    EnvironmentVars.RESDIR + "prep" + "\\mbam.exe");
            }
        }

        public static void CopyTools()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = "/MIR";
            start.FileName = EnvironmentVars.ROBOCOPY;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                exitCode = proc.ExitCode;
            }

            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Tool copy shows completed with exit code: " + exitCode, w);
            }
        }
    }
}