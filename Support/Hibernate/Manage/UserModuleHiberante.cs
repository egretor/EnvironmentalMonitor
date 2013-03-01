using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Hibernate.Manage
{
    public class UserModuleHiberante
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[user_id], {0}[module_code]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <returns>用户和模块关系</returns>
        public UserModule Parse(object[] values)
        {
            UserModule result = new UserModule();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as UserModule;

                result.UserId = DatabaseHibernate.ParseString(values[7]);
                result.ModuleCode = DatabaseHibernate.ParseString(values[8]);
            }
            catch (Exception exception)
            {
                result = null;
                EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
            }

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据集合</param>
        /// <returns>用户和模块关系集合</returns>
        public List<UserModule> Parse(List<object[]> values)
        {
            List<UserModule> results = new List<UserModule>();

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    UserModule value = this.Parse(values[i]);
                    results.Add(value);
                }
            }

            return results;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <param name="total">总数</param>
        /// <returns>用户和模块关系集合</returns>
        public List<UserModule> Query(int page, int rows, ref int total)
        {
            List<UserModule> results = new List<UserModule>();

            string sql = string.Format("select {0} from m_user_module as t order by [t].[module_code]", this.Asterisk("[t]."));
            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();

            string countSql = string.Format("select count(*) from ({0})", sql);
            parameters.Clear();

            List<object[]> values = hibernate.Read(Variable.Link, countSql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                object[] moduleValues = values[0];
                total = DatabaseHibernate.ParseInt(moduleValues[0]);
            }

            string querySql = sql;

            parameters.Clear();

            values = hibernate.Read(Variable.Link, querySql, parameters, page, rows);

            results = this.Parse(values);

            return results;
        }

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

            string sql = string.Format("select {0} from m_user_module as t where [t].[user_id] = '{1}' order by [t].[module_code]", this.Asterisk("[t]."), userGuid);
            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();

            string countSql = string.Format("select count(*) from ({0})", sql);
            parameters.Clear();

            List<object[]> values = hibernate.Read(Variable.Link, countSql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                object[] moduleValues = values[0];
                total = DatabaseHibernate.ParseInt(moduleValues[0]);
            }

            string querySql = sql;

            parameters.Clear();

            values = hibernate.Read(Variable.Link, querySql, parameters, page, rows);

            results = this.Parse(values);

            return results;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Insert(UserModule value)
        {
            bool result = false;

            string sql = string.Format("insert into m_user_module ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :user_id, :module_code)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));
            parameters.Add(new Parameter("user_id", DatabaseHibernate.Parameter(value.UserId)));
            parameters.Add(new Parameter("module_code", DatabaseHibernate.Parameter(value.ModuleCode)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool DeleteByUser(string userGuid)
        {
            bool result = false;

            string sql = string.Format("delete from m_user_module as t where [t].[user_id] = '{0}'", userGuid);
            List<Parameter> parameters = new List<Parameter>();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }
    }
}
