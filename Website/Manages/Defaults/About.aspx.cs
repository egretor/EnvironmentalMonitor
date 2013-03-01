﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EnvironmentalMonitor.Support.Module.Manage;

namespace EnvironmentalMonitor.Website.Manages.Defaults
{
    public partial class About : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DEFAULT_MODULE;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}