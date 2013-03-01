using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Manages.Defaults
{
    public partial class Log : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        private const string LOG_DIRECTORY = "Logs";

        public override string Code
        {
            get
            {
                return UserModule.PREROGATIVE_MODULE;
            }
        }

        private List<string> _LogFileUrls;
        public List<string> LogFileUrls
        {
            get
            {
                return this._LogFileUrls;
            }
            set
            {
                this._LogFileUrls = value;
            }
        }

        private List<string> _LogFiles;
        public List<string> LogFiles
        {
            get
            {
                return this._LogFiles;
            }
            set
            {
                this._LogFiles = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string logDirectory = string.Format("{0}{1}", Variable.PhysicalRootPath, Log.LOG_DIRECTORY);

            DirectoryInfo logDirectoryInfo = new DirectoryInfo(logDirectory);
            if (logDirectoryInfo.Exists)
            {
                FileInfo[] logFileInfos = logDirectoryInfo.GetFiles("*.txt", SearchOption.TopDirectoryOnly);

                if (logFileInfos != null)
                {
                    this.LogFileUrls = new List<string>();
                    this.LogFiles = new List<string>();
                    for (int i = 0; i < logFileInfos.Length; i++)
                    {
                        string logFileUrl = string.Format("{0}{1}/{2}", Variable.VirtualRootPath, Log.LOG_DIRECTORY, logFileInfos[i].Name);
                        this.LogFileUrls.Add(logFileUrl);
                        this.LogFiles.Add(logFileInfos[i].Name);
                    }
                }
            }
        }
    }
}