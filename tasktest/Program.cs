using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace tasktest
{
    public static class CallBackTask
    {
        static CallBackTask()
        {
            TaskList = new List<(string, string)>();
            Task.Run(() =>
            {
                RunTask();
            });
        }
        public static object _lock = new object();
        public static List<(string, string)> TaskList { get; set; }

        public static void AddTask(string url, string modelJson)
        {
            lock (_lock)
            {
                TaskList.Add((url, modelJson));
            }
        }
        public static void RunTask()
        {
            //var logRpc = IocUnityExtend.GetInterface<ILogContract>();

            while (true)
            {
                Thread.Sleep(1);
                var task = TaskList.FirstOrDefault();
                try
                {
                    if (!string.IsNullOrEmpty(task.Item1))
                    {
                        Send(task.Item1, task.Item2);
                        TaskList.Remove(task);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Thread.Sleep(10000);
                    }
                }
                catch (Exception ex)
                {
                    //logRpc.AddLogContract(LogTypeEnum.DevloperCallBack, "回调任务执行异常,异常信息：" + ex.Message, task.Item2, "", task.Item1).GetAwaiter().GetResult();
                    TaskList.Remove(task);
                }
            }
        }

        static HttpStatusCode Send(string url, string modelJson)
        {
            //^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$
            if (!Regex.IsMatch(url, @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$"))
            {
                //log.AddLogContract("", "回调地址有误", modelJson, "", url).GetAwaiter().GetResult();
                return HttpStatusCode.NotFound;
            }
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMilliseconds(1000 * 10);//10秒

            StringContent str = new StringContent(modelJson, Encoding.UTF8, "application/json");
            //str.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
                var result = httpClient.PostAsync(url, str).Result;
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    //log.AddLogContract("", "请求失败/超时", modelJson, "", url).GetAwaiter().GetResult();
                }
                return result.StatusCode;
            }
            catch (Exception ex)
            {
                //log.AddLogContract("", "回调任务执行异常,异常信息：" + ex.Message, modelJson, "", url).GetAwaiter().GetResult();
                return HttpStatusCode.InternalServerError;
            }


        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
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
