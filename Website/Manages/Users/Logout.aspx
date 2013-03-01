<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="EnvironmentalMonitor.Website.Manages.Users.Logout" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <% = this.ReferencejQueryJavascript %>
    <script type="text/javascript">
        $(document).ready(function () {
            var self = this;
            var parent = null;
            if (!parent && self.parentWindow) {
                parent = self.parentWindow;
            }
            if (!parent && self.parent) {
                parent = self.parent;
            }
            if (!parent && window.frameElement && window.frameElement.parentNode) {
                parent = window.frameElement.parentNode;
            }
            if (!parent && self.parentNode) {
                parent = self.parentNode;
            }
            while (self && parent && (self !== parent)) {
                self = parent;
                parent = null;
                if (!parent && self.parentWindow) {
                    parent = self.parentWindow;
                }
                if (!parent && self.parent) {
                    parent = self.parent;
                }
                if (!parent && self.window && self.window.frameElement && self.window.frameElement.parentNode) {
                    parent = self.window.frameElement.parentNode;
                }
                if (!parent && self.parentNode) {
                    parent = self.parentNode;
                }
            }

            if (self) {
                self.location = 'Login.aspx';
            }
            else {
                this.location = 'Login.aspx';
            }
        });
    </script>
</head>
<body>
</body>
</html>
