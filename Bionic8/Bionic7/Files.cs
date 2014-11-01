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
    class Files
    {
        public static List<string> GetFromDirectory(string folder_name)
        {

            if (!Directory.Exists("htmls"))
            {
                Console.WriteLine("Directory \"htmls\" hasn't been found");
                return null;
            }
            string[] filetype = ConfigurationSettings.AppSettings["filetype"].Split(';');
            string[] dirs = new string[0];
            foreach (string type in filetype)
            {
                string[] di = (Directory.GetFiles(Environment.CurrentDirectory + "\\htmls\\", type));
                    dirs = dirs.Concat(di).Distinct().ToArray();
            }
            List<string> g = new List<string>();
            foreach (string d in dirs)
            {
                g.Add(d);
            }
            return g;
  
        }
    }
}
