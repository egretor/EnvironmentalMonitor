<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Realtime.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Monitors.Realtime" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <% = EnvironmentalMonitor.Support.Resource.Variable.Product %>
    </title>
    <% = this.ReferencejQueryCss %><% = this.ReferencejQueryPluginsCss%><% = this.ReferenceCss %><% = this.ReferencejQueryJavascript %><% = this.ReferencejQueryPluginsJavascript %><% = this.ReferenceJavascript%>
    <style type="text/css">
        .draggable
        {
            width: 90px;
            height: 90px;
            padding: 0.5em;
            float: left;
            margin: 0 10px 10px 0;
        }
        #containmentFloorPlan
        {
            border: 1px solid #f7b850;
            position: absolute;
            top: 180px;
            left: 0px;
            margin: 0px;
            padding: 0px;
        }
        #floorPlanLayout
        {
            top: 0px;
            left: 0px;
            margin: 0px;
            padding: 0px;
        }
        #ImageFloorPlan
        {
            position: absolute;
            top: 0px;
            left: 0px;
            margin: 0px;
            padding: 0px;
            z-index: -1024;
        }
        .detectorContainer
        {
            position: absolute;
            display: block;
            width: 256px;
            height: 96px;
            overflow: hidden;
            font-family: 微软雅黑, 新宋体, 宋体, Tahoma, Consolas, Courier New, Arial;
            font-size: 9pt;
            font-weight: bolder;
            color: #1c94c4;
        }
        .detectorName
        {
            background-color: Transparent;
            height: 16px;
        }
        .detectorValue
        {
            background-color: Transparent;
            height: 16px;
        }
        .detectorImage
        {
            text-align: left;
            vertical-align: top;
            width: 64px;
            height: 64px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var loadJsonUrl = '<% =this.LoadJsonDataUrl %>';
            var loadDataCacheJsonUrl = '<% =this.loadDataCacheJsonUrl %>';
            var refreshJsonUrl = '<% = this.RefreshJsonUrl %>';
            var warringSoundUrl = '<% = this.WarringSoundUrl %>';
            var dataCacheInterval = parseInt('<% = this.DataCacheInterval %>');
            var modules = null;

            $('#containmentFloorPlan').css('width', $('#ImageFloorPlan').css('width'));
            $('#containmentFloorPlan').css('height', $('#ImageFloorPlan').css('height'));

            var loadLayout = function () {
                $('#floorPlanLayout').css('width', $('#ImageFloorPlan').css('width'));
                $('#floorPlanLayout').css('height', $('#ImageFloorPlan').css('height'));
                if (loadJsonUrl) {
                    $.ajax({
                        type: 'POST',
                        url: loadJsonUrl,
                        success: function (data, textStatus, jqXHR) {
                            var result = eval('(' + data + ')');
                            if (result) {
                                if (result.rows) {
                                    modules = result.rows;

                                    $('#floorPlanLayout').html('');
                                    var i = 0;
                                    for (i = 0; i < modules.length; i++) {
                                        var draggableValue = '';
                                        draggableValue += '<table id="' + modules[i].Guid + '" cellpadding="0" cellspacing="0" border="0" class="detectorContainer">';
                                        draggableValue += '<tbody>';
                                        draggableValue += '<tr class="detectorName">';
                                        draggableValue += '<td>';
                                        draggableValue += modules[i].Name;
                                        draggableValue += '</td>';
                                        draggableValue += '</tr>';
                                        draggableValue += '<tr class="detectorValue">';
                                        draggableValue += '<td id="text_' + modules[i].Guid + '">';
                                        draggableValue += '</td>';
                                        draggableValue += '</tr>';
                                        draggableValue += '<tr class="detectorImage">';
                                        draggableValue += '<td>';
                                        draggableValue += '<img id="img_' + modules[i].Guid + '" src="' + modules[i].NormalHref + '" border="0" />';
                                        draggableValue += '</td>';
                                        draggableValue += '</tr>';
                                        draggableValue += '</tbody>';
                                        draggableValue += '</table>';

                                        $('#floorPlanLayout').append(draggableValue);
                                        $("#" + modules[i].Guid).draggable({ containment: "#floorPlanLayout", scroll: false, revert: true });
                                        $("#" + modules[i].Guid).css('top', modules[i].PositionY);
                                        $("#" + modules[i].Guid).css('left', modules[i].PositionX);
                                    }
                                }
                            }
                        }
                    });
                }
            };

            loadLayout();

            var dataInitialize = function () {
                if (loadDataCacheJsonUrl) {
                    $.ajax({
                        type: 'POST',
                        url: loadDataCacheJsonUrl,
                        success: function (data, textStatus, jqXHR) {
                            if (data) {
                                var result = eval('(' + data + ')');
                                if (result) {
                                    if (result.rows) {
                                        modules = result.rows;
                                        var i = 0;
                                        for (i = 0; i < modules.length; i++) {
                                            $("#img_" + modules[i].Guid).attr('src', modules[i].ViewImage);
                                            $("#text_" + modules[i].Guid).html(modules[i].ViewValue);
                                            if (modules[i].Error) {
                                                $('#playMessage').html('<bgsound src="' + warringSoundUrl + '" loop="1"></bgsound>');
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
                }
            };
            dataInitialize();

            if ((!dataCacheInterval) || (dataCacheInterval <= 0)) {
                dataCacheInterval = 60000;
            }
            var dataInitializeInterval = setInterval(dataInitialize, dataCacheInterval);
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
                    <div class="blue-header-popup" style="width: 100%">
                        <div class="header">
                            <h2>
                                实时监控
                            </h2>
                        </div>
                        <div class="content">
                            <table cellpadding="0" cellspacing="0" border="0" width="1000">
                                <tbody>
                                    <tr>
                                        <td style="width: 150px" align="right">
                                            所属机房：
                                        </td>
                                        <td style="width: 250px" align="left">
                                            <asp:DropDownList ID="DropDownListRooms" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListRooms_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 150px" align="right">
                                            检测仪：
                                        </td>
                                        <td style="width: 250px" align="left">
                                            <asp:DropDownList ID="DropDownListMachines" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMachines_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <div id="playMessage" style="width: 0px; height: 0px; display: none">
                                            </div>
                                        </td>
                                        <td style="width: 100px" align="center">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <div id="containmentFloorPlan">
        <asp:Image ID="ImageFloorPlan" runat="server" />
        <div id="floorPlanLayout">
        </div>
    </div>
    </form>
    <iframe width="0px" height="0px" scrolling="no" frameborder="no" src="<% = this.RefreshJsonUrl %>" />
</body>
</html>
