using System;

namespace RepairTool
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Windows Repair Tool " + EnvironmentVars.APPVERSION + " running on " + Systems.WindowsVersionDetection();
            Initializer.StartScreen();
            Initializer.Initialize();
        }
    }
}