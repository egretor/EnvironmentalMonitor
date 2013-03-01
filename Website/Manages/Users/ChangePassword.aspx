<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="EnvironmentalMonitor.Website.Manages.Users.ChangePassword" %>

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
                                修改密码</h2>
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
                                            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" CssClass="errorMessage"
                                                Display="Dynamic" ErrorMessage="两次输入的新密码不一致！<br />" ControlToCompare="TextBoxPassword"
                                                ControlToValidate="TextBoxRepassword">
                                            </asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPassword" runat="server"
                                                ControlToValidate="TextBoxOldPassword" CssClass="errorMessage" Display="Dynamic"
                                                ErrorMessage="旧密码不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="新密码不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRePassword" runat="server"
                                                ControlToValidate="TextBoxRePassword" CssClass="errorMessage" Display="Dynamic"
                                                ErrorMessage="确认新密码不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            旧密码：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:TextBox ID="TextBoxOldPassword" runat="server" TextMode="Password">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            新密码：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            确认新密码：
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:TextBox ID="TextBoxRepassword" runat="server" TextMode="Password">
                                            </asp:TextBox>
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
