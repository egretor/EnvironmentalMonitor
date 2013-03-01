using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module
{
    /// <summary>
    /// 抽象模型类
    /// </summary>
    public abstract class AbstractModule
    {
        private string _Guid;
        /// <summary>
        /// 编号
        /// </summary>
        public string Guid
        {
            get
            {
                return this._Guid;
            }
            set
            {
                this._Guid = value;
            }
        }

        private string _InsertUserId;
        /// <summary>
        /// 新增操作用户编号
        /// </summary>
        public string InsertUserId
        {
            get
            {
                return this._InsertUserId;
            }
            set
            {
                this._InsertUserId = value;
            }
        }

        private DateTime _InsertTime;
        /// <summary>
        /// 新增操作时间
        /// </summary>
        public DateTime InsertTime
        {
            get
            {
                return this._InsertTime;
            }
            set
            {
                this._InsertTime = value;
            }
        }

        private string _UpdateUserId;
        /// <summary>
        /// 更新操作用户编号
        /// </summary>
        public string UpdateUserId
        {
            get
            {
                return this._UpdateUserId;
            }
            set
            {
                this._UpdateUserId = value;
            }
        }

        private DateTime _UpdateTime;
        /// <summary>
        /// 更新操作时间
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this._UpdateTime = value;
            }
        }

        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        private bool _Validity;
        /// <summary>
        /// 有效性
        /// </summary>
        public bool Validity
        {
            get
            {
                return this._Validity;
            }
            set
            {
                this._Validity = value;
            }
        }
    }
}
