using System.IO;
using System.Runtime.InteropServices;

namespace RepairTool.Core
{
    public class CreateConf
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,string val,string filePath);
        public static void NewConfiguration(string sec, string name, string data)
        {
            WritePrivateProfileString(sec,name,data, EnvironmentVars.CONFFILE);
        }

        public static void UpdateConfiguration(string sec, string name, string data)
        {
            WritePrivateProfileString(sec, name, data, EnvironmentVars.CONFFILE);
        }
    }
}