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

namespace EnvironmentalMonitor.Website.Environmentals.Querys
{
    public partial class Default : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.QUERY_MODULE;
            }
        }

        private Room _Room;
        public Room Room
        {
            get
            {
                return this._Room;
            }
            set
            {
                this._Room = value;
            }
        }

        private Machine _Machine;
        public Machine Machine
        {
            get
            {
                return this._Machine;
            }
            set
            {
                this._Machine = value;
            }
        }

        private List<Detector> _Detectors;
        public List<Detector> Detectors
        {
            get
            {
                return this._Detectors;
            }
            set
            {
                this._Detectors = value;
            }
        }

        private List<Detector> _NormalDataCaches;
        public List<Detector> NormalDataCaches
        {
            get
            {
                return this._NormalDataCaches;
            }
            set
            {
                this._NormalDataCaches = value;
            }
        }

        private List<Detector> _AlarmDataCaches;
        public List<Detector> AlarmDataCaches
        {
            get
            {
                return this._AlarmDataCaches;
            }
            set
            {
                this._AlarmDataCaches = value;
            }
        }

        private List<MessageCache> _MessageCaches;
        public List<MessageCache> MessageCaches
        {
            get
            {
                return this._MessageCaches;
            }
            set
            {
                this._MessageCaches = value;
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
            DateTime now = DateTime.Now;
            DateTime beginDate = new DateTime(now.Year, now.Month, 1);
            DateTime endDate = beginDate.AddMonths(1).AddSeconds(-1);

            this.TextBoxBeginDate.Text = beginDate.ToString("yyyy-MM-dd");
            this.TextBoxEndDate.Text = endDate.ToString("yyyy-MM-dd");
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

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            DateTime now = DateTime.Now;
            DateTime beginDate = new DateTime(now.Year, now.Month, 1);
            DateTime endDate = beginDate.AddMonths(1).AddSeconds(-1);

            try
            {
                if (!string.IsNullOrEmpty(this.TextBoxBeginDate.Text))
                {
                    beginDate = DateTime.Parse(this.TextBoxBeginDate.Text);
                }
                if (!string.IsNullOrEmpty(this.TextBoxEndDate.Text))
                {
                    endDate = DateTime.Parse(this.TextBoxEndDate.Text);
                    endDate = endDate.AddDays(1).AddSeconds(-1);
                }
            }
            finally
            {
            }

            if (this.DropDownListMachines.SelectedValue != null)
            {
                string guid = this.DropDownListMachines.SelectedValue;
                if (!string.IsNullOrEmpty(guid))
                {
                    MachineBusiness machineBusiness = new MachineBusiness();
                    this.Machine = machineBusiness.QueryByGuid(guid);

                    if (this.Machine != null)
                    {
                        if (!string.IsNullOrEmpty(this.Machine.RoomId))
                        {
                            RoomBusiness roomBusiness = new RoomBusiness();
                            this.Room = roomBusiness.QueryByGuid(this.Machine.RoomId);
                        }

                        if (!string.IsNullOrEmpty(this.Machine.Guid))
                        {
                            DetectorBusiness detectorBusiness = new DetectorBusiness();
                            this.Detectors = detectorBusiness.QueryByMachine(this.Machine.Guid);

                            this.NormalDataCaches = detectorBusiness.QueryNormalDataCacheByMachine(this.Machine.Guid, beginDate, endDate);
                            this.AlarmDataCaches = detectorBusiness.QueryAlarmDataCacheByMachine(this.Machine.Guid, beginDate, endDate);

                            MessageCacheBusiness messageCacheBusiness = new MessageCacheBusiness();
                            this.MessageCaches = messageCacheBusiness.QueryByMachine(this.Machine.Guid, beginDate, endDate);
                        }
                    }
                }
                else
                {
                    stringBuilder.Append("没有选择检测仪！");
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();
        }
    }
}