using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
namespace Bionic_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] dirs = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");
            foreach (string dir in dirs)
            {
                Assembly assembly = Assembly.LoadFrom(dir);
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetInterface("Do") != null)
                    {
                        object instanceOfMyType = Activator.CreateInstance(type);
                        MethodInfo meth = type.GetMethod("display");
                        meth.Invoke(instanceOfMyType, null);
                    }
                }
            }
            System.Console.ReadLine();
        }
    }
}
