using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentalMonitor.Support.Hibernate
{
    /// <summary>
    /// 参数类
    /// </summary>
    public class Parameter
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

        private object _Value;
        /// <summary>
        /// 值
        /// </summary>
        public object Value
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public Parameter()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public Parameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
