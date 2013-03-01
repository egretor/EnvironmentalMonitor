using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Device.Handler
{
    /// <summary>
    /// 日志处理
    /// </summary>
    public class LogHandler : EnvironmentalMonitor.Support.Device.Handler.InstructionHandler
    {
        public ProcessResult Process(EndPoint localEP, EndPoint remoteEP, byte[] values)
        {
            ProcessResult result = null;

            IPEndPoint localIP = localEP as IPEndPoint;
            IPEndPoint remoteIP = remoteEP as IPEndPoint;
            Variable.Logger.Log(localIP, remoteIP, Direction.Receive, values);

            return result;
        }
    }
}
