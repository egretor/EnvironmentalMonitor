using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvironmentalMonitor.Website.Jsons
{
    /// <summary>
    /// 保存Json数据类
    /// </summary>
    public class SaveJsonData
    {
        private bool _success;
        /// <summary>
        /// 成败
        /// </summary>
        public bool success
        {
            get
            {
                return this._success;
            }
            set
            {
                this._success = value;
            }
        }

        private string _msg;
        /// <summary>
        /// 消息
        /// </summary>
        public string msg
        {
            get
            {
                return this._msg;
            }
            set
            {
                this._msg = value;
            }
        }
    }
}