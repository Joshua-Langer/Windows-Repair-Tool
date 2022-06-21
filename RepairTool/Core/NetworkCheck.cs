using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RepairTool.Core
{
    public static class NetworkCheck
    {
        public static bool Network()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            try
            {
                foreach(var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString() == EnvironmentVars.IPADDR;
                    }
                }
            } catch
            {
                
            }
            throw new Exception("Server is not online..." + " Exit Code: " + EnvironmentVars.SERVEROFFLINE);
        }

        public static string CurrentIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            try
            {
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch
            {
                
            }
            throw new Exception("No IP Detected..." + " Exit Code: " + EnvironmentVars.SERVEROFFLINE);
        }

        public static bool CheckInternet()
        {
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(EnvironmentVars.HOST, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }

            return false;
        }
    }
}
