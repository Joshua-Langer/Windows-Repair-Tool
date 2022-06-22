using System;
using MadMilkman.Ini;

namespace RepairTool.Core
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
                    if (sectionName == "Company")
                    {
                        switch (keyName)
                        {
                            case "Company Name":
                                EnvironmentVars.COMPANYNAME = keyValue;
                                break;
                        }
                    }
                    else if (sectionName == "Resource")
                    {
                        switch (keyName)
                        {
                            case "Windows Repair":
                                EnvironmentVars.WINREP = keyValue;
                                break;
                            case "Malware Repair":
                                EnvironmentVars.WINMAL = keyValue;
                                break;
                            case "Initial Setup":
                                EnvironmentVars.INITSETUP = keyValue;
                                break;
                            case "Global Repair":
                                EnvironmentVars.GLOBALREP = keyValue;
                                break;
                        }
                    }
                    else if (sectionName == "Network")
                    {
                        switch (keyName)
                        {
                            case "IP Address":
                                EnvironmentVars.IPADDR = keyValue;
                                break;
                        }
                    }
                }
            }
        }
    }
}