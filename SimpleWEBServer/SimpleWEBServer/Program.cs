using System;
using  System.Configuration;

namespace SimpleWEBServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var port = int.Parse(appSettings["Port"]);
                var path = appSettings["WebRoot"];
                BIISServer server = new BIISServer(port,path);
                server.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

    }
}
