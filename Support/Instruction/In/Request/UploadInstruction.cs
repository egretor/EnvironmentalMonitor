using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Instruction.In.Request
{
    /// <summary>
    /// 上传环境数据指令
    /// </summary>
    public class UploadInstruction : EnvironmentalMonitor.Support.Instruction.AbstractInstruction
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
                return 0xAA;
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
        public UploadInstruction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="detectors">探头</param>
        public UploadInstruction(List<Detector> detectors)
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
                        detector.DetectorType = new DetectorType();
                        detector.DetectorType.Type = DetectorTypes.Unknown;
                        detector.Decode(values);
                        results.Add(detector);
                    }
                }

                return results;
            }
        }

        public override ProcessResult Process(string ip, InstructionTask instructionTask)
        {
            ProcessResult result = new ProcessResult();
            result.Done = false;
            result.Message = string.Empty;

            bool right = true;
            List<DataCache> dataCaches = new List<DataCache>();
            List<Detector> detectors = new List<Detector>();
            if ((instructionTask != null) && (instructionTask.Instructions != null) && (instructionTask.Instructions.Count > 0))
            {
                for (int i = 0; i < instructionTask.Instructions.Count; i++)
                {
                    UploadInstruction instruction = instructionTask.Instructions[i] as UploadInstruction;
                    if (instruction == null)
                    {
                        right = false;
                        break;
                    }
                    else
                    {
                        if (instruction.Detectors != null)
                        {
                            for (int j = 0; j < instruction.Detectors.Count; j++)
                            {
                                detectors.Add(instruction.Detectors[j]);
                            }
                        }
                    }
                }
            }
            if (right)
            {
                MachineBusiness machineBusiness = new MachineBusiness();
                Machine machine = machineBusiness.QueryByIp(ip);
                if (machine != null)
                {
                    DetectorBusiness detectorBusiness = new DetectorBusiness();
                    List<Detector> currentDetectors = detectorBusiness.QueryByMachine(machine.Guid);
                    if ((currentDetectors != null) && (currentDetectors.Count == detectors.Count))
                    {
                        for (int i = 0; i < detectors.Count; i++)
                        {
                            string userId = this.GetType().Name;
                            DateTime now = DateTime.Now;

                            DataCache dataCache = new DataCache();

                            dataCache.InsertUserId = ip;
                            dataCache.InsertTime = now;
                            dataCache.UpdateUserId = userId;
                            dataCache.UpdateTime = now;
                            dataCache.Remark = string.Empty;
                            dataCache.Validity = true;
                            dataCache.DetectorId = currentDetectors[i].Guid;

                            byte[] values = BitConverter.GetBytes(detectors[i].Value);
                            Array.Reverse(values);
                            currentDetectors[i].Decode(values);
                            dataCache.Value = currentDetectors[i].Value;

                            dataCache.RefreshTime = now;
                            dataCache.Newest = true;

                            dataCaches.Add(dataCache);
                        }
                    }
                }

                DataCacheBusiness dataCacheBusiness = new DataCacheBusiness();
                result.Done = dataCacheBusiness.Insert(dataCaches);
            }

            return result;
        }

        public override List<AbstractInstruction> ErrorAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();
            Response.UploadErrorInstruction instruction = new Response.UploadErrorInstruction();
            results.Add(instruction);
            return results;
        }

        public override List<AbstractInstruction> RightAnswer()
        {
            List<AbstractInstruction> results = new List<AbstractInstruction>();
            Response.UploadSuccessInstruction instruction = new Response.UploadSuccessInstruction();
            results.Add(instruction);
            return results;
        }
    }
}
