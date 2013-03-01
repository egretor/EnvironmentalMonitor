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
    /// 探头类型业务类
    /// </summary>
    public class DetectorTypeBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <param name="total">总数</param>
        /// <returns>探头类型集合</returns>
        public List<DetectorType> Query(int page, int rows, ref int total)
        {
            List<DetectorType> results = new List<DetectorType>();

            DetectorTypeHibernate hibernate = new DetectorTypeHibernate();
            results = hibernate.Query(page, rows, ref total);

            return results;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Insert(DetectorType value)
        {
            bool result = false;

            DetectorTypeHibernate hibernate = new DetectorTypeHibernate();
            result = hibernate.Insert(value);

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Update(DetectorType value)
        {
            bool result = false;

            DetectorTypeHibernate hibernate = new DetectorTypeHibernate();
            result = hibernate.Update(value);

            return result;
        }

        public bool Delete(DetectorType value)
        {
            bool result = false;

            DetectorTypeHibernate hibernate = new DetectorTypeHibernate();
            result = hibernate.Delete(value);

            return result;
        }

        public DetectorType QueryByNameOrCode(string name, byte code)
        {
            DetectorType result = null;

            DetectorTypeHibernate hibernate = new DetectorTypeHibernate();
            result = hibernate.QueryByNameOrCode(name, code);

            return result;
        }

        public DetectorType QueryByGuid(string guid)
        {
            DetectorType result = null;

            DetectorTypeHibernate hibernate = new DetectorTypeHibernate();
            result = hibernate.QueryByGuid(guid);

            return result;
        }
    }
}
