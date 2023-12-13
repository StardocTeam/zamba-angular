<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_WF_TaskDetails_TaskHistory" CodeBehind="TaskHistory.aspx.cs" %>

<%@ Register Src="~/Views/UC/Grid/ucHistoryGrid.ascx" TagName="ucHistoryGrid" TagPrefix="uc1" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html>

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
                        <button type="button" class="btn btn-default btn-xs" onclick="Refresh_Click()">
                            <span class="glyphicon glyphicon-refresh"></span>Refrescar
                        </button>

                        <asp:Button ID="btnMostrarTodos" runat="server"
                            Text="Mostrar todos" class="btn btn-default btn-xs"
                            OnClick="btnMostrarTodos_Click" />

                        <asp:Button ID="btnMostrarHistorialIndice" runat="server"
                            Text="Mostrar Historial de Atributos" class="btnMostrarHistorialIndice"
                            OnClick="btnMostrarHistorialIndice_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid" id="divGrid" style="padding: 0px">
            <div class="row">
                <div class="col-md-12" style="padding: 0px">
                    <uc1:ucHistoryGrid ID="ucTaskHistoryGrid" runat="server" />
                </div>
            </div>
        </div>
    </form>

    <style>
        .btnMostrarHistorialIndice{
            background-color:white ;
            color:black !important;
            border-color:#ccc !important;
        
            padding-top:0px  !important;
            padding-bottom:2px  !important;
            border-style:solid;
                border-width:1px;
                 border-radius:3px;
        

        }
        .btnMostrarHistorialIndice:hover {
            background-color: #E6E6E6 !important;
            color: black !important;
                border-color:#ccc !important;
           
       }

         .btnMostrarHistorialIndice:focus {
          background-color: white !important;
            color: black!important;
            border-color:#ccc !important;
}

        #divGrid .row {
            margin: 0px;
        }

        #divGrid {
            overflow-x: auto;
        }

        *::-webkit-scrollbar {
            width: 10px;
            height: 10px;
        }

        *::-webkit-scrollbar-track {
            background: #ffffff;
        }

        *::-webkit-scrollbar-thumb {
            background-color: #337ab7;
            border-radius: 5px;
            border: 0px solid #ffffff;
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
