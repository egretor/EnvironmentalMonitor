using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置手机报警指令
    /// </summary>
    public class MobileAlarmInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 1;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return 1;
            }
        }

        public override byte Controller
        {
            get
            {
                return 0x22;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0x0103;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MobileAlarmInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="work">工作</param>
        public MobileAlarmInstruction(bool work)
        {
            byte[] datas = new byte[this.Minimum];
            if (work)
            {
                datas[0] = 0xFF;
            }
            else
            {
                datas[0] = 0x00;
            }
            this.Data = datas;
        }

        public bool Work
        {
            get
            {
                bool result = false;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    if (this.Data[0] == 0xFF)
                    {
                        result = true;
                    }
                }

                return result;
            }
        }

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MobileAlarmErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MobileAlarmSuccessInstruction();

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
