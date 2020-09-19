
using Acb.MiddleWare.Core.Config;
using Dynamic.Core.ViewModel;

namespace MutualInsuranceThird.Plugin.Log
{
    /// <summary>
    /// 配置
    /// </summary>
    public class LogPluginConfig : PluginConfigBase
    {

        /// <summary>
        /// 2.0.0
        /// </summary>
        public DBCfgViewModel DbConfig { get; set; }
    }
}
