using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Bionic7
{
    class Program
    {
        delegate List<string> FileAction(string path);
        delegate void Action(List<string> a);
        static void Main(string[] args)
        {

            FileAction files = Files.GetFromDirectory;
            Action links = Links.CalculateAndDisplayLinks;
            links.Invoke(files.Invoke("htmls"));
            Console.ReadLine();
            
        }
    }
}
