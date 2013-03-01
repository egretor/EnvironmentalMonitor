using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Hibernate.Environmental;
using EnvironmentalMonitor.Support.Hibernate;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;

namespace EnvironmentalMonitor.Support.Business.Environmental
{
    /// <summary>
    /// 机房业务类
    /// </summary>
    public class RoomBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <param name="total">总数</param>
        /// <returns>机房集合</returns>
        public List<Room> Query(int page, int rows, ref int total)
        {
            List<Room> results = new List<Room>();

            RoomHibernate hibernate = new RoomHibernate();
            results = hibernate.Query(page, rows, ref total);

            return results;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Insert(Room value)
        {
            bool result = false;

            RoomHibernate hibernate = new RoomHibernate();
            result = hibernate.Insert(value);

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Update(Room value)
        {
            bool result = false;

            RoomHibernate hibernate = new RoomHibernate();
            result = hibernate.Update(value);

            return result;
        }

        public bool Delete(Room value)
        {
            bool result = false;

            RoomHibernate hibernate = new RoomHibernate();
            result = hibernate.Delete(value);

            return result;
        }

        public Room QueryByName(string name)
        {
            Room result = null;

            RoomHibernate hibernate = new RoomHibernate();
            result = hibernate.QueryByName(name);

            return result;
        }

        public Room QueryByGuid(string guid)
        {
            Room result = null;

            RoomHibernate hibernate = new RoomHibernate();
            result = hibernate.QueryByGuid(guid);

            return result;
        }
    }
}
