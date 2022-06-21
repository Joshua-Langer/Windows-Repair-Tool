﻿using RepairTool.Repairs.Activities.Global;

namespace RepairTool.Repairs.Activities.Setup
{
    public static class SystemPrep
    {
        public static void BeginPrep()
        {
            Installer.GoogleChrome();
            Installer.InstallAdobe();
            GlobalTaskRunner.Run();
        }
    }
}
