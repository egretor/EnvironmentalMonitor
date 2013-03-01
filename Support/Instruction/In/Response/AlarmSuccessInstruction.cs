using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Instruction.In.Response
{
    /// <summary>
    /// 环境数据报警成功应答指令
    /// </summary>
    public class AlarmSuccessInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
    {
        public override InstructionTypes InstructionType
        {
            get
            {
                return InstructionTypes.InResponse;
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
                return 0x2A;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0xAA02;
            }
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();

            result.Done = true;
            result.Message = string.Empty;

            return result;
        }
    }
}
