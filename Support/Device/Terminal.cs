using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using EnvironmentalMonitor.Support.Instruction.Out.Request;
using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Device.Handler;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Device
{
    /// <summary>
    /// 终端
    /// </summary>
    public class Terminal
    {
        /// <summary>
        /// 应答指令
        /// </summary>
        /// <param name="remoteEP">IP</param>
        /// <param name="instructions">指令集合</param>
        /// <returns>结果</returns>
        public static void AnwserInstruction(EndPoint remoteEP, List<AbstractInstruction> instructions)
        {
            if (instructions != null)
            {
                for (int i = 0; i < instructions.Count; i++)
                {
                    AbstractInstruction instruction = instructions[i];
                    try
                    {
                        byte[] buffers = instruction.Encode();
                        Hardware.Socket.SendTo(buffers, remoteEP);

                        IPEndPoint localIP = (IPEndPoint)Hardware.Socket.LocalEndPoint;
                        IPEndPoint remoteIP = (IPEndPoint)remoteEP;
                        EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(localIP, remoteIP, Direction.Send, buffers);
                    }
                    catch (Exception exception)
                    {
                        EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
                        break;
                    }
                    finally
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="remoteEP">IP</param>
        /// <param name="instructions">指令集合</param>
        /// <returns>指令集合</returns>
        public static ProcessResult ExecuteInstruction(EndPoint remoteEP, List<AbstractInstruction> instructions)
        {
            ProcessResult result = null;

            if (instructions != null)
            {
                int times = 0;
                while (times < Variable.ArqTimes)
                {
                    Socket socket = null;
                    try
                    {
                        EndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
                        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        socket.Bind(localEP);
                        socket.SendTimeout = Variable.ArqTimeout;
                        socket.ReceiveTimeout = Variable.ArqTimeout;

                        byte[] sendBuffer = null;

                        for (int i = 0; i < instructions.Count; i++)
                        {
                            AbstractInstruction instruction = instructions[i];
                            sendBuffer = instruction.Encode();
                            socket.SendTo(sendBuffer, remoteEP);
                            IPEndPoint localIP = (IPEndPoint)socket.LocalEndPoint;
                            IPEndPoint remoteIP = (IPEndPoint)remoteEP;
                            EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(localIP, remoteIP, Direction.Send, sendBuffer);
                        }

                        TimeInstruction timeInstruction = new TimeInstruction();
                        int length = timeInstruction.FrameFixedLength;
                        List<InstructionHandler> instructionHandlers = new List<InstructionHandler>();
                        while (true)
                        {
                            byte[] receiveBuffer = new byte[ushort.MaxValue];
                            remoteEP = new IPEndPoint(IPAddress.Any, 0);
                            int count = socket.ReceiveFrom(receiveBuffer, ref remoteEP);
                            if (count >= length)
                            {
                                byte[] values = new byte[count];
                                Array.Copy(receiveBuffer, values, count);
                                result = Hardware.InstructionHandler.Process(socket.LocalEndPoint, remoteEP, values);

                                if (Hardware.InstructionHandlers != null)
                                {
                                    for (int i = 0; i < Hardware.InstructionHandlers.Count; i++)
                                    {
                                        Hardware.InstructionHandlers[i].Process(socket.LocalEndPoint, remoteEP, values);
                                    }
                                }
                            }

                            if (result != null)
                            {
                                break;
                            }
                        }

                        times = Variable.ArqTimes;
                    }
                    catch (Exception exception)
                    {
                        times++;
                        EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
                    }
                    finally
                    {
                        if (socket != null)
                        {
                            socket.Close();
                        }
                    }
                }
            }

            if (result == null)
            {
                result = new ProcessResult();
                result.Done = false;
                result.Message = "访问检测仪出现错误！";
            }

            return result;
        }
    }
}
