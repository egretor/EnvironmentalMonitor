using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace EnvironmentalMonitor.Support.Instruction
{
    /// <summary>
    /// 指令抽象类
    /// </summary>
    public abstract class AbstractInstruction
    {
        private static List<InstructionTask> _InstructionTasks = new List<InstructionTask>();
        /// <summary>
        /// 指令任务集合
        /// </summary>
        public static List<InstructionTask> InstructionTasks
        {
            get
            {
                return AbstractInstruction._InstructionTasks;
            }
            set
            {
                AbstractInstruction._InstructionTasks = value;
            }
        }

        /// <summary>
        /// 指令类型
        /// </summary>
        public abstract InstructionTypes InstructionType
        {
            get;
        }

        /// <summary>
        /// 数据流最小字节
        /// </summary>
        public abstract ushort Minimum
        {
            get;
        }

        /// <summary>
        /// 数据流最大字节
        /// </summary>
        public abstract ushort Maximum
        {
            get;
        }

        /// <summary>
        /// 固定数据
        /// </summary>
        public virtual byte[] FixedData
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 控制码
        /// </summary>
        public abstract byte Controller
        {
            get;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public ushort Length
        {
            get
            {
                ushort result = 0;

                int typeLength = sizeof(ushort);
                int countLength = sizeof(byte);
                int serialLength = sizeof(byte);
                int dataLength = this.Data.Length;

                if (this.FixedData != null)
                {
                    dataLength = this.FixedData.Length;
                }

                result = (ushort)(typeLength + countLength + serialLength + dataLength);

                return result;
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public abstract ushort Type
        {
            get;
        }

        protected byte _Count = 0x00;
        /// <summary>
        /// 总帧数
        /// </summary>
        public byte Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        protected byte _Serial = 0x00;
        /// <summary>
        /// 次序
        /// </summary>
        public byte Serial
        {
            get
            {
                return this._Serial;
            }
            set
            {
                this._Serial = value;
            }
        }

        protected byte[] _Data;
        /// <summary>
        /// 数据流
        /// </summary>
        public byte[] Data
        {
            get
            {
                if (this._Data == null)
                {
                    this._Data = new byte[] { };
                }

                return this._Data;
            }
            set
            {
                if (value == null)
                {
                    value = new byte[] { };
                }
                if ((value.Length >= this.Minimum) && (value.Length <= this.Maximum))
                {
                    this._Data = value;
                }
                else
                {
                    throw new OverflowException();
                }
            }
        }

        /// <summary>
        /// 校验码
        /// </summary>
        public byte Check
        {
            get
            {
                byte result = 0x00;

                byte[] controllers = { this.Controller };
                byte[] lengths = BitConverter.GetBytes(this.Length);
                byte[] types = BitConverter.GetBytes(this.Type);
                byte[] counts = { this.Count };
                byte[] serials = { this.Serial };
                byte[] datas = this.Data;

                if (this.FixedData != null)
                {
                    datas = this.FixedData;
                }
                Array.Reverse(lengths);
                Array.Reverse(types);

                byte[][] values = { controllers, lengths, types, counts, serials, datas };

                for (int i = 0; i < values.Length; i++)
                {
                    for (int j = 0; j < values[i].Length; j++)
                    {
                        result = (byte)(result + values[i][j]);
                    }
                }

                result = (byte)(0xFF - result);

                return result;
            }
        }

        /// <summary>
        /// 帧数据长度
        /// </summary>
        public int FrameLength
        {
            get
            {
                int result = 0;

                int controllerLength = sizeof(byte);
                int lengthLength = sizeof(ushort);
                int typeLength = sizeof(ushort);
                int countLength = sizeof(byte);
                int serialLength = sizeof(byte);
                int dataLength = this.Data.Length;
                int checkLength = sizeof(byte);

                if (this.FixedData != null)
                {
                    dataLength = this.FixedData.Length;
                }

                result = controllerLength + lengthLength + typeLength + countLength + serialLength + dataLength + checkLength;

                return result;
            }
        }

        /// <summary>
        /// 帧固定数据长度
        /// </summary>
        public int FrameFixedLength
        {
            get
            {
                int result = 0;

                int controllerLength = sizeof(byte);
                int lengthLength = sizeof(ushort);
                int typeLength = sizeof(ushort);
                int countLength = sizeof(byte);
                int serialLength = sizeof(byte);
                int checkLength = sizeof(byte);

                result = controllerLength + lengthLength + typeLength + countLength + serialLength + checkLength;
                return result;
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns>帧数据</returns>
        public byte[] Encode()
        {
            byte[] result = new byte[this.FrameLength];

            byte[] controllers = { this.Controller };
            byte[] lengths = BitConverter.GetBytes(this.Length);
            byte[] types = BitConverter.GetBytes(this.Type);
            byte[] counts = { this.Count };
            byte[] serials = { this.Serial };
            byte[] datas = this.Data;
            byte[] checks = { this.Check };

            if (this.FixedData != null)
            {
                datas = this.FixedData;
            }
            Array.Reverse(lengths);
            Array.Reverse(types);

            byte[][] values = { controllers, lengths, types, counts, serials, datas, checks };

            int address = 0;
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values[i].Length; j++)
                {
                    result[address] = values[i][j];
                    address++;
                }
            }

            return result;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="frame">帧数据</param>
        public void Decode(byte[] frame)
        {
            this.Data = new byte[this.Minimum];
            int minimumFrameLength = this.FrameLength;
            this.Data = new byte[this.Maximum];
            int maximumFrameLength = this.FrameLength;
            if ((frame == null) || (frame.Length < minimumFrameLength) || (frame.Length > maximumFrameLength))
            {
                throw new OverflowException();
            }
            else
            {
                int address = 0;

                int dataLength = frame.Length - this.FrameFixedLength;

                byte[] controllers = new byte[sizeof(byte)];
                Array.Copy(frame, address, controllers, 0, sizeof(byte));
                address += sizeof(byte);

                byte[] lengths = new byte[sizeof(ushort)];
                Array.Copy(frame, address, lengths, 0, sizeof(ushort));
                address += sizeof(ushort);

                byte[] types = new byte[sizeof(ushort)];
                Array.Copy(frame, address, types, 0, sizeof(ushort));
                address += sizeof(ushort);

                byte[] counts = new byte[sizeof(byte)];
                Array.Copy(frame, address, counts, 0, sizeof(byte));
                address += sizeof(byte);

                byte[] serials = new byte[sizeof(byte)];
                Array.Copy(frame, address, serials, 0, sizeof(byte));
                address += sizeof(byte);

                byte[] datas = new byte[dataLength];
                Array.Copy(frame, address, datas, 0, dataLength);
                address += dataLength;

                byte[] checks = new byte[sizeof(byte)];
                Array.Copy(frame, address, checks, 0, sizeof(byte));
                address += sizeof(byte);

                Array.Reverse(lengths);
                Array.Reverse(types);
                this.Count = counts[0];
                this.Serial = serials[0];
                this.Data = datas;

                if (controllers[0] != this.Controller)
                {
                    throw new OverflowException();
                }
                if (BitConverter.ToUInt16(lengths, 0) != this.Length)
                {
                    throw new OverflowException();
                }
                if (BitConverter.ToUInt16(types, 0) != this.Type)
                {
                    throw new OverflowException();
                }
                if (counts[0] < serials[0])
                {
                    throw new OverflowException();
                }
                if (checks[0] != this.Check)
                {
                    throw new OverflowException();
                }
                if (this.FixedData != null)
                {
                    if (datas.Length != this.FixedData.Length)
                    {
                        throw new OverflowException();
                    }
                    else
                    {
                        for (int i = 0; i < datas.Length; i++)
                        {
                            if (datas[i] != this.FixedData[i])
                            {
                                throw new OverflowException();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 正确应答指令
        /// </summary>
        /// <returns>指令集合</returns>
        public virtual List<AbstractInstruction> RightAnswer()
        {
            return null;
        }

        /// <summary>
        /// 错误应答指令
        /// </summary>
        /// <returns>指令集合</returns>
        public virtual List<AbstractInstruction> ErrorAnswer()
        {
            return null;
        }

        /// <summary>
        /// 应答指令
        /// </summary>
        /// <returns>指令集合</returns>
        public List<AbstractInstruction> Answer(bool type)
        {
            List<AbstractInstruction> results = null;

            if (type)
            {
                results = this.RightAnswer();
            }
            else
            {
                results = this.ErrorAnswer();
            }

            return results;
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="instructionTask">指令任务</param>
        /// <returns></returns>
        public virtual ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();
            return result;
        }
    }
}
