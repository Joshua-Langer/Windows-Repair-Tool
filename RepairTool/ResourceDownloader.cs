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
    }
}