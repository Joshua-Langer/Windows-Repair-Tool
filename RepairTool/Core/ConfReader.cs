﻿using System;
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
                    else if (sectionName == "System")
                    {
                        switch (keyName)
                        {
                            case "Bin Directory":
                                EnvironmentVars.BINDIR = keyValue;
                                break;
                            case "Log Directory":
                                EnvironmentVars.LOGDIR = keyValue;
                                break;
                            case "Resources Directory":
                                EnvironmentVars.RESDIR = keyValue;
                                break;
                            case "Configuration Directory":
                                EnvironmentVars.CONFDIR = keyValue;
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
                }
            }
        }
    }
}