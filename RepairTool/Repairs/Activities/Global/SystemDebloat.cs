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
            Console.Title = "Windows Repair Tool - Debloat - Remove Bloatware by GUID " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Comparing system GUID list against blacklisted entries, please wait...", w);
            }
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
                            var runFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
                            // Prepare the process to run
                            ProcessStartInfo start = new ProcessStartInfo();
                            start.UseShellExecute = false;
                            start.RedirectStandardOutput = true;
                            // Enter in the command line arguments, everything you would enter after the executable name itself
                            start.Arguments = "/wait msiexec /qn /norestart /x " + junkWare[j];

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
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo("No GUID List to compare against. Skipping GUID Bloatware removal...", w);
                    EnvironmentVars.WarningsDetected = true;
                    CreateConf.UpdateConfiguration("Booleans", "Warnings Detected", EnvironmentVars.WarningsDetected.ToString());
                    ToolBarRemoval();
                }
            }
        }

        private static void ToolBarRemoval()
        {
            Console.Title = "Windows Repair Tool - Debloat - Remove Toolbars by GUID " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Comparing system GUID list against blacklisted entries, please wait...", w);
            }
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
                            var runFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
                            // Prepare the process to run
                            ProcessStartInfo start = new ProcessStartInfo();
                            start.UseShellExecute = false;
                            start.RedirectStandardOutput = true;
                            // Enter in the command line arguments, everything you would enter after the executable name itself
                            start.Arguments = "/wait msiexec /qn /norestart /x " + junkWare[j];

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
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo("No GUID List to compare against. Skipping GUID Toolbar removal...", w);
                    EnvironmentVars.WarningsDetected = true;
                    RemoveBloatwareByName();
                }
            }
        }

        private static void RemoveBloatwareByName()
        {
            Console.Title = "Windows Repair Tool - Debloat - Remove Toolbars by GUID " + EnvironmentVars.APPVERSION;
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
            var runFile = EnvironmentVars.GLOBALREP + "junkware.bat";
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            start.UseShellExecute = false;
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.RedirectStandardOutput = true;
            // Enter the executable to run, including the complete path
            start.FileName = runFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Normal;
            start.CreateNoWindow = false;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                var output = proc.StandardOutput.ReadToEnd();
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
        }

        private static void ClearWindowsApps()
        {
            Console.Title = "Windows Repair Tool - Debloat - Remove Windows Apps " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Attempting Windows Apps removal...", w);
            }
            var runFileFirst = EnvironmentVars.GLOBALREP + "metro\\metro_3rd_party_modern_apps_to_target_by_name.ps1";
            var starterFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "start /wait powershell -executionpolicy bypass -file " + runFileFirst;
            // Enter the executable to run, including the complete path
            start.FileName = starterFile;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                var output = proc.StandardOutput.ReadToEnd();
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
            }
            var runFileLast = EnvironmentVars.GLOBALREP + "metro\\metro_Microsoft_modern_apps_to_target_by_name.ps1";
            // Prepare the process to run
            ProcessStartInfo end = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            end.Arguments = "start /wait powershell -executionpolicy bypass -file " + runFileLast;
            // Enter the executable to run, including the complete path
            end.FileName = starterFile;
            // Do you want to show a console window?
            end.WindowStyle = ProcessWindowStyle.Hidden;
            end.CreateNoWindow = true;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                var output = proc.StandardOutput.ReadToEnd();
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo(output, w);
                }
            }
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Complete...", w);
            }
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
