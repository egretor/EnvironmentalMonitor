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
    /// 探头持久化类
    /// </summary>
    public class DetectorHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[machine_id], {0}[detector_type_id], {0}[serial], {0}[minimum_a], {0}[maximum_a], {0}[minimum_b], {0}[maximum_b], {0}[position_x], {0}[position_y]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <param name="machines">机房集合</param>
        /// <param name="detectorTypes">探头类型集合</param>
        /// <returns>探头</returns>
        public Detector Parse(object[] values, List<Machine> machines, List<DetectorType> detectorTypes)
        {
            Detector result = new Detector();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as Detector;

                result.MachineId = DatabaseHibernate.ParseString(values[7]);
                result.DetectorTypeId = DatabaseHibernate.ParseString(values[8]);
                result.Serial = DatabaseHibernate.ParseInt(values[9]);
                result.MinimumA = DatabaseHibernate.ParseInt(values[10]);
                result.MaximumA = DatabaseHibernate.ParseInt(values[11]);
                result.MinimumB = DatabaseHibernate.ParseInt(values[12]);
                result.MaximumB = DatabaseHibernate.ParseInt(values[13]);
                result.PositionX = DatabaseHibernate.ParseInt(values[14]);
                result.PositionY = DatabaseHibernate.ParseInt(values[15]);

                for (int i = 0; i < machines.Count; i++)
                {
                    if (string.Equals(result.MachineId, machines[i].Guid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result.Machine = machines[i];
                        break;
                    }
                }

                for (int i = 0; i < detectorTypes.Count; i++)
                {
                    if (string.Equals(result.DetectorTypeId, detectorTypes[i].Guid, StringComparison.CurrentCultureIgnoreCase))
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

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据集合</param>
        /// <returns>探头集合</returns>
        public List<Detector> Parse(List<object[]> values)
        {
            List<Detector> results = new List<Detector>();

            if (values != null)
            {
                int total = 0;
                DetectorTypeHibernate detectorTypeHibernate = new DetectorTypeHibernate();
                List<DetectorType> detectorTypes = detectorTypeHibernate.Query(1, int.MaxValue, ref total);

                MachineHibernate machineHibernate = new MachineHibernate();
                List<Machine> machines = machineHibernate.Query(1, int.MaxValue, ref total);

                for (int i = 0; i < values.Count; i++)
                {
                    Detector value = this.Parse(values[i], machines, detectorTypes);
                    results.Add(value);
                }
            }

            return results;
        }

        public List<Module.Environmental.Detector> QueryByMachine(string machineId)
        {
            List<Detector> results = new List<Detector>();

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_detector as t left join e_machine as u on [t].[machine_id] = [u].[guid] where [t].[machine_id] = '{1}' order by [t].[serial]", this.Asterisk("[t]."), machineId);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            results = this.Parse(values);

            return results;
        }

        public bool Insert(Detector value)
        {
            bool result = false;

            string sql = string.Format("insert into e_detector ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :machine_id, :detector_type_id, :serial, minimum_a, maximum_a, minimum_b, maximum_b, null, null)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(DatabaseHibernate.GUID())));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("machine_id", DatabaseHibernate.Parameter(value.MachineId)));
            parameters.Add(new Parameter("detector_type_id", DatabaseHibernate.Parameter(value.DetectorTypeId)));
            parameters.Add(new Parameter("serial", DatabaseHibernate.Parameter(value.Serial)));
            parameters.Add(new Parameter("minimum_a", DatabaseHibernate.Parameter(value.MinimumA)));
            parameters.Add(new Parameter("maximum_a", DatabaseHibernate.Parameter(value.MaximumA)));
            parameters.Add(new Parameter("minimum_b", DatabaseHibernate.Parameter(value.MinimumB)));
            parameters.Add(new Parameter("maximum_b", DatabaseHibernate.Parameter(value.MaximumB)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool Update(Detector value)
        {
            bool result = false;

            string sql = string.Format("update e_detector as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[remark] = :remark, [t].[validity] = :validity, [t].[machine_id] = :machine_id, [t].[detector_type_id] = :detector_type_id, [t].[serial] = :serial, [t].[minimum_a] = :minimum_a, [t].[maximum_a] = :maximum_a, [t].[minimum_b] = :minimum_b, [t].[maximum_b] = :maximum_b where [t].[guid] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("machine_id", DatabaseHibernate.Parameter(value.MachineId)));
            parameters.Add(new Parameter("detector_type_id", DatabaseHibernate.Parameter(value.DetectorTypeId)));
            parameters.Add(new Parameter("serial", DatabaseHibernate.Parameter(value.Serial)));
            parameters.Add(new Parameter("minimum_a", DatabaseHibernate.Parameter(value.MinimumA)));
            parameters.Add(new Parameter("maximum_a", DatabaseHibernate.Parameter(value.MaximumA)));
            parameters.Add(new Parameter("minimum_b", DatabaseHibernate.Parameter(value.MinimumB)));
            parameters.Add(new Parameter("maximum_b", DatabaseHibernate.Parameter(value.MaximumB)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool Delete(Detector value)
        {
            bool result = false;

            string sql = string.Format("delete from e_detector as t where [t].[guid] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public Detector QueryByGuid(string guid)
        {
            Detector result = null;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_detector as t where [t].[guid] = '{1}'", this.Asterisk("[t]."), guid);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<Detector> detectors = this.Parse(values);
            if ((detectors != null) && (detectors.Count > 0))
            {
                result = detectors[0];
            }

            return result;
        }

        public bool UpdatePosition(List<Detector> detectors)
        {
            bool result = false;

            DatabaseHibernate hibernate = new DatabaseHibernate();

            for (int i = 0; i < detectors.Count; i++)
            {
                string sql = string.Format("update e_detector as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[position_x] = :position_x, [t].[position_y] = :position_y where [t].[guid] = '{0}'", detectors[i].Guid);
                List<Parameter> parameters = new List<Parameter>();

                parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(detectors[i].UpdateUserId)));
                parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(detectors[i].UpdateTime)));

                parameters.Add(new Parameter("machine_id", DatabaseHibernate.Parameter(detectors[i].PositionX)));
                parameters.Add(new Parameter("machine_id", DatabaseHibernate.Parameter(detectors[i].PositionY)));

                result = hibernate.Write(Variable.Link, sql, parameters);

                if (!result)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <param name="machines">机房集合</param>
        /// <param name="detectorTypes">探头类型集合</param>
        /// <returns>探头</returns>
        public Detector ParseDataCache(object[] values, List<Machine> machines, List<DetectorType> detectorTypes)
        {
            Detector result = new Detector();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as Detector;

                result.MachineId = DatabaseHibernate.ParseString(values[7]);
                result.DetectorTypeId = DatabaseHibernate.ParseString(values[8]);
                result.Serial = DatabaseHibernate.ParseInt(values[9]);
                result.MinimumA = DatabaseHibernate.ParseInt(values[10]);
                result.MaximumA = DatabaseHibernate.ParseInt(values[11]);
                result.MinimumB = DatabaseHibernate.ParseInt(values[12]);
                result.MaximumB = DatabaseHibernate.ParseInt(values[13]);
                result.PositionX = DatabaseHibernate.ParseInt(values[14]);
                result.PositionY = DatabaseHibernate.ParseInt(values[15]);
                result.Value = DatabaseHibernate.ParseUshort(values[16]);
                result.RefreshTime = DatabaseHibernate.ParseDateTime(values[17]);

                for (int i = 0; i < machines.Count; i++)
                {
                    if (result.MachineId == machines[i].Guid)
                    {
                        result.Machine = machines[i];
                        break;
                    }
                }
                for (int i = 0; i < detectorTypes.Count; i++)
                {
                    if (result.DetectorTypeId == detectorTypes[i].Guid)
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

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据集合</param>
        /// <returns>探头集合</returns>
        public List<Detector> ParseDataCache(List<object[]> values)
        {
            List<Detector> results = new List<Detector>();

            if (values != null)
            {
                int total = 0;
                MachineHibernate machineHibernate = new MachineHibernate();
                List<Machine> machines = machineHibernate.Query(1, int.MaxValue, ref total);

                DetectorTypeHibernate detectorTypeHibernate = new DetectorTypeHibernate();
                List<DetectorType> detectorTypes = detectorTypeHibernate.Query(1, int.MaxValue, ref total);
                for (int i = 0; i < values.Count; i++)
                {
                    Detector value = this.ParseDataCache(values[i], machines, detectorTypes);
                    results.Add(value);
                }
            }

            return results;
        }

        public List<Detector> QueryDataCacheByMachine(string machineId)
        {
            List<Detector> results = new List<Detector>();

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0}, [v].[value], [v].[refresh_time] from (select t_a.* from e_detector as t_a left join e_machine as u_a on [t_a].[machine_id] = [u_a].[guid]) as t, e_data_cache as v where [t].[guid] = [v].[detector_id] and [v].[newest] = 1 and [t].[machine_id] = '{1}' order by [t].[serial], [v].[refresh_time] desc", this.Asterisk("[t]."), machineId);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            results = this.ParseDataCache(values);

            return results;
        }

        public List<Detector> QueryNormalDataCacheByMachine(string machineId, DateTime beginDate, DateTime endDate)
        {
            List<Detector> results = new List<Detector>();

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0}, [v].[value], [v].[refresh_time] from (select t_a.* from e_detector as t_a left join e_machine as u_a on [t_a].[machine_id] = [u_a].[guid]) as t, e_data_cache as v where [t].[guid] = [v].[detector_id] and ([v].[refresh_time] >= :beginDate and [v].[refresh_time] <= :endDate) and [v].[update_user_id] >= :type and [t].[machine_id] = '{1}' order by [v].[refresh_time] desc, [t].[serial]", this.Asterisk("[t]."), machineId);

            parameters.Add(new Parameter("beginDate", DatabaseHibernate.Parameter(beginDate)));
            parameters.Add(new Parameter("endDate", DatabaseHibernate.Parameter(endDate)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter("UploadInstruction")));
            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            results = this.ParseDataCache(values);

            return results;
        }

        public List<Detector> QueryAlarmDataCacheByMachine(string machineId, DateTime beginDate, DateTime endDate)
        {
            List<Detector> results = new List<Detector>();

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0}, [v].[value], [v].[refresh_time] from (select t_a.* from e_detector as t_a left join e_machine as u_a on [t_a].[machine_id] = [u_a].[guid]) as t, e_data_cache as v where [t].[guid] = [v].[detector_id] and ([v].[refresh_time] >= :beginDate and [v].[refresh_time] <= :endDate) and [v].[update_user_id] >= :type and [t].[machine_id] = '{1}' order by [v].[refresh_time] desc, [t].[serial]", this.Asterisk("[t]."), machineId);

            parameters.Add(new Parameter("beginDate", DatabaseHibernate.Parameter(beginDate)));
            parameters.Add(new Parameter("endDate", DatabaseHibernate.Parameter(endDate)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter("AlarmInstruction")));
            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            results = this.ParseDataCache(values);

            return results;
        }
    }
}
