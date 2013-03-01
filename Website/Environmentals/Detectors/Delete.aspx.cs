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

namespace EnvironmentalMonitor.Website.Environmentals.Detectors
{
    public partial class Delete : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DETECTOR_MODULE;
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

                this.DropDownListMachines.DataSource = machines;
                this.DropDownListMachines.DataTextField = "Name";
                this.DropDownListMachines.DataValueField = "Guid";
                this.DropDownListMachines.DataBind();
            }
        }

        private void InitializeBindDetectors()
        {
            if (this.DropDownListMachines.SelectedValue != null)
            {
                DetectorBusiness business = new DetectorBusiness();

                string machineId = this.DropDownListMachines.SelectedValue;
                List<Detector> detectors = business.QueryByMachine(machineId);

                this.CheckBoxListDetectors.DataSource = detectors;
                this.CheckBoxListDetectors.DataTextField = "Name";
                this.CheckBoxListDetectors.DataValueField = "Guid";
                this.CheckBoxListDetectors.DataBind();
            }
        }

        private void InitializeBindInput()
        {
        }

        private void InitializeBind()
        {
            this.InitializeBindRoom();
            this.InitializeBindMachine();
            this.InitializeBindDetectors();
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

            DetectorBusiness business = new DetectorBusiness();

            if (this.CheckBoxListDetectors.Items != null)
            {
                List<Detector> detectors = new List<Detector>();
                for (int i = 0; i < this.CheckBoxListDetectors.Items.Count; i++)
                {
                    if (this.CheckBoxListDetectors.Items[i].Selected)
                    {
                        Detector detector = new Detector();
                        detector.Guid = this.CheckBoxListDetectors.Items[i].Value;
                        detectors.Add(detector);
                    }
                }

                if ((detectors != null) && (detectors.Count > 0))
                {
                    int success = 0;
                    int fail = 0;
                    for (int i = 0; i < detectors.Count; i++)
                    {
                        done = business.Delete(detectors[i]);
                        if (done)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    stringBuilder.Append(string.Format("删除{0}个探头成功！", success));
                    stringBuilder.Append(string.Format("删除{0}个探头失败！", fail));
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
            this.InitializeBindDetectors();
        }

        protected void DropDownListMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindDetectors();
        }
    }
}