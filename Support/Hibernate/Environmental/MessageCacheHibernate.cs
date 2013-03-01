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
    public class MessageCacheHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[ip], {0}[machine_id], {0}[code], {0}[result], {0}[send_time], {0}[refresh_time]", prefix);

            return result;
        }

        public MessageCache Parse(object[] values, List<Machine> machines, List<DetectorType> detectorTypes)
        {
            MessageCache result = new MessageCache();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as MessageCache;

                result.Ip = DatabaseHibernate.ParseString(values[7]);
                result.MachineId = DatabaseHibernate.ParseString(values[8]);
                result.Code = DatabaseHibernate.ParseByte(values[9]);
                result.Result = DatabaseHibernate.ParseBoolean(values[10]);
                result.SendTime = DatabaseHibernate.ParseDateTime(values[11]);
                result.RefreshTime = DatabaseHibernate.ParseDateTime(values[12]);

                for (int i = 0; i < machines.Count; i++)
                {
                    if (string.Equals(result.MachineId, machines[i].Guid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.Machine = machines[i];
                        break;
                    }
                }

                result.DetectorType = new DetectorType();
                for (int i = 0; i < detectorTypes.Count; i++)
                {
                    if (result.Code == detectorTypes[i].Code)
                    {
                        result.DetectorType = detectorTypes[i];
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                result = null;
                EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
            }

            return result;
        }

        public List<MessageCache> Parse(List<object[]> values)
        {
            List<MessageCache> results = new List<MessageCache>();

            if (values != null)
            {
                int total = 0;
                DetectorTypeHibernate detectorTypeHibernate = new DetectorTypeHibernate();
                List<DetectorType> detectorTypes = detectorTypeHibernate.Query(1, int.MaxValue, ref total);

                MachineHibernate machineHibernate = new MachineHibernate();
                List<Machine> machines = machineHibernate.Query(1, int.MaxValue, ref total);

                for (int i = 0; i < values.Count; i++)
                {
                    MessageCache value = this.Parse(values[i], machines, detectorTypes);
                    results.Add(value);
                }
            }

            return results;
        }

        public bool Insert(MessageCache value)
        {
            bool result = false;

            DatabaseHibernate hibernate = new DatabaseHibernate();

            if (value != null)
            {
                string sql = string.Empty;
                List<Parameter> parameters = new List<Parameter>();

                DateTime refreshTime = DateTime.Now;
                refreshTime = refreshTime.AddDays(-100);

                sql = string.Format("delete from e_message_cache where [refresh_time] < #{0}#", refreshTime.ToString("yyyy-MM-dd HH:mm:ss"));
                parameters.Clear();
                result = hibernate.Write(Variable.Link, sql, parameters);

                if (result)
                {
                    sql = string.Format("insert into e_message_cache ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :ip, :machine, :code, :result, :send_time, :refresh_time)", this.Asterisk(""));
                    parameters.Clear();

                    parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
                    parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
                    parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
                    parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
                    parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
                    parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
                    parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

                    parameters.Add(new Parameter("ip", DatabaseHibernate.Parameter(value.Ip)));
                    parameters.Add(new Parameter("machine_id", DatabaseHibernate.Parameter(value.MachineId)));
                    parameters.Add(new Parameter("code", DatabaseHibernate.Parameter(value.Code)));
                    parameters.Add(new Parameter("result", DatabaseHibernate.Parameter(value.Result)));
                    parameters.Add(new Parameter("send_time", DatabaseHibernate.Parameter(value.SendTime)));
                    parameters.Add(new Parameter("refresh_time", DatabaseHibernate.Parameter(value.RefreshTime)));

                    result = hibernate.Write(Variable.Link, sql, parameters);
                }
            }

            return result;
        }

        public List<MessageCache> QueryByMachine(string machineId, DateTime beginDate, DateTime endDate)
        {
            List<MessageCache> results = new List<MessageCache>();

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_message_cache as t where ([t].[send_time] >= :beginDate and [t].[send_time] <= :endDate) and [t].[machine_id] = '{1}' order by [t].[send_time] desc", this.Asterisk("[t]."), machineId);

            parameters.Add(new Parameter("beginDate", DatabaseHibernate.Parameter(beginDate)));
            parameters.Add(new Parameter("endDate", DatabaseHibernate.Parameter(endDate)));
            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            results = this.Parse(values);

            return results;
        }
    }
}
