using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebBrowserTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Write("Your url:");
                try
                {
                    string url = Console.ReadLine();
                    string text;
                    do
                    {
                        text = TCPSender.GetPage(url);
                        var location = Parser.GetLocation(text);
                        if (location != "")
                        {
                            Console.WriteLine("Redirecting to " + location);
                            //if nothing changes it means that http changed to httpS
                            if(url==location)
                                throw new Exception("SSl protocol is not supported in this version, sorry :(");
                            url = location;
                        }                       
                    } while (Parser.GetCode(text)!=200);
                    Console.WriteLine("Content-length: "+Parser.GetLength(text));
                    Console.WriteLine("Title: "+Parser.GetTitle(text));
                    Console.WriteLine("Code: "+Parser.GetCode(text));
                    Console.WriteLine(text);
                    Console.WriteLine("Any key to continue, esc to quit");                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Any key to continue, esc to quit");
                }               
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
