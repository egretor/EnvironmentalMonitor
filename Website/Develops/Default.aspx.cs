using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.Net;
using System.Net.Sockets;

using EnvironmentalMonitor.Support.Device;
using EnvironmentalMonitor.Support.Instruction.In.Request;
using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Develops
{
    public partial class Default : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DEVELOP_MODULE;
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

            this.LabelIp.Text = string.Empty;

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

                        this.LabelIp.Text = value.Ip;
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

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
            this.InitializeBindInput();
        }

        protected void DropDownListMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindInput();
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
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

                        Random random = new Random();
                        if ((detectors != null) && (detectors.Count > 0))
                        {
                            for (int i = 0; i < detectors.Count; i++)
                            {
                                Detector current = detectors[i];
                                switch (current.DetectorType.Type)
                                {
                                    case DetectorTypes.Switch:
                                        current.Value = (ushort)random.Next(0, 2);
                                        break;
                                    case DetectorTypes.DoubleArea:
                                        byte valueA = (byte)random.Next(0, byte.MaxValue);
                                        byte valueB = (byte)random.Next(0, byte.MaxValue);
                                        current.Value = (ushort)((valueA * 0x100) + valueB);
                                        break;
                                }
                            }
                            string ipValue = value.Ip;
                            IPAddress ip = IPAddress.Parse(ipValue);
                            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
                            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

                            UploadInstruction instruction = new UploadInstruction(detectors);
                            instructions.Add(instruction);

                            Terminal.ExecuteInstruction(remoteEP, instructions);
                        }
                    }
                }
            }
        }

        protected void ButtonAlarm_Click(object sender, EventArgs e)
        {
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

                        Random random = new Random();
                        if ((detectors != null) && (detectors.Count > 0))
                        {
                            for (int i = 0; i < detectors.Count; i++)
                            {
                                Detector current = detectors[i];
                                switch (current.DetectorType.Type)
                                {
                                    case DetectorTypes.Switch:
                                        current.Value = (ushort)random.Next(0, 2);
                                        break;
                                    case DetectorTypes.DoubleArea:
                                        byte valueA = (byte)random.Next(0, byte.MaxValue);
                                        byte valueB = (byte)random.Next(0, byte.MaxValue);
                                        current.Value = (ushort)((valueA * 0x100) + valueB);
                                        break;
                                }
                            }

                            string ipValue = value.Ip;
                            IPAddress ip = IPAddress.Parse(ipValue);
                            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
                            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

                            AlarmInstruction instruction = new AlarmInstruction(detectors);
                            instructions.Add(instruction);

                            Terminal.ExecuteInstruction(remoteEP, instructions);
                        }
                    }
                }
            }
        }

        protected void ButtonMessage_Click(object sender, EventArgs e)
        {
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

                        Random random = new Random();
                        if ((detectors != null) && (detectors.Count > 0))
                        {
                            int address = random.Next(0, detectors.Count);
                            Detector detector = detectors[address];
                            bool result = false;
                            int resultRandom = random.Next(0, 2);
                            if (resultRandom == 0)
                            {
                                result = true;
                            }

                            string ipValue = value.Ip;
                            IPAddress ip = IPAddress.Parse(ipValue);
                            IPEndPoint remoteEP = new IPEndPoint(ip, Variable.Port);
                            List<AbstractInstruction> instructions = new List<AbstractInstruction>();

                            MessageInstruction instruction = new MessageInstruction(detector.DetectorType.Code, result, DateTime.Now);
                            instructions.Add(instruction);

                            Terminal.ExecuteInstruction(remoteEP, instructions);
                        }
                    }
                }
            }
        }
    }
}