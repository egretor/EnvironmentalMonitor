using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
using System.Text.RegularExpressions;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置MAC指令
    /// </summary>
    public class MacInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 0x21;
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
        public MacInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="MAC">MAC</param>
        public MacInstruction(string MAC)
        {
            //System.Globalization.NumberStyles.AllowHexSpecifier
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

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MacErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MacSuccessInstruction(this.MAC);

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
