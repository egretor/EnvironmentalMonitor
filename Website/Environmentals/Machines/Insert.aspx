<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Machines.Insert" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <% = EnvironmentalMonitor.Support.Resource.Variable.Product %>
    </title>
    <% = this.ReferencejQueryCss %><% = this.ReferencejQueryPluginsCss%><% = this.ReferenceCss %><% = this.ReferencejQueryJavascript %><% = this.ReferencejQueryPluginsJavascript %><% = this.ReferenceJavascript%>
</head>
<body>
    <% = this.Catalog %>
    <form id="form" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" class="containerTable">
        <tbody>
            <tr>
                <td align="center">
                    <div class="blue-header-popup" style="width: 800px">
                        <div class="header">
                            <h2>
                                新增检测仪</h2>
                        </div>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <thead>
                                    <tr style="height: 1px">
                                        <th style="width: 150px">
                                        </th>
                                        <th style="width: 250px">
                                        </th>
                                        <th style="width: 150px">
                                        </th>
                                        <th style="width: 250px">
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TextBoxName"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="名称不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorIP" runat="server" ControlToValidate="TextBoxIP"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="IP地址不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInterval" runat="server" ControlToValidate="TextBoxInterval"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="数据上传间隔秒数不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorIP" runat="server"
                                                ControlToValidate="TextBoxIP" Display="Dynamic" ErrorMessage="IP地址格式不正确！<br />"
                                                ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$"
                                                CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:RangeValidator ID="RangeValidatorInterval" runat="server" ControlToValidate="TextBoxInterval"
                                                Display="Dynamic" ErrorMessage="数据上传间隔秒数值范围从1到65535！<br />" MaximumValue="65535"
                                                MinimumValue="1" CssClass="errorMessage" Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server"
                                                ControlToValidate="TextBoxMobile" Display="Dynamic" ErrorMessage="手机号码格式不正确！<br />"
                                                ValidationExpression="^([0-9]{11})$" CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            机房：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListRooms" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            名称：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxName" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            IP地址：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxIP" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            数据上传间隔秒数：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxInterval" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            同步时间：
                                        </td>
                                        <td align="left">
                                            <asp:RadioButtonList ID="RadioButtonListTime" runat="server" RepeatDirection="Horizontal"
                                                Width="200px">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            发送短信手机号码：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMobile" runat="server" style="margin-bottom: 0px">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            平面图：
                                        </td>
                                        <td align="left">
                                            <asp:FileUpload ID="FileUploadFloorPlan" runat="server" />
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="footer">
                            <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="保存" />
                            <input type="reset" value="取消" />
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
