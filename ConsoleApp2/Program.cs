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


            Console.WriteLine("Hello World!");
        }

    }
    public enum x { 
    a,
    b,
    c,
    d
    
    }
}
