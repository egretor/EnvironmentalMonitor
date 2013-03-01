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
    public partial class Update : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
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

            this.TextBoxName.Text = string.Empty;
            this.TextBoxAccount.Text = string.Empty;
            this.TextBoxPassword.Attributes["value"] = string.Empty;
            this.TextBoxRepassword.Attributes["value"] = string.Empty;

            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

            this.CheckBoxListRooms.DataSource = sessionUser.Rooms;
            this.CheckBoxListRooms.DataTextField = "Name";
            this.CheckBoxListRooms.DataValueField = "Guid";
            this.CheckBoxListRooms.DataBind();

            this.CheckBoxListModules.DataSource = sessionUser.Catalogs;
            this.CheckBoxListModules.DataTextField = "Name";
            this.CheckBoxListModules.DataValueField = "Code";
            this.CheckBoxListModules.DataBind();

            List<User> users = new List<User>();
            User emptyUser = new User();
            emptyUser.Guid = string.Empty;
            emptyUser.Name = string.Empty;
            emptyUser.Account = string.Empty;
            users.Add(emptyUser);
            users.AddRange(sessionUser.Users);
            this.DropDownListUsers.DataSource = users;
            this.DropDownListUsers.DataTextField = "AccountName";
            this.DropDownListUsers.DataValueField = "Guid";
            this.DropDownListUsers.DataBind();
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
            string guid = this.DropDownListUsers.SelectedValue;
            if (!string.IsNullOrEmpty(guid))
            {
                bool done = false;
                StringBuilder stringBuilder = new StringBuilder();

                UserBusiness business = new UserBusiness();
                User module = business.QueryByGuid(guid);

                this.InitializeUpdateModule(module);

                module.Name = this.TextBoxName.Text;
                module.Account = this.TextBoxAccount.Text;
                module.Password = this.TextBoxPassword.Text;
                module.Prerogative = false;

                User user = business.QueryByAccount(module.Account);

                if ((user == null) || (string.Equals(user.Guid, module.Guid, StringComparison.CurrentCulture)))
                {
                    done = business.Update(module);

                    if (done)
                    {
                        stringBuilder.Append("修改用户成功！");
                    }
                    else
                    {
                        stringBuilder.Append("修改用户失败！");
                    }
                }
                else
                {
                    stringBuilder.Append("用户已经存在（账户相同）！");
                }

                if (done)
                {
                    user = business.QueryByAccount(module.Account);
                    if ((user != null) && (user.Guid != null))
                    {
                        if (this.CheckBoxListRooms.Items != null)
                        {
                            List<UserRoom> userRooms = new List<UserRoom>();
                            for (int i = 0; i < this.CheckBoxListRooms.Items.Count; i++)
                            {
                                if (this.CheckBoxListRooms.Items[i].Selected)
                                {
                                    UserRoom userRoom = new UserRoom();
                                    this.InitializeInsertModule(userRoom);

                                    userRoom.UserId = user.Guid;
                                    userRoom.RoomId = this.CheckBoxListRooms.Items[i].Value;

                                    userRooms.Add(userRoom);
                                }
                            }
                            UserRoomBusiness userRoomBusiness = new UserRoomBusiness();
                            done = userRoomBusiness.Refresh(user.Guid, userRooms);

                            if (done)
                            {
                                stringBuilder.Append("更新机房成功！");
                            }
                            else
                            {
                                stringBuilder.Append("更新机房失败！");
                            }
                        }
                    }
                    if ((user != null) && (user.Guid != null))
                    {
                        if (this.CheckBoxListModules.Items != null)
                        {
                            List<UserModule> userModules = new List<UserModule>();
                            for (int i = 0; i < this.CheckBoxListModules.Items.Count; i++)
                            {
                                if (this.CheckBoxListModules.Items[i].Selected)
                                {
                                    UserModule userModule = new UserModule();
                                    this.InitializeInsertModule(userModule);

                                    userModule.UserId = user.Guid;
                                    userModule.ModuleCode = this.CheckBoxListModules.Items[i].Value;

                                    userModules.Add(userModule);
                                }
                            }
                            UserModuleBusiness userModuleBusiness = new UserModuleBusiness();
                            done = userModuleBusiness.Refresh(user.Guid, userModules);

                            if (done)
                            {
                                stringBuilder.Append("更新权限成功！");
                            }
                            else
                            {
                                stringBuilder.Append("更新权限失败！");
                            }
                        }
                    }
                }

                this.LabelMessage.Text = stringBuilder.ToString();

                this.InitializeBind();
            }
        }

        protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string guid = this.DropDownListUsers.SelectedValue;
            if (!string.IsNullOrEmpty(guid))
            {
                this.LabelMessage.Text = string.Empty;
                UserBusiness business = new UserBusiness();

                User currentUser = business.QueryByGuid(guid);

                if (currentUser != null)
                {
                    this.TextBoxName.Text = currentUser.Name;
                    this.TextBoxAccount.Text = currentUser.Account;
                    this.TextBoxPassword.Attributes["value"] = currentUser.Password;
                    this.TextBoxRepassword.Attributes["value"] = currentUser.Password;

                    business.Refresh(currentUser);

                    this.CheckBoxListRooms.ClearSelection();
                    if (currentUser.Rooms != null)
                    {
                        for (int i = 0; i < currentUser.Rooms.Count; i++)
                        {
                            for (int j = 0; j < this.CheckBoxListRooms.Items.Count; j++)
                            {
                                if (string.Equals(currentUser.Rooms[i].Guid, this.CheckBoxListRooms.Items[j].Value, StringComparison.CurrentCulture))
                                {
                                    this.CheckBoxListRooms.Items[j].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    this.CheckBoxListModules.ClearSelection();
                    if (currentUser.UserModules != null)
                    {
                        for (int i = 0; i < currentUser.UserModules.Count; i++)
                        {
                            for (int j = 0; j < this.CheckBoxListModules.Items.Count; j++)
                            {
                                if (string.Equals(currentUser.UserModules[i].ModuleCode, this.CheckBoxListModules.Items[j].Value, StringComparison.CurrentCulture))
                                {
                                    this.CheckBoxListModules.Items[j].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}