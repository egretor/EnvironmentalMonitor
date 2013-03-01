using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Instruction.Out.Response
{
    /// <summary>
    /// 设置手机报警成功应答指令
    /// </summary>
    public class MobileAlarmSuccessInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return new byte[] { 0x00 };
            }
        }

        public override byte Controller
        {
            get
            {
                return 0xA2;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0x0103;
            }
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();

            result.Done = true;
            result.Message = "设置检测仪手机报警成功！";

            return result;
        }
    }
}
