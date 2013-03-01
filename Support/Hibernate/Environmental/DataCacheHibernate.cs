using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Hibernate.Environmental
{
    /// <summary>
    /// 数据缓存持久化类
    /// </summary>
    public class DataCacheHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[detector_id], {0}[value], {0}[refresh_time], {0}[newest]", prefix);

            return result;
        }

        public bool Insert(List<Module.Environmental.DataCache> values)
        {
            bool result = false;

            DatabaseHibernate hibernate = new DatabaseHibernate();

            if (values != null)
            {
                string sql = string.Empty;
                List<Parameter> parameters = new List<Parameter>();
                for (int i = 0; i < values.Count; i++)
                {
                    DateTime refreshTime = DateTime.Now;
                    refreshTime = refreshTime.AddDays(-100);

                    sql = string.Format("delete from e_data_cache where [refresh_time] < #{0}#", refreshTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    parameters.Clear();
                    result = hibernate.Write(Variable.Link, sql, parameters);
                    if (!result)
                    {
                        break;
                    }

                    sql = string.Format("update e_data_cache set [newest] = :newest where [detector_id] = '{0}'", values[i].DetectorId);
                    parameters.Clear();
                    parameters.Add(new Parameter("newest", DatabaseHibernate.Parameter(false)));
                    result = hibernate.Write(Variable.Link, sql, parameters);
                    if (!result)
                    {
                        break;
                    }

                    sql = string.Format("insert into e_data_cache ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :detector_id, :value, :refresh_time, :newest)", this.Asterisk(""));
                    parameters.Clear();

                    parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
                    parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(values[i].InsertUserId)));
                    parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(values[i].InsertTime)));
                    parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(values[i].UpdateUserId)));
                    parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(values[i].UpdateTime)));
                    parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(values[i].Remark)));
                    parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(values[i].Validity)));

                    parameters.Add(new Parameter("detector_id", DatabaseHibernate.Parameter(values[i].DetectorId)));
                    parameters.Add(new Parameter("value", DatabaseHibernate.Parameter(values[i].Value)));
                    parameters.Add(new Parameter("refresh_time", DatabaseHibernate.Parameter(values[i].RefreshTime)));
                    parameters.Add(new Parameter("newest", DatabaseHibernate.Parameter(values[i].Newest)));

                    result = hibernate.Write(Variable.Link, sql, parameters);
                    if (!result)
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}
