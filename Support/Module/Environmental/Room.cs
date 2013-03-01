using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    /// <summary>
    /// 机房
    /// </summary>
    public class Room : EnvironmentalMonitor.Support.Module.AbstractModule
    {
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

        private string _Address;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this._Address = value;
            }
        }

        private string _Contact;
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact
        {
            get
            {
                return this._Contact;
            }
            set
            {
                this._Contact = value;
            }
        }
        private string _Phone;
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                this._Phone = value;
            }
        }
    }
}
