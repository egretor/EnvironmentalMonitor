using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace EnvironmentalMonitor.Support.Instruction.Out.Response
{
    /// <summary>
    /// 设置IP成功应答指令
    /// </summary>
    public class IpSuccessInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 0xA1;
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
        public IpSuccessInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="IP">IP</param>
        /// <param name="netmask">掩码</param>
        /// <param name="gateway">网关</param>
        public IpSuccessInstruction(string IP, string netmask, string gateway)
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

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();

            result.Done = true;
            result.Message = "设置检测仪IP地址成功！";

            return result;
        }
    }
}
