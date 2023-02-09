<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_WF_TaskDetails_TaskHistory" Codebehind="TaskHistory.aspx.cs" %>
<%@ Register src="~/Views/UC/Grid/ucHistoryGrid.ascx" tagname="ucHistoryGrid" tagprefix="uc1" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html >

<head id="Head1" runat="server">
    <title>Historial</title>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>    
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
     <%--   <%: Styles.Render("~/Content/bootstrap-responsive.css")%>--%>
        <%: Styles.Render("~/bundles/Styles/masterblankStyles")%>
        <%: Styles.Render("~/bundles/Styles/grids")%>

         <%:Scripts.Render("~/bundles/jqueryCore") %>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/tabber") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
    </asp:PlaceHolder>
</head>

<body class="body-TaskHistory">
    <form id="form2" runat="server" class="Frm-Task-History">
        <div id="divButtons" class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div style="margin: 5px">
                        <button type="button" class="btn btn-default btn-xs"  onclick="Refresh_Click()">
                            <span class="glyphicon glyphicon-refresh"></span> Refrescar
                        </button>

                        <asp:Button ID="btnMostrarTodos" runat="server" 
                            Text="Mostrar todos" class="btn btn-default btn-xs" 
                            OnClick="btnMostrarTodos_Click"/>

                        <asp:Button ID="btnMostrarHistorialIndice" runat="server" 
                            Text="Mostrar Historial de Atributos" class="btn btn-default btn-xs" 
                            OnClick="btnMostrarHistorialIndice_Click"  />
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid" id="divGrid" style="padding:0px">
            <div class="row">
                <div class="col-md-12" style="padding:0px">
                    <uc1:ucHistoryGrid ID="ucTaskHistoryGrid" runat="server" />
                </div>
            </div>
        </div>
    </form>

    <style>
        #divGrid .row {
            margin:0px;
        }
    </style>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    var screenHeight = $(window).height() - $("#divButtons").height();
        //    $("#ucTaskHistoryGrid_grdHistory").css('max-height', screenHeight);
        //});

        function Refresh_Click() {
            ShowLoadingAnimation();
            document.location = document.location;
        }

        $(window).on("load", function () {
            parent.parent.hideLoading();
            hideLoading();
            parent.parent.SetTabContentHeight();
        });

        //$(window).load(function () {
        //    parent.parent.hideLoading();
        //    hideLoading();
        //});
</script>
</body>

</html>