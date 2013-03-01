using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EnvironmentalMonitor.Website
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Redirect(string.Format("{0}Manages/Users/Login.aspx", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath));
        }
    }
}