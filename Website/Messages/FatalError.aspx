<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FatalError.aspx.cs" Inherits="EnvironmentalMonitor.Website.Messages.FatalError" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <% = EnvironmentalMonitor.Support.Resource.Variable.Product %>
    </title>
    <% = string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Resources/Javascripts/custom/global.css\" />", EnvironmentalMonitor.Support.Resource.Variable.VirtualRootPath)%>
</head>
<body>
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
                                网络初始化错误！
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
