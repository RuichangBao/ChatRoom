using Server;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Proto;
using System.Threading.Channels;
using System.Reflection;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetPublicIP());
        }
        //获取本地ip
        private string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1"; // 默认回环地址
        }
        //获取公网ip
        private static readonly string[] IpApiServices =
    {
        "https://api.ipify.org",                    // 最稳定
        "https://api64.ipify.org",                  // IPv6优先
        "https://icanhazip.com",                    // 简洁
        "https://ipinfo.io/ip",                     // 带更多信息
        "http://checkip.amazonaws.com",             // AWS服务
        "https://ifconfig.me/ip",                   // 国外
        "https://ip.seeip.org",                     // 国内访问快
        "https://myexternalip.com/raw",             // 备用
        "http://ip.3322.net",                       // 国内服务
        "https://4.ipw.cn",                         // 纯IPv4
    };
        /// <summary>
        /// 获取公网IP（同步方法）
        /// </summary>
        public static string GetPublicIP()
        {
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("User-Agent", "Mozilla/5.0");

                // 尝试多个服务，直到成功
                foreach (var api in IpApiServices)
                {
                    try
                    {
                        string ip = client.DownloadString(api).Trim();

                        // 验证IP格式
                        if (IsValidIP(ip))
                        {
                            Console.WriteLine($"使用 {api} 获取到IP: {ip}");
                            return ip;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"API {api} 失败: {ex.Message}");
                        continue;
                    }
                }

                throw new Exception("所有IP查询服务都失败了");
            }
        }

        /// <summary>
        /// 验证IP地址格式
        /// </summary>
        private static bool IsValidIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return false;

            // IPv4验证
            if (IPAddress.TryParse(ip, out IPAddress address))
            {
                // 排除私有IP和内网地址
                byte[] bytes = address.GetAddressBytes();

                // 检查是否是私有IP
                if (address.IsIPv6LinkLocal || address.IsIPv6SiteLocal)
                    return false;

                // IPv4私有地址范围
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    // 10.0.0.0/8, 172.16.0.0/12, 192.168.0.0/16
                    if (bytes[0] == 10) return false;
                    if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) return false;
                    if (bytes[0] == 192 && bytes[1] == 168) return false;
                    if (bytes[0] == 169 && bytes[1] == 254) return false; // 链路本地
                    if (bytes[0] == 127) return false; // 环回地址
                }

                return true;
            }

            return false;
        }
    }
}