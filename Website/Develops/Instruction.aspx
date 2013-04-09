<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instruction.aspx.cs" Inherits="EnvironmentalMonitor.Website.Develops.Instruction" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
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
                                指令
                            </h2>
                        </div>
                        <div class="content">
                            <%
                                foreach (string type in this.instructionTypes)
                                {
                            %>
                            <% = type %><br />
                            <%
                                }
                            %>
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
