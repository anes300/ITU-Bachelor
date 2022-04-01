using System;
using System.Net;
using System.Net.Sockets;

namespace NodeEngine.Utils
{
    public static class IpUtils
    {
        public static string GetLocalIp()
        {
            // Get Local IP Address
            string nodeIp = default;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var localIp in host.AddressList)
            {
                if (localIp.AddressFamily == AddressFamily.InterNetwork && localIp.ToString() != "127.0.0.1")
                {
                    nodeIp = localIp.ToString();
                }
            }
            return nodeIp;
        }
    }
}