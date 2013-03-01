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
    public partial class Insert : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            bool done = false;
            StringBuilder stringBuilder = new StringBuilder();

            RoomBusiness business = new RoomBusiness();
            Room module = new Room();

            this.InitializeInsertModule(module);

            module.Name = this.TextBoxName.Text;
            module.Address = this.TextBoxAddress.Text;
            module.Contact = this.TextBoxContact.Text;
            module.Phone = this.TextBoxPhone.Text;

            Room room = business.QueryByName(module.Name);

            if (room == null)
            {
                done = business.Insert(module);

                if (done)
                {
                    stringBuilder.Append("新增机房成功！");
                }
                else
                {
                    stringBuilder.Append("新增机房失败！");
                }
            }
            else
            {
                stringBuilder.Append("机房已经存在（名称相同）！");
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}