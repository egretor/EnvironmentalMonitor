using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置手机号码指令
    /// </summary>
    public class MobileInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 18;
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
                return 0x0102;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MobileInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="first">手机号码一</param>
        /// <param name="second">手机号码二</param>
        /// <param name="third">手机号码三</param>
        public MobileInstruction(string first, string second, string third)
        {
            const int mobileLength = 11;
            const int bcdLength = 2;
            const int byteBitWide = 4;
            int count = 0;
            byte[] firstDatas = null;
            byte[] secondDatas = null;
            byte[] thirdDatas = null;
            int address = 0;
            string pattern = "^[0-9]+$";

            if (!string.IsNullOrEmpty(first) && (first.Length == mobileLength))
            {
                if (Regex.IsMatch(first, pattern))
                {
                    address = 0;
                    firstDatas = new byte[this.Minimum];
                    count += this.Minimum;
                    first = string.Format("0{0}", first);
                    for (int i = 0; i < (first.Length / bcdLength); i++)
                    {
                        byte highValue = (byte)(byte.Parse(first[address].ToString()) << byteBitWide);
                        address++;
                        byte lowValue = (byte)byte.Parse(first[address].ToString());
                        address++;
                        firstDatas[i] = (byte)(highValue + lowValue);
                    }
                }
            }

            if (!string.IsNullOrEmpty(second) && (second.Length == mobileLength))
            {
                if (Regex.IsMatch(second, pattern))
                {
                    address = 0;
                    secondDatas = new byte[this.Minimum];
                    count += this.Minimum;
                    second = string.Format("0{0}", second);
                    for (int i = 0; i < (second.Length / bcdLength); i++)
                    {
                        byte highValue = (byte)(byte.Parse(second[address].ToString()) << byteBitWide);
                        address++;
                        byte lowValue = (byte)byte.Parse(second[address].ToString());
                        address++;
                        secondDatas[i] = (byte)(highValue + lowValue);
                    }
                }
            }

            if (!string.IsNullOrEmpty(third) && (third.Length == mobileLength))
            {
                if (Regex.IsMatch(third, pattern))
                {
                    address = 0;
                    thirdDatas = new byte[this.Minimum];
                    count += this.Minimum;
                    third = string.Format("0{0}", third);
                    for (int i = 0; i < (third.Length / bcdLength); i++)
                    {
                        byte highValue = (byte)(byte.Parse(third[address].ToString()) << byteBitWide);
                        address++;
                        byte lowValue = (byte)byte.Parse(third[address].ToString());
                        address++;
                        thirdDatas[i] = (byte)(highValue + lowValue);
                    }
                }
            }

            address = 0;
            this.Data = new byte[count];
            byte[][] datas = { firstDatas, secondDatas, thirdDatas };
            for (int i = 0; i < datas.Length; i++)
            {
                if (datas[i] != null)
                {
                    for (int j = 0; j < datas[i].Length; j++)
                    {
                        this.Data[address] = datas[i][j];
                        address++;
                    }
                }
            }
        }

        /// <summary>
        /// 手机号码集合
        /// </summary>
        public string[] Mobiles
        {
            get
            {
                const int byteBitWide = 4;
                string[] results = new string[3];

                int address = 0;
                StringBuilder stringBuilder = new StringBuilder();
                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    for (int i = 0; i < results.Length; i++)
                    {
                        if (this.Data.Length >= this.Minimum * (i + 1))
                        {
                            stringBuilder = new StringBuilder();
                            for (int j = 0; j < this.Minimum; j++)
                            {
                                byte high = (byte)(this.Data[address] >> byteBitWide);
                                byte low = (byte)(this.Data[address] & 0x0F);
                                address++;
                                if (j != 0)
                                {
                                    stringBuilder.Append(high);
                                }
                                stringBuilder.Append(low);
                            }
                            results[i] = stringBuilder.ToString();
                        }
                    }
                }

                return results;
            }
        }

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MobileErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MobileSuccessInstruction();

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
