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
    /// 探头类型持久化类
    /// </summary>
    public class DetectorTypeHibernate
    {
        /// <summary>
        /// 所有字段
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>所有字段</returns>
        public string Asterisk(string prefix)
        {
            string result = string.Empty;

            result = string.Format("{0}[guid], {0}[insert_user_id], {0}[insert_time], {0}[update_user_id], {0}[update_time], {0}[remark], {0}[validity], {0}[name], {0}[type], {0}[code], {0}[description_a], {0}[description_b], {0}[unit_a], {0}[unit_b]", prefix);

            return result;
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="values">数据</param>
        /// <returns>探头类型</returns>
        public DetectorType Parse(object[] values)
        {
            DetectorType result = new DetectorType();

            try
            {
                result = DatabaseHibernate.ParseModule(result, values) as DetectorType;

                result.Name = DatabaseHibernate.ParseString(values[7]);
                result.Type = DatabaseHibernate.ParseDetectorTypes(values[8]);
                result.Code = DatabaseHibernate.ParseByte(values[9]);
                result.DescriptionA = DatabaseHibernate.ParseString(values[10]);
                result.DescriptionB = DatabaseHibernate.ParseString(values[11]);
                result.UnitA = DatabaseHibernate.ParseString(values[12]);
                result.UnitB = DatabaseHibernate.ParseString(values[13]);
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
        /// <returns>探头类型集合</returns>
        public List<DetectorType> Parse(List<object[]> values)
        {
            List<DetectorType> results = new List<DetectorType>();

            if (values != null)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    DetectorType value = this.Parse(values[i]);
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
        /// <returns>探头类型集合</returns>
        public List<DetectorType> Query(int page, int rows, ref int total)
        {
            List<DetectorType> results = new List<DetectorType>();

            string sql = string.Format("select {0} from e_detector_type as t order by [t].[name]", this.Asterisk("[t]."));
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
        public bool Insert(DetectorType value)
        {
            bool result = false;

            string sql = string.Format("insert into e_detector_type ({0}) values (:guid, :insert_user_id, :insert_time, :update_user_id, :update_time, :remark, :validity, :name, :type, :code, :description_a, :description_b, :unit_a, :unit_b)", this.Asterisk(""));
            List<Parameter> parameters = new List<Parameter>();

            if (string.IsNullOrEmpty(value.Guid))
            {
                value.Guid = DatabaseHibernate.GUID();
            }

            parameters.Add(new Parameter("guid", DatabaseHibernate.Parameter(value.Guid)));
            parameters.Add(new Parameter("insert_user_id", DatabaseHibernate.Parameter(value.InsertUserId)));
            parameters.Add(new Parameter("insert_time", DatabaseHibernate.Parameter(value.InsertTime)));
            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("type", DatabaseHibernate.Parameter((int)value.Type)));
            parameters.Add(new Parameter("code", DatabaseHibernate.Parameter(value.Code)));
            parameters.Add(new Parameter("description_a", DatabaseHibernate.Parameter(value.DescriptionA)));
            parameters.Add(new Parameter("description_b", DatabaseHibernate.Parameter(value.DescriptionB)));
            parameters.Add(new Parameter("unit_a", DatabaseHibernate.Parameter(value.UnitA)));
            parameters.Add(new Parameter("unit_b", DatabaseHibernate.Parameter(value.UnitB)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

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

            string sql = string.Format("update e_detector_type as t set [t].[update_user_id] = :update_user_id, [t].[update_time] = :update_time, [t].[remark] = :remark, [t].[validity] = :validity, [t].[name] = :name, [t].[type] = :type, [t].[code] = :code, [t].[description_a] = :description_a, [t].[description_b] = :description_b, [t].[unit_a] = :unit_a, [t].[unit_b] = :unit_b where [t].[guid] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(new Parameter("update_user_id", DatabaseHibernate.Parameter(value.UpdateUserId)));
            parameters.Add(new Parameter("update_time", DatabaseHibernate.Parameter(value.UpdateTime)));
            parameters.Add(new Parameter("remark", DatabaseHibernate.Parameter(value.Remark)));
            parameters.Add(new Parameter("validity", DatabaseHibernate.Parameter(value.Validity)));

            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(value.Name)));
            parameters.Add(new Parameter("type", DatabaseHibernate.Parameter((int)value.Type)));
            parameters.Add(new Parameter("code", DatabaseHibernate.Parameter(value.Code)));
            parameters.Add(new Parameter("description_a", DatabaseHibernate.Parameter(value.DescriptionA)));
            parameters.Add(new Parameter("description_b", DatabaseHibernate.Parameter(value.DescriptionB)));
            parameters.Add(new Parameter("unit_a", DatabaseHibernate.Parameter(value.UnitA)));
            parameters.Add(new Parameter("unit_b", DatabaseHibernate.Parameter(value.UnitB)));

            DatabaseHibernate hibernate = new DatabaseHibernate();

            result = hibernate.Write(Variable.Link, sql, parameters);

            return result;
        }

        public bool Delete(DetectorType value)
        {
            bool result = false;

            DatabaseHibernate hibernate = new DatabaseHibernate();

            string sql = string.Format("delete from e_detector as t where [t].[detector_type_id] = '{0}'", value.Guid);
            List<Parameter> parameters = new List<Parameter>();

            result = hibernate.Write(Variable.Link, sql, parameters);

            if (result)
            {
                sql = string.Format("delete from e_detector_type as t where [t].[guid] = '{0}'", value.Guid);
                parameters.Clear();
                result = hibernate.Write(Variable.Link, sql, parameters);
            }

            return result;
        }

        public DetectorType QueryByNameOrCode(string name, byte code)
        {
            DetectorType result = null;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_detector_type as t where (([t].[name] = :name) or ([t].[code] = :code))", this.Asterisk("[t]."));
            parameters.Add(new Parameter("name", DatabaseHibernate.Parameter(name)));
            parameters.Add(new Parameter("code", DatabaseHibernate.Parameter(code)));

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<DetectorType> results = this.Parse(values);
            if ((results != null) && (results.Count > 0))
            {
                result = results[0];
            }

            return result;
        }

        public DetectorType QueryByGuid(string guid)
        {
            DetectorType result = null;

            List<Parameter> parameters = new List<Parameter>();
            DatabaseHibernate hibernate = new DatabaseHibernate();
            string sql = string.Format("select {0} from e_detector_type as t where [t].[guid] = '{1}'", this.Asterisk("[t]."), guid);

            List<object[]> values = hibernate.Read(Variable.Link, sql, parameters);
            List<DetectorType> results = this.Parse(values);
            if ((results != null) && (results.Count > 0))
            {
                result = results[0];
            }

            return result;
        }
    }
}
