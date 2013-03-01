<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyFalse.aspx.cs" Inherits="EnvironmentalMonitor.Website.Messages.VerifyFalse" %>

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
                                错误</h2>
                        </div>
                        <div class="content">
                            <div align="center" style="font-size: 12pt; font-weight: bolder; color: #ff0000;
                                height: 100px">
                                未获得该页面的访问授权！
                            </div>
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
