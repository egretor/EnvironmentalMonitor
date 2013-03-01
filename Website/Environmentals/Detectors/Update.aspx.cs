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
    public partial class Update : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DETECTOR_MODULE;
            }
        }

        private void InitializeBindCurrentRoom()
        {
            this.RefreshUser();

            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;

            this.DropDownListCurrentRooms.DataSource = sessionUser.Rooms;
            this.DropDownListCurrentRooms.DataTextField = "Name";
            this.DropDownListCurrentRooms.DataValueField = "Guid";
            this.DropDownListCurrentRooms.DataBind();
        }

        private void InitializeBindCurrentMachine()
        {
            if (this.DropDownListCurrentRooms.SelectedValue != null)
            {
                MachineBusiness business = new MachineBusiness();

                string roomId = this.DropDownListCurrentRooms.SelectedValue;
                List<Machine> machines = business.QueryByRoom(roomId);

                this.DropDownListCurrentMachines.DataSource = machines;
                this.DropDownListCurrentMachines.DataTextField = "Name";
                this.DropDownListCurrentMachines.DataValueField = "Guid";
                this.DropDownListCurrentMachines.DataBind();
            }
        }

        private void InitializeBindDetectors()
        {
            if (this.DropDownListCurrentMachines.SelectedValue != null)
            {
                DetectorBusiness business = new DetectorBusiness();

                string machineId = this.DropDownListCurrentMachines.SelectedValue;
                List<Detector> detectors = business.QueryByMachine(machineId);
                Detector emptyDetector = new Detector();
                detectors.Insert(0, emptyDetector);

                this.DropDownListDetectors.DataSource = detectors;
                this.DropDownListDetectors.DataTextField = "Name";
                this.DropDownListDetectors.DataValueField = "Guid";
                this.DropDownListDetectors.DataBind();
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

        private void InitializeBindDetectorType()
        {
            DetectorTypeBusiness business = new DetectorTypeBusiness();

            int total = 0;
            List<DetectorType> detectorTypes = business.Query(1, int.MaxValue, ref total);

            this.DropDownListDetectorTypes.DataSource = detectorTypes;
            this.DropDownListDetectorTypes.DataTextField = "Name";
            this.DropDownListDetectorTypes.DataValueField = "Guid";
            this.DropDownListDetectorTypes.DataBind();
        }

        private void InitializeBindType()
        {
            if (this.DropDownListDetectorTypes.SelectedValue != null)
            {
                string guid = this.DropDownListDetectorTypes.SelectedValue;
                DetectorTypeBusiness business = new DetectorTypeBusiness();
                DetectorType detectorType = business.QueryByGuid(guid);
                switch (detectorType.Type)
                {
                    case Support.Resource.DetectorTypes.Switch:
                        this.LabelMinimumA.Visible = false;
                        this.LabelMaximumA.Visible = false;
                        this.LabelMinimumB.Visible = false;
                        this.LabelMaximumB.Visible = false;
                        this.TextBoxMinimumA.Visible = false;
                        this.TextBoxMaximumA.Visible = false;
                        this.TextBoxMinimumB.Visible = false;
                        this.TextBoxMaximumB.Visible = false;

                        this.RequiredFieldValidatorMinimumA.Visible = false;
                        this.RequiredFieldValidatorMaximumA.Visible = false;
                        this.RequiredFieldValidatorMinimumB.Visible = false;
                        this.RequiredFieldValidatorMaximumB.Visible = false;
                        this.RangeValidatorMinimumA.Visible = false;
                        this.RangeValidatorMaximumA.Visible = false;
                        this.RangeValidatorMinimumB.Visible = false;
                        this.RangeValidatorMaximumB.Visible = false;

                        this.RequiredFieldValidatorMinimumA.Enabled = false;
                        this.RequiredFieldValidatorMaximumA.Enabled = false;
                        this.RequiredFieldValidatorMinimumB.Enabled = false;
                        this.RequiredFieldValidatorMaximumB.Enabled = false;
                        this.RangeValidatorMinimumA.Enabled = false;
                        this.RangeValidatorMaximumA.Enabled = false;
                        this.RangeValidatorMinimumB.Enabled = false;
                        this.RangeValidatorMaximumB.Enabled = false;

                        this.LabelMinimumA.Text = string.Format("{0}：", detectorType.DescriptionA);
                        break;
                    case Support.Resource.DetectorTypes.DoubleArea:
                        this.LabelMinimumA.Visible = true;
                        this.LabelMaximumA.Visible = true;
                        this.LabelMinimumB.Visible = true;
                        this.LabelMaximumB.Visible = true;
                        this.TextBoxMinimumA.Visible = true;
                        this.TextBoxMaximumA.Visible = true;
                        this.TextBoxMinimumB.Visible = true;
                        this.TextBoxMaximumB.Visible = true;

                        this.RequiredFieldValidatorMinimumA.Visible = true;
                        this.RequiredFieldValidatorMaximumA.Visible = true;
                        this.RequiredFieldValidatorMinimumB.Visible = true;
                        this.RequiredFieldValidatorMaximumB.Visible = true;
                        this.RangeValidatorMinimumA.Visible = true;
                        this.RangeValidatorMaximumA.Visible = true;
                        this.RangeValidatorMinimumB.Visible = true;
                        this.RangeValidatorMaximumB.Visible = true;

                        this.RequiredFieldValidatorMinimumA.Enabled = true;
                        this.RequiredFieldValidatorMaximumA.Enabled = true;
                        this.RequiredFieldValidatorMinimumB.Enabled = true;
                        this.RequiredFieldValidatorMaximumB.Enabled = true;
                        this.RangeValidatorMinimumA.Enabled = true;
                        this.RangeValidatorMaximumA.Enabled = true;
                        this.RangeValidatorMinimumB.Enabled = true;
                        this.RangeValidatorMaximumB.Enabled = true;

                        this.LabelMinimumA.Text = string.Format("{0}阀值下限：<br />（{1}）", detectorType.DescriptionA, detectorType.UnitA);
                        this.LabelMaximumA.Text = string.Format("{0}阀值上限：<br />（{1}）", detectorType.DescriptionA, detectorType.UnitA);
                        this.LabelMinimumB.Text = string.Format("{0}阀值下限：<br />（{1}）", detectorType.DescriptionB, detectorType.UnitB);
                        this.LabelMaximumB.Text = string.Format("{0}阀值上限：<br />（{1}）", detectorType.DescriptionB, detectorType.UnitB);

                        this.RequiredFieldValidatorMinimumA.ErrorMessage = string.Format("{0}阀值下限不允许为空！<br />", detectorType.DescriptionA);
                        this.RequiredFieldValidatorMaximumA.ErrorMessage = string.Format("{0}阀值上限不允许为空！<br />", detectorType.DescriptionA);
                        this.RequiredFieldValidatorMinimumB.ErrorMessage = string.Format("{0}阀值下限不允许为空！<br />", detectorType.DescriptionB);
                        this.RequiredFieldValidatorMaximumB.ErrorMessage = string.Format("{0}阀值上限不允许为空！<br />", detectorType.DescriptionB);
                        this.RangeValidatorMinimumA.ErrorMessage = string.Format("{0}阀值下限范围从0到255！<br />", detectorType.DescriptionA);
                        this.RangeValidatorMaximumA.ErrorMessage = string.Format("{0}阀值上限范围从0到255！<br />", detectorType.DescriptionA);
                        this.RangeValidatorMinimumB.ErrorMessage = string.Format("{0}阀值下限范围从0到255！<br />", detectorType.DescriptionB);
                        this.RangeValidatorMaximumB.ErrorMessage = string.Format("{0}阀值上限范围从0到255！<br />", detectorType.DescriptionB);
                        break;
                }
            }
        }

        private void InitializeBindInput()
        {
            this.TextBoxSerial.Text = string.Empty;
            this.TextBoxMinimumA.Text = string.Empty;
            this.TextBoxMaximumA.Text = string.Empty;
            this.TextBoxMinimumB.Text = string.Empty;
            this.TextBoxMaximumB.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.DropDownListDetectors.SelectedValue))
            {
                DetectorBusiness business = new DetectorBusiness();

                string guid = this.DropDownListDetectors.SelectedValue;
                Detector value = business.QueryByGuid(guid);

                if (value != null)
                {
                    this.DropDownListRooms.SelectedValue = value.Machine.RoomId;

                    this.InitializeBindMachine();

                    this.DropDownListMachines.SelectedValue = value.MachineId;
                    this.DropDownListDetectorTypes.SelectedValue = value.DetectorTypeId;
                    this.TextBoxSerial.Text = value.Serial.ToString();
                    this.TextBoxMinimumA.Text = value.MinimumA.ToString();
                    this.TextBoxMaximumA.Text = value.MaximumA.ToString();
                    this.TextBoxMinimumB.Text = value.MinimumB.ToString();
                    this.TextBoxMaximumB.Text = value.MaximumB.ToString();
                }
            }
        }

        private void InitializeBind()
        {
            this.InitializeBindCurrentRoom();
            this.InitializeBindCurrentMachine();
            this.InitializeBindDetectors();
            this.InitializeBindRoom();
            this.InitializeBindMachine();
            this.InitializeBindDetectorType();
            this.InitializeBindInput();
            this.InitializeBindType();
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

            string machineId = this.DropDownListMachines.SelectedValue;
            if (!string.IsNullOrEmpty(machineId))
            {
                try
                {
                    if (!this.TextBoxMinimumA.Visible)
                    {
                        this.TextBoxMinimumA.Text = "0";
                        this.TextBoxMaximumA.Text = "1";
                        this.TextBoxMinimumB.Text = "0";
                        this.TextBoxMaximumB.Text = "1";
                    }

                    DetectorBusiness business = new DetectorBusiness();
                    Detector module = new Detector();

                    this.InitializeUpdateModule(module);

                    module.Guid = this.DropDownListDetectors.SelectedValue;
                    module.MachineId = this.DropDownListMachines.SelectedValue;
                    module.DetectorTypeId = this.DropDownListDetectorTypes.SelectedValue;
                    module.Serial = int.Parse(this.TextBoxSerial.Text);
                    module.MinimumA = int.Parse(this.TextBoxMinimumA.Text);
                    module.MaximumA = int.Parse(this.TextBoxMaximumA.Text);
                    module.MinimumB = int.Parse(this.TextBoxMinimumB.Text);
                    module.MaximumB = int.Parse(this.TextBoxMaximumB.Text);

                    int mum = 0;
                    if (module.MinimumA > module.MaximumA)
                    {
                        mum = module.MinimumA;
                        module.MinimumA = module.MaximumA;
                        module.MaximumA = mum;
                    }
                    if (module.MinimumB > module.MaximumB)
                    {
                        mum = module.MinimumB;
                        module.MinimumB = module.MaximumB;
                        module.MaximumB = mum;
                    }

                    done = business.Update(module);

                    if (done)
                    {
                        stringBuilder.Append("修改探头成功！");
                    }
                    else
                    {
                        stringBuilder.Append("修改探头失败！");
                    }
                }
                catch (Exception exception)
                {
                    stringBuilder.Append("修改探头错误！");
                    Variable.Logger.Log(exception);
                }
            }
            else
            {
                stringBuilder.Append("没有选择检测仪！");
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
        }

        protected void DropDownListDetectorTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindType();
        }

        protected void DropDownListCurrentRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindCurrentMachine();
            this.InitializeBindDetectors();
        }

        protected void DropDownListCurrentMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindDetectors();
        }

        protected void DropDownListDetectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindInput();
            this.InitializeBindType();
        }
    }
}