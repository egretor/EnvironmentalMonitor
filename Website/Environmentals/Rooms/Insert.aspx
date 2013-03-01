<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Rooms.Insert" %>

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
                                新增机房</h2>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TextBoxName"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="名称不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
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
                                            地址：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxAddress" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            负责人：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxContact" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            电话：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxPhone" runat="server">
                                            </asp:TextBox>
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
