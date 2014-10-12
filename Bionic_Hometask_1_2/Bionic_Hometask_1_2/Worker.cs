using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionic_Hometask_1_2
{
    class Worker
    {
        int num;
        bool positive;
        List<int> mas = new List<int>(); 

        public Worker()
        {
            Console.Write("Task 2\nEnter your number: ");
            num = Convert.ToInt32(Console.ReadLine());
            if (num >= 0) positive = true;
            else positive = false;
        }

        private int fill_list()
        {
            int p = System.Math.Abs(num);
            while (p  > 0)
            {
                mas.Add(p % 10);
                p /= 10;
            }       
            return p%10;
        }
        private void change()
        {
            mas.Reverse();
            int t = mas[1];
            mas.RemoveAt(1);
            mas.Add(t);
            mas.Reverse();
        }
        private int join()
        {
            int k=0;
            for (int i = 0; i < mas.Count(); i++)
            {
                k += Convert.ToInt32(System.Math.Pow(10, i) *mas[i]);
            }
            return positive==true?k:-k;
        }
        public int perform()
        {
            fill_list();
            change();
            return join();
        }
    }
}
