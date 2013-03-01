using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EnvironmentalMonitor.Support.Business.Manage;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Manages.Users
{
    public partial class Login : EnvironmentalMonitor.Website.Abstracts.AbstractPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            UserBusiness business = new UserBusiness();
            string account = this.TextBoxUsername.Text;
            string password = this.TextBoxPassword.Text;
            
            User user = business.Login(account, password);

            if ((user != null) && (user.Authentication))
            {
                this.Session[Constant.SESSION_KEY_USER] = user;
                this.Response.Redirect(string.Format("{0}Manages/Defaults/About.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
            }
        }
    }
}