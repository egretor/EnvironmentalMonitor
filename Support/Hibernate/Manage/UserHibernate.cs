using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Hibernate.Manage
{
    /// <summary>
    /// 用户持久化
    /// </summary>
    public class UserHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[name], {0}[account], {0}[password], {0}[prerogative]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <returns>用户</returns>
        public User Parse(object[] values)
        {
            User result = new User();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as User;

                result.Name = DatabaseHibernate.ParseString(values[7]);
                result.Account = DatabaseHibernate.ParseString(values[8]);
                result.Password = DatabaseHibernate.ParseString(values[9]);
                result.Prerogative = DatabaseHibernate.ParseBoolean(values[10]);
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
        /// <returns>用户集合</returns>
        public List<User> Parse(List<object[]> values)
        {
            List<User> results = new List<User>();

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    User value = this.Parse(values[i]);
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
        /// <returns>用户集合</returns>
        public List<User> Query(int page, int rows, ref int total)
        {
            List<User> results = new List<User>();

            string sql = string.Format("select {0} from m_user as t order by [t].[name]", this.Asterisk("[t]."));
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
        /// 总数
        /// </summary>
        /// <returns>总数</returns>
        public int Count()
        {
            int result = -1;

            string sql = "select count([t].[guid]) from m_user as t";
            List<Parameter> parameters = new List<Parameter>();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                object[] moduleValues = values[0];
                result = DatabaseHibernate.ParseInt(moduleValues[0]);
            }

            return result;
        }

        /// <summary>
        /// 通过主键查询
        /// </summary>
        /// <param name="guid">主键</param>
        /// <returns>用户</returns>
        public User QueryByGuid(string guid)
        {
            User result = null;

            string sql = string.Format("select {0} from m_user as t where [t].[guid] = :guid", this.Asterisk("[t]."));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(guid)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                result = new User();

                object[] moduleValues = values[0];

                result = this.Parse(moduleValues);
            }

            return result;
        }

        /// <summary>
        /// 通过帐户查询
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns>用户</returns>
        public User SelectByAccount(string account)
        {
            User result = null;

            string sql = string.Format("select {0} from m_user as t where ucase([t].[account]) = ucase(:account)", this.Asterisk("[t]."));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("account", DatabaseHibernate.Parameter(account)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                result = new User();

                object[] moduleValues = values[0];

                result = this.Parse(moduleValues);
            }

            return result;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Insert(User value)
        {
            bool result = false;

            string sql = string.Format("insert into m_user ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :name, :account, :password, :prerogative)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));
            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("account", DatabaseHibernate.Parameter(value.Account.ToUpper())));
            parameters.Add(new Parameter("password", DatabaseHibernate.Parameter(value.Password)));
            parameters.Add(new Parameter("prerogative", DatabaseHibernate.Parameter(value.Prerogative)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Update(User value)
        {
            bool result = false;

            string sql = string.Format("update m_user as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[remark] = :remark, [t].[validity] = :validity, [t].[name] = :name, [t].[account] = :account, [t].[password] = :password, [t].[prerogative] = :prerogative where [t].[guid] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("account", DatabaseHibernate.Parameter(value.Account.ToUpper())));
            parameters.Add(new Parameter("password", DatabaseHibernate.Parameter(value.Password)));
            parameters.Add(new Parameter("prerogative", DatabaseHibernate.Parameter(value.Prerogative)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool Delete(User value)
        {
            bool result = false;

            string sql = string.Format("delete from m_user_module as t where [t].[user_id] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            if (result)
            {
                sql = string.Format("delete from e_user_room as t where [t].[user_id] = '{0}'", value.Guid);
                parameters.Clear();
                result = hibernate.Write(Variable.Link, sql, parameters);
            }

            if (result)
            {
                sql = string.Format("delete from m_user as t where [t].[guid] = '{0}'", value.Guid);
                parameters.Clear();
                result = hibernate.Write(Variable.Link, sql, parameters);
            }

            return result;
        }

        public bool ChangePassword(string guid, string password)
        {
            bool result = false;

            string sql = string.Format("update m_user as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[password] = :password where [t].[guid] = '{0}'", guid);
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(guid)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(DateTime.Now)));

            parameters.Add(new Parameter("password", DatabaseHibernate.Parameter(password)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public List<User> QueryByInsertUser(string insertUserId)
        {
            List<User> results = null;

            string sql = string.Format("select {0} from m_user as t where[t].[insert_user_id] = '{1}'", this.Asterisk("[t]."), insertUserId);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Clear();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);

            results = this.Parse(values);

            return results;
        }
    }
}
