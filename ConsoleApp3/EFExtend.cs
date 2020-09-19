using Dynamic.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MutualInsuranceThird.Common.Extend
{
    public static class EfExtend
    {
        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<bool> InsertASync(this IEntity model, DbContext dbContext)
        {
            dbContext.Set<IEntity>().Attach(model);
            dbContext.Entry(model).State = EntityState.Added;
            var result =await dbContext.SaveChangesAsync();
            dbContext.Entry(model).State = EntityState.Detached;
            return result>0;
        }

        /// <summary>
        /// 新增 同步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertSync(this IEntity model, DbContext dbContext)
        {
            dbContext.Set<IEntity>().Attach(model);
            dbContext.Entry(model).State = EntityState.Added;
            var result = dbContext.SaveChanges();
            dbContext.Entry(model).State = EntityState.Detached;
            return result > 0;
        }


        /// <summary>
        /// 更新 同步（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateSync(this IEntity model, DbContext dbContext)
        {
            dbContext.Set<IEntity>().Attach(model);
            dbContext.Entry(model).State = EntityState.Modified;
            var result = dbContext.SaveChanges() > 0;
            dbContext.Entry(model).State = EntityState.Detached;
            return result;
        }

        /// <summary>
        /// 更新列表同步（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateListSync<T>(this List<T> model, DbContext dbContext) where T : IEntity
        {
            foreach (var item in model)
            {
                dbContext.Set<IEntity>().Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
            };
            var result = dbContext.SaveChanges() == model.Count;
            foreach (var item in model)
            {
                dbContext.Entry(item).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// 更新列表同步（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateListAsync<T>(this List<T> model, DbContext dbContext) where T : IEntity
        {
            foreach (var item in model)
            {
                dbContext.Set<IEntity>().Attach(item);
                dbContext.Entry(item).State = EntityState.Modified;
            };
            var result = await dbContext.SaveChangesAsync() == model.Count;
            foreach (var item in model)
            {
                dbContext.Entry(item).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// 新增列表同步（所有字段）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertListSync<T>(this List<T> model, DbContext dbContext) where T : IEntity
        {
            foreach (var item in model)
            {
                dbContext.Set<IEntity>().Attach(item);
                dbContext.Entry(item).State = EntityState.Added;
            };
            var result = dbContext.SaveChanges() == model.Count;
            foreach (var item in model)
            {
                dbContext.Entry(item).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// 新增列表异步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<bool> InsertListAsync<T>(this List<T> model, DbContext dbContext) where T : IEntity
        {
            foreach (var item in model)
            {
                dbContext.Set<IEntity>().Attach(item);
                dbContext.Entry(item).State = EntityState.Added;
            };
            var result = await dbContext.SaveChangesAsync() == model.Count;
            foreach (var item in model)
            {
                dbContext.Entry(item).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// 将sql语句查询字段动态化
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbContext"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this string sql, DbContext dbContext, params SqlParameter[] parameters)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql; ;//检查到sql不安全

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        var dbParameter = cmd.CreateParameter();
                        dbParameter.DbType = p.DbType;
                        dbParameter.ParameterName = p.ParameterName;
                        dbParameter.Value = p.Value;
                        cmd.Parameters.Add(dbParameter);
                    }
                }
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }
                        yield return row;
                    }
                }
            }
        }

        /// <summary>
        /// 将sql语句查询字段动态化
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbContext"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this string sql, DbContext dbContext, params SqlParameter[] parameters) where T : new()
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql; ;//检查到sql不安全

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        var dbParameter = cmd.CreateParameter();
                        dbParameter.DbType = p.DbType;
                        dbParameter.ParameterName = p.ParameterName;
                        dbParameter.Value = p.Value;
                        cmd.Parameters.Add(dbParameter);
                    }
                }
                using (var dataReader = cmd.ExecuteReader())
                {
                    Type type = typeof(T);
                    while (dataReader.Read())
                    {
                        T t = new T();
                        var propertyInfo = TestGetProperties<T>.Get();
                        List<string> notProp = new List<string>();
                        List<string> dataReaderFieldNames = new List<string>();
                        if (!TestGetProperties<T>.IsSetNotField)
                        {
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                dataReaderFieldNames.Add(dataReader.GetName(i).Trim());
                            }
                            foreach (var item in propertyInfo)
                            {
                                if (!dataReaderFieldNames.Contains(item.Name))
                                {
                                    notProp.Add(item.Name);
                                }
                            }
                            TestGetProperties<T>.SetNotField(notProp);
                        }
                        notProp = TestGetProperties<T>.GetNotField();
                        foreach (var prop in propertyInfo)
                        {
                            if (notProp.Contains(prop.Name)) continue;
                            var readerData = dataReader[prop.Name];
                            if (readerData is DBNull)
                            {
                                prop.SetValue(t, null);
                                continue;
                            }
                            if (readerData is DateTime && prop.PropertyType == typeof(string))
                            {
                                prop.SetValue(t, ((DateTime)readerData).ToString("yyyy/MM/dd HH:mm:ss"));
                                continue;
                            }
                            prop.SetValue(t, readerData);
                        }
                        yield return t;
                    }
                }
            }
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbContext"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> GetCount(this string sql, DbContext dbContext, params SqlParameter[] parameters)
        {
            int result = 0;
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql; ;//检查到sql不安全

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        var dbParameter = cmd.CreateParameter();
                        dbParameter.DbType = p.DbType;
                        dbParameter.ParameterName = p.ParameterName;
                        dbParameter.Value = p.Value;
                        cmd.Parameters.Add(dbParameter);
                    }
                }
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        yield return dataReader;
                    }
                }
            }
        }

        public static bool RunSql(this string sql, DbContext dbContext, SqlParameter[] sqlParameters = null)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql; ;//检查到sql不安全

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                if (sqlParameters != null)
                {
                    foreach (var p in sqlParameters)
                    {
                        var dbParameter = cmd.CreateParameter();
                        dbParameter.DbType = p.DbType;
                        dbParameter.ParameterName = p.ParameterName;
                        dbParameter.Value = p.Value;
                        cmd.Parameters.Add(dbParameter);
                    }
                }
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public static class TestGetProperties<T>
    {
        private static List<PropertyInfo> Info { get; set; }

        private static List<string> NotField { get; set; }

        public static bool IsSetNotField = false;

        public static List<PropertyInfo> Get()
        {
            if (Info == null)
            {
                Type type = typeof(T);
                Info = type.GetProperties().ToList();
            }
            return Info;
        }

        public static List<string> GetNotField()
        {
            return NotField;
        }

        public static bool SetNotField(List<string> notField)
        {
            IsSetNotField = true;
            NotField = notField;
            return true;
        }
    }
}
