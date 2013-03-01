using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置时间指令
    /// </summary>
    public class TimeInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 0x0002;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TimeInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="time">时间</param>
        public TimeInstruction(DateTime time)
        {
            const int byteBitWide = 4;
            string value = time.ToString("ssmmHHddMMyy");
            byte[] datas = new byte[this.Minimum];
            int address = 0;
            for (int i = 0; i < datas.Length; i++)
            {
                byte highValue = (byte)(byte.Parse(value[address].ToString()) << byteBitWide);
                address++;
                byte lowValue = byte.Parse(value[address].ToString());
                address++;
                datas[i] = (byte)(highValue + lowValue);
            }
            this.Data = datas;
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get
            {
                const int byteBitWide = 4;
                const int decade = 10;
                const int hundred = 100;
                DateTime result = DateTime.MinValue;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    byte highSecond = (byte)(this.Data[0] >> byteBitWide);
                    byte lowSecond = (byte)(this.Data[0] & 0x0F);
                    byte highMinute = (byte)(this.Data[1] >> byteBitWide);
                    byte lowMinute = (byte)(this.Data[1] & 0x0F);
                    byte highHour = (byte)(this.Data[2] >> byteBitWide);
                    byte lowHour = (byte)(this.Data[2] & 0x0F);
                    byte highDay = (byte)(this.Data[3] >> byteBitWide);
                    byte lowDay = (byte)(this.Data[3] & 0x0F);
                    byte highMonth = (byte)(this.Data[4] >> byteBitWide);
                    byte lowMonth = (byte)(this.Data[4] & 0x0F);
                    byte highYear = (byte)(this.Data[5] >> byteBitWide);
                    byte lowYear = (byte)(this.Data[5] & 0x0F);

                    int year = (DateTime.Now.Year / hundred) * hundred + highYear * decade + lowYear;
                    int month = highMonth * decade + lowMonth;
                    int day = highDay * decade + lowDay;
                    int hour = highHour * decade + lowHour;
                    int minute = highMinute * decade + lowMinute;
                    int second = highSecond * decade + lowSecond;

                    result = new DateTime(year, month, day, hour, minute, second, 0);
                }

                return result;
            }
        }

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.TimeErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                DateTime now = DateTime.Now;
                AbstractInstruction instruction = new Out.Response.TimeSuccessInstruction(now);

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
