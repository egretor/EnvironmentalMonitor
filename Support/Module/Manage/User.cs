using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Environmental;

namespace EnvironmentalMonitor.Support.Module.Manage
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : EnvironmentalMonitor.Support.Module.AbstractModule
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

        private string _Account;
        /// <summary>
        /// 帐户
        /// </summary>
        public string Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                this._Account = value;
            }
        }

        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        private bool _Prerogative;
        /// <summary>
        /// 特权
        /// </summary>
        public bool Prerogative
        {
            get
            {
                return this._Prerogative;
            }
            set
            {
                this._Prerogative = value;
            }
        }

        private bool _Authentication;
        /// <summary>
        /// 验证
        /// </summary>
        public bool Authentication
        {
            get
            {
                return this._Authentication;
            }
            set
            {
                this._Authentication = value;
            }
        }

        public string AccountName
        {
            get
            {
                string result = string.Empty;

                if (string.IsNullOrEmpty(this.Name))
                {
                    result = this.Account;
                }
                else
                {
                    result = string.Format("{0}[{1}]", this.Account, this.Name);
                }

                return result;
            }
        }

        private List<User> _Users;
        /// <summary>
        /// 用户集合
        /// </summary>
        public List<User> Users
        {
            get
            {
                return this._Users;
            }
            set
            {
                this._Users = value;
            }
        }

        private List<Room> _Rooms;
        /// <summary>
        /// 机房集合
        /// </summary>
        public List<Room> Rooms
        {
            get
            {
                return this._Rooms;
            }
            set
            {
                this._Rooms = value;
            }
        }

        private List<UserModule> _UserModules;
        /// <summary>
        /// 用户和模块关系集合
        /// </summary>
        public List<UserModule> UserModules
        {
            get
            {
                return this._UserModules;
            }
            set
            {
                this._UserModules = value;
            }
        }

        private List<Catalog> _Catalogs;
        /// <summary>
        /// 分类集合
        /// </summary>
        public List<Catalog> Catalogs
        {
            get
            {
                return this._Catalogs;
            }
            set
            {
                this._Catalogs = value;
            }
        }
    }
}
