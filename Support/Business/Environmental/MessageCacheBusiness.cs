using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;

namespace EnvironmentalMonitor.Support.Business.Environmental
{
    public class MessageCacheBusiness
    {
        public bool Insert(MessageCache value)
        {
            bool result = false;

            MessageCacheHibernate hibernate = new MessageCacheHibernate();
            result = hibernate.Insert(value);

            return result;
        }

        public List<MessageCache> QueryByMachine(string machineId, DateTime beginDate, DateTime endDate)
        {
            List<MessageCache> results = new List<MessageCache>();

            MessageCacheHibernate hibernate = new MessageCacheHibernate();
            results = hibernate.QueryByMachine(machineId, beginDate, endDate);

            return results;
        }
    }
}
