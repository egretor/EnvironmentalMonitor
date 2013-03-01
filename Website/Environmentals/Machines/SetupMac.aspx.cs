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
    public partial class SetupMac : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.MACHINE_ADVANCED_MODULE;
            }
        }

        private void InitializeBindRoom()
        {
            this.RefreshUser();

            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

            this.DropDownListCurrentRooms.DataSource = sessionUser.Rooms;
            this.DropDownListCurrentRooms.DataTextField = "Name";
            this.DropDownListCurrentRooms.DataValueField = "Guid";
            this.DropDownListCurrentRooms.DataBind();
        }

        private void InitializeBindMachine()
        {
            if (this.DropDownListCurrentRooms.SelectedValue != null)
            {
                MachineBusiness business = new MachineBusiness();

                string roomId = this.DropDownListCurrentRooms.SelectedValue;
                List<Machine> machines = business.QueryByRoom(roomId);
                Machine emptyMachine = new Machine();
                machines.Insert(0, emptyMachine);

                this.DropDownListMachines.DataSource = machines;
                this.DropDownListMachines.DataTextField = "Name";
                this.DropDownListMachines.DataValueField = "Guid";
                this.DropDownListMachines.DataBind();
            }
        }

        private void InitializeBindInput()
        {
            this.TextBoxMac.Text = string.Empty;

            if (this.DropDownListMachines.SelectedValue != null)
            {
                string guid = this.DropDownListMachines.SelectedValue;
                if (!string.IsNullOrEmpty(guid))
                {
                    MachineBusiness business = new MachineBusiness();
                    Machine value = business.QueryByGuid(guid);

                    this.TextBoxMac.Text = value.Mac;
                }
            }
        }

        private void InitializeBind()
        {
            this.InitializeBindRoom();
            this.InitializeBindMachine();
            this.InitializeBindInput();
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

            if (this.DropDownListMachines.SelectedValue != null)
            {
                string guid = this.DropDownListMachines.SelectedValue;
                if (!string.IsNullOrEmpty(guid))
                {
                    try
                    {
                        MachineBusiness business = new MachineBusiness();
                        Machine module = business.QueryByGuid(guid);

                        if (module != null)
                        {
                            this.InitializeUpdateModule(module);

                            module.Mac = this.TextBoxMac.Text;

                            done = business.Update(module);

                            if (done)
                            {
                                stringBuilder.Append("修改检测仪成功！");

                                string message = string.Empty;

                                MachineSetup setup = new MachineSetup();
                                message = setup.Mac(module);
                                stringBuilder.Append(message);
                            }
                            else
                            {
                                stringBuilder.Append("修改检测仪失败！");
                            }
                        }
                        else
                        {
                            stringBuilder.Append("检测仪不存在！");
                        }
                    }
                    catch (Exception exception)
                    {
                        stringBuilder.Append("修改检测仪错误！");
                        Variable.Logger.Log(exception);
                    }
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }

        protected void DropDownListMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindInput();
        }

        protected void DropDownListCurrentRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
            this.InitializeBindInput();
        }
    }
}