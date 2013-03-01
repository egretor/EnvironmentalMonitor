using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Module.Manage
{
    /// <summary>
    /// 分类
    /// </summary>
    public class Catalog
    {
        private string _Code;
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
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

        private string _Url;
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public Catalog()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="url">网址</param>
        public Catalog(string code, string name, string url)
        {
            this.Code = code;
            this.Name = name;
            this.Url = url;
        }
    }
}