using System;
using System.Diagnostics;
using System.IO;
using RepairTool.Core;

namespace RepairTool.Repairs.Activities.OS
{
    public static class Cleanup
    {
        public static void DiskCheck()
        {
            var runFile = EnvironmentVars.WINDIR + "system32\\chkdsk.exe";
            var repairType = "Repair";
            var taskName = "Check Disk";
            var arguments = EnvironmentVars.SYSDRIVE;
            var exitCode = 0;
            var errorCodeToAvoid = 0;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode, errorCodeToAvoid);
            if (EnvironmentVars.WarningsDetected)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogWarning("TDSS Killer failed to run correctly.", w);
                }
            }

            if (EnvironmentVars.ErrorsDetected)
            {
                FileSystemUtil();
            }
        }

        private static void FileSystemUtil()
        {
            var runFile = EnvironmentVars.WINDIR + "system32\\fsutil.exe";
            var repairType = "Repair";
            var taskName = "Schedule Check Disk";
            var arguments = "dirty set " + EnvironmentVars.SYSDRIVE;
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments);
            EnvironmentVars.RebootRequired = true;
            }

        public static void CleanupMsi()
        {
            var runFile = EnvironmentVars.WINREP + "msi_cleanup\\msizap.exe";
            var repairType = "Repair";
            var taskName = "Clean Orphaned MSI Files";
            var arguments = "G!";
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments);
        }

        public static void RepairFileExtensions()
        {
            var runFile = EnvironmentVars.WINREP + "repair_file_extensions\\repair_file_extensions.bat";
            var repairType = "Repair";
            var taskName = "Repair File Extensions";
            var arguments = "";
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments);
        }
    }
}