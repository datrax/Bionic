using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionic_Hometask_1_2
{
    class Program
    {
        static int fuct(int a)
        {
            if (a == 0) return 1;
            else return a*fuct(a - 1);
        }
        static double pow(int a, int n)
        {
            double k=1;
            for (int i = 0; i < n; i++)
                k *= a;
            return k;
        }
        static double expression()
        {
            Console.Write("Task 1\nEnter a:");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n:");
            int n = Convert.ToInt32(Console.ReadLine());
            return fuct(a)+pow(a,n);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Result : "+expression());
            Worker number = new Worker();
            Console.WriteLine("Result: "+number.perform());
            Console.ReadLine();
        }
    }
}
