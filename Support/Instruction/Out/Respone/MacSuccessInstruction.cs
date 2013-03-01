using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
using System.Text.RegularExpressions;

namespace EnvironmentalMonitor.Support.Instruction.Out.Response
{
    /// <summary>
    /// 设置MAC成功应答指令
    /// </summary>
    public class MacSuccessInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 6;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return 6;
            }
        }

        public override byte Controller
        {
            get
            {
                return 0xA1;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0x0001;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MacSuccessInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="MAC">MAC</param>
        public MacSuccessInstruction(string MAC)
        {
            const int hexLength = 2;
            if ((!string.IsNullOrEmpty(MAC)) && (MAC.Length <= (hexLength * this.Minimum)) && (MAC.Length >= (hexLength * this.Maximum)))
            {
                string pattern = "^[0-9a-fA-F]+$";
                if (Regex.IsMatch(MAC, pattern))
                {
                    int length = this.Minimum;
                    byte[] datas = new byte[length];
                    for (int i = 0; i < length; i++)
                    {
                        datas[i] = byte.Parse(MAC.Substring(i * hexLength, hexLength).ToUpper(), NumberStyles.AllowHexSpecifier);
                    }
                    this.Data = datas;
                }
            }
        }

        /// <summary>
        /// MAC
        /// </summary>
        public string MAC
        {
            get
            {
                string result = string.Empty;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < this.Data.Length; i++)
                    {
                        stringBuilder.Append(this.Data[i].ToString("X2"));
                    }

                    result = stringBuilder.ToString();
                }

                return result;
            }
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();

            result.Done = true;
            result.Message = "设置检测仪物理地址成功！";

            return result;
        }
    }
}
