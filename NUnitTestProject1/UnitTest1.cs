using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

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
            var t= "{'NodeCss':'iconfont icon-drxx11 txt-snd8'}";
            object tt = JsonConvert.DeserializeObject<object>(t);
            //var tt =JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(t);
            Assert.AreEqual(tt, "");
            Assert.Pass();
        }
    }
}