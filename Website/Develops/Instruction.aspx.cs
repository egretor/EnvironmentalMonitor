using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Reflection;
using System.Text;

using EnvironmentalMonitor.Support.Instruction;
using EnvironmentalMonitor.Support.Module.Manage;

namespace EnvironmentalMonitor.Website.Develops
{
    public partial class Instruction : EnvironmentalMonitor.Website.Abstracts.AbstractSecurityPage
    {
        protected List<string> instructionTypes;

        public override string Code
        {
            get
            {
                return UserModule.DEVELOP_MODULE;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.instructionTypes = new List<string>();

            Type abstractInstructionType = typeof(AbstractInstruction);
            Type[] types = abstractInstructionType.Assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == abstractInstructionType)
                {
                    AbstractInstruction instruction = type.Assembly.CreateInstance(type.FullName) as AbstractInstruction;
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(instruction.Controller.ToString("X2"));
                    stringBuilder.Append(" ");
                    byte[] values = BitConverter.GetBytes(instruction.Type);
                    stringBuilder.Append(values[1].ToString("X2"));
                    stringBuilder.Append(" ");
                    stringBuilder.Append(values[0].ToString("X2"));
                    stringBuilder.Append(" ");
                    stringBuilder.Append(type.FullName);
                    this.instructionTypes.Add(stringBuilder.ToString());
                    this.instructionTypes.Sort();
                }
            }
        }
    }
}