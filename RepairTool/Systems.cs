using System;
using System.IO;
using System.Net.NetworkInformation;

namespace RepairTool
{
    public static class Systems
    {
        /// <summary>
        /// Gets the current Date Time on the system when called
        /// </summary>
        /// <returns></returns>
        public static string CurrentDateTime()
        {
            return DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
        }
        
        /// <summary>
        /// Detect the version of Windows and returns it as a string when called.
        /// </summary>
        /// <returns></returns>
        public static string WindowsVersionDetection()
        {
            return Environment.OSVersion.VersionString;
        }

        /// <summary>
        /// Returns the language of the current system
        /// </summary>
        /// <returns></returns>
        public static string SystemLanguage()
        {
            return global::System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        /// <summary>
        /// Checks the current Network Status.
        /// </summary>
        /// <returns></returns>
        public static NetworkStatus InternetAccess()
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(EnvironmentVars.HOST, 3000);
                if (reply.Status == IPStatus.Success)
                {
                    return NetworkStatus.ONLINE;
                }
            }
            catch
            {
                return NetworkStatus.OFFLINE;
            }

            return NetworkStatus.UNDETECTED;
        }
    }
}