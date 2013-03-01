using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.In.Request
{
    /// <summary>
    /// 短信报警数据
    /// </summary>
    public class MessageInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
    {
        public override InstructionTypes InstructionType
        {
            get
            {
                return InstructionTypes.InRequest;
            }
        }

        public override ushort Minimum
        {
            get
            {
                return 8;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return 8;
            }
        }

        public override byte Controller
        {
            get
            {
                return 0xAA;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0xAA02;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="values">探头</param>
        public MessageInstruction(byte code, bool result, DateTime sendTime)
        {
            const int byteBitWide = 4;
            string value = sendTime.ToString("ssmmHHddMMyy");
            byte[] datas = new byte[this.Minimum];
            datas[0] = code;
            if (result)
            {
                datas[1] = 0x00;
            }
            else
            {
                datas[1] = 0x01;
            }
            int address = 0;
            int begin = 2;
            for (int i = begin; i < datas.Length; i++)
            {
                byte highValue = (byte)(byte.Parse(value[address].ToString()) << byteBitWide);
                address++;
                byte lowValue = byte.Parse(value[address].ToString());
                address++;
                datas[i] = (byte)(highValue + lowValue);
            }
            this.Data = datas;
        }

        public byte Code
        {
            get
            {
                byte result = 0x00;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    result = this.Data[0];
                }

                return result;
            }
        }

        public bool Result
        {
            get
            {
                bool result = true;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    if (this.Data[1] != 0x00)
                    {
                        result = false;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime SendTime
        {
            get
            {
                const int byteBitWide = 4;
                const int decade = 10;
                const int hundred = 100;
                DateTime result = DateTime.MinValue;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    byte highSecond = (byte)(this.Data[2] >> byteBitWide);
                    byte lowSecond = (byte)(this.Data[2] & 0x0F);
                    byte highMinute = (byte)(this.Data[3] >> byteBitWide);
                    byte lowMinute = (byte)(this.Data[3] & 0x0F);
                    byte highHour = (byte)(this.Data[4] >> byteBitWide);
                    byte lowHour = (byte)(this.Data[4] & 0x0F);
                    byte highDay = (byte)(this.Data[5] >> byteBitWide);
                    byte lowDay = (byte)(this.Data[5] & 0x0F);
                    byte highMonth = (byte)(this.Data[6] >> byteBitWide);
                    byte lowMonth = (byte)(this.Data[6] & 0x0F);
                    byte highYear = (byte)(this.Data[7] >> byteBitWide);
                    byte lowYear = (byte)(this.Data[7] & 0x0F);

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

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();
            result.Done = false;
            result.Message = string.Empty;

            if ((instructionTask != null) && (instructionTask.Instructions != null) && (instructionTask.Instructions.Count > 0))
            {
                for (int i = 0; i < instructionTask.Instructions.Count; i++)
                {
                    MessageInstruction instruction = instructionTask.Instructions[i] as MessageInstruction;
                    if (instruction != null)
                    {
                        string userId = this.GetType().Name;
                        DateTime now = DateTime.Now;

                        MessageCache messageCache = new MessageCache();

                        messageCache.InsertUserId = ip;
                        messageCache.InsertTime = now;
                        messageCache.UpdateUserId = userId;
                        messageCache.UpdateTime = now;
                        messageCache.Remark = string.Empty;
                        messageCache.Validity = true;

                        messageCache.Ip = ip;
                        messageCache.Code = instruction.Code;
                        messageCache.Result = instruction.Result;
                        messageCache.SendTime = instruction.SendTime;

                        MachineBusiness machineBusiness = new MachineBusiness();
                        Machine machine = machineBusiness.QueryByIp(ip);
                        if (machine != null)
                        {
                            messageCache.MachineId = machine.Guid;
                        }

                        messageCache.RefreshTime = now;

                        MessageCacheBusiness messageCacheBusiness = new MessageCacheBusiness();
                        result.Done = messageCacheBusiness.Insert(messageCache);
                    }
                }
            }

            return result;
        }

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();
            Response.MessageErrorInstruction instruction = new Response.MessageErrorInstruction();
            results.Add(instruction);
            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();
            Response.MessageSuccessInstruction instruction = new Response.MessageSuccessInstruction();
            results.Add(instruction);
            return results;
        }
    }
}
