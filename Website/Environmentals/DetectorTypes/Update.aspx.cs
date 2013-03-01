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
    public partial class Update : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        public override string Code
        {
            get
            {
                return UserModule.DETECTOR_TYPE_MODULE;
            }
        }

        private void InitializeBindDetectorType()
        {
            DetectorTypeBusiness business = new DetectorTypeBusiness();

            int total = 0;
            List<DetectorType> detectorTypes = business.Query(1, int.MaxValue, ref total);
            DetectorType emptyDetectorType = new DetectorType();
            emptyDetectorType.Guid = string.Empty;
            emptyDetectorType.Name = string.Empty;
            detectorTypes.Insert(0, emptyDetectorType);

            this.DropDownListDetectorTypes.DataSource = detectorTypes;
            this.DropDownListDetectorTypes.DataTextField = "Name";
            this.DropDownListDetectorTypes.DataValueField = "Guid";
            this.DropDownListDetectorTypes.DataBind();
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
            this.InitializeBindDetectorType();
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
            string guid = this.DropDownListDetectorTypes.SelectedValue;
            if (!string.IsNullOrEmpty(guid))
            {
                bool done = false;
                StringBuilder stringBuilder = new StringBuilder();

                try
                {
                    DetectorTypeBusiness business = new DetectorTypeBusiness();
                    DetectorType module = business.QueryByGuid(guid);

                    if (module != null)
                    {
                        this.InitializeUpdateModule(module);

                        module.Name = this.TextBoxName.Text;
                        module.Type = (EnvironmentalMonitor.Support.Resource.DetectorTypes)(int.Parse(this.DropDownListTypes.SelectedValue));
                        module.Code = byte.Parse(this.TextBoxCode.Text);
                        module.DescriptionA = this.TextBoxDescriptionA.Text;
                        module.DescriptionB = this.TextBoxDescriptionB.Text;
                        module.UnitA = this.TextBoxUnitA.Text;
                        module.UnitB = this.TextBoxUnitB.Text;

                        DetectorType detectorType = business.QueryByNameOrCode(module.Name, module.Code);

                        if ((detectorType == null) || (string.Equals(detectorType.Guid, module.Guid, StringComparison.CurrentCulture)))
                        {
                            done = business.Update(module);

                            if (done)
                            {
                                stringBuilder.Append("修改探头类型成功！");
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
                                stringBuilder.Append("修改探头类型失败！");
                            }
                        }
                        else
                        {
                            stringBuilder.Append("探头类型已经存在（名称或代码相同）！");
                        }
                    }
                    else
                    {
                        stringBuilder.Append("探头类型不存在！");
                    }
                }
                catch (Exception exception)
                {
                    stringBuilder.Append("修改探头类型错误！");
                    Variable.Logger.Log(exception);
                }

                this.LabelMessage.Text = stringBuilder.ToString();
                this.InitializeBind();
            }
        }

        protected void DropDownListDetectorTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string guid = this.DropDownListDetectorTypes.SelectedValue;
            if (!string.IsNullOrEmpty(guid))
            {
                this.LabelMessage.Text = string.Empty;
                DetectorTypeBusiness business = new DetectorTypeBusiness();

                DetectorType currentDetectorType = business.QueryByGuid(guid);

                if (currentDetectorType != null)
                {
                    this.TextBoxName.Text = currentDetectorType.Name;
                    this.DropDownListTypes.SelectedValue = currentDetectorType.TypeValue.ToString();
                    this.TextBoxCode.Text = currentDetectorType.Code.ToString();
                    this.TextBoxDescriptionA.Text = currentDetectorType.DescriptionA;
                    this.TextBoxDescriptionB.Text = currentDetectorType.DescriptionB;
                    this.TextBoxUnitA.Text = currentDetectorType.UnitA;
                    this.TextBoxUnitB.Text = currentDetectorType.UnitB;

                    this.ImageNormal.ImageUrl = currentDetectorType.NormalHref;
                    this.ImageError.ImageUrl = currentDetectorType.ErrorHref;
                }
            }
        }
    }
}