using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    /// <summary>
    /// 探头
    /// </summary>
    public class Detector : EnvironmentalMonitor.Support.Module.AbstractModule
    {
        public const int THRESHOLD_VALUE_LENGTH = 3;
        public const int DATA_VALUE_LENGTH = 2;

        private string _MachineId;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineId
        {
            get
            {
                return this._MachineId;
            }
            set
            {
                this._MachineId = value;
            }
        }

        private string _DetectorTypeId;
        /// <summary>
        /// 探头类型编号
        /// </summary>
        public string DetectorTypeId
        {
            get
            {
                return this._DetectorTypeId;
            }
            set
            {
                this._DetectorTypeId = value;
            }
        }

        private int _Serial;
        /// <summary>
        /// 序号
        /// </summary>
        public int Serial
        {
            get
            {
                return this._Serial;
            }
            set
            {
                this._Serial = value;
            }
        }

        private int _MinimumA;
        /// <summary>
        /// 最小值A
        /// </summary>
        public int MinimumA
        {
            get
            {
                return this._MinimumA;
            }
            set
            {
                this._MinimumA = value;
            }
        }

        private int _MaximumA;
        /// <summary>
        /// 最大值A
        /// </summary>
        public int MaximumA
        {
            get
            {
                return this._MaximumA;
            }
            set
            {
                this._MaximumA = value;
            }
        }

        private int _MinimumB;
        /// <summary>
        /// 最小值B
        /// </summary>
        public int MinimumB
        {
            get
            {
                return this._MinimumB;
            }
            set
            {
                this._MinimumB = value;
            }
        }

        private int _MaximumB;
        /// <summary>
        /// 最大值B
        /// </summary>
        public int MaximumB
        {
            get
            {
                return this._MaximumB;
            }
            set
            {
                this._MaximumB = value;
            }
        }

        private int _PositionX;
        /// <summary>
        /// X坐标位置
        /// </summary>
        public int PositionX
        {
            get
            {
                return this._PositionX;
            }
            set
            {
                this._PositionX = value;
            }
        }

        private int _PositionY;
        /// <summary>
        /// Y坐标位置
        /// </summary>
        public int PositionY
        {
            get
            {
                return this._PositionY;
            }
            set
            {
                this._PositionY = value;
            }
        }

        private Machine _Machine;
        public Machine Machine
        {
            get
            {
                return this._Machine;
            }
            set
            {
                this._Machine = value;
            }
        }

        private DetectorType _DetectorType;
        public DetectorType DetectorType
        {
            get
            {
                return this._DetectorType;
            }
            set
            {
                this._DetectorType = value;
            }
        }

        public string Name
        {
            get
            {
                string result = string.Empty;

                if (this.DetectorType != null)
                {
                    string serial = this.Serial.ToString("000");
                    string name = this.DetectorType.Name;
                    result = string.Format("（{0}）{1}", serial, name);
                }

                return result;
            }
        }

        public string NormalHref
        {
            get
            {
                string result = string.Empty;

                if (this.DetectorType != null)
                {
                    result = this.DetectorType.NormalHref;
                }

                return result;
            }
        }

        public string ErrorHref
        {
            get
            {
                string result = string.Empty;

                if (this.DetectorType != null)
                {
                    result = this.DetectorType.ErrorHref;
                }

                return result;
            }
        }

        private ushort _Value;
        /// <summary>
        /// 值
        /// </summary>
        public ushort Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }

        private DateTime _RefreshTime;
        /// <summary>
        /// 刷新时间
        /// </summary>
        public DateTime RefreshTime
        {
            get
            {
                return this._RefreshTime;
            }
            set
            {
                this._RefreshTime = value;
            }
        }

        public string RefreshTimeText
        {
            get
            {
                return this.RefreshTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public bool Error
        {
            get
            {
                bool result = true;

                if (this.DetectorType != null)
                {
                    switch (this.DetectorType.Type)
                    {
                        case DetectorTypes.Switch:
                            if (this.Value == this.MaximumA)
                            {
                                result = false;
                            }
                            break;
                        case DetectorTypes.DoubleArea:
                            byte[] values = BitConverter.GetBytes(this.Value);
                            if (((values[0] > this.MinimumA) && (values[0] < this.MaximumA)) && ((values[1] > this.MinimumB) && (values[1] < this.MaximumB)))
                            {
                                result = false;
                            }
                            break;
                    }
                }

                return result;
            }
        }

        public string ViewValue
        {
            get
            {
                string result = string.Empty;

                if (this.DetectorType != null)
                {
                    switch (this.DetectorType.Type)
                    {
                        case DetectorTypes.Switch:
                            if (this.Value == this.MinimumA)
                            {
                                result = string.Format("<font style=\"color:#ff0000\">{0}</font>", this.DetectorType.DescriptionA);
                            }
                            else
                            {
                                result = this.DetectorType.DescriptionB;
                            }
                            break;
                        case DetectorTypes.DoubleArea:
                            StringBuilder stringBuilder = new StringBuilder();
                            byte[] values = BitConverter.GetBytes(this.Value);
                            if ((values[0] > this.MinimumA) && (values[0] < this.MaximumA))
                            {
                                stringBuilder.Append(string.Format("{0}：{1}{2}", this.DetectorType.DescriptionA, values[0], this.DetectorType.UnitA));
                            }
                            else
                            {
                                stringBuilder.Append("<font style=\"color:#ff0000\">");
                                stringBuilder.Append(string.Format("{0}：{1}{2}", this.DetectorType.DescriptionA, values[0], this.DetectorType.UnitA));
                                stringBuilder.Append("</font>");
                            }
                            stringBuilder.Append(" ");
                            if ((values[1] > this.MinimumB) && (values[1] < this.MaximumB))
                            {
                                stringBuilder.Append(string.Format("{0}：{1}{2}", this.DetectorType.DescriptionB, values[1], this.DetectorType.UnitB));
                            }
                            else
                            {
                                stringBuilder.Append("<font style=\"color:#ff0000\">");
                                stringBuilder.Append(string.Format("{0}：{1}{2}", this.DetectorType.DescriptionB, values[1], this.DetectorType.UnitB));
                                stringBuilder.Append("</font>");
                            }
                            result = stringBuilder.ToString();
                            break;
                    }
                }

                return result;
            }
        }

        public string ViewImage
        {
            get
            {
                string result = string.Empty;
                if (this.DetectorType != null)
                {
                    if (this.Error)
                    {
                        result = this.ErrorHref;
                    }
                    else
                    {
                        result = this.NormalHref;
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns>数据</returns>
        public byte[] EncodeMinimum()
        {
            byte[] results = new byte[Detector.THRESHOLD_VALUE_LENGTH];

            results[0] = this.DetectorType.Code;
            results[1] = (byte)this.MinimumA;
            results[2] = (byte)this.MinimumB;

            return results;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="values">数据</param>
        public void DecodeMinimum(byte[] values)
        {
            if ((values != null) && (values.Length == Detector.THRESHOLD_VALUE_LENGTH))
            {
                if (this.DetectorType == null)
                {
                    this.DetectorType = new DetectorType();
                }
                this.DetectorType.Code = values[0];
                this.MinimumA = values[1];
                this.MinimumB = values[2];
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns>数据</returns>
        public byte[] EncodeMaximum()
        {
            byte[] results = new byte[Detector.THRESHOLD_VALUE_LENGTH];

            results[0] = this.DetectorType.Code;
            results[1] = (byte)this.MaximumA;
            results[2] = (byte)this.MaximumB;

            return results;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="values">数据</param>
        public void DecodeMaximum(byte[] values)
        {
            if ((values != null) && (values.Length == Detector.THRESHOLD_VALUE_LENGTH))
            {
                if (this.DetectorType == null)
                {
                    this.DetectorType = new DetectorType();
                }
                this.DetectorType.Code = values[0];
                this.MaximumA = values[1];
                this.MaximumB = values[2];
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <returns>数据</returns>
        public byte[] Encode()
        {
            byte[] results = new byte[Detector.DATA_VALUE_LENGTH];

            switch (this.DetectorType.Type)
            {
                case Resource.DetectorTypes.DoubleArea:
                    results = BitConverter.GetBytes(this.Value);
                    Array.Reverse(results);
                    break;
                case Resource.DetectorTypes.Switch:
                    if (this.Value == 0)
                    {
                        results = new byte[] { 0x00, 0x00 };
                    }
                    else
                    {
                        results = new byte[] { 0x01, 0x01 };
                    }
                    break;
                default:
                    results = BitConverter.GetBytes(this.Value);
                    Array.Reverse(results);
                    break;
            }

            return results;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="values">数据</param>
        public void Decode(byte[] values)
        {
            switch (this.DetectorType.Type)
            {
                case Resource.DetectorTypes.DoubleArea:
                    Array.Reverse(values);
                    this.Value = BitConverter.ToUInt16(values, 0);
                    break;
                case Resource.DetectorTypes.Switch:
                    this.Value = values[0];
                    break;
                default:
                    Array.Reverse(values);
                    this.Value = BitConverter.ToUInt16(values, 0);
                    break;
            }
        }
    }
}
