using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Xml;

namespace EnvironmentalMonitor.Support.Resource
{
    /// <summary>
    /// 变量
    /// </summary>
    public class Variable
    {
        private static bool _Debug = true;
        /// <summary>
        /// 调试
        /// </summary>
        public static bool Debug
        {
            get
            {
                return Variable._Debug;
            }
            set
            {
                Variable._Debug = value;
            }
        }

        private static string _Product;
        /// <summary>
        /// 产品
        /// </summary>
        public static string Product
        {
            get
            {
                return Variable._Product;
            }
            set
            {
                Variable._Product = value;
            }
        }

        private static string _VirtualRootPath;
        public static string VirtualRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(Variable._VirtualRootPath))
                {
                    int index = HttpRuntime.AppDomainAppVirtualPath.Length - 1;
                    string separator = string.Empty;
                    if (HttpRuntime.AppDomainAppVirtualPath[index] != '/')
                    {
                        separator = "/";
                    }
                    Variable._VirtualRootPath = string.Format("{0}{1}", HttpRuntime.AppDomainAppVirtualPath, separator);
                }

                return Variable._VirtualRootPath;
            }
        }

        private static string _PhysicalRootPath;
        public static string PhysicalRootPath
        {
            get
            {
                if (string.IsNullOrEmpty(Variable._PhysicalRootPath))
                {
                    int index = HttpRuntime.AppDomainAppPath.Length - 1;
                    string separator = string.Empty;
                    if (HttpRuntime.AppDomainAppPath[index] != '\\')
                    {
                        separator = "\\";
                    }
                    Variable._PhysicalRootPath = string.Format("{0}{1}", HttpRuntime.AppDomainAppPath, separator);
                }

                return Variable._PhysicalRootPath;
            }
        }

        private static int _Port = 5010;
        /// <summary>
        /// 通信端口
        /// </summary>
        public static int Port
        {
            get
            {
                return Variable._Port;
            }
            set
            {
                Variable._Port = value;
            }
        }

        private static int _ArqTimes = 3;
        /// <summary>
        /// ARQ应答次数
        /// </summary>
        public static int ArqTimes
        {
            get
            {
                return Variable._ArqTimes;
            }
            set
            {
                Variable._ArqTimes = value;
            }
        }

        private static int _ArqTimeout = 5;
        /// <summary>
        /// ARQ应答超时
        /// </summary>
        public static int ArqTimeout
        {
            get
            {
                return Variable._ArqTimeout;
            }
            set
            {
                Variable._ArqTimeout = value;
            }
        }

        private static string _LogDirectory;
        /// <summary>
        /// 日志目录
        /// </summary>
        public static string LogDirectory
        {
            get
            {
                return Variable._LogDirectory;
            }
            set
            {
                Variable._LogDirectory = value;
            }
        }

        private static Logger _Logger = new Logger();
        /// <summary>
        /// 日志记录器
        /// </summary>
        public static Logger Logger
        {
            get
            {
                return Variable._Logger;
            }
            set
            {
                Variable._Logger = value;
            }
        }

        private static string _Database;
        /// <summary>
        /// 数据库
        /// </summary>
        public static string Database
        {
            get
            {
                return Variable._Database;
            }
            set
            {
                Variable._Database = value;
            }
        }

        private static string _Link;
        /// <summary>
        /// 数据库链接
        /// </summary>
        public static string Link
        {
            get
            {
                return Variable._Link;
            }
            set
            {
                Variable._Link = value;
            }
        }

        private static byte[] _Shutdown;
        /// <summary>
        /// 关闭套接字代码
        /// </summary>
        public static byte[] Shutdown
        {
            get
            {
                if (Variable._Shutdown == null)
                {
                    Variable._Shutdown = new byte[5];
                    Random random = new Random();
                    random.NextBytes(Variable._Shutdown);
                }
                return Variable._Shutdown;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configurationFile">配置文件</param>
        public static void Initialize(string configurationFile)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(configurationFile);
            XmlElement root = xmlDocument.DocumentElement;
            if (root != null)
            {
                XmlElement node = root["appSettings"];
                if (node != null)
                {
                    XmlNodeList parameters = node.ChildNodes;
                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            XmlAttribute keyAtribute = parameters[i].Attributes["key"];
                            XmlAttribute valueAtribute = parameters[i].Attributes["value"];
                            if (keyAtribute != null)
                            {
                                if (keyAtribute.Value == "Debug")
                                {
                                    if (!string.IsNullOrEmpty(valueAtribute.Value))
                                    {
                                        try
                                        {
                                            Variable._Debug = bool.Parse(valueAtribute.Value);
                                        }
                                        finally
                                        {
                                        }
                                    }
                                    continue;
                                }
                                if (keyAtribute.Value == "Product")
                                {
                                    Variable.Product = valueAtribute.Value;
                                    continue;
                                }
                                if (keyAtribute.Value == "Port")
                                {
                                    if (!string.IsNullOrEmpty(valueAtribute.Value))
                                    {
                                        try
                                        {
                                            Variable.Port = int.Parse(valueAtribute.Value);
                                        }
                                        finally
                                        {
                                        }
                                    }
                                    continue;
                                }
                                if (keyAtribute.Value == "ArqTimes")
                                {
                                    if (!string.IsNullOrEmpty(valueAtribute.Value))
                                    {
                                        try
                                        {
                                            Variable.ArqTimes = int.Parse(valueAtribute.Value);
                                        }
                                        finally
                                        {
                                        }
                                    }
                                    continue;
                                }
                                if (keyAtribute.Value == "ArqTimeout")
                                {
                                    if (!string.IsNullOrEmpty(valueAtribute.Value))
                                    {
                                        try
                                        {
                                            Variable.ArqTimeout = int.Parse(valueAtribute.Value);
                                        }
                                        finally
                                        {
                                        }
                                    }
                                    continue;
                                }
                                if (keyAtribute.Value == "LogDirectory")
                                {
                                    Variable.LogDirectory = valueAtribute.Value;
                                    continue;
                                }
                                if (keyAtribute.Value == "Database")
                                {
                                    Variable.Database = valueAtribute.Value;
                                    Variable.Link = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}{1}", Variable.PhysicalRootPath, valueAtribute.Value);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
