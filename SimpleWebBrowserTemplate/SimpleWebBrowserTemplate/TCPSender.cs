using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebBrowserTemplate
{
    class TCPSender
    {
        public static string GetPage(string url)
        {
            TcpClient tcpClient = new TcpClient();
            var siteHost = url;           
            string fileName = "http://" + siteHost + "/";
            var slashPosition = url.IndexOf('/');
            if (slashPosition > 0)
            {
                siteHost = url.Substring(0, slashPosition);
                fileName = "http://" + url;
            }

            NetworkStream stream = null;
            StreamReader reader = null;
            try
            {
                var hostips = Dns.GetHostEntry(siteHost);
                var ip = hostips.AddressList[0];
                tcpClient.Connect(ip, 80);
                byte[] request = Encoding.UTF8.GetBytes(@"GET " + fileName + @" HTTP/1.1
Accept-Language: en-US
User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)
Accept: text/plain
Connection: close
Host: " + siteHost + "\r\n\r\n");
                stream = tcpClient.GetStream();
                stream.Write(request, 0, request.Length);
                stream.Flush();
                reader = new StreamReader(stream);
                string answer = reader.ReadToEnd();
                return (answer);
            }
            catch
            {
                throw new Exception("Wrong url");
            }

            finally
            {
                stream?.Close();
                reader?.Close();
            }
        }
    }
}
