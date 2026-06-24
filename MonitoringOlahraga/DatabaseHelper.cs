using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms; // Pastikan namespace ini ada untuk MessageBox

namespace MonitoringOlahraga
{
    public static class DatabaseHelper
    {
        // a. Function untuk mengambil IP Address Server (Sesuai modul)
        public static string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            try
            {
                var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting local IP address: " + ex.Message);
            }
            return localIP;
        }

        // b. Ubah string connection (Sesuai modul)
        public static string GetConnectionString()
        {
            

            string connectionString = $"Data Source={GetLocalIPAddress()}\\ARBYPANGESTU;Initial Catalog=DB_MonitoringOlahraga;Integrated Security=True;";
            return connectionString;
        }
    }
}
