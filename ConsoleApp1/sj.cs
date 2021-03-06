﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class RandomHelper
    {
        /// <summary>
        ///生成制定回位数的随机码（数字答）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
    }
}
