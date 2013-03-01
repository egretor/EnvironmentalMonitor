<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnvironmentalMonitor.Website.Develops.Default" %>

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
                                开发调试
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
                                            所属机房：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListRooms" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DropDownListRooms_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            检测仪：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListMachines" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMachines_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="LabelIp" runat="server" Text=""></asp:Label>
                                            <asp:GridView ID="GridViewDetectors" runat="server" Height="30px" ShowHeader="False"
                                                Width="1000px" AutoGenerateColumns="False">
                                                <Columns>
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
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="footer">
                            <asp:Button ID="ButtonUpload" runat="server" Text="上传普通数据" OnClick="ButtonUpload_Click" />
                            <asp:Button ID="ButtonAlarm" runat="server" Text="上传报警数据" OnClick="ButtonAlarm_Click" />
                            <asp:Button ID="ButtonMessage" runat="server" Text="上传报警短信" 
                                onclick="ButtonMessage_Click" />
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
