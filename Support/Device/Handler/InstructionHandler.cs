using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using EnvironmentalMonitor.Support.Instruction;

namespace EnvironmentalMonitor.Support.Device.Handler
{
    /// <summary>
    /// 抽象指令处理接口
    /// </summary>
    public interface InstructionHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="remoteEP">远程地址</param>
        /// <param name="values">数据</param>
        /// <returns></returns>
        ProcessResult Process(EndPoint localEP, EndPoint remoteEP, byte[] values);
    }
}
