using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    public class MessageCache : EnvironmentalMonitor.Support.Module.AbstractModule
    {
        private string _Ip;
        /// <summary>
        /// Ip
        /// </summary>
        public string Ip
        {
            get
            {
                return this._Ip;
            }
            set
            {
                this._Ip = value;
            }
        }

        private string _MachineId;
        /// <summary>
        /// 检测仪编号
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

        private byte _Code;
        /// <summary>
        /// 探头代码
        /// </summary>
        public byte Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                this._Code = value;
            }
        }

        private bool _Result;
        /// <summary>
        /// 结果
        /// </summary>
        public bool Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                this._Result = value;
            }
        }

        private DateTime _SendTime;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get
            {
                return this._SendTime;
            }
            set
            {
                this._SendTime = value;
            }
        }

        public string ResultText
        {
            get
            {
                string result = string.Empty;

                if (this.Result)
                {
                    result = "发送成功";
                }
                else
                {
                    result = "发送失败";
                }

                return result;
            }
        }

        public string SendTimeText
        {
            get
            {
                return this.SendTime.ToString("yyyy-MM-dd HH:mm:ss");
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
        /// <summary>
        /// 探头类型
        /// </summary>
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
    }
}
