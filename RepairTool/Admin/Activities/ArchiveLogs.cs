using RepairTool.Core;
using System;
using System.IO;
using System.IO.Compression;

namespace RepairTool.Admin.Activities
{
    public static class ArchiveLogs
    {
        public static void ZipCurrentLogs()
        {
            var dirToCompress = EnvironmentVars.LOGDIR;
            var quarterNum = "";
            var quarterYear = "";
            Console.Clear();
            Console.Title = "Windows Repair Tool - Admin Screen - Archive Menu " + EnvironmentVars.APPVERSION;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("           Windows Repair Tool - Admin Screen - Archive Menu" + EnvironmentVars.APPVERSION);
            Console.WriteLine("Please enter the quarter number");
            quarterNum = Console.ReadLine();
            Console.WriteLine("Please enter the year");
            quarterYear = Console.ReadLine();

            var targetDir = EnvironmentVars.ARCHIVE + "Q" + quarterNum + quarterYear + ".zip";

            ZipFile.CreateFromDirectory(dirToCompress, targetDir);

            using (StreamWriter w = File.AppendText(EnvironmentVars.SYSTEMLOGS))
            {
                Logger.LogInfo("Archive Created, stored at: " + targetDir, w);
            }
        }
    }
}
