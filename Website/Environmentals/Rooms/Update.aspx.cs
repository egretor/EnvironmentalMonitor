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
    public partial class Update : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
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
            this.TextBoxName.Text = string.Empty;
            this.TextBoxAddress.Text = string.Empty;
            this.TextBoxContact.Text = string.Empty;
            this.TextBoxPhone.Text = string.Empty;

            RoomBusiness business = new RoomBusiness();

            int total = 0;
            List<Room> rooms = business.Query(1, int.MaxValue, ref total);
            Room emptyRoom = new Room();
            emptyRoom.Guid = string.Empty;
            emptyRoom.Name = string.Empty;
            rooms.Insert(0, emptyRoom);
            this.DropDownListRooms.DataSource = rooms;
            this.DropDownListRooms.DataTextField = "Name";
            this.DropDownListRooms.DataValueField = "Guid";
            this.DropDownListRooms.DataBind();
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
            string guid = this.DropDownListRooms.SelectedValue;
            if (!string.IsNullOrEmpty(guid))
            {
                bool done = false;
                StringBuilder stringBuilder = new StringBuilder();

                RoomBusiness business = new RoomBusiness();
                Room module = business.QueryByGuid(guid);

                if (module != null)
                {
                    this.InitializeUpdateModule(module);

                    module.Name = this.TextBoxName.Text;
                    module.Address = this.TextBoxAddress.Text;
                    module.Contact = this.TextBoxContact.Text;
                    module.Phone = this.TextBoxPhone.Text;

                    Room room = business.QueryByName(module.Name);

                    if ((room == null) || (string.Equals(room.Guid, module.Guid, StringComparison.CurrentCulture)))
                    {
                        done = business.Update(module);

                        if (done)
                        {
                            stringBuilder.Append("修改机房成功！");
                        }
                        else
                        {
                            stringBuilder.Append("修改机房失败！");
                        }
                    }
                    else
                    {
                        stringBuilder.Append("机房已经存在（名称相同）！");
                    }
                }
                else
                {
                    stringBuilder.Append("机房不存在！");
                }

                this.LabelMessage.Text = stringBuilder.ToString();
                this.InitializeBind();
            }
        }

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            string guid = this.DropDownListRooms.SelectedValue;
            if (!string.IsNullOrEmpty(guid))
            {
                this.LabelMessage.Text = string.Empty;
                RoomBusiness business = new RoomBusiness();

                Room currentRoom = business.QueryByGuid(guid);

                if (currentRoom != null)
                {
                    this.TextBoxName.Text = currentRoom.Name;
                    this.TextBoxAddress.Text = currentRoom.Address;
                    this.TextBoxContact.Text = currentRoom.Contact;
                    this.TextBoxPhone.Text = currentRoom.Phone;
                }
            }
        }
    }
}