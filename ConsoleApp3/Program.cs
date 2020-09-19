using Dynamic.Core.Log;
using MutualInsuranceThird.Plugin.Log;
using MutualInsuranceThird.Plugin.Log.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerManager.InitLogger(new LogConfig());
            //var data = new ThirdAppInputOrderDto();
            //data.InputOrderType = 0;
            //data.Phone = "13551455604";
            //data.Name = "吴昊";
            //data.MechanismId = "bfff40f0d2a64361a15e6dcc69c2fa2b";
            //data.VehicleFrameNumber = "LVHEK7894M6000239";
            //data.Number = "*_*";
            //data.Type = "东风本田/本田/INSPIRE";
            //data.AreaCode = "110111";
            //data.ProductId = "fd0828ae7390c8ed4be408d7895d916c";
            //data.Amount = 99.9000M;
            //data.AuthKey = "authc01a7b4da106c3aab79a08d821cef2db";
            //data.AuthSecret = "6d43ebd31b9e35253761777ebae5d9cf";
            //data.IsNewVehicle = false;
            //data.AutomaticDeductions = false;
            //data.VehicleLicenseImage = "http://file.i-cbao.com/uploads/pictures/2020/0909/eea924f3dad4cfb6.jpg";
            //data.SourceId = "9a80d5bb8689c247725408d84db219e6";
            //data.AppKey = "appf04097aa3e00c94c480c08d83ea81728";
            //data.AppSecret = "fe3cd1273439b9793602b95a8038c7e5";
            //data.AutoPayEndTime = DateTime.Now;
            //data.AccountCodes = 10000000000001;
            //data.CustomerName = "吴昊";
            //data.CustomerPhone = "13901326930";
            //data.Images = new List<string>();

            var data = new AddInputOrderDto();
            data.Phone = "13551455604";
            data.FullName = "吴昊";
            data.VehicleFrameNumber = "LVHEK7894M6000239";
            data.VehicleNumber = "*_*";
            data.VehicleBrand = "东风本田/本田/INSPIRE";
            data.ProductId = "fd0828ae7390c8ed4be408d7895d916c";
            data.ProductName = "轮胎修补计划";
            data.Amount = 99.9000M;
            data.IsNewVehicle = true;
            data.AutomaticDeductions = false;
            data.AutoPayEndTime = DateTime.Now;
            data.JoinType = 0;
            data.VehicleLicenseImage = "http://file.i-cbao.com/uploads/pictures/2020/0909/eea924f3dad4cfb6.jpg";
            data.Images = new List<ImageListReturnDto>();
            data.IsActive = false;
            data.DiscountPackageId = "A98F07DAA35149E4921C6B06DDC1140D";

            var rendom = new Random();
            List<Task> tasks = new List<Task>();
            var testUrl = "http://192.168.0.252:7019";
            var localUrl = "http://192.168.2.134:9099";

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    //for (int j = 0; j < 1; j++)
                    //{
                    HttpExtend http = new HttpExtend();
                    data.Phone = "130" + rendom.Next(10000000, 99999999);
                    data.VehicleFrameNumber = DateTime.Now.ToString("MMddHHmmss") + rendom.Next(1000000, 9999999);
                    Console.WriteLine(JsonConvert.SerializeObject(http.Post<AddInputOrderDto, dynamic>($"{testUrl}/api/InputOrder/sales/AddInputOrderHttp", data).Result));
                    //}

                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.ReadKey();
            //LogPluginConfig logPluginConfig = new LogPluginConfig()
            //{
            //    DbConfig = new Dynamic.Core.ViewModel.DBCfgViewModel
            //    {
            //        AutoOrmType = 0,
            //        CurrentDBType = Dynamic.Core.ViewModel.DBType.PgSql,
            //        ExtendConnectStr = null,
            //        _AccessDBViewModel = null,
            //        _DB2DBViewModel = null,
            //        _MySqlDBViewModel = null,
            //        _OracleDBViewModel = null,
            //        _PgSqlDBViewModel = new Dynamic.Core.ViewModel.PgSqlDBViewModel
            //        {
            //            DataSource = "MutualAid2_0_0_Third",
            //            DBAddress = "192.168.0.234",
            //            Password = "",
            //            Port = 5432,
            //            UserID = "postgres",
            //        },
            //        _SqlServerDBViewModel = null,
            //    }
            //};

            //LogRepository logRepository = new LogRepository(logPluginConfig.DbConfig);

            //for (int i = 0; i < 10000; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        //HttpExtend http = new HttpExtend();
            //        //data.VehicleFrameNumber = DateTime.Now.ToString("MMddHHmmss") + rendom.Next(1000000, 9999999);
            //        //var ret = http.Get<AddInputOrderDto, dynamic>($"{localUrl}/api/Organization/manage/test1", data).Result;
            //        //Console.WriteLine(JsonConvert.SerializeObject(ret));
            //        var ret=  logRepository.InsertSync1(new MutualInsuranceThird.Plugin.Log.Entities.TLog
            //        {
            //            Creater = "测试",
            //            CreateTime = DateTime.Now,
            //            Describe = "***",
            //            Id = Guid.NewGuid().ToString("N"),
            //            JsonExtension = "***",
            //            RelationId = "***",
            //            Routing = "***",
            //            Type = 1
            //        });
            //    //Console.WriteLine(JsonConvert.SerializeObject(ret));
            //    });
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        HttpExtend http = new HttpExtend();
            //        data.VehicleFrameNumber = DateTime.Now.ToString("MMddHHmmss") + rendom.Next(1000000, 9999999);
            //        var ret = http.Get<AddInputOrderDto, dynamic>($"{localUrl}/api/InputOrder/sales/TestTrans", data).Result;
            //        Console.WriteLine(JsonConvert.SerializeObject(ret));
            //    });
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        HttpExtend http = new HttpExtend();
            //        data.VehicleFrameNumber = DateTime.Now.ToString("MMddHHmmss") + rendom.Next(1000000, 9999999);
            //        var ret = http.Get<para, dynamic>($"https://ifch.i-cbao.com/hzdevelop/api/Organization/manage/GetOrgHttp", new para {  id= "b667509e2888cb4a3a5208d8500f49a6" }).Result;
            //        Console.WriteLine(JsonConvert.SerializeObject(ret));
            //    });
            //}
            //Console.ReadKey();


        }

  

    }
    public class para
    {
        public string id { get; set; }
    }

    public class HttpExtend
    {
        /// <summary>
        /// 实体转成get参数
        /// </summary>
        /// <typeparam name="T">请求参数实体类型</typeparam>
        /// <typeparam name="K">返回结果实体类型</typeparam>
        /// <param name="url">请求url</param>
        /// <param name="param">请求参数实体</param>
        /// <returns></returns>
        public async Task<K> Get<T, K>(string url, T param)
        {
            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.DefaultRequestHeaders.Add("sToken", "sToken:ad38ee1ee36dc44cc12a08d85992a3f9");
            //httpClient.DefaultRequestHeaders.Add("sOrgId", "749f3b9bc97acfbca91308d8597f0a50");
            var paramStr = ModelToHttpParam<T>.ToGetParam(param);
            var requstResult = await httpClient.GetAsync(url + paramStr);
            var result = string.Empty;
            using (requstResult)
            {
                result = await requstResult.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<K>(result);
        }

        /// <summary>
        /// 实体转成Delete参数
        /// </summary>
        /// <typeparam name="T">请求参数实体类型</typeparam>
        /// <typeparam name="K">返回结果实体类型</typeparam>
        /// <param name="url">请求url</param>
        /// <param name="param">请求参数实体</param>
        /// <returns></returns>
        public async Task<K> Delete<T, K>(string url, T param)
        {
            HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var paramStr = ModelToHttpParam<T>.ToGetParam(param);
            var requstResult = await httpClient.DeleteAsync(url + paramStr);
            var result = string.Empty;
            using (requstResult)
            {
                result = await requstResult.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<K>(result);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T">请求参数实体类型</typeparam>
        /// <typeparam name="K">返回结果实体类型</typeparam>
        /// <param name="url">请求url</param>
        /// <param name="param">请求参数实体</param>
        /// <returns></returns>
        public async Task<K> Post<T, K>(string url, T param)
        {
            HttpClient httpClient = new HttpClient();

            var paramStr = ModelToHttpParam<T>.ToPostParam(param);

            var strContent = new StringContent(paramStr);
            strContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");// ("content-type", "application/json");
            httpClient.DefaultRequestHeaders.Add("sToken", "sToken:f1ddf7b979f7c8f72c1108d85c8e80a6");
            httpClient.DefaultRequestHeaders.Add("sOrgId", "a518ed31d76fccfe77f108d85c888ff5");
            var requstResult = await httpClient.PostAsync(url, strContent);
            var result = string.Empty;
            using (requstResult)
            {
                result = await requstResult.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<K>(result);
        }
    }

    /// <summary>
    /// 实体转Http请求参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ModelToHttpParam<T>
    {
        /// <summary>
        /// 当前实体属性列表
        /// </summary>
        public static PropertyInfo[] PropertyInfos { get; set; }

        /// <summary>
        /// 转成get请求参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToGetParam(T model)
        {
            if (model == null) return "";
            if (PropertyInfos == null || PropertyInfos.Count() == 0)
            {
                PropertyInfos = typeof(T).GetProperties();
            }
            var paramList = PropertyInfos.Select(i =>
            {
                var name = i.Name;
                var value = i.GetValue(model);
                return value == null ? "" : $"{name}={value}";
            }).Where(i => !string.IsNullOrEmpty(i)).ToList();
            return "?" + string.Join("&", paramList);
        }

        /// <summary>
        /// 转成POST请求参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToPostParam(T model)
        {
            if (model == null) return "{}";
            return JsonConvert.SerializeObject(model);
        }
    }
    public class ImageListReturnDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>

        public string ImageUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }
        /// <summary>
        /// 索引
        /// </summary>

        public int Index { get; set; }

        /// <summary>
        /// 关键ID 必须结合TYPE使用
        /// </summary>
        public string CorrelationID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }


    }
    /// <summary>
    /// dto
    /// </summary>
    public class ThirdAppInputOrderDto
    {
        ///<summary>
        ///录单类型(0：个人，1：法人)
        ///</summary>
        public int? InputOrderType { get; set; }

        ///<summary>
        ///用户手机
        ///</summary>
        public string Phone { get; set; }

        ///<summary>
        ///用户姓名
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///*机构ID
        ///</summary>
        public string MechanismId { get; set; }

        ///<summary>
        ///*车架号
        ///</summary>
        public string VehicleFrameNumber { get; set; }

        ///<summary>
        ///车辆号码
        ///</summary>
        public string Number { get; set; }
        ///<summary>
        ///*车辆类型
        ///</summary>
        public string Type { get; set; }

        ///<summary>
        ///*地区代码
        ///</summary>
        public string AreaCode { get; set; }

        ///<summary>
        ///*产品ID
        ///</summary>
        public string ProductId { get; set; }

        ///<summary>
        ///*充值金额
        ///</summary>
        public decimal Amount { get; set; } = 0;

        /// <summary>
        /// *开发者Key
        /// </summary>
        public string AuthKey { get; set; }

        /// <summary>
        /// *开发者Secret
        /// </summary>
        public string AuthSecret { get; set; }

        /// <summary>
        /// 是否是新车
        /// </summary>
        public bool IsNewVehicle { get; set; }

        /// <summary>
        /// 是否自动扣款
        /// </summary>
        public bool AutomaticDeductions { get; set; }

        /// <summary>
        /// 行驶证
        /// </summary>
        public string VehicleLicenseImage { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string SourceId { get; set; }

        ///<summary>
        ///应用KEY
        ///</summary>
        public string AppKey { get; set; }

        ///<summary>
        ///应用Secret
        ///</summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 自动扣费结束时间
        /// </summary>           
        public DateTime AutoPayEndTime { get; set; }

        ///<summary>
        ///账户号
        ///</summary>
        public long AccountCodes { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// 客户手机
        /// </summary>
        public string CustomerPhone { get; set; } = string.Empty;

        /// <summary>
        /// 客户手机
        /// </summary>
        public List<string> Images { get; set; }
    }
    public enum JoinTypeEnum
    {
        /// <summary>
        /// 个人
        /// </summary>
        Personal = 0,

        /// <summary>
        /// 法人
        /// </summary>
        LegalPerson = 1

    }
    public class AddInputOrderDto
    {
        ///<summary>
        ///手机
        ///</summary>
        public string Phone { get; set; }

        ///<summary>
        ///法人手机
        ///</summary>
        public string LPhone { get; set; }

        ///<summary>
        ///姓名
        ///</summary>
        public string FullName { get; set; }

        ///<summary>
        ///车牌号
        ///</summary>
        public string VehicleNumber { get; set; }

        ///<summary>
        ///车架号
        ///</summary>
        public string VehicleFrameNumber { get; set; }
        ///<summary>
        ///车辆品牌
        ///</summary>
        public string VehicleBrand { get; set; }
        ///<summary>
        ///车辆所在区域
        ///</summary>
        public string VehicleArea { get; set; }
        ///<summary>
        ///产品Id
        ///</summary>
        public string ProductId { get; set; }

        ///<summary>
        ///产品名称
        ///</summary>
        public string ProductName { get; set; }

        ///<summary>
        ///录单金额
        ///</summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 是否是新车
        /// </summary>
        public bool IsNewVehicle { get; set; }

        /// <summary>
        /// 是否自动扣款
        /// </summary>
        public bool AutomaticDeductions { get; set; }

        /// <summary>
        /// 自动扣费结束时间
        /// </summary>           
        public DateTime AutoPayEndTime { get; set; }

        /// <summary>
        /// 录单类型
        /// </summary>
        public JoinTypeEnum JoinType { get; set; }

        /// <summary>
        /// 行驶证照片
        /// </summary>
        public string VehicleLicenseImage { get; set; }

        /// <summary>
        /// 激活图片组
        /// </summary>
        public List<ImageListReturnDto> Images { get; set; }

        ///<summary>
        ///是激活
        ///</summary>
        public bool IsActive { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        ///账户号
        ///</summary>
        public long AccountCode { get; set; }

        ///<summary>
        ///优惠包Id
        ///</summary>
        public string DiscountPackageId { get; set; }

        ///<summary>
        ///手机验证码
        ///</summary>
        public string MobileCaptcha { get; set; }

        ///<summary>
        ///验证码请求Id
        ///</summary>
        public string MobileCaptchaRequestId { get; set; }

        ///<summary>
        ///渠道id
        ///</summary>
        public string ChannelId { get; set; }



    }
}
