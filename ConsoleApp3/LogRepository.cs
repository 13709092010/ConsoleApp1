
using CDynamic.EF.Repository;
using Dynamic.Core.Log;
using Dynamic.Core.ViewModel;
using MutualInsuranceThird.Common.Extend;
using MutualInsuranceThird.Plugin.Log.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MutualInsuranceThird.Plugin.Log.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class LogRepository : DRepository<TLog>
    {
        private readonly object _lockObjTj = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBConfig"></param>
        public LogRepository(DBCfgViewModel dBConfig) : base(dBConfig)
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Insert(TLog model)
        {
            return await base.Insert(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  bool InsertSync1(TLog model)
        {
            return model.InsertSync(GetDbContext());
        }

        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> InsertSync(TLog model)
        {
            Stopwatch stopwatch = new Stopwatch();
            lock (_lockObjTj)
            {
                stopwatch.Start();
            }
                var context = GetDbContext();
            lock(_lockObjTj)
            {

            stopwatch.Stop();
            }
            
            //this._testLogger.Trace("获取dbcontext时间:" + stopwatch.ElapsedMilliseconds);
           // Console.WriteLine("获取dbcontext时间:"+stopwatch.ElapsedMilliseconds);
            lock (_lockObjTj)
            {
                stopwatch.Reset();
                stopwatch.Start();
            }
          
            var abc= await model.InsertASync(context);
            lock (_lockObjTj)
            {
                stopwatch.Stop();
                //this._testLogger.Trace("插入操作时间:" + stopwatch.ElapsedMilliseconds);
            }
            

           // Console.WriteLine("插入操作时间:" + stopwatch.ElapsedMilliseconds);
            
            return abc;
        }


        /// <summary>
        /// 更新（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public new async Task<bool> Update(TLog model)
        {
            return await base.Update(model);

        }
        /// <summary>
        /// 新增同步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertListSync(List<TLog> model)
        {
            return model.InsertListSync(GetDbContext());
        }
        /// <summary>
        /// 更新 同步（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSync(TLog model)
        {
            return model.UpdateSync(GetDbContext());
        }

        /// <summary>
        /// 更新列表同步（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateListSync(List<TLog> model)
        {
            return model.UpdateListSync(GetDbContext());
        }

        /// <summary>
        /// 更新（部分字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public new async Task<bool> UpdateAttrs(TLog model)
        {
            return await base.UpdateAttrs(model) > 0;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<TLog> List()
        {
            return Query(i => true);
        }
    }
}
