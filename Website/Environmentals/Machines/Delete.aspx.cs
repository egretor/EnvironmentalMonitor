using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;

using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Environmentals.Machines
{
    public partial class Delete : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.MACHINE_MODULE;
            }
        }

        private void InitializeBindRoom()
        {
            this.RefreshUser();

            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

            this.DropDownListRooms.DataSource = sessionUser.Rooms;
            this.DropDownListRooms.DataTextField = "Name";
            this.DropDownListRooms.DataValueField = "Guid";
            this.DropDownListRooms.DataBind();
        }

        private void InitializeBindMachine()
        {
            if (this.DropDownListRooms.SelectedValue != null)
            {
                MachineBusiness business = new MachineBusiness();

                string roomId = this.DropDownListRooms.SelectedValue;
                List<Machine> machines = business.QueryByRoom(roomId);

                this.CheckBoxListMachines.DataSource = machines;
                this.CheckBoxListMachines.DataTextField = "Name";
                this.CheckBoxListMachines.DataValueField = "Guid";
                this.CheckBoxListMachines.DataBind();
            }
        }

        private void InitializeBind()
        {
            this.InitializeBindRoom();
            this.InitializeBindMachine();
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

            MachineBusiness business = new MachineBusiness();

            if (this.CheckBoxListMachines.Items != null)
            {
                List<Machine> machines = new List<Machine>();
                for (int i = 0; i < this.CheckBoxListMachines.Items.Count; i++)
                {
                    if (this.CheckBoxListMachines.Items[i].Selected)
                    {
                        Machine machine = new Machine();
                        machine.Guid = this.CheckBoxListMachines.Items[i].Value;
                        machines.Add(machine);
                    }
                }

                if ((machines != null) && (machines.Count > 0))
                {
                    int success = 0;
                    int fail = 0;
                    for (int i = 0; i < machines.Count; i++)
                    {
                        string imageFile = this.MapPath(machines[i].FloorPlanHref);
                        this.UploadFileDelete(imageFile);

                        done = business.Delete(machines[i]);
                        if (done)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    stringBuilder.Append(string.Format("删除{0}个检测仪成功！", success));
                    stringBuilder.Append(string.Format("删除{0}个检测仪失败！", fail));
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
        }
    }
}