using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Web;

namespace EnvironmentalMonitor.Support.Resource
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Logger
    {
        private DirectoryInfo _Root;
        /// <summary>
        /// 日志根目录
        public DirectoryInfo Root
        {
            get
            {
                if (this._Root == null)
                {
                    if (!string.IsNullOrEmpty(Variable.LogDirectory))
                    {
                        try
                        {
                            this._Root = new DirectoryInfo(Variable.LogDirectory);
                            if (!this._Root.Exists)
                            {
                                this._Root.Create();
                            }
                        }
                        finally
                        {
                        }
                    }
                    if (this._Root == null)
                    {
                        try
                        {
                            string logDirectory = string.Format("{0}Logs", HttpRuntime.AppDomainAppPath);
                            this._Root = new DirectoryInfo(logDirectory);
                            if (!this._Root.Exists)
                            {
                                this._Root.Create();
                            }
                        }
                        finally
                        {
                        }
                    }
                }

                return this._Root;
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="exception">异常</param>
        public void Log(Exception exception, string message)
        {
            DirectoryInfo directoryInfo = this.Root;
            if (directoryInfo != null)
            {
                Type type = exception.TargetSite.DeclaringType;
                string timeValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string namespaceValue = type.Namespace;
                string classValue = type.Name;
                string methodValue = exception.TargetSite.Name;
                string positionValue = exception.StackTrace;
                string messageValue = string.Format("{0}{1}{2}", exception.Message, Environment.NewLine, message);

                StringBuilder log = new StringBuilder();
                log.Append(timeValue);
                log.Append(Environment.NewLine);
                log.Append(namespaceValue);
                log.Append(Environment.NewLine);
                log.Append(classValue);
                log.Append(Environment.NewLine);
                log.Append(methodValue);
                log.Append(Environment.NewLine);
                log.Append(positionValue);
                log.Append(Environment.NewLine);
                log.Append(messageValue);
                log.Append(Environment.NewLine);
                log.Append(new string('=', 100));
                log.Append(Environment.NewLine);

                lock (this)
                {
                    string logFile = string.Format("{0}\\{1}.exception.txt", directoryInfo.FullName, DateTime.Now.ToString("yyyy-MM-dd"));
                    StreamWriter streaWriter = new StreamWriter(logFile, true, Encoding.UTF8);
                    streaWriter.Write(log);
                    streaWriter.Close();
                }
            }
        }

        public void Log(Exception exception)
        {
            this.Log(exception, string.Empty);
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="remoteEP">远程IP</param>
        /// <param name="values">数据</param>
        public void Log(IPEndPoint localEP, IPEndPoint remoteEP, Direction direction, byte[] values)
        {
            DirectoryInfo directoryInfo = this.Root;
            if (directoryInfo != null)
            {
                string timeValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string remoteIP = string.Empty;

                if (remoteEP != null)
                {
                    remoteIP = remoteEP.ToString();
                }

                StringBuilder log = new StringBuilder();
                log.Append(timeValue);
                log.Append(Environment.NewLine);
                log.Append(localEP);
                log.Append(Environment.NewLine);
                log.Append(remoteIP);
                log.Append(Environment.NewLine);
                log.Append(direction);
                log.Append(Environment.NewLine);
                for (int i = 0; i < values.Length; i++)
                {
                    log.Append(values[i].ToString("X2"));
                    log.Append(" ");
                }
                log.Append(Environment.NewLine);
                log.Append(new string('=', 100));
                log.Append(Environment.NewLine);

                lock (this)
                {
                    string logFile = string.Format("{0}\\{1}.net.txt", directoryInfo.FullName, DateTime.Now.ToString("yyyy-MM-dd"));
                    StreamWriter streaWriter = new StreamWriter(logFile, true, Encoding.UTF8);
                    streaWriter.Write(log);
                    streaWriter.Close();
                }
            }
        }
    }
}
