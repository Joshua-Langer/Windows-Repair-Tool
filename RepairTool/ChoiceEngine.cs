using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool
{
    public static class ChoiceEngine
    {

        private const bool RANFROMDEBUGMENU = true;

        /// <summary>
        /// Initializes the engine to decide what steps we are automating 
        /// for configuration of a system.
        /// Int choice will process which function group we are running.
        /// </summary>
        /// <param name="choice"></param>
        public static void ActionToTake(int choice)
        {
            switch (choice)
            {
                case 1:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Temp Cleaner", w);
                    }
                    TempCleaner.RunTasks(RANFROMDEBUGMENU);
                    break;
                case 2:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Debloater", w);
                    }
                    SystemDebloat.RunTasks(RANFROMDEBUGMENU);
                    break;
                case 3:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Disinfector (NYI)", w);
                    }

                    break;
                case 4:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - OS Repair (NYI)", w);
                    }

                    break;
                case 5:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Patch OS (NYI)", w);
                    }

                    break;
                case 6:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Optimize OS (NYI)", w);
                    }

                    break;
                case 7:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Repair All", w);
                    }
                    Prep.RunTasks();
                    break;
                case 8:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("System Exiting with exit code: " + EnvironmentVars.NORMALEXITCODE, w);
                    }
                    Environment.Exit(EnvironmentVars.NORMALEXITCODE);
                    break;
                default:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogWarning("Invalid Selection was made.\n Returning to Main Menu.", w);
                    }

                    Console.Clear();
                    Menu.Start();
                    break;
            }
        }
    }
}
