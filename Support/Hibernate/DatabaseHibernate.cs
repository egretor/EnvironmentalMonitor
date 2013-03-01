using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;

using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Support.Hibernate
{
    /// <summary>
    /// 数据库持久化类
    /// </summary>
    public class DatabaseHibernate
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果</returns>
        public List<object[]> Read(string database, string sql, List<Parameter> parameters, int page, int rows)
        {
            List<object[]> results = null;

            int begin = (page - 1) * rows + 1;
            int end = page * rows;

            OleDbConnection connection = null;
            OleDbCommand command = null;
            OleDbDataReader dataReader = null;

            try
            {
                connection = new OleDbConnection(database);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;

                command.Parameters.Clear();
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        command.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                    }
                }

                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    results = new List<object[]>();
                    int count = 0;
                    while (dataReader.Read())
                    {
                        count++;
                        if (count > end)
                        {
                            break;
                        }
                        if (count >= begin)
                        {
                            object[] values = new object[dataReader.FieldCount];
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                values[i] = dataReader.GetValue(i);
                            }
                            results.Add(values);
                        }
                    }
                }
                dataReader.Close();
            }
            catch (Exception exception)
            {
                EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception, sql);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return results;
        }

        public List<object[]> Read(string database, string sql, List<Parameter> parameters)
        {
            return this.Read(database, sql, parameters, 1, int.MaxValue);
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>结果</returns>
        public bool Write(string database, string sql, List<Parameter> parameters)
        {
            bool result = false;

            OleDbConnection connection = null;
            OleDbCommand command = null;

            try
            {
                connection = new OleDbConnection(database);
                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;

                command.Parameters.Clear();
                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        command.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                    }
                }

                int count = command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception exception)
            {
                result = false;
                EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception, sql);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return result;
        }

        #region 参数转换
        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="value">整型</param>
        /// <returns>整型</returns>
        public static int Parameter(int value)
        {
            int result = 0;

            result = value;

            return result;
        }

        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="value">布尔值</param>
        /// <returns>整型</returns>
        public static int Parameter(bool value)
        {
            int result = 0;

            if (value)
            {
                result = 1;
            }

            return result;
        }

        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>字符串</returns>
        public static string Parameter(string value)
        {
            string result = string.Empty;

            if (value != null)
            {
                result = value;
            }

            return result;
        }

        /// <summary>
        /// 参数
        /// </summary>
        /// <param name="value">日期</param>
        /// <returns>日期</returns>
        public static string Parameter(DateTime value)
        {
            string result = string.Empty;

            result = value.ToString("yyyy-MM-dd HH:mm:ss");

            return result;
        }
        #endregion

        #region 值转换
        /// <summary>
        /// 探头类型枚举值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>探头类型枚举</returns>
        public static DetectorTypes ParseDetectorTypes(object value)
        {
            DetectorTypes result = DetectorTypes.Switch;

            if ((value != null) && (value != DBNull.Value))
            {
                result = (DetectorTypes)int.Parse(value.ToString());
            }

            return result;
        }

        /// <summary>
        /// 非负字节整型值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>非负字节整型</returns>
        public static byte ParseByte(object value)
        {
            byte result = 0;

            if ((value != null) && (value != DBNull.Value))
            {
                result = byte.Parse(value.ToString());
            }

            return result;
        }

        /// <summary>
        /// 非负短整型值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>非负短整型</returns>
        public static ushort ParseUshort(object value)
        {
            ushort result = 0;

            if ((value != null) && (value != DBNull.Value))
            {
                result = ushort.Parse(value.ToString());
            }

            return result;
        }

        /// <summary>
        /// 整型值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>整型</returns>
        public static int ParseInt(object value)
        {
            int result = 0;

            if ((value != null) && (value != DBNull.Value))
            {
                result = int.Parse(value.ToString());
            }

            return result;
        }

        /// <summary>
        /// 字符串值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字符串</returns>
        public static string ParseString(object value)
        {
            string result = null;

            if ((value != null) && (value != DBNull.Value))
            {
                result = value.ToString();
            }

            return result;
        }

        /// <summary>
        /// 日期值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>日期</returns>
        public static DateTime ParseDateTime(object value)
        {
            DateTime result = DateTime.MinValue;

            if ((value != null) && (value != DBNull.Value))
            {
                result = (DateTime)value;
            }

            return result;
        }

        /// <summary>
        /// 布尔值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>布尔</returns>
        public static bool ParseBoolean(object value)
        {
            bool result = false;

            if ((value != null) && (value != DBNull.Value))
            {
                if (int.Parse(value.ToString()) == 1)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 模型类
        /// </summary>
        /// <param name="module">模型值</param>
        /// <param name="values">值集合</param>
        /// <returns>模型值</returns>
        public static AbstractModule ParseModule(AbstractModule module, object[] values)
        {
            AbstractModule result = module;

            result.Guid = DatabaseHibernate.ParseString(values[0]);
            result.InsertUserId = DatabaseHibernate.ParseString(values[1]);
            result.InsertTime = DatabaseHibernate.ParseDateTime(values[2]);
            result.UpdateUserId = DatabaseHibernate.ParseString(values[3]);
            result.UpdateTime = DatabaseHibernate.ParseDateTime(values[4]);
            result.Remark = DatabaseHibernate.ParseString(values[5]);
            result.Validity = DatabaseHibernate.ParseBoolean(values[6]);

            return result;
        }
        #endregion

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns>主键</returns>
        public static string GUID()
        {
            string result = string.Empty;

            string values = Guid.NewGuid().ToString().ToUpper();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                if ((values[i] >= '0') && (values[i] <= '9'))
                {
                    stringBuilder.Append(values[i]);
                }
                if ((values[i] >= 'A') && (values[i] <= 'Z'))
                {
                    stringBuilder.Append(values[i]);
                }
            }
            result = stringBuilder.ToString();

            return result;
        }
    }
}