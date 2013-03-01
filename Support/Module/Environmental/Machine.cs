using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    /// <summary>
    /// 设备
    /// </summary>
    public class Machine : EnvironmentalMonitor.Support.Module.AbstractModule
    {
        private string _RoomId;
        /// <summary>
        /// 机房编号
        /// </summary>
        public string RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
            }
        }

        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        private string _Ip;
        /// <summary>
        /// IP
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

        private string _Netmask;
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string Netmask
        {
            get
            {
                return this._Netmask;
            }
            set
            {
                this._Netmask = value;
            }
        }

        private string _Gateway;
        /// <summary>
        /// 网关
        /// </summary>
        public string Gateway
        {
            get
            {
                return this._Gateway;
            }
            set
            {
                this._Gateway = value;
            }
        }

        private string _Mac;
        /// <summary>
        /// 物理地址
        /// </summary>
        public string Mac
        {
            get
            {
                return this._Mac;
            }
            set
            {
                this._Mac = value;
            }
        }

        private string _MobileA;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileA
        {
            get
            {
                return this._MobileA;
            }
            set
            {
                this._MobileA = value;
            }
        }

        private string _MobileB;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileB
        {
            get
            {
                return this._MobileB;
            }
            set
            {
                this._MobileB = value;
            }
        }

        private string _MobileC;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileC
        {
            get
            {
                return this._MobileC;
            }
            set
            {
                this._MobileC = value;
            }
        }

        private bool _Alarm;
        /// <summary>
        /// 手机报警
        /// </summary>
        public bool Alarm
        {
            get
            {
                return this._Alarm;
            }
            set
            {
                this._Alarm = value;
            }
        }

        private int _Interval;
        /// <summary>
        /// 上传数据时间间隔
        /// </summary>
        public int Interval
        {
            get
            {
                return this._Interval;
            }
            set
            {
                this._Interval = value;
            }
        }

        private string _Mobile;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get
            {
                return this._Mobile;
            }
            set
            {
                this._Mobile = value;
            }
        }

        /// <summary>
        /// 平面图链接
        /// </summary>
        public string FloorPlanHref
        {
            get
            {
                return string.Format("{0}Resources\\Uploads\\Machine\\{1}.jpg", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath, this.Guid);
            }
        }
    }
}
