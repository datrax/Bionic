using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SimpleWEBServer
{
    class CompilerAndAssemblyManager

    {
        class LoadMyAssembly : MarshalByRefObject
        {
            private Assembly _assembly;
            System.Type MyType = null;
            object inst = null;
            public override object InitializeLifetimeService()
            {
                return null;
            }
            public void LoadAssembly(string path)
            {
                _assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
            }
            public object ExecuteMethod(string nameSpace, string className, string methodName, params object[] parameters)
            {
                string answer = "";
                object obj = _assembly.CreateInstance(nameSpace + "." + className);
                Type type = _assembly.GetType(nameSpace + "." + className);
                MethodInfo method = type.GetMethod(methodName);
                answer = method.Invoke(obj, parameters).ToString();

                return answer;
            }
        }
        /// <summary>
        /// Executes method from chosen assembly in new domain and then uload domain
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nameSpace"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [STAThread]
        public static string ExecuteMethod(string path, string nameSpace, string className, string methodName, params object[] parameters)
        {
            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            AppDomain newDomain = AppDomain.CreateDomain("newDomain", AppDomain.CurrentDomain.Evidence, setup); //Create an instance of loader class in new appdomain
            System.Runtime.Remoting.ObjectHandle obj = newDomain.CreateInstance(typeof(LoadMyAssembly).Assembly.FullName, typeof(LoadMyAssembly).FullName);

            LoadMyAssembly loader = (LoadMyAssembly)obj.Unwrap();//As the object we are creating is from another appdomain hence we will get that object in wrapped format and hence in next step we have unwrappped it

            //Call loadassembly method so that the assembly will be loaded into the new appdomain amd the object will also remain in new appdomain only.
            loader.LoadAssembly(path);

            //Call exceuteMethod and pass the name of the method from assembly and the parameters.
            string methodReply = loader.ExecuteMethod(nameSpace, className, methodName, parameters).ToString();

            AppDomain.Unload(newDomain);
            return methodReply;

        }


        /// <summary>
        /// Compiles cs file
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        public static string CompileDll(String sourceName)
        {
            FileInfo sourceFile = new FileInfo(sourceName);
            CodeDomProvider provider = null;
            bool compileOk = false;

            // Select the code provider based on the input file extension.
            if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS")
            {
                provider = CodeDomProvider.CreateProvider("CSharp");
            }
            else if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".VB")
            {
                provider = CodeDomProvider.CreateProvider("VisualBasic");
            }
            else
            {
                Console.WriteLine("Source file must have a .cs or .vb extension");
            }
            string name = "";
            if (provider != null)
            {

                // Format the executable file name.
                // Build the output assembly path using the current directory
                // and <source>_cs.exe or <source>_vb.exe.

                String exeName = String.Format(@"{0}\{1}.dll",
                    System.Environment.CurrentDirectory,
                    sourceFile.Name.Replace(".", "_"));
                name = exeName;
                CompilerParameters cp = new CompilerParameters();

                // Generate an executable instead of 
                // a class library.
                cp.GenerateExecutable = false;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = exeName;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                    sourceName);

                if (cr.Errors.Count > 0)
                {
                    // Display compilation errors.
                    Console.WriteLine("Errors building {0} into {1}",
                        sourceName, cr.PathToAssembly);
                    foreach (CompilerError ce in cr.Errors)
                    {
                        Console.WriteLine("  {0}", ce.ToString());
                        Console.WriteLine();
                    }
                }
                else
                {
                    // Display a successful compilation message.
                    return name;
                }

                // Return the results of the compilation.

            }
            return "fatal error";
        }
    }

}

