using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

using EnvironmentalMonitor.Support.Business.Manage;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Manages.Users
{
    public partial class ChangePassword : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DEFAULT_MODULE;
            }
        }

        private void InitializeBind()
        {
            this.TextBoxOldPassword.Attributes["value"] = string.Empty;
            this.TextBoxPassword.Attributes["value"] = string.Empty;
            this.TextBoxRepassword.Attributes["value"] = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            bool done = false;
            StringBuilder stringBuilder = new StringBuilder();

            EnvironmentalMonitor.Support.Module.Manage.User sessionUser = this.Session[Constant.SESSION_KEY_USER] as EnvironmentalMonitor.Support.Module.Manage.User;

            UserBusiness business = new UserBusiness();
            User user = business.QueryByAccount(sessionUser.Account);

            if ((user != null) && (string.Equals(user.Password, this.TextBoxOldPassword.Text, StringComparison.CurrentCulture)))
            {
                done = business.ChangePassword(sessionUser.Guid, this.TextBoxPassword.Text);
            }
            if (done)
            {
                sessionUser.Password = this.TextBoxPassword.Text;
                this.Session[Constant.SESSION_KEY_USER] = sessionUser;
                stringBuilder.Append("修改密码成功！");
            }
            else
            {
                stringBuilder.Append("修改密码失败！");
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}