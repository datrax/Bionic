using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first_dll_proj
{

    public class Class1 :Interface.IMyInterface
    {
        public void display()
        {
            System.Console.WriteLine("Hello from the first class (ANOTHER DLL)");
        }
    }
    public class Some :Interface.IMyInterface
    {
        public void display()
        {
            System.Console.WriteLine("Hello from the second class (ANOTHER DLL)");      
        }
    }
}
