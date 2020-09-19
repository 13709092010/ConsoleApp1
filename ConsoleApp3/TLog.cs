using Dynamic.Core.Entities;
using Dynamic.Core.Serialize;
using System;

namespace MutualInsuranceThird.Plugin.Log.Entities
{
    /// <summary>
    /// 日志
    /// </summary>
    [Naming("log")]
    public class TLog : IEntity
    {
        ///<summary>
        ///主键
        ///</summary>
        public string Id { get; set; }

        ///<summary>
        ///日志类型
        ///</summary>
        public int Type { get; set; }

        ///<summary>
        ///日志扩展
        ///</summary>
        public string JsonExtension { get; set; }

        ///<summary>
        ///路由
        ///</summary>
        public string Routing { get; set; }

        ///<summary>
        ///关联ID
        ///</summary>
        public string RelationId { get; set; }

        ///<summary>
        ///描述
        ///</summary>
        public string Describe { get; set; }

        ///<summary>
        ///创建人
        ///</summary>
        public string Creater { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }
    }
}
