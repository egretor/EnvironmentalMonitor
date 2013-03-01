using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Data;
using System.Text;

using EnvironmentalMonitor.Website.Jsons;
using EnvironmentalMonitor.Support.Business.Environmental;
using EnvironmentalMonitor.Support.Module.Environmental;
using EnvironmentalMonitor.Support.Module.Manage;
using EnvironmentalMonitor.Support.Resource;

namespace EnvironmentalMonitor.Website.Environmentals.Monitors
{
    public partial class FloorPlanLoadJson : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.MONITOR_MODULE;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            const string MACHINE_ID = "machineId";

            string machineId = this.ParameterString(MACHINE_ID);

            DetectorBusiness business = new DetectorBusiness();
            List<Detector> modules = business.QueryByMachine(machineId);

            LoadJsonData loadJsonData = new LoadJsonData();
            loadJsonData.total = modules.Count;
            loadJsonData.rows = modules;
            string json = JsonConvert.SerializeObject(loadJsonData);

            this.Response.Write(json);
            this.Response.Flush();
            this.Response.End();
        }
    }
}