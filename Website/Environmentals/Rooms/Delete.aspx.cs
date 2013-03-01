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

namespace EnvironmentalMonitor.Website.Environmentals.Rooms
{
    public partial class Delete : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.ROOM_MODULE;
            }
        }

        private void InitializeBind()
        {
            RoomBusiness business = new RoomBusiness();

            int total = 0;
            List<Room> rooms = business.Query(1, int.MaxValue, ref total);

            this.CheckBoxListRooms.DataSource = rooms;
            this.CheckBoxListRooms.DataTextField = "Name";
            this.CheckBoxListRooms.DataValueField = "Guid";
            this.CheckBoxListRooms.DataBind();
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

            RoomBusiness business = new RoomBusiness();

            if (this.CheckBoxListRooms.Items != null)
            {
                List<Room> rooms = new List<Room>();
                for (int i = 0; i < this.CheckBoxListRooms.Items.Count; i++)
                {
                    if (this.CheckBoxListRooms.Items[i].Selected)
                    {
                        Room room = new Room();
                        room.Guid = this.CheckBoxListRooms.Items[i].Value;
                        rooms.Add(room);
                    }
                }

                if ((rooms != null) && (rooms.Count > 0))
                {
                    int success = 0;
                    int fail = 0;
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        done = business.Delete(rooms[i]);
                        if (done)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    stringBuilder.Append(string.Format("删除{0}个机房成功！", success));
                    stringBuilder.Append(string.Format("删除{0}个机房失败！", fail));
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}