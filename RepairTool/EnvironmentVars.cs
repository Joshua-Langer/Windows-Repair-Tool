using System;
using System.IO;

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
        public static InternetStatus InternetStatus = InternetStatus.UNDETECTED;
        
        // constants for system directories
        public const string SYSDRIVE = "C:\\";
        public const string WINDIR = "C:\\Windows\\";
        public const string WMIC = WINDIR + "System32\\wbem\\wmic.exe";
        public const string FIND = WINDIR + "System32\\find.exe";
        public const string FINDSTR = WINDIR + "System32\\findstr.exe";
        public const string REG = WINDIR + "System32\\reg.exe";
        public const string ROBOCOPY = "System32\\robocopy.exe";
        
        // Additional constants used throughout the program - shown as statics for file paths as the Current Directory may change
        public static string BINDIR = Directory.GetCurrentDirectory();
        public static string LOGDIR = BINDIR +"\\logs\\";
        public static string RESDIR = BINDIR + "\\resources\\";
        public static string LOGFILE = LOGDIR + "work.log";
        public static string CONFFILE = RESDIR + "conf.ini";
        public const string HOST = "www.google.com";
        public const string NETWORK = "192.168.254.83";
        public const string NETWORKTEST = "10.0.10.7";
        public static string ROBOCOPYARGS = "\\" + NETWORK + "\\Tools\\Tron\\tron\\resources\\" + BINDIR + "/MIR";
        public const string APPVERSION = "v.0.1.9 Build 75";

        // Exit Codes
        public const int NORMALEXITCODE = 0;
        

        // Working directories for each phase of repair
        public static string STAGE0 = RESDIR + "Prep\\";
        public static string STAGE1 = RESDIR + "TempClean\\";
        public static string STAGE2 = RESDIR + "Debloat\\";
        public static string STAGE3 = RESDIR + "Disinfect\\";
        public static string STAGE4 = RESDIR + "OSRepair\\";
        public static string STAGE5 = RESDIR + "OSPatch\\";
        public static string STAGE6 = RESDIR + "Optimize\\";
        public static string STAGE7 = RESDIR + "WrapUp\\";


    }
}