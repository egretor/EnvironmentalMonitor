using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置环境数据上传时间间隔指令
    /// </summary>
    public class IntervalInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
    {
        public override InstructionTypes InstructionType
        {
            get
            {
                return InstructionTypes.OutRequest;
            }
        }

        public override ushort Minimum
        {
            get
            {
                return 2;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return 2;
            }
        }

        public override byte Controller
        {
            get
            {
                return 0x21;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0x0010;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IntervalInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="second">秒</param>
        public IntervalInstruction(ushort second)
        {
            byte[] datas = BitConverter.GetBytes(second);
            Array.Reverse(datas);
            this.Data = datas;
        }

        /// <summary>
        /// 秒
        /// </summary>
        public ushort Second
        {
            get
            {
                ushort result = 0;

                byte[] datas = this.Data;
                Array.Reverse(datas);
                result = BitConverter.ToUInt16(datas, 0);

                return result;
            }
        }

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.IntervalErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.IntervalSuccessInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = null;

            if (Variable.Debug)
            {
                result = new ProcessResult();

                Random random = new Random();
                int randomValue = random.Next(0, 2);
                if (randomValue == 0)
                {
                    result.Done = false;
                }
                else
                {
                    result.Done = true;
                }
            }

            return result;
        }
    }
}
