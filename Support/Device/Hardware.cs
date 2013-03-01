using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;

using EnvironmentalMonitor.Support.Device.Handler;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Device
{
    /// <summary>
    /// 硬件
    /// </summary>
    public class Hardware
    {
        private static bool _FatalError;
        /// <summary>
        /// 致命错误
        /// </summary>
        public static bool FatalError
        {
            get
            {
                return Hardware._FatalError;
            }
        }

        private static InstructionHandler _InstructionHandler;
        /// <summary>
        /// 指令处理
        /// </summary>
        public static InstructionHandler InstructionHandler
        {
            get
            {
                return Hardware._InstructionHandler;
            }
            set
            {
                Hardware._InstructionHandler = value;
            }
        }

        private static List<InstructionHandler> _InstructionHandlers;
        /// <summary>
        /// 指令处理集合
        /// </summary>
        public static List<InstructionHandler> InstructionHandlers
        {
            get
            {
                return Hardware._InstructionHandlers;
            }
            set
            {
                Hardware._InstructionHandlers = value;
            }
        }

        private static Socket _Socket;
        /// <summary>
        /// 套接字
        /// </summary>
        public static Socket Socket
        {
            get
            {
                if ((Hardware._Socket == null) && (!Hardware._FatalError))
                {
                    Hardware._Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    EndPoint localEP = new IPEndPoint(IPAddress.Any, Variable.Port);
                    try
                    {
                        Hardware._Socket.Bind(localEP);
                    }
                    catch (Exception exception)
                    {
                        Hardware._FatalError = true;
                        EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
                    }
                    finally
                    {
                    }
                }

                return Hardware._Socket;
            }
        }
    }
}
