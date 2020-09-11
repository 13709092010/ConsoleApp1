using ConsoleApp1.Helper;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //637267722491323654
            //637267723141871912
            //637267723407956658
            var sj = DateTime.Now.ToLongDateString();
            var sj1 = DateTime.Now.ToLongTimeString();
            var sj2 = DateTime.Now.ToString("yyyyMMddHHmmss");

            var xx = RandomHelper.GenerateRandomCode(13);

            for (int i = 0; i < 10000; i++)
            {
               
                Console.WriteLine(OrderHelper.GenerateNo());
            }

            if (!Regex.Match("12345678", @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,30}$").Success)
            {

            }
            if (!Regex.Match("12345x78", @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,30}$").Success)
            {

            }
            if (!Regex.Match("12345X78", @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,30}$").Success)
            {

            }
            if (!Regex.Match("123xx8", @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,30}$").Success)
            {

            }
            if (!Regex.Match("1234城567", @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,30}$").Success)
            {

            }
            if (!Regex.Match("￥#12356s", @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,30}$").Success)
            {

            }


            Console.WriteLine("Hello World!");
        }
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        ThreadStart num = new ThreadStart(PrintNum);
    //        Thread ConstrolNum = new Thread(num);
    //        ThreadStart str = new ThreadStart(PrintStr);
    //        Thread ConstrolStr = new Thread(str);
    //        Stopwatch watch = new Stopwatch();
    //        watch.Start();
    //        ConstrolNum.Start();
    //        ConstrolStr.Start();
    //        while (true)
    //        {
    //            if (ConstrolNum.ThreadState == System.Threading.ThreadState.Stopped && ConstrolStr.ThreadState == System.Threading.ThreadState.Stopped)
    //            {
    //                watch.Stop();
    //                Console.WriteLine(watch.Elapsed.TotalMilliseconds);
    //                break;
    //            }
    //        }
    //        Console.ReadKey();
    //    }
    //    private static void PrintNum()
    //    {
    //        for (int i = 1; i < 1000; i++)
    //        {
    //            Console.WriteLine(i);
    //        }
    //    }
    //    private static void PrintStr()
    //    {
    //        for (int i = 1; i < 1000; i++)
    //        {
    //            Console.WriteLine("当前数为:{0}", i);
    //        }
    //    }
    //}

}
