using RepairTool.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace RepairTool.Repairs.Activities.Global
{
    public static class SystemDebloat
    {
        static bool b_SingleRun = false;

        public static void RunTasks(bool singleRun)
        {
            b_SingleRun = singleRun;
            RemoveBloatwareByGUID();
            ClearWindowsApps();
            DebloatCompleted();
        }

        private static void RemoveBloatwareByGUID()
        {
            var repairType = "Debloat";
            var taskName = "Remove Bloatware by GUID";
            var runFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
            var arguments = "/wait msiexec /qn /norestart /x "; // += junkware[j]
            var exitCode = -1;

            
            string[] junkWare = File.ReadAllLines(EnvironmentVars.GLOBALREP + "oem\\programs_to_target_by_GUID.txt");
            if (File.Exists(EnvironmentVars.RAWLOGDIR + "wmic_dump.log"))
            {
                string[] currentGUIDList = File.ReadAllLines(EnvironmentVars.RAWLOGDIR + "wmic_dump.log");

                for (int i = 0; i < currentGUIDList.Length; i++)
                {
                    for (int j = 0; j < junkWare.Length; j++)
                    {
                        if (currentGUIDList[i] == junkWare[j])
                        {
                            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                            {
                                Logger.LogInfo(currentGUIDList[i] + " MATCH from target list, uninstalling...", w);
                            }
                            arguments += junkWare[j];
                            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
                            if (EnvironmentVars.WarningsDetected)
                            {
                                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                                {
                                    Logger.LogWarning(junkWare[j] + " was unabled to be removed...", w);
                                }
                                EnvironmentVars.WarningsDetected = false;
                            }
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo("No GUID List to compare against. Skipping GUID Bloatware removal...", w);
                    ToolBarRemoval();
                }
            }
        }

        private static void ToolBarRemoval()
        {
            var repairType = "Debloat";
            var taskName = "Remove Toolbars by GUID";
            var runFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
            var arguments = "/wait msiexec /qn /norestart /x "; // += junkware[j]
            var exitCode = -1;


            string[] junkWare = File.ReadAllLines(EnvironmentVars.GLOBALREP + "oem\\toolbars_BHOs_to_target_by_GUID.txt");
            if (File.Exists(EnvironmentVars.RAWLOGDIR + "wmic_dump.log"))
            {
                string[] currentGUIDList = File.ReadAllLines(EnvironmentVars.RAWLOGDIR + "wmic_dump.log");

                for (int i = 0; i < currentGUIDList.Length; i++)
                {
                    for (int j = 0; j < junkWare.Length; j++)
                    {
                        if (currentGUIDList[i] == junkWare[j])
                        {
                            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                            {
                                Logger.LogInfo(currentGUIDList[i] + " MATCH from target list, uninstalling...", w);
                            }
                            arguments += junkWare[j];
                            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments, exitCode);
                            if (EnvironmentVars.WarningsDetected)
                            {
                                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                                {
                                    Logger.LogWarning(junkWare[j] + " was unabled to be removed...", w);
                                }
                                EnvironmentVars.WarningsDetected = false;
                            }
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo("No GUID List to compare against. Skipping GUID Bloatware removal...", w);
                    RemoveBloatwareByName();
                }
            }
        }

        private static void RemoveBloatwareByName()
        {
            var repairType = "Debloat";
            var taskName = "Remove Bloatware by Name";
            var runFile = EnvironmentVars.GLOBALREP + "junkware.bat";
            var arguments = "";

            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Attempting junkware removal...", w);
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Application is NOT stalled here, this portion just takes a long time...", w);
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Errors about 'SHUTTING DOWN' are safe to ignore...", w);
            }

            string[] junkWare = File.ReadAllLines(EnvironmentVars.GLOBALREP + "oem\\programs_to_target_by_name.txt");
            ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments);
        }

        private static void ClearWindowsApps()
        {
            var repairType = "Debloat";
            var taskName = "Remove Windows Apps";
            var runFileFirst = EnvironmentVars.GLOBALREP + "metro\\metro_3rd_party_modern_apps_to_target_by_name.ps1";
            var runFileSecond = EnvironmentVars.GLOBALREP + "metro\\metro_Microsoft_modern_apps_to_target_by_name.ps1";
            var startFire = EnvironmentVars.WINDIR + "system32\\cmd.exe";
            var argumentsOne = "start /wait powershell -executionpolicy bypass -file " + runFileFirst;
            var argumentsTwo = "start /wait powershell -executionpolicy bypass -file " + runFileSecond;
            ProcessRunner.TaskRunner(repairType, taskName, startFire, argumentsOne);
            System.Threading.Thread.Sleep(2500);
            ProcessRunner.TaskRunner(repairType, taskName, startFire, argumentsTwo);
            System.Threading.Thread.Sleep(2500);
        }

        private static void DebloatCompleted()
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Debloat complete...", w);
            }
        }
    }
}
