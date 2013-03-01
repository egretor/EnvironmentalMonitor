using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Resource
{
    /// <summary>
    /// 探头类型
    /// </summary>
    public enum DetectorTypes
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 开关量类型
        /// 0 错误
        /// 1 正常
        /// </summary>
        Switch = 1,
        /// <summary>
        /// 双范围值类型
        /// </summary>
        DoubleArea = 2
    }
}
