using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleWEBServer
{
    class BIISExtension
    {
        string SourceCode;
        string Parametrs;
        string PathToFile;
        Dictionary<string, string> paramDictionary = new Dictionary<string, string>();

        public BIISExtension(string sourceCode, string parametrs, string pathToFile)
        {
            SourceCode = sourceCode;
            Parametrs = parametrs;
            ParametrsToDictionary();
            PathToFile = pathToFile;
        }

        private string _code = "";
        public string GetUpdatedHTMLFilePath()
        {

            BIISExpParser.ReplacementCounter = 0;
            List<string[]> labels =
            BIISExpParser.GetAndRemoveLabels(ref SourceCode);
            List<string[]> buttons =
            BIISExpParser.GetAndRemoveButtons(ref SourceCode);

            File.WriteAllText("GeneratedCode.cs", "");

            var reader = new StreamReader("../../BIISObjects.cs");
            bool canRead = true;
            while (canRead)
            {
                var line = reader.ReadLine();
                _code += line + Environment.NewLine;
                if (line.IndexOf("List<IComponents> list = new List<IComponents>()") > 0)
                {
                    canRead = false;
                }
            }
            reader.Close();

            _code += @"public string Method()
{";
            //Generates code for labels
            foreach (var label in labels)
            {
                _code += string.Format("Label {0}=new Label(\"{0}\",\"{1}\");\n", label[0], label[1]);
                _code += string.Format("list.Add({0});", label[0]);
            }

            string onClickMethodName = "";
            //Generates code for buttons
            foreach (var button in buttons)
            {
                if (paramDictionary.ContainsKey(button[0]))
                    onClickMethodName = button[2];
                _code += string.Format("Button {0}=new Button(\"{0}\",\"{1}\");\n", button[0], button[1]);
                _code += string.Format("list.Add({0});", button[0]);
            }
            if (onClickMethodName != "")
            {
                _code +=
                     GetMethodBodyFromHandlerFile(PathToFile, onClickMethodName);
            }

            _code += " string answer=\"\";" +
            @"foreach (var record in list)
            {
                answer+=(record.ReturnHTMLCode())+Environment.NewLine;
            }
            return answer;
        }
    }
}";
            File.WriteAllText("GeneratedCode.cs", _code);

            var pathToCSFile = CompilerAndAssemblyManager.CompileDll("./GeneratedCode.cs");
            string response = CompilerAndAssemblyManager.ExecuteMethod(pathToCSFile, "SimpleWEBServer", "ServiceClass", "Method", null);

            //Replace bis with html markup
            var stringsForReplacement = response.Split(new char[] { '\n' });

            for (int i = 0; i < stringsForReplacement.Length; i++)
            {
                SourceCode = SourceCode.Replace("{" + i + "}", stringsForReplacement[i]);
            }

            return MakeHTMLOutput();
        }

        public string MakeHTMLOutput()
        {
            File.WriteAllText("./output.html", SourceCode);
            return "./output.html";
        }

        public void ParametrsToDictionary()
        {
            if (Parametrs == "?" || Parametrs == "" || Parametrs==null) return;
            var parMas = Parametrs.Split('&');
            foreach (var VARIABLE in parMas)
            {
                var i = VARIABLE.IndexOf("=");
                paramDictionary.Add(VARIABLE.Substring(0, i), VARIABLE.Substring(i));
            }
        }

        private string GetMethodBodyFromHandlerFile(string fileName, string methodName)
        {
            fileName = fileName.Replace(".html", ".biis");
            string method = "";
            using (StreamReader str = new StreamReader(fileName))
            {
                method = str.ReadToEnd();
            }
            var i = method.IndexOf(methodName);
            method = method.Substring(i);
            i = method.IndexOf("{");
            method = method.Substring(i + 1);
            i = method.IndexOf("}");
            method = method.Substring(0, i);
            return method;
        }
    }
}
