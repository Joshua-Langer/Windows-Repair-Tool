using RepairTool.Core;
using System;
using System.IO;

namespace RepairTool.Admin.Activities
{
    public static class UpdateCompanyName
    {
        public static void NewName()
        {
            var oldName = EnvironmentVars.COMPANYNAME;
            Console.Clear();
            Console.WriteLine("Enter your company name: ");
            EnvironmentVars.COMPANYNAME = Console.ReadLine();
            CreateConf.UpdateConfiguration("Company", "Company Name", EnvironmentVars.COMPANYNAME);
            using (StreamWriter w = File.AppendText(EnvironmentVars.SYSTEMLOGS))
            {
                Logger.LogInfo("Company Name was updated from " + oldName + " to " + EnvironmentVars.COMPANYNAME, w);
            }
            AdminMaintenance.AdminMenu();
        }
    }
}
