using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleWebBrowserTemplate
{
    class Parser
    {
        public static string GetLength(string source)
        {
            Regex reg = new Regex("\r\nContent-Length: (.*?)\r\n");
            Match m = reg.Match(source);
            return m.Groups[1].ToString();
        }
        public static string GetTitle(string source)
        {
            Regex reg = new Regex("<title>(.*?)</title>");
            Match m = reg.Match(source);
            return m.Groups[1].ToString();
        }
        public static int GetCode(string source)
        {
            Regex reg = new Regex("HTTP/(.*?)\r\n");
            Match m = reg.Match(source);
            var str = m.Groups[1].ToString().Substring(4,3);
            return int.Parse(str);

        }
        public static string GetLocation(string source)
        {
            
            Regex reg = new Regex("\r\nLocation: (.*?)\r\n");
            Match m = reg.Match(source);
            var str = m.Groups[1].ToString();
            if (str.Any())
            {
                if (str[str.Length - 1] == '/')
                    str = str.Substring(0, str.Length - 1);
                return str.Replace("http://", "").Replace("https://", "");
            }
            return "";
        }
    }
}
