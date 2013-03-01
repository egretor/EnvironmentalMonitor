using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvironmentalMonitor.Website.Jsons
{
    public class LoadJsonData
    {
        private int _total;
        /// <summary>
        /// 总数
        /// </summary>
        public int total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }

        private object _rows;
        /// <summary>
        /// 行集合
        /// </summary>
        public object rows
        {
            get
            {
                return this._rows;
            }
            set
            {
                this._rows = value;
            }
        }
    }
}