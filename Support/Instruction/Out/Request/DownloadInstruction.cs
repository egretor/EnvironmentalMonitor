using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 下载环境数据指令
    /// </summary>
    public class DownloadInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 4;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return 4;
            }
        }

        public override byte Controller
        {
            get
            {
                return 0x23;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0xAA01;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DownloadInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IP">IP</param>
        public DownloadInstruction(string IP)
        {
            IPAddress outAddress = IPAddress.Any;
            if (IPAddress.TryParse(IP, out outAddress))
            {
                byte[] datas = IPAddress.Parse(IP).GetAddressBytes();
                this.Data = datas;
            }
        }

        /// <summary>
        /// IP
        /// </summary>
        public string IP
        {
            get
            {
                string result = string.Empty;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(this.Data[0].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[1].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[2].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[3].ToString());

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
                AbstractInstruction instruction = new Out.Response.DownloadErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                List<Detector> detectors = new List<Detector>();
                // 返回探头数据
                AbstractInstruction instruction = new Out.Response.DownloadSuccessInstruction(detectors);

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
