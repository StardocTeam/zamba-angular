<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_WF_TaskDetails_TaskForum" EnableEventValidation="false" Codebehind="TaskForum.aspx.cs" %>
<%@ Register Src="~/Views/UC/Forum/Forum.ascx" TagName="Forum" TagPrefix="uc4" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Foro</title>
    <asp:PlaceHolder runat="server">
<link rel="Stylesheet" type="text/css" href="../../../Content/Styles/GridThemes/WhiteChromeGridView.css" />
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%:Scripts.Render("~/bundles/jqueryCore") %>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
</head>
<body class="Body-Task-Forum">
    <form id="form2" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
         <%--   <Services>
                <asp:ServiceReference Path="~/Services/TasksService.asmx"  InlineScript="true"/>
            </Services>--%>
           <%-- <Scripts>
          
          
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
           
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />--%>
     
       <%--     </Scripts>--%>
        </asp:ScriptManager>
         <div id="tbForum" >
            <uc4:Forum ID="ucForum" runat="server" />
        </div>
    </form>


    <script type="text/javascript">
        $(document).ready(function () {
            var screenHeight = $(window).height();
            $("#tbForum").height(screenHeight - 50);
            $("#tbForum").width($(window).width());

            screenHeight = screenHeight - $("#divButtons").outerHeight(true) - 10;
            $("#divForum").height(screenHeight);
            $("#ucForum_pnlTabs").height(screenHeight - 50);
            $("#ucForum_pnlTabs").width("100%");
            $("#ucForum_divMessages").height(screenHeight);
            $("#ucForum_divMessages").width("100%");
            $("#ucForum_tvwMensajesForo").height(screenHeight - 17);
            $("#ucForum_tvwMensajesForo").width("100%");
            $("#ucForum_TabForo").height(screenHeight);
            $("#ucForum_TabForo").width("100%");
            //$("#ucForum_TabForo_body").height(screenHeight - $("#ucForum_TabForo_header").height() -50 );
        });

        function Refresh_Click() {
            ShowLoadingAnimation();
            document.location = document.location;
        }

        $(window).on("load",function () {
            parent.parent.hideLoading();
            hideLoading();
        });
    </script>
</body>
</html>
