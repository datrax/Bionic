using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionic_2
{
    class Program
    {
        public class Officedock
        {
            String name;
            Double size;

            public Officedock()
            {
                Console.Write("Write the file name:");
                name = Console.ReadLine();
                Console.Write("Write the file size:");
                bool repeat;
                do
                {
                    repeat = false;
                    if (!Double.TryParse(Console.ReadLine(), out size))
                    {
                        repeat = true;
                        Console.WriteLine("It's not a number. Try again");
                    };
                }
                while (repeat);
                Console.Write("Write the measuruments for your type (b,kb,mb,gb,tb):");

                do
                {
                    repeat = false;
                    string format = Console.ReadLine();
                    if (format == "b") size /= 1024 * 1024;
                    else if (format == "kb") size /= 1024;
                    else if (format == "gb") size *= 1024;
                    else if (format == "tb") size *= 1024*1024;
                    else if(format!="mb")
                    {
                        repeat = true;
                        Console.WriteLine("Invalid measurument. Try again");
                    };
                }
                while (repeat);
            }
            public void get_all_inf()
            {
                Console.WriteLine();
                Console.WriteLine("Information about the file:");
                Console.WriteLine("file name:" + name);
                Console.WriteLine("file size in megabytes:" + size);
            }
        }
        public class XLS : Officedock
        {
            const string type = "XLS";
            Int32 amount_of_tables;
            public XLS()
            {
                Console.Write("Write the amount of tables:");
                bool repeat;
                do
                {
                    repeat = false;
                    if (!Int32.TryParse(Console.ReadLine(), out amount_of_tables))
                    {
                        repeat = true;
                        Console.WriteLine("It's not a number. Try again");
                    };
                }
                while (repeat);
            }
            public void get_all_inf()
            {
                base.get_all_inf();
                Console.WriteLine("tables amount:" + amount_of_tables);
                Console.WriteLine("File type:" + type);
            }
        }
        public class WORD : Officedock
        {
            const string type = "Word";
            Int32 amount_of_strings;
            public WORD()
            {
                Console.Write("Write the amount of strings:");
                bool repeat;
                do
                {
                    repeat = false;
                    if (!Int32.TryParse(Console.ReadLine(), out amount_of_strings))
                    {
                        repeat = true;
                        Console.WriteLine("It's not a number. Try again");
                    };
                }
                while (repeat);

            }
            public void get_all_inf()
            {
                base.get_all_inf();
                Console.WriteLine("Strings amount:" + amount_of_strings);
                Console.WriteLine("File type:" + type);
            }

        }
        public class PPT : Officedock
        {
            const string type = "PPT";
            Int32 amount_of_pages;
            public PPT()
            {
                Console.Write("Write the amount of pages:");
                bool repeat;
                do
                {
                    repeat = false;
                    if (!Int32.TryParse(Console.ReadLine(), out amount_of_pages))
                    {
                        repeat = true;
                        Console.WriteLine("It's not a number. Try again");
                    };
                }
                while (repeat);
            }
            public void get_all_inf()
            {
                base.get_all_inf();
                Console.WriteLine("pages amount:" + amount_of_pages);
                Console.WriteLine("File type:" + type);
            }
        }
        static void Main(string[] args)
        {
            bool repeat;
            do
            {
                repeat = false;
                Console.Write("Write format of the file you want to create:");
                string format = Console.ReadLine();
                if (format.Contains("PPT"))
                {
                    PPT t = new PPT();
                    t.get_all_inf();
                }
                else if (format.Contains("WORD"))
                {
                    WORD t = new WORD();
                    t.get_all_inf();
                }
                else if (format.Contains("XLS"))
                {
                    XLS t = new XLS();
                    t.get_all_inf();
                }
                else
                {
                    repeat = true;
                    Console.WriteLine("Invalid type. PPT,WORD,XLS - expected");
                };
            }
            while (repeat);
            Console.ReadLine();
        }
    }
}
