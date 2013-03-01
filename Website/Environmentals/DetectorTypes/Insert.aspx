<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.DetectorTypes.Insert" %>

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
                                新增探头类型
                            </h2>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCode" runat="server" ControlToValidate="TextBoxCode"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="代码不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidatorCode" runat="server" ControlToValidate="TextBoxCode"
                                                Display="Dynamic" ErrorMessage="代码值范围从0到255！<br />" MaximumValue="255" MinimumValue="0"
                                                CssClass="errorMessage" Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
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
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            类型：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListTypes" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            代码：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxCode" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            数值一描述：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxDescriptionA" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            数值二描述：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxDescriptionB" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            数值一单位：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxUnitA" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            数值二单位：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxUnitB" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            普通图：
                                        </td>
                                        <td align="left">
                                            <asp:FileUpload ID="FileUploadNormal" runat="server" />
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            错误图：
                                        </td>
                                        <td align="left">
                                            <asp:FileUpload ID="FileUploadError" runat="server" />
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
