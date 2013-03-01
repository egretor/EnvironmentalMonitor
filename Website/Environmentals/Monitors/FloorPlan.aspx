<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FloorPlan.aspx.cs" Inherits="EnvironmentalMonitor.Website.Environmentals.Monitors.FloorPlan" %>

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
            var saveJsonUrl = '<% =this.SaveJsonDataUrl %>';
            var modules = null;

            $('#containmentFloorPlan').css('width', $('#ImageFloorPlan').css('width'));
            $('#containmentFloorPlan').css('height', $('#ImageFloorPlan').css('height'));

            $('#loadLayout').click(function () {
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
                                        $("#" + modules[i].Guid).draggable({ containment: "#floorPlanLayout", scroll: false });
                                        $("#" + modules[i].Guid).css('top', modules[i].PositionY);
                                        $("#" + modules[i].Guid).css('left', modules[i].PositionX);
                                    }
                                }
                            }
                        }
                    });
                }
            });

            $('#loadLayout').trigger('click');

            $('#saveLayout').click(function () {
                if (modules) {
                    var i = 0;
                    var data = {};
                    for (i = 0; i < modules.length; i++) {
                        data[i] = {};
                        data[i].Guid = modules[i].Guid;
                        data[i].PositionX = parseInt($("#" + modules[i].Guid).css('left'));
                        data[i].PositionY = parseInt($("#" + modules[i].Guid).css('top'));
                    }
                    $.ajax({
                        type: 'POST',
                        url: saveJsonUrl,
                        data: data,
                        success: function (data, textStatus, jqXHR) {
                            var result = eval('(' + data + ')');
                            alert(result.msg);
                        }
                    });
                }
            });
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
                                平面图配置
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
                                            <input type="button" id="loadLayout" value="载入布局" />
                                        </td>
                                        <td style="width: 100px" align="center">
                                            <input type="button" id="saveLayout" value="保存布局" />
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
</body>
</html>
