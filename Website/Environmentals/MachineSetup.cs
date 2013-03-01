using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Net.Sockets;
using System.Text;

using EnvironmentalMonitor.Support.Device;
using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Instruction.Out.Request;
using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Environmentals
{
    public class MachineSetup
    {
        public string Interval(Machine value)
        {
            string result = null;

            string ipValue = value.Ip;
            ushort intervalValue = (ushort)value.Interval;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            IntervalInstruction instruction = new IntervalInstruction(intervalValue);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }

        public string Ip(Machine value, string currentIp)
        {
            string result = null;
            StringBuilder stringBuilder = new StringBuilder();

            string ipValue = value.Ip;
            string netmaskValue = value.Netmask;
            string gatewayValue = value.Gateway;

            IPAddress ip = IPAddress.Parse(currentIp);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            IpInstruction instruction = new IpInstruction(ipValue, netmaskValue, gatewayValue);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                stringBuilder.Append(processResult.Message);
                if (!processResult.Done)
                {
                    MachineBusiness business = new MachineBusiness();
                    value.Ip = currentIp;
                    bool done = business.Update(value);
                    if (done)
                    {
                        stringBuilder.Append("恢复检测仪IP地址成功！");
                    }
                    else
                    {
                        stringBuilder.Append(string.Format("恢复检测仪IP地址失败，请重新设置检测仪IP地址为{0}！", currentIp));
                    }
                }
            }
            result = stringBuilder.ToString();

            return result;
        }

        public string Mac(Machine value)
        {
            string result = null;

            string ipValue = value.Ip;
            string macValue = value.Mac;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            MacInstruction instruction = new MacInstruction(macValue);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }

        public string Time(Machine value)
        {
            string result = null;

            string ipValue = value.Ip;
            DateTime now = DateTime.Now;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            TimeInstruction instruction = new TimeInstruction(now);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }

        private string MinimumThreshold(Machine value, List<Detector> detectors)
        {
            string result = null;

            string ipValue = value.Ip;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();


            MinimumThresholdInstruction instruction = new MinimumThresholdInstruction(detectors);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }

        private string MaximumThreshold(Machine value, List<Detector> detectors)
        {
            string result = null;

            string ipValue = value.Ip;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            MaximumThresholdInstruction instruction = new MaximumThresholdInstruction(detectors);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }

        public string Threshold(Machine value)
        {
            string result = null;

            StringBuilder stringBuilder = new StringBuilder();
            DetectorBusiness detectorBusiness = new DetectorBusiness();
            List<Detector> detectors = detectorBusiness.QueryByMachine(value.Guid);

            if ((detectors != null) && (detectors.Count > 0))
            {
                bool rightAddress = true;
                for (int i = 0; i < detectors.Count; i++)
                {
                    if (detectors[i].Serial != i)
                    {
                        rightAddress = false;
                        break;
                    }
                }

                if (rightAddress)
                {
                    stringBuilder.Append(this.MinimumThreshold(value, detectors));
                    stringBuilder.Append(this.MaximumThreshold(value, detectors));
                }
                else
                {
                    stringBuilder.Append("探头地址错误！");
                }
            }

            result = stringBuilder.ToString();

            return result;
        }

        public string MobileAlarm(Machine value)
        {
            string result = null;

            string ipValue = value.Ip;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            MobileAlarmInstruction instruction = new MobileAlarmInstruction(value.Alarm);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }

        public string Mobile(Machine value)
        {
            string result = null;

            string ipValue = value.Ip;

            IPAddress ip = IPAddress.Parse(ipValue);
            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

            MobileInstruction instruction = new MobileInstruction(value.MobileA, value.MobileB, value.MobileC);
            instructions.Add(instruction);

            ProcessResult processResult = Terminal.ExecuteInstruction(remoteEP, instructions);
            if (processResult != null)
            {
                result = processResult.Message;
            }

            return result;
        }
    }
}