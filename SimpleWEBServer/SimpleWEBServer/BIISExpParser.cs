using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SimpleWEBServer
{
    class BIISExpParser
    {
        public static int ReplacementCounter = 0;
        /// <summary>
        /// return biis labels in specific format and replace them with {X}
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string[]> GetAndRemoveLabels(ref string source)
        {
            Regex reg = new Regex("<BIIS:Label(.*?)/>");
            List < string[]> answer = new List<string[]>();
            foreach (Match match in reg.Matches(source))
            {
                answer.Add(new[] { GetName(match.Value),GetValue(match.Value)});
                source=source.Replace(match.Value, "{"+ReplacementCounter++ +"}");

            }
          
            return answer;
        }
        /// <summary>
        /// return biis buttons in specific format and replace them with {X}
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<string[]> GetAndRemoveButtons(ref string source)
        {
            Regex reg = new Regex("<BIIS:Button(.*?)/>");
            List<string[]> answer = new List<string[]>();
            foreach (Match match in reg.Matches(source))
            {
                answer.Add(new[] { GetName(match.Value), GetValue(match.Value), GetMethodName(match.Value) });
                source = source.Replace(match.Value, "{" + ReplacementCounter++ + "}");

            }
            return answer;
        }

        private static string GetName(string str)
        {
            Regex reg = new Regex("name=\"(.*?)\"");
            Match m = reg.Match(str);
            return m.Groups[1].ToString();
        }
        private static string GetValue(string str)
        {
            Regex reg = new Regex("value=\"(.*?)\"");
            Match m = reg.Match(str);
            return m.Groups[1].ToString();
        }
        private static string GetMethodName(string str)
        {
            Regex reg = new Regex("OnClick=\"(.*?)\"");
            Match m = reg.Match(str);
            return m.Groups[1].ToString();
        }
    }
}
