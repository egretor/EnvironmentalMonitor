<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EnvironmentalMonitor.Website.Manages.Users.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <% = EnvironmentalMonitor.Support.Resource.Variable.Product %>
    </title>
    <% = this.ReferencejQueryJavascript %>
    <style type="text/css">
        html, body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            overflow: hidden;
            font-family: 微软雅黑, 新宋体, 宋体, Tahoma, Consolas, Courier New, Arial;
            font-size: 12pt;
            font-weight: bolder;
        }
        
        input
        {
            font-family: 微软雅黑, 新宋体, 宋体, Tahoma, Consolas, Courier New, Arial;
            font-size: 12pt;
            font-weight: bolder;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var layout = function () {
                var container = $('#container');
                var panel = $('#panel');
                var backgroundImage = $('#backgroundImage');

                var containerWidth = parseInt(container.css('width'));
                var containerHeight = parseInt(container.css('height'));

                var backgroundImageWidth = parseInt(backgroundImage.css('width'));
                var backgroundImageHeight = parseInt(backgroundImage.css('height'));

                backgroundImageHeight = backgroundImageHeight * (containerWidth / backgroundImageWidth);
                backgroundImageWidth = containerWidth;

                backgroundImage.css('width', backgroundImageWidth);
                backgroundImage.css('height', backgroundImageHeight);

                var panelWidth = parseInt(panel.css('width'));
                var panelHeight = parseInt(panel.css('height'));

                var panelTop = (containerHeight - panelHeight) / 2;
                var panelLeft = (containerWidth - panelWidth) / 2;

                panel.css('top', panelTop);
                panel.css('left', panelLeft);
            };

            layout();

            $('#TextBoxUsername').val('');
            $('#TextBoxPassword').val('');
        });
    </script>
</head>
<body id="container">
    <form id="majorForm" runat="server">
    <img id="backgroundImage" border="0" src="../../Resources/Images/User_Login_Background.jpg"
        style="position: absolute; z-index: 1024" />
    <table id="panel" border="0" cellpadding="0" cellspacing="0" style="width: 640px;
        height: 360px; position: absolute; z-index: 2048">
        <tbody>
            <tr style="height: 35px">
                <td style="width: 10px; text-align: center">
                    &nbsp;
                </td>
                <td style="width: 160px; text-align: right">
                    &nbsp;
                </td>
                <td style="width: 320px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 150px; text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 50px">
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    帐号：
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBoxUsername" runat="server" Width="160px" Height="30px"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 50px">
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    密码：
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Width="160px"
                        Height="30px"></asp:TextBox>
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 50px">
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Button ID="ButtonSubmit" runat="server" Text="登录" Width="160px" Height="30px"
                        OnClick="ButtonSubmit_Click" />
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 175px">
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
