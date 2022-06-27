using RepairTool.Repairs.Activities.Global;

namespace RepairTool.Repairs.Activities.Setup
{
    public static class SystemPrep
    {
        public static void BeginPrep()
        {
            EnvironmentVars.WarningsDetected = false;
            Installer.GoogleChrome();
            EnvironmentVars.WarningsDetected = false;
            Installer.InstallAdobe();
            EnvironmentVars.WarningsDetected = false;
            GlobalTaskRunner.Run();
        }
    }
}
