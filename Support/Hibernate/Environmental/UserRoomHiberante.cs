using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Hibernate.Environmental
{
    public class UserRoomHiberante
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[user_id], {0}[room_id]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <returns>用户和房间关系</returns>
        public UserRoom Parse(object[] values)
        {
            UserRoom result = new UserRoom();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as UserRoom;

                result.UserId = DatabaseHibernate.ParseString(values[7]);
                result.RoomId = DatabaseHibernate.ParseString(values[8]);
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
        /// <returns>用户和房间关系集合</returns>
        public List<UserRoom> Parse(List<object[]> values)
        {
            List<UserRoom> results = new List<UserRoom>();

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    UserRoom value = this.Parse(values[i]);
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
        /// <param name="userGuid"></param>
        /// <returns>用户和房间关系集合</returns>
        public List<UserRoom> QueryByUser(int page, int rows, ref int total, string userGuid)
        {
            List<UserRoom> results = new List<UserRoom>();

            string sql = string.Format("select {0} from e_user_room as t, e_room as u where [t].[room_id] = [u].[guid] and [t].[user_id] = '{1}' order by [u].[name]", this.Asterisk("[t]."), userGuid);
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
        public bool Insert(UserRoom value)
        {
            bool result = false;

            string sql = string.Format("insert into e_user_room ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :user_id, :room_id)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));
            parameters.Add(new Parameter("user_id", DatabaseHibernate.Parameter(value.UserId)));
            parameters.Add(new Parameter("room_id", DatabaseHibernate.Parameter(value.RoomId)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool DeleteByUser(string userGuid)
        {
            bool result = false;

            string sql = string.Format("delete from e_user_room as t where [t].[user_id] = '{0}'", userGuid);
            List<Parameter> parameters = new List<Parameter>();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }
    }
}
