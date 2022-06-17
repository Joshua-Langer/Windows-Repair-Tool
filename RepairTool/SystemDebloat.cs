using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool
{
    public static class SystemDebloat
    {
        static bool b_SingleRun = false;

        public static void RunTasks(bool singleRun)
        {
            b_SingleRun = singleRun;

            DebloatCompleted();
        }

        private static void DebloatCompleted()
        {
            EnvironmentVars.DebloatCompleted = true;
            CreateConf.UpdateConfiguration("Work State", "Debloat", EnvironmentVars.DebloatCompleted.ToString());
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo("Prep complete...", w);
            }

            if (b_SingleRun)
                Menu.Start();
            else
                Menu.Start(); // Temp, TODO: move to what the next step is here.
        }
    }
}
