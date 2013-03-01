<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EnvironmentalMonitor.Website.Manages.Defaults.About" %>

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
                                关于</h2>
                        </div>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <thead>
                                    <tr style="height: 1px">
                                        <th style="width: 200px">
                                        </th>
                                        <th style="width: 200px">
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
                                            机房环境检测系统：
                                        </td>
                                        <td align="left" colspan="3">
                                            海南隆远自动化技术有限公司
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            地址：
                                        </td>
                                        <td align="left" colspan="3">
                                            海南省海口市滨海大道103号财富广场公寓楼11B
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            联系电话：
                                        </td>
                                        <td align="left" colspan="3">
                                            (+86)0898-36337257
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="footer">
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
