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
    public partial class Insert : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DETECTOR_TYPE_MODULE;
            }
        }

        private void InitializeBindCode()
        {
            DetectorType switchDetectorType = new DetectorType();
            switchDetectorType.Type = EnvironmentalMonitor.Support.Resource.DetectorTypes.Switch;
            DetectorType doubleAreaDetectorType = new DetectorType();
            doubleAreaDetectorType.Type = EnvironmentalMonitor.Support.Resource.DetectorTypes.DoubleArea;

            List<DetectorType> detectorTypes = new List<DetectorType>();
            detectorTypes.Add(switchDetectorType);
            detectorTypes.Add(doubleAreaDetectorType);

            this.DropDownListTypes.DataSource = detectorTypes;
            this.DropDownListTypes.DataTextField = "TypeText";
            this.DropDownListTypes.DataValueField = "TypeValue";
            this.DropDownListTypes.DataBind();
        }

        private void InitializeBindInput()
        {
            this.TextBoxName.Text = string.Empty;
            this.TextBoxCode.Text = string.Empty;
            this.TextBoxDescriptionA.Text = string.Empty;
            this.TextBoxDescriptionB.Text = string.Empty;
            this.TextBoxUnitA.Text = string.Empty;
            this.TextBoxUnitB.Text = string.Empty;
        }

        private void InitializeBind()
        {
            this.InitializeBindCode();
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

            try
            {
                DetectorTypeBusiness business = new DetectorTypeBusiness();
                DetectorType module = new DetectorType();

                this.InitializeInsertModule(module);

                module.Name = this.TextBoxName.Text;
                module.Type = (EnvironmentalMonitor.Support.Resource.DetectorTypes)(int.Parse(this.DropDownListTypes.SelectedValue));
                module.Code = byte.Parse(this.TextBoxCode.Text);
                module.DescriptionA = this.TextBoxDescriptionA.Text;
                module.DescriptionB = this.TextBoxDescriptionB.Text;
                module.UnitA = this.TextBoxUnitA.Text;
                module.UnitB = this.TextBoxUnitB.Text;

                DetectorType detectorType = business.QueryByNameOrCode(module.Name, module.Code);

                if (detectorType == null)
                {
                    done = business.Insert(module);

                    if (done)
                    {
                        stringBuilder.Append("新增探头类型成功！");
                        detectorType = business.QueryByNameOrCode(module.Name, module.Code);
                        if (detectorType != null)
                        {
                            string[] imageFiles = { this.MapPath(detectorType.NormalHref), this.MapPath(detectorType.ErrorHref) };
                            FileUpload[] fileUploads = { this.FileUploadNormal, this.FileUploadError };
                            for (int i = 0; i < imageFiles.Length; i++)
                            {
                                this.UploadFileSave(fileUploads[i], imageFiles[i]);
                            }
                        }
                    }
                    else
                    {
                        stringBuilder.Append("新增探头类型失败！");
                    }
                }
                else
                {
                    stringBuilder.Append("探头类型已经存在（名称或代码相同）！");
                }
            }
            catch (Exception exception)
            {
                stringBuilder.Append("新增探头类型错误！");
                Variable.Logger.Log(exception);
            }

            this.LabelMessage.Text = stringBuilder.ToString();

            this.InitializeBind();
        }
    }
}