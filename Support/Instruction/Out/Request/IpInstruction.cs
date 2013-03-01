using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置IP指令
    /// </summary>
    public class IpInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 12;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return 12;
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
                return 0x0000;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IpInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IP">IP</param>
        /// <param name="netmask">掩码</param>
        /// <param name="gateway">网关</param>
        public IpInstruction(string IP, string netmask, string gateway)
        {
            IPAddress outAddress = IPAddress.Any;
            if (IPAddress.TryParse(IP, out outAddress) && IPAddress.TryParse(netmask, out outAddress) && IPAddress.TryParse(gateway, out outAddress))
            {
                byte[] ips = IPAddress.Parse(IP).GetAddressBytes();
                byte[] netmasks = IPAddress.Parse(netmask).GetAddressBytes();
                byte[] gateways = IPAddress.Parse(gateway).GetAddressBytes();

                int address = 0;
                byte[] datas = new byte[this.Minimum];

                Array.Copy(ips, 0, datas, address, ips.Length);
                address += ips.Length;
                Array.Copy(netmasks, 0, datas, address, netmasks.Length);
                address += netmasks.Length;
                Array.Copy(gateways, 0, datas, address, gateways.Length);
                address += gateways.Length;

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

        /// <summary>
        /// 掩码
        /// </summary>
        public string Netmask
        {
            get
            {
                string result = string.Empty;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(this.Data[4].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[5].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[6].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[7].ToString());

                    result = stringBuilder.ToString();
                }

                return result;
            }
        }

        /// <summary>
        /// 网关
        /// </summary>
        public string Gateway
        {
            get
            {
                string result = string.Empty;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(this.Data[8].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[9].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[10].ToString());
                    stringBuilder.Append(".");
                    stringBuilder.Append(this.Data[11].ToString());

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
                AbstractInstruction instruction = new Out.Response.IpErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.IpSuccessInstruction(this.IP, this.Netmask, this.Gateway);

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
