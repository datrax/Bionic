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

        static void Main(string[] args)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            if (!Directory.Exists("htmls"))
            {
                Console.WriteLine("Directory \"htmls\" hasn't been found");
                return;
            }
            string[] filetype = ConfigurationSettings.AppSettings["filetype"].Split(';');
            string[] dirs = new string[0];
            foreach (string type in filetype)
            {
                string[] di = (Directory.GetFiles(Environment.CurrentDirectory + "\\htmls\\", type));
                dirs = dirs.Concat(di).Distinct().ToArray();
            }



            foreach (string dir in dirs)
            {
                Console.WriteLine("file detected: " + dir.Substring(dir.LastIndexOf("\\") + 1));
                StreamReader re = new StreamReader(dir);
                RegexOptions options = System.Text.RegularExpressions.RegexOptions.Singleline;
                MatchCollection links = Regex.Matches(re.ReadToEnd().ToString(), "(?<=href=\")(.*?)(?=\")", options);//regular expression that returns link             

                foreach (Match link in links)
                {
                    string linkstring = Regex.Replace(link.ToString(), "\r\n", String.Empty);//delete all end of lines in the link
                    if (dic.ContainsKey(linkstring))
                        dic[linkstring]++;
                    else dic.Add(linkstring, 1);
                }
                re.Close();
            }
            foreach (KeyValuePair<string, int> kvp in dic)
            {
                Console.WriteLine(kvp.Key + " found " + kvp.Value + " times");
            }
            Console.ReadLine();
        }
    }
}