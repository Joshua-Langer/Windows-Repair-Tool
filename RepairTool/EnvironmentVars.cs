using System;
using System.IO;

namespace RepairTool
{
    public static class EnvironmentVars
    {
        // Boolean values to denote issues and debugging.
        public static bool ErrorsDetected = false;
        public static bool WarningsDetected = false;
        public static bool TargetMetro = false;
        public static bool Help = false;
        public static bool SkipCheckUpdate = false;
        public static bool DebugMode = false;
        public static bool RebootRequired = false;


        public static bool ApplicationOnServer = false;

        // Ints for space values
        public static int FreeSpaceAfter = 0;
        public static int FreeSpaceBefore = 0;
        public static int FreeSpaceSaved = 0;
        
        // Enums to denote data to the technician
        public static NetworkStatus NetworkStatus = NetworkStatus.UNDETECTED;
        public static InternetStatus InternetStatus = InternetStatus.UNDETECTED;
        
        // constants
        public const string SYSDRIVE = "C:\\";
        public const string WINDIR = "C:\\Windows\\";
        public const string WMIC = WINDIR + "System32\\wbem\\wmic.exe";
        public const string FIND = WINDIR + "System32\\find.exe";
        public const string FINDSTR = WINDIR + "System32\\findstr.exe";
        public const string REG = WINDIR + "System32\\reg.exe";
        public const string ROBOCOPY = "System32\\robocopy.exe";
        public const string HOST = "www.google.com";

        // Additional strings used throughout the program - shown as statics for file paths as the Current Directory may change
        public static string ROOTDIR = "\\" + IPADDR;
        public static string BINDIR = Directory.GetCurrentDirectory() + "\\wrtbin";
        public static string LOGDIR = BINDIR +"\\logs\\";
        public static string RAWLOGDIR = BINDIR + "";
        public static string RESDIR = BINDIR + "\\resources\\";
        public static string CONFDIR = BINDIR + "\\configurations\\";
        public static string LOGFILE = LOGDIR + "";
        public static string CONFFILE = CONFDIR + "companyconfiguration.ini";
        public static string ARCHIVE = BINDIR + "\\Archives\\";
        public static string COMPANYNAME = "";
        public static string IPADDR = "";
        public static string SETUPLOG = Directory.GetCurrentDirectory() + "\\setuplog\\";
        public static string SYSTEMLOGS = BINDIR + "\\SystemLogs\\";
        public static string APPVERSION = "v.0.3.1 Build 157 - " + "Test";

        // Exit Codes
        public const int NORMALEXITCODE = 0;
        public const int INITIALSETUPCOMPLETE = 1;
        public const int SETUPFAILEDTOCREATEMAINDIRS = 14;
        public const int SETUPFAILEDTOCREATERESDIRS = 15;
        public const int SETUPFAILEDTOINSTALLRESOURCE = 16;
        public const int SETUPFAILEDTOGETIPADDR = 17;
        public const int SETUPFAILEDTOCREATECONF = 18;
        public const int SERVEROFFLINE = 404;
        

        // Working directories for each phase of repair
        public static string WINREP = RESDIR + "WindowsRepair\\";
        public static string WINMAL = RESDIR + "MalwareScans\\";
        public static string INITSETUP = RESDIR + "InitialSetup\\";
        public static string GLOBALREP = RESDIR + "GlobalRepairs\\";
    }
}