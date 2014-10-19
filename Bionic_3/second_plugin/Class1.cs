using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace second_plugin
{
    public class Fsaf : Interface.IMyInterface
    {
        public void display()
        {
            System.Console.WriteLine("Hello from the second dll");
        }
    }
}
