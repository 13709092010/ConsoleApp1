using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //元组
            //List<(x, int)> statuses = new List<(x, int)>();
            //var xx = statuses.FirstOrDefault(t => t.Item1 == x.d);
            //List<(x?, int)> statusess = new List<(x?, int)>();
            //var xxx = statusess.FirstOrDefault(t => t.Item1 == x.d);

            //Func<string, int> a = new Func<string, int>(Test2);

            //Console.WriteLine("Hello World!");
            //MyDerivedClass myDerivedClass1 = new MyDerivedClass();
            //MyDerivedClass myDerivedClass2 = new MyDerivedClass(3);
            //MyDerivedClass myDerivedClass3 = new MyDerivedClass(1, 3);
            var testasync = new testasync();
            await testasync.Test3();
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

    public class MyBaseClass
    {
        public MyBaseClass()
        {
            Console.WriteLine("MyBaseClass()");
        }

        public MyBaseClass(int i)
        {
            Console.WriteLine("MyBaseClass(int i)");
        }
    }

    public class MyDerivedClass : MyBaseClass
    {
        public MyDerivedClass()
        {
            Console.WriteLine("MyDerivedClass()");
        }
        public MyDerivedClass(int i) : base(i)
        {
            Console.WriteLine("MyDerivedClass(int i)");
        }

        public MyDerivedClass(int i, int j) : base(i)
        {
            Console.WriteLine("MyDerivedClass(int i,int j)");
        }
    }

    public class testasync
    {
        public async Task Test3()
        {
            var a1 =  test4();
            System.Console.WriteLine("test4:" + a1);
            var a2 = await test5();
            System.Console.WriteLine("test5:" + a2);
            System.Console.WriteLine($"test3:线程id:{ Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：11111");
            var t = 0;
            var t1 = 00;
            var t3 = 000;
            var t4 = 000;
            var t5 = 000;
            var t6 = 000;
            var t7 = 000;
            var t8 = 000;
            var t9 = 000;
            var t10 = 000;
            var t11 = 000;
            var t12 = 000;
            var t13 = 000;
            var t14 = 000;
            var t15 = 000;
            var t16 = 000;
            var t17 = 000;
        }

        public async Task<string> test4()
        {
            //System.Console.WriteLine($"test4:线程id:{Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：22222-1");
            //Int64 x = 0;
            //for (Int64 i = 0; i < 9000000000000000000; i++)
            //{
            //    //Task.Delay(100);
            //    x = i;
            //}
            //return $"线程id:{Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：22222-2----------{x}";

            return await Task.Run(() =>
            {
                System.Console.WriteLine($"test4:线程id:{Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：22222-1");
                var x = 0;
                for (int i = 0; i < 100000000; i++)
                {
                    Task.Delay(100);
                    x = i;
                }
                return $"线程id:{Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：22222-2----------{x}";

            });
        }

        public async Task<string> test5()
        {
            return await Task.Run(() => { System.Console.WriteLine($"test5:线程id:{Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：33333-1"); return $"线程id:{Thread.CurrentThread.ManagedThreadId.ToString()},线程内容：33333-2"; });
        }
    }
}
