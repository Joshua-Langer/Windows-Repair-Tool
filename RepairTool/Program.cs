using RepairTool.Core;
using System;

namespace RepairTool
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = EnvironmentVars.COMPANYNAME + " Maintenance Tool " + EnvironmentVars.APPVERSION;
            SystemCheck.Initialize();
        }
    }
}