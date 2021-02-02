using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //元组
            List<(x, int)> statuses = new List<(x, int)>();
            var xx = statuses.FirstOrDefault(t => t.Item1 == x.d);
            List<(x?, int)> statusess = new List<(x?, int)>();
            var xxx = statusess.FirstOrDefault(t => t.Item1 == x.d);

            Func<string, int> a = new Func<string, int>(Test2);

            Console.WriteLine("Hello World!");
        }

        static int Test2(string str)
        {
            return 100;
        }
    }
    public enum x
    {
        a,
        b,
        c,
        d

    }



}
