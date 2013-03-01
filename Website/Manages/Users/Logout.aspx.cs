using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EnvironmentalMonitor.Support.Module;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Manages.Users
{
    /// <summary>
    /// 注销页面类
    /// </summary>
    public partial class Logout : EnvironmentalMonitor.Website.Abstracts.AbstractPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EnvironmentalMonitor.Support.Module.Manage.User user = new EnvironmentalMonitor.Support.Module.Manage.User();
            user.Prerogative = false;
            this.Session.Add(Constant.SESSION_KEY_USER, user);
        }
    }
}