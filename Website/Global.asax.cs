using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Configuration;
using System.IO;
using System.Reflection;

using EnvironmentalMonitor.Support.Device;
using EnvironmentalMonitor.Support.Device.Handler;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website
{
    public class Global : System.Web.HttpApplication
    {
        public delegate int CompareType(Type x, Type y);

        protected void Application_Start(object sender, EventArgs e)
        {
            #region 参数配置
            string configurationFile = string.Format("{0}Parameter.config", HttpRuntime.AppDomainAppPath);
            Variable.Initialize(configurationFile);
            #endregion

            #region 日志目录
            DirectoryInfo logDirectory = Variable.Logger.Root;
            if (logDirectory != null)
            {
                Variable.LogDirectory = logDirectory.FullName;
            }
            #endregion

            #region 网络监听
            Hardware.InstructionHandler = new InstanceHandler();

            Hardware.InstructionHandlers = new List<InstructionHandler>();

            LogHandler logHandler = new LogHandler();
            Hardware.InstructionHandlers.Add(logHandler);

            if (!Hardware.FatalError)
            {
                Listener.Run();
            }
            #endregion
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            EnvironmentalMonitor.Support.Module.Manage.User user = new EnvironmentalMonitor.Support.Module.Manage.User();
            user.Prerogative = false;
            this.Session.Add(Constant.SESSION_KEY_USER, user);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Listener.Shutdown();
        }
    }
}