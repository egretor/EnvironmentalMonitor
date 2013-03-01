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
    public partial class BackupDatabase : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.PREROGATIVE_MODULE;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            const string postfix = ".backup";

            string databaseFile = string.Format("{0}{1}", Variable.PhysicalRootPath, Variable.Database);
            string databaseBackupFile = string.Format("{0}{1}{2}", Variable.PhysicalRootPath, Variable.Database, postfix);
            string databaseBackupUrl = string.Format("{0}{1}{2}", Variable.VirtualRootPath, Variable.Database, postfix);

            bool done = false;
            try
            {
                FileInfo databaseBackupFileInfo = new FileInfo(databaseBackupFile);
                FileInfo databaseFileInfo = new FileInfo(databaseFile);

                if (databaseBackupFileInfo.Exists)
                {
                    databaseBackupFileInfo.Delete();
                }

                if (databaseFileInfo.Exists)
                {
                    databaseFileInfo.CopyTo(databaseBackupFileInfo.FullName, true);
                }

                done = true;
            }
            catch
            {
                done = false;
            }

            if (done)
            {
                this.Response.Redirect(databaseBackupUrl);
            }
        }
    }
}