using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool
{
    public class UsageAgreement
    {
        public static void Agreement()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("************************DISCLAIMER*************************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine(" This program performs a variety of tasks in an automated ");
            Console.WriteLine("manner such as clearing temp files, cookies, update files");
            Console.WriteLine("etc...");
            Console.WriteLine("Many tools are ran from this such as MalwareBytes and ");
            Console.WriteLine("ccleaner, to resolve various issues with PCs.");
            Console.WriteLine("Make sure, if any data is requested for safe keeping,");
            Console.WriteLine("to do a data backup ahead of time.");
            Console.WriteLine("Input your name showing you read and understand this.");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("***********************************************************");
            System.Threading.Thread.Sleep(1500);
            string name = Console.ReadLine();
            Record(name);
        }

        private static void Record(string name)
        {
            using (StreamWriter w = File.AppendText(EnvironmentVars.LOGFILE))
            {
                Logger.LogInfo(name + " is running this repair job on " + Systems.CurrentDateTime(), w);
            }
        }


    }
}
