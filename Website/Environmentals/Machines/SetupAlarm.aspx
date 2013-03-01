<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetupAlarm.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Machines.SetupAlarm" %>

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
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                报警设置
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <thead>
                                    <tr style="height: 1px">
                                        <th style="width: 250px">
                                        </th>
                                        <th style="width: 250px">
                                        </th>
                                        <th style="width: 250px">
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
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobileA" runat="server"
                                                ControlToValidate="TextBoxMobileA" Display="Dynamic" ErrorMessage="手机号码一格式不正确！<br />"
                                                ValidationExpression="^([0-9]{11})$" CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobileB" runat="server"
                                                ControlToValidate="TextBoxMobileB" Display="Dynamic" ErrorMessage="手机号码二格式不正确！<br />"
                                                ValidationExpression="^([0-9]{11})$" CssClass="errorMessage">
                                            </asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobileC" runat="server"
                                                ControlToValidate="TextBoxMobileC" Display="Dynamic" ErrorMessage="手机号码三格式不正确！<br />"
                                                ValidationExpression="^([0-9]{11})$" CssClass="errorMessage">
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
                                        <td colspan="4">
                                            <asp:GridView ID="GridViewDetectors" runat="server" Height="30px" ShowHeader="False"
                                                Width="1000px" AutoGenerateColumns="False">
                                                <columns>
                                                    <asp:BoundField DataField="C1">
                                                        <ItemStyle Width="100px" />   
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="C2">
                                                        <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="C3">
                                                        <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="C4">
                                                        <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="C5">
                                                        <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="C6">
                                                        <ItemStyle Width="180px" />
                                                    </asp:BoundField>
                                                </columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            手机报警：
                                        </td>
                                        <td align="left">
                                            <asp:RadioButtonList ID="RadioButtonListMobileAlarm" runat="server" RepeatDirection="Horizontal"
                                                Width="200px">
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right">
                                            接收短信手机号码一：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMobileA" runat="server" style="margin-bottom: 0px">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            接收短信手机号码二：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMobileB" runat="server" style="margin-bottom: 0px">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            接收短信手机号码三：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMobileC" runat="server" style="margin-bottom: 0px">
                                            </asp:TextBox>
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
