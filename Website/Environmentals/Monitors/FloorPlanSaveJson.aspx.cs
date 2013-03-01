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
    public partial class FloorPlanSaveJson : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
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
            User sessionUser = this.Session[Constant.SESSION_KEY_USER] as User;
            DateTime now = DateTime.Now;

            SaveJsonData saveJsonData = new SaveJsonData();

            DetectorBusiness business = new DetectorBusiness();

            const int attributeCount = 3;
            int count = this.Request.Form.Count / attributeCount;
            List<Detector> detectors = new List<Detector>();
            for (int i = 0; i < count; i++)
            {
                Detector detector = new Detector();
                detector.Guid = this.Request.Form[(i * attributeCount) + 0];
                detector.UpdateUserId = sessionUser.Guid;
                detector.UpdateTime = now;
                try
                {
                    detector.PositionX = int.Parse(this.Request.Form[(i * attributeCount) + 1]);
                }
                catch (Exception exception)
                {
                    EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
                }
                try
                {
                    detector.PositionY = int.Parse(this.Request.Form[(i * attributeCount) + 2]);
                }
                catch (Exception exception)
                {
                    EnvironmentalMonitor.Support.Resource.Variable.Logger.Log(exception);
                }
                detectors.Add(detector);
            }
            saveJsonData.success = business.UpdatePosition(detectors);
            if (saveJsonData.success)
            {
                saveJsonData.msg = "布局保存成功！";
            }
            else
            {
                saveJsonData.msg = "布局保存失败";
            }

            string json = JsonConvert.SerializeObject(saveJsonData);

            this.Response.Write(json);
            this.Response.Flush();
            this.Response.End();
        }
    }
}