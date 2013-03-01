using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Hibernate.Environmental
{
    /// <summary>
    /// 机房持久化类
    /// </summary>
    public class RoomHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[name], {0}[address], {0}[contact], {0}[phone]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <returns>机房</returns>
        public Room Parse(object[] values)
        {
            Room result = new Room();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as Room;

                result.Name = DatabaseHibernate.ParseString(values[7]);
                result.Address = DatabaseHibernate.ParseString(values[8]);
                result.Contact = DatabaseHibernate.ParseString(values[9]);
                result.Phone = DatabaseHibernate.ParseString(values[10]);
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
        /// <returns>机房集合</returns>
        public List<Room> Parse(List<object[]> values)
        {
            List<Room> results = new List<Room>();

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    Room value = this.Parse(values[i]);
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
        /// <returns>机房集合</returns>
        public List<Room> Query(int page, int rows, ref int total)
        {
            List<Room> results = new List<Room>();

            string sql = string.Format("select {0} from e_room as t order by [t].[name]", this.Asterisk("[t]."));
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
        public bool Insert(Room value)
        {
            bool result = false;

            string sql = string.Format("insert into e_room ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :name, :address, :contact, :phone)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("address", DatabaseHibernate.Parameter(value.Address)));
            parameters.Add(new Parameter("contact", DatabaseHibernate.Parameter(value.Contact)));
            parameters.Add(new Parameter("phone", DatabaseHibernate.Parameter(value.Phone)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

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

            string sql = string.Format("update e_room as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[remark] = :remark, [t].[validity] = :validity, [t].[name] = :name, [t].[address] = :address, [t].[contact] = :contact, [t].[phone] = :phone where [t].[guid] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("address", DatabaseHibernate.Parameter(value.Address)));
            parameters.Add(new Parameter("contact", DatabaseHibernate.Parameter(value.Contact)));
            parameters.Add(new Parameter("phone", DatabaseHibernate.Parameter(value.Phone)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool Delete(Room value)
        {
            bool result = false;

            MachineHibernate machineHibernate = new MachineHibernate();
            result = machineHibernate.DeleteByRoom(value.Guid);

            DatabaseHibernate hibernate = new DatabaseHibernate();
            List<Parameter> parameters = new List<Parameter>();

            if (result)
            {
                string sql = string.Format("delete from e_user_room as t where [t].[room_id] = '{0}'", value.Guid);
                parameters.Clear();

                result = hibernate.Write(Variable.Link, sql, parameters);
            }

            if (result)
            {
                string sql = string.Format("delete from e_room as t where [t].[guid] = '{0}'", value.Guid);
                parameters.Clear();

                result = hibernate.Write(Variable.Link, sql, parameters);
            }

            return result;
        }

        public Room QueryByName(string name)
        {
            Room result = null;

            string sql = string.Format("select {0} from e_room as t where ucase([t].[name]) = ucase(:name)", this.Asterisk("[t]."));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(name)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                result = new Room();

                object[] moduleValues = values[0];

                result = this.Parse(moduleValues);
            }

            return result;
        }

        public Room QueryByGuid(string guid)
        {
            Room result = null;

            string sql = string.Format("select {0} from e_room as t where [t].[guid] = '{1}'", this.Asterisk("[t]."), guid);
            List<Parameter> parameters = new List<Parameter>();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);

            if ((values != null) && (values.Count == 1))
            {
                result = new Room();

                object[] moduleValues = values[0];

                result = this.Parse(moduleValues);
            }

            return result;
        }
    }
}
