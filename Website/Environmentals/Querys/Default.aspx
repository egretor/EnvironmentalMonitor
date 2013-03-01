<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Querys.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <% = EnvironmentalMonitor.Support.Resource.Variable.Product %>
    </title>
    <% = this.ReferencejQueryCss %><% = this.ReferencejQueryPluginsCss%><% = this.ReferenceCss %><% = this.ReferencejQueryJavascript %><% = this.ReferencejQueryPluginsJavascript %><% = this.ReferenceJavascript%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#TextBoxBeginDate").datepicker($.datepicker.regional["zh-CN"]);
            $("#TextBoxEndDate").datepicker($.datepicker.regional["zh-CN"]);
        });
    </script>
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
                                查询模式
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <thead>
                                    <tr style="height: 1px">
                                        <th style="width: 200px">
                                        </th>
                                        <th style="width: 300px">
                                        </th>
                                        <th style="width: 200px">
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
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            所属机房：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListRooms" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRooms_SelectedIndexChanged">
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
                                        <td align="right">
                                            开始时间：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxBeginDate" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            结束时间：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxEndDate" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="footer">
                            <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="查询" />
                            <input type="reset" value="取消" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <% if (this.Room != null)
                       {
                    %>
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                机房数据
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="1" cellspacing="1" border="0" style="background-color: #e6e6e6">
                                <tbody style="background-color: #f9f9f9">
                                    <tr>
                                        <td align="right" style="width: 500px">
                                            名称：
                                        </td>
                                        <td align="left" style="width: 500px">
                                            <% =this.Room.Name %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            地址：
                                        </td>
                                        <td align="left">
                                            <% =this.Room.Address %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            联系人：
                                        </td>
                                        <td align="left">
                                            <% =this.Room.Contact%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            电话：
                                        </td>
                                        <td align="left">
                                            <% =this.Room.Phone %>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%
                       }
                    %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <% if (this.Machine != null)
                       {
                    %>
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                检测仪数据
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="1" cellspacing="1" border="0" style="background-color: #e6e6e6">
                                <tbody style="background-color: #f9f9f9">
                                    <tr>
                                        <td align="right" style="width: 500px">
                                            名称：
                                        </td>
                                        <td align="left" style="width: 500px">
                                            <% =this.Machine.Name %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            IP：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.Ip %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            子网掩码：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.Netmask %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            网关：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.Gateway %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            物理地址：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.Mac %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            上传探头数据时间间隔（秒）：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.Interval %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            是否手机短信报警：
                                        </td>
                                        <td align="left">
                                            <% if (this.Machine.Alarm)
                                               {
                                            %>是
                                            <%
                                               }
                                               else
                                               {
                                            %>否
                                            <%
                                               }
                                            %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            发送报警短信手机号码：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.Mobile %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            接受报警短信手机号码一：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.MobileA %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            接受报警短信手机号码二：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.MobileB %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            接受报警短信手机号码三：
                                        </td>
                                        <td align="left">
                                            <% =this.Machine.MobileC %>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%
                       }
                    %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <% if ((this.Detectors != null) && (this.Detectors.Count > 0))
                       {
                    %>
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                探头数据
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="1" cellspacing="1" border="0" style="background-color: #e6e6e6">
                                <tbody style="background-color: #f9f9f9">
                                    <%
                           for (int i = 0; i < this.Detectors.Count; i++)
                           {
                                    %>
                                    <tr>
                                        <td align="left" style="width: 100px">
                                            <% = this.Detectors[i].Serial %>
                                        </td>
                                        <td align="left" style="width: 180px">
                                            <% = this.Detectors[i].DetectorType.Name %>
                                        </td>
                                        <%
                               if (this.Detectors[i].DetectorType.Type == EnvironmentalMonitor.Support.Resource.DetectorTypes.Switch)
                               {
                                        %>
                                        <td align="left" style="width: 180px">
                                            <% = string.Format("{0}阀值下限{1}{2}", this.Detectors[i].DetectorType.DescriptionA, this.Detectors[i].MinimumA, this.Detectors[i].DetectorType.UnitA) %>
                                        </td>
                                        <td align="left" style="width: 180px">
                                            <% = string.Format("{0}阀值上限{1}{2}", this.Detectors[i].DetectorType.DescriptionA, this.Detectors[i].MaximumA, this.Detectors[i].DetectorType.UnitA) %>
                                        </td>
                                        <td align="left" style="width: 180px">
                                            <% = string.Format("{0}阀值下限{1}{2}", this.Detectors[i].DetectorType.DescriptionB, this.Detectors[i].MinimumB, this.Detectors[i].DetectorType.UnitB) %>
                                        </td>
                                        <td align="left" style="width: 180px">
                                            <% = string.Format("{0}阀值上限{1}{2}", this.Detectors[i].DetectorType.DescriptionB, this.Detectors[i].MaximumB, this.Detectors[i].DetectorType.UnitB) %>
                                        </td>
                                        <%
                               }
                               else
                               {
                                        %>
                                        <td align="left" style="width: 180px">
                                            <% = this.Detectors[i].DetectorType.DescriptionA %>
                                        </td>
                                        <td align="left" style="width: 180px">
                                            <% = this.Detectors[i].DetectorType.DescriptionB %>
                                        </td>
                                        <td align="left" style="width: 180px">
                                        </td>
                                        <td align="left" style="width: 180px">
                                        </td>
                                        <%
                               }
                                        %>
                                    </tr>
                                    <%
                           }
                                    %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%
                       }
                    %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <% if ((this.NormalDataCaches != null) && (this.NormalDataCaches.Count > 0))
                       {
                    %>
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                探头正常数据
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="1" cellspacing="1" border="0" style="background-color: #e6e6e6">
                                <tbody style="background-color: #f9f9f9">
                                    <%
                           for (int i = 0; i < this.NormalDataCaches.Count; i++)
                           {
                                    %>
                                    <tr>
                                        <td align="left" style="width: 100px">
                                            <% = this.NormalDataCaches[i].Serial %>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <% = this.NormalDataCaches[i].DetectorType.Name %>
                                        </td>
                                        <td align="left" style="width: 500px">
                                            <% = this.NormalDataCaches[i].ViewValue %>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <% = this.NormalDataCaches[i].RefreshTimeText %>
                                        </td>
                                    </tr>
                                    <%
                           }
                                    %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%
                       }
                    %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <% if ((this.AlarmDataCaches != null) && (this.AlarmDataCaches.Count > 0))
                       {
                    %>
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                探头报警数据
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="1" cellspacing="1" border="0" style="background-color: #e6e6e6">
                                <tbody style="background-color: #f9f9f9">
                                    <%
                           for (int i = 0; i < this.AlarmDataCaches.Count; i++)
                           {
                                    %>
                                    <tr>
                                        <td align="left" style="width: 100px">
                                            <% = this.AlarmDataCaches[i].Serial%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <% = this.AlarmDataCaches[i].DetectorType.Name%>
                                        </td>
                                        <td align="left" style="width: 500px">
                                            <% = this.AlarmDataCaches[i].ViewValue%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <% = this.AlarmDataCaches[i].RefreshTimeText%>
                                        </td>
                                    </tr>
                                    <%
                           }
                                    %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%
                       }
                    %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <% if ((this.MessageCaches != null) && (this.MessageCaches.Count > 0))
                       {
                    %>
                    <div class="blue-header-popup" style="width: 1000px">
                        <div class="header">
                            <h2>
                                探头报警短信数据
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="1" cellspacing="1" border="0" style="background-color: #e6e6e6">
                                <tbody style="background-color: #f9f9f9">
                                    <%
                           for (int i = 0; i < this.MessageCaches.Count; i++)
                           {
                                    %>
                                    <tr>
                                        <td align="left" style="width: 300px">
                                            <% = this.MessageCaches[i].DetectorType.Name%>
                                        </td>
                                        <td align="left" style="width: 500px">
                                            <% = this.MessageCaches[i].ResultText%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <% = this.MessageCaches[i].SendTimeText%>
                                        </td>
                                    </tr>
                                    <%
                           }
                                    %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <%
                       }
                    %>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
