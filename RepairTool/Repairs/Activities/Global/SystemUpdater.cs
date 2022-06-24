using RepairTool.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool.Repairs.Activities.Global
{
    public static class SystemUpdater
    {
        public static void CheckForUpdates()
        {
            Console.Title = "Windows Repair Tool - Updates - " + EnvironmentVars.APPVERSION;
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Updates has not been implemented yet...", w);
            }
        }
    }
}
