using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Instruction
{
    /// <summary>
    /// 指令类型枚举
    /// </summary>
    public enum InstructionTypes
    {
        /// <summary>
        /// 下位机向上位机请求
        /// </summary>
        InRequest,
        /// <summary>
        /// 上位机应答下位机
        /// </summary>
        InResponse,
        /// <summary>
        /// 上位机向下位机请求
        /// </summary>
        OutRequest,
        /// <summary>
        /// 下位机应答上位机
        /// </summary>
        OutResponse
    }
}
