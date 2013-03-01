using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.Out.Request
{
    /// <summary>
    /// 设置探测器阀值指令
    /// </summary>
    public class MaximumThresholdInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 0x31;
            }
        }

        public override ushort Type
        {
            get
            {
                return 0xAA03;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MaximumThresholdInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="detectors">探头</param>
        public MaximumThresholdInstruction(List<Detector> detectors)
        {
            if (detectors != null)
            {
                int address = 0;
                byte[] datas = new byte[detectors.Count * Detector.THRESHOLD_VALUE_LENGTH];
                for (int i = 0; i < detectors.Count; i++)
                {
                    if (detectors[i] != null)
                    {
                        byte[] values = detectors[i].EncodeMaximum();
                        datas[address] = values[0];
                        address++;
                        datas[address] = values[1];
                        address++;
                        datas[address] = values[2];
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
                    int count = this.Data.Length / Detector.THRESHOLD_VALUE_LENGTH;
                    results = new List<Detector>();
                    for (int i = 0; i < count; i++)
                    {
                        byte[] values = new byte[Detector.THRESHOLD_VALUE_LENGTH];

                        Detector detector = new Detector();
                        values[0] = this.Data[i * Detector.THRESHOLD_VALUE_LENGTH + 0];
                        values[1] = this.Data[i * Detector.THRESHOLD_VALUE_LENGTH + 1];
                        values[2] = this.Data[i * Detector.THRESHOLD_VALUE_LENGTH + 2];
                        detector.DecodeMaximum(values);
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
                AbstractInstruction instruction = new Out.Response.MaximumThresholdErrorInstruction();

                results.Add(instruction);
            }

            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();

            if (Variable.Debug)
            {
                AbstractInstruction instruction = new Out.Response.MaximumThresholdSuccessInstruction();

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
