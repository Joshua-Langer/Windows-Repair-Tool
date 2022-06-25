using RepairTool.Core;
using System;
using System.Diagnostics;
using System.IO;

namespace RepairTool.Repairs.Activities.OS
{
    public static class InternetFixes
    {
        public static void WinsockReset()
        {
	        var runFile = EnvironmentVars.WINDIR + "system32\\netsh.exe";
	        var repairType = "Repair";
	        var taskName = "Winsock Reset";
	        var arguments = "winsock reset";
	        ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments);
	        EnvironmentVars.RebootRequired = true;
        }

		public static void RepairDNS()
        {
	        var runFile = EnvironmentVars.WINDIR + "system32\\cmd.exe";
	        var repairType = "Repair";
	        var taskName = "DNS Repair";
	        var arguments = "ipconfig /flushdns && ipconfig /registerdns";
	        ProcessRunner.TaskRunner(repairType, taskName, runFile, arguments);
        }
    }
}
