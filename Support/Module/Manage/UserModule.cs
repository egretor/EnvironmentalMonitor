using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Manage
{
    /// <summary>
    /// 用户和模块关系
    /// </summary>
    public class UserModule : EnvironmentalMonitor.Support.Module.AbstractModule
    {
        /// <summary>
        /// 默认模块
        /// </summary>
        public const string DEFAULT_MODULE = "m0101default";
        /// <summary>
        /// 管理模块
        /// </summary>
        public const string MANAGE_MODULE = "m0201manage";
        /// <summary>
        /// 机房模块
        /// </summary>
        public const string ROOM_MODULE = "m0301room";
        /// <summary>
        /// 设备模块
        /// </summary>
        public const string MACHINE_MODULE = "m0401machine";
        /// <summary>
        /// 设备配置模块
        /// </summary>
        public const string MACHINE_ADVANCED_MODULE = "m0402machineAdvanced";
        /// <summary>
        /// 探头类型模块
        /// </summary>
        public const string DETECTOR_TYPE_MODULE = "m0501detectorType";
        /// <summary>
        /// 探头模块
        /// </summary>
        public const string DETECTOR_MODULE = "m0502detector";
        /// <summary>
        /// 报警模块
        /// </summary>
        public const string ALARM_MODULE = "m0601alarm";
        /// <summary>
        /// 监控模块
        /// </summary>
        public const string MONITOR_MODULE = "m0701monitor";
        /// <summary>
        /// 查询模块
        /// </summary>
        public const string QUERY_MODULE = "m0801query";
        /// <summary>
        /// 特权模块
        /// </summary>
        public const string PREROGATIVE_MODULE = "m9801prerogative";
        /// <summary>
        /// 调试模块
        /// </summary>
        public const string DEBUG_MODULE = "m9901debug";

        public static string[] Modules
        {
            get
            {
                string[] results = { UserModule.MANAGE_MODULE, UserModule.ROOM_MODULE, UserModule.MACHINE_MODULE, UserModule.MACHINE_ADVANCED_MODULE, UserModule.DETECTOR_TYPE_MODULE, UserModule.DETECTOR_MODULE, UserModule.ALARM_MODULE, UserModule.MONITOR_MODULE, UserModule.QUERY_MODULE };
                return results;
            }
        }

        private string _UserId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }

        private string _ModuleCode;
        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode
        {
            get
            {
                return this._ModuleCode;
            }
            set
            {
                this._ModuleCode = value;
            }
        }
    }
}
