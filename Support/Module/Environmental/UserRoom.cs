using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    /// <summary>
    /// 用户和机房关系
    /// </summary>
    public class UserRoom : EnvironmentalMonitor.Support.Module.AbstractModule
    {
        private string _UserId;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }

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
    }
}
