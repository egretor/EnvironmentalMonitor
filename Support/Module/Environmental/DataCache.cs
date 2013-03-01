using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    /// <summary>
    /// 数据缓存
    /// </summary>
    public class DataCache : EnvironmentalMonitor.Support.Module.AbstractModule
    {
        private string _DetectorId;
        /// <summary>
        /// 探头编号
        /// </summary>
        public string DetectorId
        {
            get
            {
                return this._DetectorId;
            }
            set
            {
                this._DetectorId = value;
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

        private bool _Newest;
        /// <summary>
        /// 最新
        /// </summary>
        public bool Newest
        {
            get
            {
                return this._Newest;
            }
            set
            {
                this._Newest = value;
            }
        }
    }
}
