using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace SimpleWEBServer
{
    class BIISServer
    {
        public int Port { get; set; }
        public string WebRoot { get; set; }
        public BIISServer(int port, string webroot)
        {
            Port = port;
            WebRoot = webroot;
        }

        public void Start()
        {
           
            Console.WriteLine("Server started on port "+Port);
            Console.WriteLine("Available web pages:");
            string[] files = Directory.GetFiles(WebRoot+"/","." ,SearchOption.AllDirectories);

            foreach (var t in files)
                Console.WriteLine(t.Substring(9).Replace("\\","/"));
            TcpListener listener = new TcpListener(Dns.Resolve("localhost").AddressList[0], Port);
            do
            {
                listener.Start();

                var socket=listener.AcceptSocket();
                var networkStream = new NetworkStream(socket);
                try
                {                  
                    byte[] bucket = new byte[4000];
                    int bytesRec = socket.Receive(bucket);
                    var source = Encoding.ASCII.GetString(bucket, 0, bytesRec);

                    var request=new RequestParser(source);

                    var fileUrl = request.FullUrl;

                    var fullPath = WebRoot + fileUrl;

                    ///BIIS EXTENSION calling
                    if (File.Exists(fullPath)&&MimeMapping.GetMimeMapping(fullPath) == "text/html")
                    {
                        using (StreamReader reader = new StreamReader(fullPath))
                        {
                            var code = reader.ReadToEnd();
                            var bIISExtension = new BIISExtension(code, request.Parametrs,fullPath);
                            var newPath=bIISExtension.GetUpdatedHTMLFilePath();
                            fullPath = Environment.CurrentDirectory + newPath;
                        }
                    }
                    ///
                
                    if (!File.Exists(fullPath))
                    {
                        string body = @"<html>
<h1>404 Page not found</h1>
</html>" + "\r\n";
                        var t = Encoding.ASCII.GetBytes(body).Length;
                        string responseHeaders = "HTTP/1.1 200 OK\r\n" +
                                                 "Server: The best one\r\n" +
                                                 "Content-Length: " + t + "\r\n" +
                                                 "Content-Type: text/html\r\n" +
                                                 "\r\n";
                        responseHeaders += body;
                        var headers = Encoding.ASCII.GetBytes(responseHeaders);
                        socket.Send(headers, headers.Length, SocketFlags.None);
                    }
                    else
                    {
                        string responseHeaders = "HTTP/1.1 200 OK\r\n" +
                                                 "Server: The best one\r\n" +
                                                 "Content-Length: " + new FileInfo(fullPath).Length + "\r\n" +
                                                 "Content-Type: " + MimeMapping.GetMimeMapping(fullPath) + "\r\n" +
                                                 "\r\n";

                        var headers = Encoding.ASCII.GetBytes(responseHeaders);
                        socket.Send(headers, headers.Length, SocketFlags.None);
               
                        socket.SendFile(fullPath);
                    }                   
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}\r\nTrace: \r\n{1}", e.Message, e.StackTrace);
                }
                finally
                {
                    networkStream.Close();
                    socket.Close();
                }
            } while (true);
        }
    }
}
