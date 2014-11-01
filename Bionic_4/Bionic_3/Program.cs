using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Configuration;
namespace Bionic_3
{
    class Program
    {
        static public void Load_references(string path)
        {
            try
            {

                string[] dirs = Directory.GetFiles(path, "*.dll");
                foreach (string dir in dirs)
                {
                    Assembly assembly = Assembly.LoadFrom(dir);
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.GetInterface("IMyInterface") != null)
                        {
                            object instanceOfMyType = Activator.CreateInstance(type);
                            MethodInfo meth = type.GetMethod("display");
                            meth.Invoke(instanceOfMyType, null);
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
                Console.WriteLine("Interface wasn't found. Make sure you added it to this assembly.");
                throw;
            }
            catch
            {
                throw;
            }

        }
        static void Main(string[] args)
        {
            try
            {
                    switch (args[0])
                    {
                        case "-folder": Load_references(Environment.CurrentDirectory + "\\RefFolder\\"); break;
                        case "-config": Load_references(Environment.CurrentDirectory + ConfigurationManager.AppSettings["Path"]); break;
                        default: { Console.WriteLine("You have to pick either -config or -folder"); return; } break;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            System.Console.ReadLine();
        }
    }
}
