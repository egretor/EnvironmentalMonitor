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
    public partial class SetupAlarm : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.ALARM_MODULE;
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

            const string KeyText = "Key";
            const string ValueText = "Value";
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(KeyText);
            dataTable.Columns.Add(ValueText);
            dataTable.Rows.Add(new object[] { "是", true });
            dataTable.Rows.Add(new object[] { "否", false });

            this.RadioButtonListMobileAlarm.DataSource = dataTable;
            this.RadioButtonListMobileAlarm.DataTextField = KeyText;
            this.RadioButtonListMobileAlarm.DataValueField = ValueText;
            this.RadioButtonListMobileAlarm.DataBind();
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
            this.GridViewDetectors.DataSource = null;
            this.GridViewDetectors.DataBind();
            this.RadioButtonListMobileAlarm.SelectedValue = false.ToString();
            this.TextBoxMobileA.Text = string.Empty;
            this.TextBoxMobileB.Text = string.Empty;
            this.TextBoxMobileC.Text = string.Empty;

            if (this.DropDownListMachines.SelectedValue != null)
            {
                string guid = this.DropDownListMachines.SelectedValue;
                if (!string.IsNullOrEmpty(guid))
                {
                    MachineBusiness business = new MachineBusiness();
                    Machine value = business.QueryByGuid(guid);

                    if ((value != null) && (!string.IsNullOrEmpty(value.Guid)))
                    {
                        DetectorBusiness detectorBusiness = new DetectorBusiness();
                        List<Detector> detectors = detectorBusiness.QueryByMachine(value.Guid);

                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("C1");
                        dataTable.Columns.Add("C2");
                        dataTable.Columns.Add("C3");
                        dataTable.Columns.Add("C4");
                        dataTable.Columns.Add("C5");
                        dataTable.Columns.Add("C6");
                        for (int i = 0; i < detectors.Count; i++)
                        {
                            object[] datas = new object[dataTable.Columns.Count];
                            datas[0] = detectors[i].Serial;
                            datas[1] = detectors[i].DetectorType.Name;
                            if (detectors[i].DetectorType.Type == Support.Resource.DetectorTypes.Switch)
                            {
                                datas[2] = detectors[i].DetectorType.DescriptionA;
                                datas[3] = detectors[i].DetectorType.DescriptionB;
                                datas[4] = string.Empty;
                                datas[5] = string.Empty;
                            }
                            else
                            {
                                datas[2] = string.Format("{0}阀值下限{1}{2}", detectors[i].DetectorType.DescriptionA, detectors[i].MinimumA, detectors[i].DetectorType.UnitA);
                                datas[3] = string.Format("{0}阀值上限{1}{2}", detectors[i].DetectorType.DescriptionA, detectors[i].MaximumA, detectors[i].DetectorType.UnitA);
                                datas[4] = string.Format("{0}阀值下限{1}{2}", detectors[i].DetectorType.DescriptionB, detectors[i].MinimumB, detectors[i].DetectorType.UnitB);
                                datas[5] = string.Format("{0}阀值上限{1}{2}", detectors[i].DetectorType.DescriptionB, detectors[i].MaximumB, detectors[i].DetectorType.UnitB);
                            }
                            dataTable.Rows.Add(datas);
                        }

                        this.GridViewDetectors.DataSource = dataTable;
                        this.GridViewDetectors.DataBind();

                        this.RadioButtonListMobileAlarm.SelectedValue = value.Alarm.ToString();
                        this.TextBoxMobileA.Text = value.MobileA;
                        this.TextBoxMobileB.Text = value.MobileB;
                        this.TextBoxMobileC.Text = value.MobileC;
                    }
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

                            module.Alarm = bool.Parse(this.RadioButtonListMobileAlarm.SelectedValue);
                            module.MobileA = this.TextBoxMobileA.Text;
                            module.MobileB = this.TextBoxMobileB.Text;
                            module.MobileC = this.TextBoxMobileC.Text;

                            done = business.Update(module);

                            if (done)
                            {
                                stringBuilder.Append("修改检测仪成功！");

                                string message = string.Empty;

                                MachineSetup setup = new MachineSetup();
                                message = setup.Threshold(module);
                                stringBuilder.Append(message);
                                message = setup.MobileAlarm(module);
                                stringBuilder.Append(message);
                                message = setup.Mobile(module);
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

        protected void DropDownListCurrentRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
            this.InitializeBindInput();
        }

        protected void DropDownListMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindInput();
        }
    }
}