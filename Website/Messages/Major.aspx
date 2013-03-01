<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Major.aspx.cs" Inherits="EnvironmentalMonitor.Website.Layouts.Major" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <% = this.ReferencejQueryCss %>
    <% = this.ReferenceCss %>
    <% = this.ReferencejQueryJavascript %>
    <script type="text/javascript">
        $(document).ready(function () {

            var major = $('#major');
            var tabs = $('#tabs');
            var tabContent = $('.tabContent');

            tabs.tabs();
            major.css('background-color', tabs.css('background-color'));

            var tabContentHeight = parseInt(major.css('height')) - parseInt(tabContent.offset().top) * 2;
            tabContent.css('height', tabContentHeight);
        });
    </script>
</head>
<body id="major">
    <div id="tabs">
        <ul>
            <%
                for (int i = 0; i < this.Catalogs.Count; i++)
                {                
            %>
            <li><a href="#<% = this.Catalogs[i].Code %>">
                <% = this.Catalogs[i].Name%></a></li>
            <%
                }
            %>
        </ul>
        <%
            for (int i = 0; i < this.Catalogs.Count; i++)
            {                
        %>
        <div class="tabContent" id="<% = this.Catalogs[i].Code %>">
            <iframe src="<% = this.Catalogs[i].Url %>" frameborder="0" style="width: 100%; height: 100%">
            </iframe>
        </div>
        <%
            }
        %>
    </div>
</body>
</html>
