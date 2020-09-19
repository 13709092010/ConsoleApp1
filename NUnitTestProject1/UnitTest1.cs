using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //var t= "{'NodeCss':'iconfont icon-drxx11 txt-snd8'}";
            //object tt = JsonConvert.DeserializeObject<object>(t);
            ////var tt =JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(t);
            //Assert.AreEqual(tt, "");
            //Assert.Pass();

            var p = "15982288753 ";
            var oo = Regex.IsMatch(p, @"^1[3,4,5,6,7,8,9]\d{9}$");
       var xx = oo;

        }
    }
}