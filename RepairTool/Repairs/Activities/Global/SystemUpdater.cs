using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool.Repairs.Activities.Global
{
    public static class SystemUpdater
    {
        public static void CheckForUpdates()
        {


            SystemWrapUp.SystemFileChecker();
        }
    }
}
