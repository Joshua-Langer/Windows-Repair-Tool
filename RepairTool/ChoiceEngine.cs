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
                    break;
                case 2:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Windows Repair Tool - Debloater (NYI)", w);
                    }
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
                        Logger.LogInfo("Windows Repair Tool - Repair All (NYI)", w);
                    }
                    break;
                case 8:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Technician Requeseted an Exit.", w);
                    }
                    Environment.Exit(EnvironmentVars.NORMALEXITCODE);
                    break;
                case 9:
                    using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                    {
                        Logger.LogInfo("Debug a function is not enabled.\n Returning to Main Menu.", w);
                    }
                    Console.Clear();
                    Menu.Start();
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
