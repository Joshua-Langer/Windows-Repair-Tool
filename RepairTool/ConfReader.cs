using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using MadMilkman.Ini;

namespace RepairTool
{
    public static class ConfReader
    {
        public static void ConfigRead(string confFile)
        {
            IniFile ini = new IniFile();
            ini.Load(confFile);

            foreach (IniSection sec in ini.Sections)
            {
                foreach (IniKey key in sec.Keys)
                {
                    string sectionName = sec.Name;
                    string keyName = key.Name;
                    string keyValue = key.Value;
                    if (sectionName == "Booleans")
                    {
                        switch (keyName)
                        {
                            case "Errors Detected":
                                EnvironmentVars.ErrorsDetected = Boolean.Parse(keyValue);
                                break;
                            case "Warnings Detected":
                                EnvironmentVars.WarningsDetected = Boolean.Parse(keyValue);
                                break;
                            case "TargetMetro":
                                EnvironmentVars.TargetMetro = Boolean.Parse(keyValue);
                                break;
                            case "Help":
                                EnvironmentVars.Help = Boolean.Parse(keyValue);
                                break;
                            case "Skip Check Updates":
                                EnvironmentVars.SkipCheckUpdate = Boolean.Parse(keyValue);
                                break;
                        }
                    }
                    else if (sectionName == "Storage")
                    {
                        switch (keyName)
                        {
                            case "Free Space After":
                                EnvironmentVars.FreeSpaceAfter = Int32.Parse(keyValue);
                                break;
                            case "Free Space Before":
                                EnvironmentVars.FreeSpaceBefore = Int32.Parse(keyValue);
                                break;
                            case "Free Space Saved":
                                EnvironmentVars.FreeSpaceSaved = Int32.Parse(keyValue);
                                break;
                        }
                    }
                    else if (sectionName == "Work State")
                    {
                        switch (keyName)
                        {
                            case "Initialize":
                                EnvironmentVars.InitializeCompleted = Boolean.Parse(keyValue);
                                break;
                            case "Prep":
                                EnvironmentVars.PrepCompleted = Boolean.Parse(keyValue);
                                break;
                            case "Temp":
                                EnvironmentVars.DisinfectCompleted = Boolean.Parse(keyValue);
                                break;
                        }
                    }
                }
            }
        }
    }
}