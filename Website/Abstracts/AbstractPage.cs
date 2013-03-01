using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.IO;

using EnvironmentalMonitor.Support.Device;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Abstracts
{
    /// <summary>
    /// 抽象页面类
    /// </summary>
    public abstract class AbstractPage : System.Web.UI.Page
    {
        public void UploadFileSave(FileUpload fileUpload, string fileName)
        {
            if (fileUpload.HasFile)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }

                fileUpload.SaveAs(fileInfo.FullName);
            }
        }

        public void UploadFileDelete(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }

        /// <summary>
        /// 字符串参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>值</returns>
        public string ParameterString(string name)
        {
            string result = null;

            if (this.Request.QueryString[name] != null)
            {
                result = this.Request.QueryString[name];
            }

            if (this.Request.Form[name] != null)
            {
                result = this.Request.Form[name];
            }

            return result;
        }

        /// <summary>
        /// 引用jQuery样式
        /// </summary>
        public string ReferencejQueryCss
        {
            get
            {
                string result = string.Empty;

                StringBuilder stringBuilder = new StringBuilder();

                result = stringBuilder.ToString();

                return result;
            }
        }

        /// <summary>
        /// 引用jQuery脚本
        /// </summary>
        public string ReferencejQueryJavascript
        {
            get
            {
                string result = string.Empty;

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}Resources/Javascripts/jQuery/jquery-1.8.3.js\"></script>", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);

                result = stringBuilder.ToString();

                return result;
            }
        }

        /// <summary>
        /// 引用样式
        /// </summary>
        public string ReferencejQueryPluginsCss
        {
            get
            {
                string result = string.Empty;

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Resources/javascripts/jQuery/dropmenu/dropmenu.css\" />", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);
                stringBuilder.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Resources/javascripts/jQuery/ui/jquery-ui-1.8.24.custom.css\" />", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);

                result = stringBuilder.ToString();

                return result;
            }
        }

        /// <summary>
        /// 引用脚本
        /// </summary>
        public string ReferencejQueryPluginsJavascript
        {
            get
            {
                string result = string.Empty;

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}Resources/Javascripts/jQuery/dropmenu/dropmenu.js\"></script>", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);
                stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}Resources/Javascripts/jQuery/ui/jquery-ui-1.8.24.custom.min.js\"></script>", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);

                result = stringBuilder.ToString();

                return result;
            }
        }

        /// <summary>
        /// 引用样式
        /// </summary>
        public string ReferenceCss
        {
            get
            {
                string result = string.Empty;

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendFormat("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Resources/javascripts/custom/global.css\" />", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);

                result = stringBuilder.ToString();

                return result;
            }
        }

        /// <summary>
        /// 引用脚本
        /// </summary>
        public string ReferenceJavascript
        {
            get
            {
                string result = string.Empty;

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}Resources/Javascripts/custom/global.js\"></script>", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);

                result = stringBuilder.ToString();

                return result;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Response.Expires = 0;
            this.Response.Buffer = true;
            this.Response.ExpiresAbsolute = DateTime.Now.AddYears(-1);
            this.Response.AddHeader("pragma", "no-cache");
            this.Response.CacheControl = "no-cache";

            if (Hardware.FatalError)
            {
                this.Response.Redirect(string.Format("{0}Messages/FatalError.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
            }
        }
    }
}