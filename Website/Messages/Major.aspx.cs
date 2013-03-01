using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Layouts
{
    public partial class Major : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DEFAULT_MODULE;
            }
        }

        private List<Catalog> _Catalogs;
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<Catalog> Catalogs
        {
            get
            {
                return this._Catalogs;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._Catalogs = new List<Catalog>();
            this._Catalogs.Add(new Catalog(UserModule.DEFAULT_MODULE, "首页", string.Format("{0}WebForm1.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)));

            User user = this.Session[Constant.SESSION_KEY_USER] as User;
            if (user.Catalogs != null)
            {
                this.Catalogs.AddRange(user.Catalogs);
            }
        }
    }
}