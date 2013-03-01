using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Hibernate;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Business.Environmental
{
    /// <summary>
    /// 设备业务类
    /// </summary>
    public class MachineBusiness
    {
        public Machine QueryByGuid(string guid)
        {
            Machine result = null;

            MachineHibernate hibernate = new MachineHibernate();
            result = hibernate.QueryByGuid(guid);

            return result;
        }

        public Machine QueryByIp(string ip)
        {
            Machine result = null;

            MachineHibernate hibernate = new MachineHibernate();
            result = hibernate.QueryByIp(ip);

            return result;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Insert(Machine value)
        {
            bool result = false;

            MachineHibernate hibernate = new MachineHibernate();
            result = hibernate.Insert(value);

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Update(Machine value)
        {
            bool result = false;

            MachineHibernate hibernate = new MachineHibernate();
            result = hibernate.Update(value);

            return result;
        }

        public bool Delete(Machine value)
        {
            bool result = false;

            MachineHibernate hibernate = new MachineHibernate();
            result = hibernate.Delete(value);

            return result;
        }

        public Machine QueryByNameOrIp(string name, string roomId, string ip)
        {
            Machine result = null;

            MachineHibernate hibernate = new MachineHibernate();
            result = hibernate.QueryByNameOrIp(name, roomId, ip);

            return result;
        }

        public List<Machine> QueryByRoom(string roomId)
        {
            List<Machine> results = new List<Machine>();

            MachineHibernate hibernate = new MachineHibernate();
            results = hibernate.QueryByRoom(roomId);

            return results;
        }
    }
}
