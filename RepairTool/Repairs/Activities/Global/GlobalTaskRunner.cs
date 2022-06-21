namespace RepairTool.Repairs.Activities.Global
{
    public static class GlobalTaskRunner
    {
        public static void Run()
        {
            TempCleaner.RunTasks(false);
            SystemDebloat.RunTasks(false);
            SystemUpdater.CheckForUpdates();
            SystemWrapUp.SystemFileChecker();
            SystemWrapUp.ComponentStoreChecker();
            SystemWrapUp.SystemFileRepair();
            SystemWrapUp.ComponentStoreRepair();
            EmailLog.Send();
        }
    }
}