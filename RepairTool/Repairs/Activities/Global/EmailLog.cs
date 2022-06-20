using System;
using System.Collections.Generic;
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
                RebootSystem.SystemReboot();
            }
        }
    }
}
