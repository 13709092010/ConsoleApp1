using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    namespace Helper
    {
        /// <summary>
        /// 单号生成帮助类
        /// 2019-10-15 12:10 by boxuming 创建
        /// </summary>
        public class OrderHelper
        {
            private static readonly object Locker = new object();
            private static int _sn = 0;

            /// <summary>
            /// 生成单号
            /// </summary>
            /// <param name="pre">单号前缀</param>
            /// <returns></returns>
            public static string GenerateNo(string pre = "")
            {
                lock (Locker)   //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
                {
                    if (_sn == 1000)
                    {
                        _sn = 0;
                    }
                    else
                    {
                        _sn++;
                    }
                    Thread.Sleep(100);
                    return pre + DateTime.Now.ToString("yyyyMMddHHmmss") + _sn.ToString().PadLeft(3, '0');
                }
            }
        }
    }
}
