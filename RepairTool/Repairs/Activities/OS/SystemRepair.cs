

using RepairTool.Repairs.Activities.Global;

namespace RepairTool.Repairs.Activities.OS
{
    public static class SystemRepair
    {
        public static void BeginRepair()
        {
            EnvironmentVars.WarningsDetected = false;
            InternetFixes.WinsockReset();
            EnvironmentVars.WarningsDetected = false;
            InternetFixes.RepairDNS();
            EnvironmentVars.WarningsDetected = false;
            Cleanup.CleanupMsi();
            EnvironmentVars.WarningsDetected = false;
            Cleanup.RepairFileExtensions();
            EnvironmentVars.WarningsDetected = false;
            Cleanup.DiskCheck();
            EnvironmentVars.WarningsDetected = false;
            GlobalTaskRunner.Run();
        }
    }
}
