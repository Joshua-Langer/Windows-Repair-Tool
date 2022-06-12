namespace RepairTool
{
    public static class EnvironmentVars
    {
        // Boolean values to denote possible issues
        public static bool ErrorsDetected = false;
        public static bool WarningsDetected = false;
        public static bool TargetMetro = false;
        public static bool Help = false;
        public static bool SkipCheckUpdate = false;
        
        // Boolean values for stage completion
        public static bool InitializeCompleted = false;

        // Ints for space values
        public static int FreeSpaceAfter = 0;
        public static int FreeSpaceBefore = 0;
        public static int FreeSpaceSaved = 0;
        
        // Enums to denote data to the technician
        public static NetworkStatus NetworkStatus = NetworkStatus.UNDETECTED;
        
        // constants for system directories
        public const string WMIC = "C:\\Windows\\System32\\wbem\\wmic.exe";
        public const string FIND = "C:\\Windows\\System32\\find.exe";
        public const string FINDSTR = "C:\\Windows\\System32\\findstr.exe";
        public const string REG = "C:\\Windows\\System32\\reg.exe";
        
        // Additional constants used throughout the program
        public const string BINDIR = "C:\\techbin\\";
        public const string LOGDIR = BINDIR +"logs\\";
        public const string RESDIR = BINDIR + "resources\\";
        public const string LOGFILE = LOGDIR + "work.log";
        public const string CONFFILE = RESDIR + "conf.ini";
        public const string HOST = "www.google.com";
        public const string NETWORK = "192.168.254.83";
        public const string NETWORKTEST = "10.0.10.7";
        

    }
}