using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using EnvironmentalMonitor.Support.Device.Handler;
using EnvironmentalMonitor.Support.Instruction.Out.Request;
using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Device
{
    /// <summary>
    /// 监听
    /// </summary>
    public class Listener
    {
        private static bool _Initialize;
        /// <summary>
        /// 初始化
        /// </summary>
        public static bool Initialize
        {
            get
            {
                return Listener._Initialize;
            }
            set
            {
                Listener._Initialize = value;
            }
        }

        private static Thread _Thread;
        /// <summary>
        /// 线程
        /// </summary>
        public static Thread Thread
        {
            get
            {
                return Listener._Thread;
            }
            set
            {
                Listener._Thread = value;
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        public static void Run()
        {
            if (!Listener.Initialize)
            {
                Listener.Initialize = true;
                ThreadStart threadStart = new ThreadStart(Instance);
                Listener.Thread = new Thread(threadStart);
                Listener.Thread.Start();
            }
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static void Instance()
        {
            TimeInstruction instruction = new TimeInstruction();
            int length = instruction.FrameFixedLength;

            if (Hardware.Socket != null)
            {
                while (true)
                {
                    byte[] buffer = new byte[ushort.MaxValue];
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    try
                    {
                        int count = Hardware.Socket.ReceiveFrom(buffer, ref remoteEP);
                        if (count >= length)
                        {
                            byte[] values = new byte[count];
                            Array.Copy(buffer, values, count);
                            Hardware.InstructionHandler.Process(Hardware.Socket.LocalEndPoint, remoteEP, values);

                            if (Hardware.InstructionHandlers != null)
                            {
                                for (int i = 0; i < Hardware.InstructionHandlers.Count; i++)
                                {
                                    Hardware.InstructionHandlers[i].Process(Hardware.Socket.LocalEndPoint, remoteEP, values);
                                }
                            }
                        }
                        else
                        {
                            bool shutdown = true;
                            for (int i = 0; i < Variable.Shutdown.Length; i++)
                            {
                                if (buffer[i] != Variable.Shutdown[i])
                                {
                                    shutdown = false;
                                }
                            }
                            if (shutdown)
                            {
                                Hardware.Socket.Close();
                                break;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
                    }
                    finally
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Shutdown()
        {
            if (Listener.Initialize)
            {
                Listener.Initialize = false;
                EndPoint remoteEP = new IPEndPoint(IPAddress.Loopback, Variable.Port);
                Hardware.Socket.SendTo(Variable.Shutdown, remoteEP);
            }

        }
    }
}
