using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Instruction.Out.Response
{
    /// <summary>
    /// 设置手机号码错误应答指令
    /// </summary>
    public class MobileErrorInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
    {
        public override InstructionTypes InstructionType
        {
            get
            {
                return InstructionTypes.OutResponse;
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

        public override byte[] FixedData
        {
            get
            {
                return new byte[] { 0x01 };
            }
        }

        public override byte Controller
        {
            get
            {
                return 0xE2;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0x0102;
            }
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();

            result.Done = false;
            result.Message = "设置检测仪手机报警接受短信手机号码失败！";

            return result;
        }
    }
}
