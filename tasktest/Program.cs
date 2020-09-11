using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tasktest
{
    class Program
    {


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            List<string> vs = new List<string>();
            //for (int i = 0; i < 10; i++)
            //{
            //    vs.Add(10023564100541 + i);
            //}
            for (int i = 0; i < 100000; i++)
            {
                //var x = xx.CardNo(vs);
                var x = xx.Code();
                vs.Add(x);
                Console.WriteLine(x);
            }
            // 创建文件。如果文件存在则覆盖
            FileStream fs = File.Open(@"d:\内容.txt", FileMode.Create);
            // 创建写入流
            StreamWriter wr = new StreamWriter(fs);
            // 将ArrayList中的每个项逐一写入文件
            for (int i = 0; i < vs.Count; i++)
            {
                wr.WriteLine(vs[i]);
            }
            // 关闭写入流
            wr.Flush();
            wr.Close();

            // 关闭文件
            fs.Close();

        }

        public class xx
        {
            private static readonly object Locker = new object();
            private static readonly object Locker1 = new object();
            private static int _snId = 0;
            public static long CardNo(List<long> cards)
            {
                lock (Locker)
                {
                    if (cards.Count != 0)
                    {
                        var max = cards.Max();
                        Random rd = new Random();
                        int rdNum1 = rd.Next(100, 1000);
                        int rdNum2 = rd.Next(100, 1000);
                        var maxStr = max.ToString();
                        maxStr = maxStr.ToString().Remove(4, 3);
                        maxStr = maxStr.Insert(4, rdNum1.ToString());
                        maxStr = maxStr.ToString().Remove(11, 3);
                        maxStr = maxStr.Insert(11, rdNum2.ToString());
                        var formatNum = Convert.ToInt64(maxStr);
                        if (cards.Contains(formatNum))
                        {
                            return max + 1;
                        }
                        return formatNum;
                    }
                    else
                    {
                        return 10012300000123;
                    }
                }
            }

            /// <summary>
            /// 生成单号
            /// </summary>
            /// <param name="pre">单号前缀</param>
            /// <returns></returns>
            public static string Code(string pre = "")
            {
                lock (Locker1)
                {
                    if (_snId == 999)
                    {
                        _snId = 0;
                    }
                    else
                    {
                        _snId++;
                    }
                    return pre + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + _snId.ToString().PadLeft(3, '0');
                }
            }

        }
        public class xxx : xx
        {
            public string x3 { get; set; }
        }
    }
}
