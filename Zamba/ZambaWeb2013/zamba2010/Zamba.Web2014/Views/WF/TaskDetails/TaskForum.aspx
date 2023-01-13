<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskForum.aspx.cs" Inherits="Views_WF_TaskDetails_TaskForum" EnableEventValidation="false" %>
<%@ Register Src="~/Views/UC/Forum/Forum.ascx" TagName="Forum" TagPrefix="uc4" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Foro</title>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%: Styles.Render("~/Content/bootstrap-responsive.css")%>
        <link rel="Stylesheet" type="text/css" href="/Content/Styles/GridThemes/WhiteChromeGridView.css" />

        <%: GetJqueryCoreScript()%>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
</head>
<body class="Body-Task-Forum">
    <form id="form2" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="_scriptmngr" runat="server" ScriptMode="Release">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="tbForum" style="overflow: auto;">
            <uc4:Forum ID="ucForum" runat="server" />
        </div>
    </form>

    <style>
        .container-fluid {
            padding: 0px; 
            width: 100%;
        }
        
        .container-fluid .row {
            margin: 0px;
            padding: 0px; 
        }
        
        .container-fluid .row div {
            margin: 0px;
            padding: 0px; 
        }

        #divMessages {
            background-color: #EFF5FB;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var screenHeight = $(window).height();
            $("#tbForum").height(screenHeight);
            $("#tbForum").width($(window).width());

            screenHeight = screenHeight - $("#divButtons").height() - 10;
            $("#divForum").height(screenHeight);
            $("#ucForum_pnlTabs").height(screenHeight);
            $("#ucForum_pnlTabs").width("100%");
            $("#ucForum_divMessages").height(screenHeight);
            $("#ucForum_divMessages").width("100%");
            $("#ucForum_tvwMensajesForo").height(screenHeight - 17);
            $("#ucForum_tvwMensajesForo").width("100%");
            $("#ucForum_TabForo").height(screenHeight);
            $("#ucForum_TabForo").width("100%");
            $("#ucForum_TabForo_body").height(screenHeight - $("#ucForum_TabForo_header").height() - 5);
        });

        function Refresh_Click() {
            ShowLoadingAnimation();
            document.location = document.location;
        }

        $(window).load(function () {
            parent.parent.hideLoading();
            hideLoading();
        });
    </script>
</body>
</html>
