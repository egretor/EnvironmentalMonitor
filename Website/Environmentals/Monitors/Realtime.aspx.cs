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

namespace EnvironmentalMonitor.Website.Environmentals.Monitors
{
    public partial class Realtime : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        private const string ROOM_ID = "roomId";
        private const string MACHINE_ID = "machineId";

        public override string Code
        {
            get
            {
                return UserModule.MONITOR_MODULE;
            }
        }

        private string _RoomId;
        public string RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
            }
        }

        private string _MachineId;
        public string MachineId
        {
            get
            {
                return this._MachineId;
            }
            set
            {
                this._MachineId = value;
            }
        }

        public string LoadJsonDataUrl
        {
            get
            {
                string result = string.Empty;

                if ((!string.IsNullOrEmpty(this.RoomId)) && (!string.IsNullOrEmpty(this.MachineId)))
                {
                    result = string.Format("{0}Environmentals/Monitors/FloorPlanLoadJson.aspx?roomId={1}&machineId={2}", Variable.VirtualRootPath, this.RoomId, this.MachineId);
                }

                return result;
            }
        }

        public string loadDataCacheJsonUrl
        {
            get
            {
                string result = string.Empty;

                if ((!string.IsNullOrEmpty(this.RoomId)) && (!string.IsNullOrEmpty(this.MachineId)))
                {
                    result = string.Format("{0}Environmentals/Monitors/RealtimeLoadJson.aspx?roomId={1}&machineId={2}", Variable.VirtualRootPath, this.RoomId, this.MachineId);
                }

                return result;
            }
        }

        public string RefreshJsonUrl
        {
            get
            {
                return string.Format("{0}Environmentals/Monitors/Refresh.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);
            }
        }

        public string WarringSoundUrl
        {
            get
            {
                return string.Format("{0}Resources/Images/Default.wav", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath);
            }
        }

        private int _DataCacheInterval;
        public int DataCacheInterval
        {
            get
            {
                return this._DataCacheInterval;
            }
            set
            {
                this._DataCacheInterval = value;
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

            if (!string.IsNullOrEmpty(this.RoomId))
            {
                for (int i = 0; i < sessionUser.Rooms.Count; i++)
                {
                    if (string.Equals(this.RoomId, sessionUser.Rooms[i].Guid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.DropDownListRooms.SelectedValue = this.RoomId;
                        break;
                    }
                }
            }
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

                if (!string.IsNullOrEmpty(this.MachineId))
                {
                    for (int i = 0; i < machines.Count; i++)
                    {
                        if (string.Equals(this.MachineId, machines[i].Guid, StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.DropDownListMachines.SelectedValue = this.MachineId;
                            break;
                        }
                    }
                }
            }
        }

        private void InitializeBindInput()
        {
            if (this.DropDownListMachines.SelectedValue != null)
            {
                string guid = this.DropDownListMachines.SelectedValue;
                if (!string.IsNullOrEmpty(guid))
                {
                    MachineBusiness business = new MachineBusiness();
                    Machine value = business.QueryByGuid(guid);

                    if (value != null)
                    {
                        this.RoomId = value.RoomId;
                        this.MachineId = value.Guid;
                        this.ImageFloorPlan.ImageUrl = value.FloorPlanHref;
                        this.ImageFloorPlan.Visible = true;
                        this.DataCacheInterval = value.Interval * 1000;
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
            this.ImageFloorPlan.Visible = false;
            this.RoomId = this.ParameterString(Realtime.ROOM_ID);
            this.MachineId = this.ParameterString(Realtime.MACHINE_ID);

            if (!this.Page.IsPostBack)
            {
                this.InitializeBind();
            }
        }

        protected void DropDownListMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindInput();

            string path = string.Format("{0}?roomId={1}&machineId={2}", this.Request.Path, this.RoomId, this.MachineId);
            this.Response.Redirect(path);
        }

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitializeBindMachine();
            this.InitializeBindInput();
        }
    }
}