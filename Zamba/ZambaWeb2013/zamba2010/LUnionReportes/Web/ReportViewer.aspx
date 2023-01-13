<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Web.ReportViewer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
<meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>

    <title>HDI Zamba Reportes</title>
    <%--    <meta id="MetaRefresh" http-equiv="refresh" content="60;url=ReportViewer.aspx" runat="server" />--%>
    
    <%--<link href="Content/css/ZambaUIWeb.css" rel="stylesheet" type="text/css" />--%>
   
     <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.4.min.js" type="text/javascript"></script>
    <script src="Scripts/modernizr-2.7.2.js" type="text/javascript"></script> 

    <script type="text/javascript">
        $(document).ready(function () {
            ShowLoadingAnimation();
            setTimeout("Refresh();", 120000);
        });

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.search);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        function ResizeIF() {
            if (parent.setIframeHeight != null) {
                parent.setIframeHeight("if" + getParameterByName("Report"));
            }
        }

        function FullSizeReport(strUrl) {
            var popup = window.open(strUrl, "_blank", 'width=' + (screen.width) + ',height=' + (screen.height) + ',left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=no,toolbar=no,scrollbars=yes');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="loading" class="loading">
            <table style="width: 90%; height: 100%">
                <tr style="height: 50%">
                    <td>
                    </td>
                </tr>
                <tr style="height: 1%">
                    <td style="text-align: center">
                        <img src="Content/Images/Loader.gif" style="top: 50%" />
                    </td>
                </tr>
                <tr style="height: 49%">
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
                ShowLoadingAnimation();
            }

            function onRequestEnd(sender, args) {
                hideLoading();
            }
    </script>
    <telerik:RadScriptManager ID="RadScriptManager2" runat="server" />
    <telerik:RadAjaxManager ID="RadScriptManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" OnResponseEnd="onRequestEnd" />
    </telerik:RadAjaxManager>
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                hideLoading();
            }

            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
            function initializeRequestHandler(sender, args) {
                ShowLoadingAnimation();
            }
            function Refresh() {
//                document.location = "about:blank";
//                document.location = document.location;
                __doPostBack('', '');
            }

            function ShowLoadingAnimation() {
                $(document).scrollTop(0);
                $("#loading").fadeIn("slow", function () {
                    $("#loading").css("filter", "alpha(opacity=30)");
                    $("#loading").css("opacity", "0.3");
                });
            }
            function hideLoading() {
                $("#loading").fadeOut("slow");
            }

            window.onload = function () {
                hideLoading();
            };

            function Imprimir_Click1(objId) {
                var popup = window.open("about:blank", "_blank", 'width=490,height=300,left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=no,toolbar=no');
                popup.document.open();
                popup.document.write('<html><head><title>Grafico para imprimir</title></head><body onload="window.print()"><img src="' + $("#" + objId).attr("src") + '" /></body></html>');
                popup.document.close();
            }
        </script>
        <div style="float:right;cursor:pointer" onclick="Refresh();"><img src="Content/Images/Refresh.png" alt="Refrescar" title="Refrescar" /></div>    
        <div id="divContainer" runat="server">
        </div>
    </form>
</body>
</html>
