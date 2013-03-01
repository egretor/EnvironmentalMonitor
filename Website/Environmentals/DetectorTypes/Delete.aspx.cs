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

namespace EnvironmentalMonitor.Website.Environmentals.DetectorTypes
{
    public partial class Delete : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DETECTOR_TYPE_MODULE;
            }
        }

        private void InitializeBindInput()
        {
            DetectorTypeBusiness business = new DetectorTypeBusiness();

            int total = 0;
            List<DetectorType> detectorTypes = business.Query(1, int.MaxValue, ref total);

            this.CheckBoxListDetectorTypes.DataSource = detectorTypes;
            this.CheckBoxListDetectorTypes.DataTextField = "Name";
            this.CheckBoxListDetectorTypes.DataValueField = "Guid";
            this.CheckBoxListDetectorTypes.DataBind();
        }

        private void InitializeBind()
        {
            this.InitializeBindInput();
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

            DetectorTypeBusiness business = new DetectorTypeBusiness();

            if (this.CheckBoxListDetectorTypes.Items != null)
            {
                List<DetectorType> detectorTypes = new List<DetectorType>();
                for (int i = 0; i < this.CheckBoxListDetectorTypes.Items.Count; i++)
                {
                    if (this.CheckBoxListDetectorTypes.Items[i].Selected)
                    {
                        DetectorType detectorType = new DetectorType();
                        detectorType.Guid = this.CheckBoxListDetectorTypes.Items[i].Value;
                        detectorTypes.Add(detectorType);
                    }
                }

                if ((detectorTypes != null) && (detectorTypes.Count > 0))
                {
                    int success = 0;
                    int fail = 0;
                    for (int i = 0; i < detectorTypes.Count; i++)
                    {
                        try
                        {
                            this.UploadFileDelete(this.MapPath(detectorTypes[i].NormalHref));
                            this.UploadFileDelete(this.MapPath(detectorTypes[i].ErrorHref));
                        }
                        finally
                        {
                        }
                        done = business.Delete(detectorTypes[i]);
                        if (done)
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    stringBuilder.Append(string.Format("删除{0}个探头类型成功！", success));
                    stringBuilder.Append(string.Format("删除{0}个探头类型失败！", fail));
                }
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}