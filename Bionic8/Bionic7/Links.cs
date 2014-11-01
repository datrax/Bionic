using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bionic7
{
   public static class Links
    {
       
       public static void CalculateAndDisplayLinks(List<string> files)
       {
           Dictionary<string, int> dic = new Dictionary<string, int>();
           foreach (string fil in files)
           {
               Console.WriteLine("file detected: " + fil.Substring(fil.LastIndexOf("\\") + 1));
               StreamReader re = new StreamReader(fil);
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
       }
    }
}
