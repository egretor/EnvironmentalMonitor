<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Detectors.Update" %>

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
                                修改探头
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSerial" runat="server" ControlToValidate="TextBoxSerial"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="地址不允许为空！<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMinimumA" runat="server" ControlToValidate="TextBoxMinimumA"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMaximumA" runat="server" ControlToValidate="TextBoxMaximumA"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMinimumB" runat="server" ControlToValidate="TextBoxMinimumB"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMaximumB" runat="server" ControlToValidate="TextBoxMaximumB"
                                                CssClass="errorMessage" Display="Dynamic" ErrorMessage="<br />">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidatorSerial" runat="server" ControlToValidate="TextBoxSerial"
                                                Display="Dynamic" ErrorMessage="地址范围从0到255！<br />" MaximumValue="255" MinimumValue="0"
                                                CssClass="errorMessage" Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:RangeValidator ID="RangeValidatorMinimumA" runat="server" ControlToValidate="TextBoxMinimumA"
                                                Display="Dynamic" ErrorMessage="<br />" MaximumValue="255" MinimumValue="0" CssClass="errorMessage"
                                                Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:RangeValidator ID="RangeValidatorMaximumA" runat="server" ControlToValidate="TextBoxMaximumA"
                                                Display="Dynamic" ErrorMessage="<br />" MaximumValue="255" MinimumValue="0" CssClass="errorMessage"
                                                Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:RangeValidator ID="RangeValidatorMinimumB" runat="server" ControlToValidate="TextBoxMinimumB"
                                                Display="Dynamic" ErrorMessage="<br />" MaximumValue="255" MinimumValue="0" CssClass="errorMessage"
                                                Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:RangeValidator ID="RangeValidatorMaximumB" runat="server" ControlToValidate="TextBoxMaximumB"
                                                Display="Dynamic" ErrorMessage="<br />" MaximumValue="255" MinimumValue="0" CssClass="errorMessage"
                                                Type="Integer">
                                            </asp:RangeValidator>
                                            <asp:Label ID="LabelMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            所属机房：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListCurrentRooms" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="DropDownListCurrentRooms_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            所属检测仪：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListCurrentMachines" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="DropDownListCurrentMachines_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            探头：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListDetectors" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="DropDownListDetectors_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            机房：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListRooms" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownListRooms_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            检测仪：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListMachines" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            类型：
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DropDownListDetectorTypes" runat="server" AutoPostBack="True"
                                                onselectedindexchanged="DropDownListDetectorTypes_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            地址：
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxSerial" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LabelMinimumA" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMinimumA" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="LabelMaximumA" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMaximumA" runat="server">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="LabelMinimumB" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMinimumB" runat="server">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="LabelMaximumB" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxMaximumB" runat="server">
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
