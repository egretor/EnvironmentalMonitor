using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;

using EnvironmentalMonitor.Support.Business.Manage;
using EnvironmentalMonitor.Support.Business;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Abstracts
{
    /// <summary>
    /// 安全页面类
    /// </summary>
    public abstract class AbstractSecurityPage : EnvironmentalMonitor.Website.Abstracts.AbstractPage
    {
        public abstract string Code
        {
            get;
        }

        public void InitializeInsertModule(AbstractModule module)
        {
            EnvironmentalMonitor.Support.Module.Manage.User sessionUser = this.Session[Constant.SESSION_KEY_USER] as EnvironmentalMonitor.Support.Module.Manage.User;
            DateTime now = DateTime.Now;
            module.Guid = null;
            module.InsertUserId = sessionUser.Guid;
            module.InsertTime = now;
            module.UpdateUserId = sessionUser.Guid;
            module.UpdateTime = now;
            module.Remark = string.Empty;
            module.Validity = true;
        }

        public void InitializeUpdateModule(AbstractModule module)
        {
            EnvironmentalMonitor.Support.Module.Manage.User sessionUser = this.Session[Constant.SESSION_KEY_USER] as EnvironmentalMonitor.Support.Module.Manage.User;
            DateTime now = DateTime.Now;
            module.UpdateUserId = sessionUser.Guid;
            module.UpdateTime = now;
        }

        public string Catalog
        {
            get
            {
                string result = string.Empty;

                User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

                if (sessionUser != null)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    List<Catalog> catalogs = new List<Catalog>();
                    Catalog homeCatalog = new Catalog(UserModule.DEFAULT_MODULE, "首页", "#");
                    homeCatalog.Catalogs = new List<Catalog>();
                    homeCatalog.Catalogs.Add(new Catalog(UserModule.DEFAULT_MODULE, string.Format("注销（{0}）", sessionUser.AccountName), string.Format("{0}Manages/Users/Logout.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                    homeCatalog.Catalogs.Add(new Catalog(UserModule.DEFAULT_MODULE, "修改密码", string.Format("{0}Manages/Users/ChangePassword.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                    if (sessionUser.Prerogative)
                    {
                        homeCatalog.Catalogs.Add(new Catalog(UserModule.PREROGATIVE_MODULE, "备份数据库", string.Format("{0}Manages/Defaults/BackupDatabase.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                        homeCatalog.Catalogs.Add(new Catalog(UserModule.PREROGATIVE_MODULE, "系统日志", string.Format("{0}Manages/Defaults/Log.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                    }
                    homeCatalog.Catalogs.Add(new Catalog(UserModule.DEFAULT_MODULE, "关于", string.Format("{0}Manages/Defaults/About.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));
                    catalogs.Add(homeCatalog);

                    User user = this.Session[Constant.SESSION_KEY_USER] as User;
                    if (user.Catalogs != null)
                    {
                        catalogs.AddRange(user.Catalogs);
                    }

                    if (Variable.Debug && user.Prerogative)
                    {
                        Catalog debugCatalog = new Catalog(UserModule.DEBUG_MODULE, "开发调试", string.Format("{0}Debugs/Default.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
                        catalogs.Add(debugCatalog);
                    }

                    stringBuilder.Append("<ul id=\"catalog\" class=\"dropmenu\">");
                    for (int i = 0; i < catalogs.Count; i++)
                    {
                        stringBuilder.Append("<li>");
                        stringBuilder.Append(string.Format("<a href=\"{0}\">{1}</a>", catalogs[i].Url, catalogs[i].Name));

                        if (catalogs[i].Catalogs != null)
                        {
                            stringBuilder.Append("<ul>");
                            stringBuilder.Append("<li>");
                            for (int j = 0; j < catalogs[i].Catalogs.Count; j++)
                            {
                                stringBuilder.Append(string.Format("<a href=\"{0}\">{1}</a>", catalogs[i].Catalogs[j].Url, catalogs[i].Catalogs[j].Name));
                            }
                            stringBuilder.Append("</li>");
                            stringBuilder.Append("</ul>");
                        }
                        stringBuilder.Append("</li>");
                    }
                    stringBuilder.Append("</ul>");

                    result = stringBuilder.ToString();
                }

                return result;
            }
        }

        public void RefreshUser()
        {
            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;
            if (sessionUser != null)
            {
                if ((sessionUser.Authentication) || (sessionUser.Prerogative))
                {
                    UserBusiness business = new UserBusiness();
                    business.Refresh(sessionUser);
                    this.Session[Constant.SESSION_KEY_USER] = sessionUser;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            bool login = false;

            User user = this.Session[Constant.SESSION_KEY_USER] as User;
            if (user != null)
            {
                if ((user.Authentication) || (user.Prerogative))
                {
                    login = true;
                }
            }

            if (!login)
            {
                this.Response.Redirect(string.Format("{0}Manages/Users/Logout.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
            }
            else
            {
                Type pageType = this.GetType();
                UserBusiness business = new UserBusiness();
                bool allow = business.VerifyPage(user, pageType);

                if (!allow)
                {
                    this.Response.Redirect(string.Format("{0}Messages/VerifyFalse.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
                }
            }
        }
    }
}