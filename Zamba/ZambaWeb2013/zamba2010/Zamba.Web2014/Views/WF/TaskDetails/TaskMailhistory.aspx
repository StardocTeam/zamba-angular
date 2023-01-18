<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskMailhistory.aspx.cs"
    Inherits="Views_WF_TaskDetails_TaskMailhistory" %>
<%@ Register Src="~/Views/UC/Grid/ucHistoryGrid.ascx" TagName="ucHistoryGrid" TagPrefix="uc1" %>

<%@ Import Namespace="System.Web.Optimization" %>
<!DOCTYPE html>
<html >
<head id="Head1" runat="server">
    <title>Historial</title>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>    
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <%: Styles.Render("~/bundles/Styles/masterblank")%>
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%: Styles.Render("~/Content/bootstrap-responsive.css")%>
        <%: Styles.Render("~/bundles/Styles/grids")%>

        <%: GetJqueryCoreScript()%>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/tabber") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>

</head>
<body class="body-TaskHistory">
    <form id="form2" runat="server">
        <div id="divButtons">
            <button id="btnRefresh" type="button" class="btn btn-default btn-xs"  onclick="Refresh_Click()" style="margin:5px">
                <span class="glyphicon glyphicon-refresh"></span> Refrescar                                   
            </button>
        </div>
        <div id="divGrid" style="overflow:auto;width:100%">
            <uc1:ucHistoryGrid ID="ucMails" runat="server" />
        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ucMails_formBrowser").width($(window).width() - 30);
            $("#ucMails_formBrowser").height(500);
        });

        function Refresh_Click() {
            ShowLoadingAnimation();
            document.location = document.location;
        }

        $(window).load(function () {
            var screenHeight = $(window).height() - $("#divButtons").height();
            $("#divGrid").height(screenHeight);
            parent.parent.hideLoading();
            hideLoading();
        });
    </script>
</body>
</html>
