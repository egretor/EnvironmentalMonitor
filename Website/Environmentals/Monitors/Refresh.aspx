<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Refresh.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Monitors.Refresh" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta http-equiv="refresh" content="10" />
</head>
<body>
    <% = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>
</body>
</html>
