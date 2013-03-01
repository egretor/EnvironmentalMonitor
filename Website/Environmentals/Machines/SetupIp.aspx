<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetupIp.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Machines.SetupIp" %>

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
                            <h2>检测仪IP设置
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorIP" runat="server" ControlToValidate="TextBoxIP"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="IP地址不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorIP" runat="server"
                                                ControlToValidate="TextBoxIP" Display="Dynamic" ErrorMessage="IP地址格式不正确！<br />" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$"
                                                CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNetmask" runat="server" ControlToValidate="TextBoxNetmask"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="子网掩码不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorNetmask" runat="server"
                                                ControlToValidate="TextBoxNetmask" Display="Dynamic" ErrorMessage="子网掩码格式不正确！<br />"
                                                ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$"
                                                CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorGateway" runat="server" ControlToValidate="TextBoxGateway"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="网关不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorGateway" runat="server"
                                                ControlToValidate="TextBoxGateway" Display="Dynamic" ErrorMessage="网关格式不正确！<br />"
                                                ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])(\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])){3}$"
                                                CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            所属机房：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListCurrentRooms" runat="server" AutoPostBack="True"
                                                onselectedindexchanged="DropDownListCurrentRooms_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            检测仪：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListMachines" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownListMachines_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            IP：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxIP" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            子网掩码：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxNetmask" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            网关：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxGateway" runat="server">
                                            </asp:TextBox>
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
                            <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="设置" />
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
