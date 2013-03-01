<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="EnvironmentalMonitor.Website.Manages.Users.Update" %>

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
                                修改用户</h2>
                        </div>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <thead>
                                    <tr style="height: 1px">
                                        <th style="width: 100px">
                                        </th>
                                        <th style="width: 300px">
                                        </th>
                                        <th style="width: 100px">
                                        </th>
                                        <th style="width: 300px">
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="TextBoxPassword"
                                                ControlToValidate="TextBoxRepassword" CssClass="errorMessage" ErrorMessage="两次输入的密码不一致！<br />"
                                                Display="Dynamic"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TextBoxName"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="姓名不允许为空！<br />"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccount" runat="server" ControlToValidate="TextBoxAccount"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="账户不允许为空！<br />"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="密码不允许为空！<br />"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRePassword" runat="server" ControlToValidate="TextBoxRePassword"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="确认密码不允许为空！<br />"></asp:RequiredFieldValidator>
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            用户：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListUsers" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="DropDownListUsers_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            姓名：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            账户：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxAccount" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            密码：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            确认密码：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxRepassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            管理权限：
                                        </td>
                                        <td align="left">
                                            <asp:CheckBoxList ID="CheckBoxListModules" runat="server" RepeatLayout="Flow">
                                            </asp:CheckBoxList>
                                        </td>
                                        <td align="right">
                                            管理机房：
                                        </td>
                                        <td align="left">
                                            <asp:CheckBoxList ID="CheckBoxListRooms" runat="server" RepeatLayout="Flow">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="footer">
                            <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="修改" />
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
