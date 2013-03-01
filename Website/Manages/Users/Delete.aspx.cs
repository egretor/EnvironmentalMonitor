using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Business.Manage;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Manages.Users
{
    public partial class Delete : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.MANAGE_MODULE;
            }
        }

        private void InitializeBind()
        {
            this.RefreshUser();

            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

            this.CheckBoxListUsers.DataSource = sessionUser.Users;
            this.CheckBoxListUsers.DataTextField = "AccountName";
            this.CheckBoxListUsers.DataValueField = "Guid";
            this.CheckBoxListUsers.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.InitializeBind();
            }
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            bool done = false;
            StringBuilder stringBuilder = new StringBuilder();

            UserBusiness business = new UserBusiness();

            if (this.CheckBoxListUsers.Items != null)
            {
                List<User> users = new List<User>();
                for (int i = 0; i < this.CheckBoxListUsers.Items.Count; i++)
                {
                    if (this.CheckBoxListUsers.Items[i].Selected)
                    {
                        User user = new User();
                        user.Guid = this.CheckBoxListUsers.Items[i].Value;
                        users.Add(user);
                    }
                }

                if ((users != null) && (users.Count > 0))
                {
                    int success = 0;
                    int fail = 0;
                    for (int i = 0; i < users.Count; i++)
                    {
                        done = business.Delete(users[i]);
                        if (done)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    stringBuilder.Append(string.Format("删除{0}个用户成功！", success));
                    stringBuilder.Append(string.Format("删除{0}个用户失败！", fail));
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}