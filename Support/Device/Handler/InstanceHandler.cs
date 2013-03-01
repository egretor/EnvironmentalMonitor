using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Reflection;

using EnvironmentalMonitor.Support.Instruction.In.Request;
using EnvironmentalMonitor.Support.Instruction.In.Response;
using EnvironmentalMonitor.Support.Instruction.Out.Request;
using EnvironmentalMonitor.Support.Instruction.Out.Response;
using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Device.Handler
{
    /// <summary>
    /// 实例处理
    /// </summary>
    public class InstanceHandler : EnvironmentalMonitor.Support.Device.Handler.InstructionHandler
    {
        /// <summary>
        /// 分析指令
        /// </summary>
        /// <param name="frame">帧数据</param>
        /// <returns>指令</returns>
        private AbstractInstruction AnalysisInstruction(byte[] frame)
        {
            const int minimumFrameLength = sizeof(byte) + sizeof(ushort) + sizeof(ushort);
            AbstractInstruction result = null;

            try
            {
                if ((frame != null) && (frame.Length > minimumFrameLength))
                {
                    List<AbstractInstruction> abstractInstructions = new List<AbstractInstruction>();
                    Type abstractInstructionType = typeof(AbstractInstruction);
                    Assembly abstractInstructionAssembly = abstractInstructionType.Assembly;
                    Type[] types = abstractInstructionAssembly.GetTypes();
                    if (types != null)
                    {
                        for (int i = 0; i < types.Length; i++)
                        {
                            if (types[i] != null)
                            {
                                if (types[i].IsSubclassOf(abstractInstructionType))
                                {
                                    object value = abstractInstructionAssembly.CreateInstance(types[i].FullName);
                                    if (value != null)
                                    {
                                        AbstractInstruction abstractInstructionValue = value as AbstractInstruction;
                                        abstractInstructions.Add(abstractInstructionValue);
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < abstractInstructions.Count; i++)
                    {
                        if (abstractInstructions[i].Controller == frame[0])
                        {
                            byte[] typeValues = new byte[sizeof(ushort)];
                            typeValues[0] = frame[4];
                            typeValues[1] = frame[3];
                            ushort type = BitConverter.ToUInt16(typeValues, 0);
                            if (abstractInstructions[i].Type == type)
                            {
                                abstractInstructions[i].Decode(frame);
                                result = abstractInstructions[i];
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Variable.Logger.Log(exception);
            }

            return result;
        }

        /// <summary>
        /// 处理任务
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="instruction">指令</param>
        /// <returns></returns>
        private ProcessResult ProcessTask(string ip, AbstractInstruction instruction)
        {
            ProcessResult result = null;

            InstructionTask instructionTask = new InstructionTask();
            if (AbstractInstruction.InstructionTasks != null)
            {
                bool has = false;
                for (int i = AbstractInstruction.InstructionTasks.Count - 1; i >= 0; i--)
                {
                    if (AbstractInstruction.InstructionTasks[i].Done)
                    {
                        AbstractInstruction.InstructionTasks.RemoveAt(i);
                    }
                }
                for (int i = 0; i < AbstractInstruction.InstructionTasks.Count; i++)
                {
                    if ((AbstractInstruction.InstructionTasks[i].Ip == ip) && (AbstractInstruction.InstructionTasks[i].Type == instruction.GetType().FullName) && (AbstractInstruction.InstructionTasks[i].Instructions.Count == instruction.Serial))
                    {
                        instructionTask = AbstractInstruction.InstructionTasks[i];
                        has = true;
                        break;
                    }
                }
                if (!has)
                {
                    instructionTask.Ip = ip;
                    instructionTask.Type = instruction.GetType().FullName;
                    instructionTask.Instructions = new List<AbstractInstruction>();
                    instructionTask.Done = false;
                    AbstractInstruction.InstructionTasks.Add(instructionTask);
                }
                instructionTask.Instructions.Add(instruction);

                if ((instructionTask.Instructions.Count - 1) == instruction.Count)
                {
                    instructionTask.Done = true;
                    result = instruction.Process(ip, instructionTask);
                }
            }

            return result;
        }

        public ProcessResult Process(EndPoint localEP, EndPoint remoteEP, byte[] values)
        {
            ProcessResult result = null;

            AbstractInstruction instruction = this.AnalysisInstruction(values);

            IPEndPoint ipEndPoint = remoteEP as IPEndPoint;
            string ip = string.Empty;
            if ((ipEndPoint != null) && (ipEndPoint.Address != null))
            {
                ip = ipEndPoint.Address.ToString();
            }

            if (instruction != null)
            {
                ProcessResult processResult = this.ProcessTask(ip, instruction);
                if (processResult != null)
                {
                    result = processResult;
                    List<AbstractInstruction> answerInstructions = instruction.Answer(processResult.Done);
                    if ((answerInstructions != null) && (answerInstructions.Count > 0))
                    {
                        Terminal.AnwserInstruction(remoteEP, answerInstructions);
                    }
                }
            }

            return result;
        }
    }
}
