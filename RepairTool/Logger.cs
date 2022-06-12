using System;
using System.IO;

namespace RepairTool
{
    public static class Logger
    {
        /// <summary>
        /// Write General Information to the log file
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="w"></param>
        public static void LogInfo(string logMessage, TextWriter w)
        {
            w.Write($"{Systems.CurrentDateTime()}");
            w.Write(" : ");
            w.Write($"{logMessage}\n");
            w.Flush();
            w.Close();
            Console.WriteLine($"{Systems.CurrentDateTime()} : {logMessage}");
        }
        
        /// <summary>
        /// Write Warnings to the log file
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="w"></param>
        public static void LogWarning(string logMessage, TextWriter w)
        {
            w.Write("WARN : ");
            w.Write($"{Systems.CurrentDateTime()}");
            w.Write(" : ");
            w.Write($"{logMessage}\n");
            w.Flush();
            Console.WriteLine($"WARN : {Systems.CurrentDateTime()} : {logMessage}");
        }
        
        /// <summary>
        /// Write Errors to the log file
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="w"></param>
        public static void LogError(string logMessage, TextWriter w)
        {
            w.Write("ERROR : ");
            w.Write($"{Systems.CurrentDateTime()}");
            w.Write(" : ");
            w.Write($"{logMessage}\n");
            w.Flush();
            Console.WriteLine($"ERROR : {Systems.CurrentDateTime()} : {logMessage}");
        }
    }
}