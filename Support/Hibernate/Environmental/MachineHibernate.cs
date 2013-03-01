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
    /// 设备持久化类
    /// </summary>
    public class MachineHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[room_id], {0}[name], {0}[ip], {0}[netmask], {0}[gateway], {0}[mac], {0}[mobile_a], {0}[mobile_b], {0}[mobile_c], {0}[alarm], {0}[interval], {0}[mobile]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <param name="rooms">机房集合</param>
        /// <returns>设备</returns>
        public Machine Parse(object[] values)
        {
            Machine result = new Machine();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as Machine;

                result.RoomId = DatabaseHibernate.ParseString(values[7]);
                result.Name = DatabaseHibernate.ParseString(values[8]);
                result.Ip = DatabaseHibernate.ParseString(values[9]);
                result.Netmask = DatabaseHibernate.ParseString(values[10]);
                result.Gateway = DatabaseHibernate.ParseString(values[11]);
                result.Mac = DatabaseHibernate.ParseString(values[12]);
                result.MobileA = DatabaseHibernate.ParseString(values[13]);
                result.MobileB = DatabaseHibernate.ParseString(values[14]);
                result.MobileC = DatabaseHibernate.ParseString(values[15]);
                result.Alarm = DatabaseHibernate.ParseBoolean(values[16]);
                result.Interval = DatabaseHibernate.ParseInt(values[17]);
                result.Mobile = DatabaseHibernate.ParseString(values[18]);
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
        /// <returns>设备集合</returns>
        public List<Machine> Parse(List<object[]> values)
        {
            List<Machine> results = new List<Machine>();

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    Machine value = this.Parse(values[i]);
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
        /// <returns>设备集合</returns>
        public List<Machine> Query(int page, int rows, ref int total)
        {
            List<Machine> results = new List<Machine>();

            string sql = string.Format("select {0} from e_machine as t order by [t].[name]", this.Asterisk("[t]."));
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

        public Machine QueryByGuid(string guid)
        {
            Machine result = null;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_machine as t, e_room as u where [t].[room_id] = [u].[guid] and [t].[guid] = '{1}'", this.Asterisk("[t]."), guid);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<Machine> results = this.Parse(values);
            if ((results != null) && (results.Count > 0))
            {
                result = results[0];
            }

            return result;
        }

        public Machine QueryByIp(string ip)
        {
            Machine result = null;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_machine as t, e_room as u where [t].[room_id] = [u].[guid] and [t].[ip] = '{1}'", this.Asterisk("[t]."), ip);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<Machine> results = this.Parse(values);
            if ((results != null) && (results.Count > 0))
            {
                result = results[0];
            }

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

            string sql = string.Format("insert into e_machine ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :room_id, :name, :ip, :netmask, :gateway, :mac, :mobile_a, :mobile_b, :mobile_c, :alarm, :interval, :mobile)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("room_id", DatabaseHibernate.Parameter(value.RoomId)));
            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("ip", DatabaseHibernate.Parameter(value.Ip)));
            parameters.Add(new Parameter("netmask", DatabaseHibernate.Parameter(value.Netmask)));
            parameters.Add(new Parameter("gateway", DatabaseHibernate.Parameter(value.Gateway)));
            parameters.Add(new Parameter("mac", DatabaseHibernate.Parameter(value.Mac)));
            parameters.Add(new Parameter("mobile_a", DatabaseHibernate.Parameter(value.MobileA)));
            parameters.Add(new Parameter("mobile_b", DatabaseHibernate.Parameter(value.MobileB)));
            parameters.Add(new Parameter("mobile_c", DatabaseHibernate.Parameter(value.MobileC)));
            parameters.Add(new Parameter("alarm", DatabaseHibernate.Parameter(value.Alarm)));
            parameters.Add(new Parameter("interval", DatabaseHibernate.Parameter(value.Interval)));
            parameters.Add(new Parameter("mobile", DatabaseHibernate.Parameter(value.Mobile)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

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

            string sql = string.Format("update e_machine as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[remark] = :remark, [t].[validity] = :validity, [t].[room_id] = :room_id, [t].[name] = :name, [t].[ip] = :ip, [t].[netmask] = :netmask, [t].[gateway] = :gateway, [t].[mac] = :mac, [t].[mobile_a] = :mobile_a, [t].[mobile_b] = :mobile_b, [t].[mobile_c] = :mobile_c, [t].[alarm] = :alarm, [t].[interval] = :interval, [t].[mobile] = :mobile where [t].[guid] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("room_id", DatabaseHibernate.Parameter(value.RoomId)));
            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("ip", DatabaseHibernate.Parameter(value.Ip)));
            parameters.Add(new Parameter("netmask", DatabaseHibernate.Parameter(value.Netmask)));
            parameters.Add(new Parameter("gateway", DatabaseHibernate.Parameter(value.Gateway)));
            parameters.Add(new Parameter("mac", DatabaseHibernate.Parameter(value.Mac)));
            parameters.Add(new Parameter("mobile_a", DatabaseHibernate.Parameter(value.MobileA)));
            parameters.Add(new Parameter("mobile_b", DatabaseHibernate.Parameter(value.MobileB)));
            parameters.Add(new Parameter("mobile_c", DatabaseHibernate.Parameter(value.MobileC)));
            parameters.Add(new Parameter("alarm", DatabaseHibernate.Parameter(value.Alarm)));
            parameters.Add(new Parameter("interval", DatabaseHibernate.Parameter(value.Interval)));
            parameters.Add(new Parameter("mobile", DatabaseHibernate.Parameter(value.Mobile)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool Delete(Machine value)
        {
            bool result = false;

            DatabaseHibernate hibernate = new DatabaseHibernate();

            string sql = string.Format("delete from e_detector as t where [t].[machine_id] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            result = hibernate.Write(Variable.Link, sql, parameters);

            if (result)
            {
                sql = string.Format("delete from e_machine as t where [t].[guid] = '{0}'", value.Guid);
                parameters.Clear();
                result = hibernate.Write(Variable.Link, sql, parameters);
            }

            return result;
        }

        public bool DeleteByRoom(string roomGuid)
        {
            bool result = false;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_machine as t where [t].[room_id] = '{1}'", this.Asterisk("[t]."), roomGuid);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<Machine> results = this.Parse(values);
            if ((results != null) && (results.Count > 0))
            {
                for (int i = 0; i < results.Count; i++)
                {
                    result = this.Delete(results[i]);
                    if (!result)
                    {
                        break;
                    }
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        public Machine QueryByNameOrIp(string name, string roomId, string ip)
        {
            Machine result = null;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_machine as t where ([t].[name] = :name and [t].[room_id] = :roomId) or ([t].[ip] = :ip)", this.Asterisk("[t]."));
            parameters.Add(new Parameter("name", name));
            parameters.Add(new Parameter("roomId", roomId));
            parameters.Add(new Parameter("ip", ip));

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<Machine> results = this.Parse(values);
            if ((results != null) && (results.Count > 0))
            {
                result = results[0];
            }

            return result;
        }

        public List<Machine> QueryByRoom(string roomId)
        {
            List<Machine> results = new List<Machine>();

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();

            string sql = string.Format("select {0} from e_machine as t where [t].[room_id] = :room_id", this.Asterisk("[t]."));
            parameters.Add(new Parameter("room_id", roomId));

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            results = this.Parse(values);

            return results;
        }
    }
}
