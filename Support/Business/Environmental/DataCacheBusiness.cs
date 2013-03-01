using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;

namespace EnvironmentalMonitor.Support.Business.Environmental
{
    /// <summary>
    /// 数据缓存业务类
    /// </summary>
    public class DataCacheBusiness
    {
        public bool Insert(List<DataCache> values)
        {
            bool result = false;

            DataCacheHibernate hibernate = new DataCacheHibernate();
            result = hibernate.Insert(values);

            return result;
        }
    }
}
