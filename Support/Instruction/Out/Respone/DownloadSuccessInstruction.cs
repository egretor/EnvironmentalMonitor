using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Response
{
    /// <summary>
    /// 下载环境数据成功应答指令
    /// </summary>
    public class DownloadSuccessInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return Constant.FRAME_DATA_MINIMUM;
            }
        }

        public override ushort Maximum
        {
            get
            {
                return Constant.FRAME_DATA_MAXIMUM;
            }
        }

        public override byte Controller
        {
            get
            {
                return 0xA3;
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
        public DownloadSuccessInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="detectors">探头</param>
        public DownloadSuccessInstruction(List<Detector> detectors)
        {
            if (detectors != null)
            {
                int address = 0;
                byte[] datas = new byte[detectors.Count * Detector.DATA_VALUE_LENGTH];
                for (int i = 0; i < detectors.Count; i++)
                {
                    if (detectors[i] != null)
                    {
                        byte[] values = detectors[i].Encode();
                        datas[address] = values[0];
                        address++;
                        datas[address] = values[1];
                        address++;
                    }
                }
                this.Data = new byte[address];
                Array.Copy(datas, 0, this.Data, 0, address);
            }
        }

        /// <summary>
        /// 探头
        /// </summary>
        public List<Detector> Detectors
        {
            get
            {
                List<Detector> results = null;

                if ((this.Data != null) && (this.Data.Length >= this.Minimum) && (this.Data.Length <= this.Maximum))
                {
                    int count = this.Data.Length / Detector.DATA_VALUE_LENGTH;
                    results = new List<Detector>();
                    for (int i = 0; i < count; i++)
                    {
                        byte[] values = new byte[Detector.DATA_VALUE_LENGTH];
                        values[0] = this.Data[i * Detector.DATA_VALUE_LENGTH];
                        values[1] = this.Data[i * Detector.DATA_VALUE_LENGTH + 1];
                        Detector detector = new Detector();
                        detector.Decode(values);
                    }
                }

                return results;
            }
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();

            result.Done = true;
            result.Message = "查询检测仪探头数据成功！";

            return result;
        }
    }
}
