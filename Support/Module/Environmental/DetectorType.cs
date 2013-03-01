using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Module.Environmental
{
    /// <summary>
    /// 探头类型
    /// </summary>
    public class DetectorType : EnvironmentalMonitor.Support.Module.AbstractModule
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

        private DetectorTypes _Type;
        /// <summary>
        /// 类型
        /// </summary>
        public DetectorTypes Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        private byte _Code;
        /// <summary>
        /// 代码
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

        private string _DescriptionA;
        /// <summary>
        /// 描述A
        /// 正确开关值描述
        /// 范围值描述
        /// 双范围值第一个值描述
        /// </summary>
        public string DescriptionA
        {
            get
            {
                return this._DescriptionA;
            }
            set
            {
                this._DescriptionA = value;
            }
        }

        private string _DescriptionB;
        /// <summary>
        /// 描述B
        /// 错误开关值描述
        /// 
        /// 双范围值第二个值描述
        /// </summary>
        public string DescriptionB
        {
            get
            {
                return this._DescriptionB;
            }
            set
            {
                this._DescriptionB = value;
            }
        }

        private string _UnitA;
        /// <summary>
        /// 单位A
        /// 
        /// 双范围值第一个值单位
        /// </summary>
        public string UnitA
        {
            get
            {
                return this._UnitA;
            }
            set
            {
                this._UnitA = value;
            }
        }

        private string _UnitB;
        /// <summary>
        /// 单位A
        /// 
        /// 双范围值第二个值单位
        /// </summary>
        public string UnitB
        {
            get
            {
                return this._UnitB;
            }
            set
            {
                this._UnitB = value;
            }
        }

        /// <summary>
        /// 类型文本
        /// </summary>
        public string TypeText
        {
            get
            {
                string result = string.Empty;

                switch (this.Type)
                {
                    case DetectorTypes.Switch:
                        result = "开关值";
                        break;
                    case DetectorTypes.DoubleArea:
                        result = "双范围值";
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// 类型值
        /// </summary>
        public int TypeValue
        {
            get
            {
                return (int)this.Type;
            }
        }

        /// <summary>
        /// 普通图链接
        /// </summary>
        public string NormalHref
        {
            get
            {
                return string.Format("{0}Resources\\Uploads\\DetectorType\\Normal\\{1}.jpg", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath, this.Guid);
            }
        }

        /// <summary>
        /// 错误图链接
        /// </summary>
        public string ErrorHref
        {
            get
            {
                return string.Format("{0}Resources\\Uploads\\DetectorType\\Error\\{1}.jpg", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath, this.Guid);
            }
        }
    }
}
