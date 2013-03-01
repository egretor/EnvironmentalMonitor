using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Hibernate.Manage;
using EnvironmentalMonitor.Support.Hibernate;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;

namespace EnvironmentalMonitor.Support.Business.Manage
{
    public class UserModuleBusiness
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <param name="total">总数</param>
        /// <param name="userGuid"></param>
        /// <returns>用户和模块关系集合</returns>
        public List<UserModule> QueryByUser(int page, int rows, ref int total, string userGuid)
        {
            List<UserModule> results = new List<UserModule>();

            UserModuleHiberante hibernate = new UserModuleHiberante();
            results = hibernate.QueryByUser(page, rows, ref total, userGuid);

            return results;
        }

        public bool Refresh(string userGuid, List<UserModule> values)
        {
            bool result = false;

            UserModuleHiberante hibernate = new UserModuleHiberante();
            result = hibernate.DeleteByUser(userGuid);
            if (result)
            {
                if (values != null)
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        result = hibernate.Insert(values[i]);
                        if (!result)
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
