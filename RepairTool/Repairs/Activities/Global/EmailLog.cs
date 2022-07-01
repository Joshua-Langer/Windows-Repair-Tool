using RepairTool.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool.Repairs.Activities.Global
{
    public static class EmailLog
    {
        public static void Send()
        {
            // TODO: Implement email send


            if (EnvironmentVars.RebootRequired)
            {
                
            }
            if (EnvironmentVars.RebootRequired)
            {
                using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
                {
                    Logger.LogInfo("A system reboot is required... System will reboot once repairs are completed.", w);
                }
                RebootSystem.SystemReboot();
            }
        }
    }
}
