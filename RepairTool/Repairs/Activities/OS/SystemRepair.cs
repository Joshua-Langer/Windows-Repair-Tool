

using RepairTool.Repairs.Activities.Global;

namespace RepairTool.Repairs.Activities.OS
{
    public static class SystemRepair
    {
        public static void BeginRepair()
        {
            Prep.RunTasks();
            InternetFixes.WinsockReset();
            InternetFixes.RepairDNS();
            Cleanup.CleanupMsi();
            Cleanup.RepairFileExtensions();
            Cleanup.DiskCheck();
            GlobalTaskRunner.Run();
        }
    }
}
