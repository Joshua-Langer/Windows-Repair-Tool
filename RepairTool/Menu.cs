using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool
{
    public static class Menu
    {
        public static void Start()
        {
            Console.Clear();
            Console.Title = "Windows Repair Tool - " + EnvironmentVars.APPVERSION;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("           Windows Repair Tool " + EnvironmentVars.APPVERSION);
            Console.WriteLine("Please enter a selection for your phase of repair");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("           1. Temp Clean");
            Console.WriteLine("           2. Debloat OS");
            Console.WriteLine("           3. Disinfect");
            Console.WriteLine("           4. OS Repairs");
            Console.WriteLine("           5. OS Patches");
            Console.WriteLine("           6. Optimize");
            Console.WriteLine("           7. Run All");
            Console.WriteLine("           8. Exit");
            var choice = ReadInt("Enter your phase: ");
            ChoiceEngine.ActionToTake(choice);
        }

        private static int ReadInt(string text)
        {
            Console.WriteLine(text);
            var line = Console.ReadLine();
            return int.Parse(line);
        }


    }
}
