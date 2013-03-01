using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;

using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Environmentals.Machines
{
    public partial class Insert : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.MACHINE_MODULE;
            }
        }

        private void InitializeBind()
        {
            this.RefreshUser();

            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

            this.DropDownListRooms.DataSource = sessionUser.Rooms;
            this.DropDownListRooms.DataTextField = "Name";
            this.DropDownListRooms.DataValueField = "Guid";
            this.DropDownListRooms.DataBind();

            const string KeyText = "Key";
            const string ValueText = "Value";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(KeyText);
            dataTable.Columns.Add(ValueText);
            dataTable.Rows.Add(new object[] { "是", true });
            dataTable.Rows.Add(new object[] { "否", false });

            this.RadioButtonListTime.DataSource = dataTable;
            this.RadioButtonListTime.DataTextField = KeyText;
            this.RadioButtonListTime.DataValueField = ValueText;
            this.RadioButtonListTime.DataBind();

            this.TextBoxName.Text = string.Empty;
            this.TextBoxIP.Text = string.Empty;
            this.TextBoxInterval.Text = string.Empty;
            this.TextBoxMobile.Text = string.Empty;
            this.RadioButtonListTime.SelectedIndex = 0;
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

            try
            {
                MachineBusiness business = new MachineBusiness();
                Machine module = new Machine();

                this.InitializeInsertModule(module);

                int intervalValue = int.Parse(this.TextBoxInterval.Text);

                module.RoomId = this.DropDownListRooms.SelectedValue;
                module.Name = this.TextBoxName.Text;
                module.Ip = this.TextBoxIP.Text;

                module.Netmask = string.Empty;
                module.Gateway = string.Empty;
                module.Mac = string.Empty;
                module.MobileA = string.Empty;
                module.MobileB = string.Empty;
                module.MobileC = string.Empty;
                module.Alarm = false;

                module.Interval = intervalValue;
                module.Mobile = this.TextBoxMobile.Text;

                Machine machine = business.QueryByNameOrIp(module.Name, module.RoomId, module.Ip);

                if (machine == null)
                {
                    done = business.Insert(module);

                    if (done)
                    {
                        stringBuilder.Append("新增检测仪成功！");

                        machine = business.QueryByNameOrIp(module.Name, module.RoomId, module.Ip);
                        if (machine != null)
                        {
                            string imageFile = this.MapPath(machine.FloorPlanHref);
                            this.UploadFileSave(this.FileUploadFloorPlan, imageFile);

                            string message = string.Empty;

                            MachineSetup setup = new MachineSetup();
                            // 设置检测仪上传数据时间间隔
                            message = setup.Interval(machine);
                            stringBuilder.Append(message);
                            // 设置检测仪时间
                            if (this.RadioButtonListTime.SelectedValue != null)
                            {
                                bool sync = false;
                                try
                                {
                                    sync = bool.Parse(this.RadioButtonListTime.SelectedValue);
                                }
                                finally
                                {
                                }
                                if (sync)
                                {
                                    message = setup.Time(machine);
                                    stringBuilder.Append(message);
                                }
                            }
                        }
                    }
                    else
                    {
                        stringBuilder.Append("新增检测仪失败！");
                    }
                }
                else
                {
                    stringBuilder.Append("检测仪已经存在（名称或IP地址相同）！");
                }
            }
            catch (Exception exception)
            {
                stringBuilder.Append("新增检测仪错误！");
                Variable.Logger.Log(exception);
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}