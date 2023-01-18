<%@ Page Language="C#" AutoEventWireup="false" Inherits="_Main"
    MasterPageFile="~/MasterBlankPage.Master" Codebehind="WF.aspx.cs" %>

<%@ Register Src="~/Views/UC/WF/Arbol.ascx" TagName="UCArbol" TagPrefix="UC1" %>
<%@ Register Src="~/Views/UC/Task/TaskGrid.ascx" TagName="UCTask" TagPrefix="UC2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="header_css" runat="Server">
    <title>Zamba Web - Tareas</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="header_js" runat="Server">
    <style type="text/css">
        #sidebar
        {
            float: left;
            width: 15%;
        }
        #content
        {
            float: left;
             width: 85%;
            
        }
        
        <%--Oculta los resizer que se agregar por jquery--%>
        .ui-resizable-handle {
            display: none !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            
            //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
            //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
            $("input[type=text]").focus(function () {
                $(this).select();
            });

            <%--Setea la funcionalidad para autoajustar el arbol de WF y la grilla--%>
            SetContentSideBarFuncionality();

            $(window).on("resize", function () {
                SetContentSideBarFuncionality();
            });
        });
        
        function SetGridHeight() {
            var wfIframeHeight;

            if (typeof (parent.getWFIf) == "function") {
                wfIframeHeight = parent.getWFIf().height();
            }
            else {
                wfIframeHeight = 550;
            }

            var menuHeight = parent.getMainMenuHeightFromParent();
            var parentIframeHeight = wfIframeHeight - menuHeight - 3;
            var contentHeight = getFilterControl().height() + $("#divPager").height() + 3;

            if (getGrid().height() + 30 > parentIframeHeight - contentHeight)
            {
                $("#content").height(parentIframeHeight - contentHeight);
            }
            else
            {
                $("#content").height(getGrid().height() - 50);
            }
        }

        function SetTreeHeight() {
            var wfIframeHeight;

            if (typeof (parent.getWFIf) == "function") {
                wfIframeHeight = parent.getWFIf().height();
            }
            else {
                wfIframeHeight = 600;
            }

            if ($("#divTreeContainer").height() > wfIframeHeight)
                $("#divTreeContainer").height(wfIframeHeight)
            
            //$("#sidebar").height($("#divTreeContainer").height() + $("#spnToolbar").height());
        }
       
        function SetContentSideBarFuncionality() {
            var screenWidth = getAvailableWidth();
            //$("#divTreeContainer").width(screenWidth * 0.15);

            //if (document.config.isOldBrowser)
              //  $("#spnToolbar").width(screenWidth * 0.15);

            //$("#content").width(screenWidth * 0.85 - 5);
            SetGridHeight();
            SetTreeHeight();
        }

        function getGrid() {
            return $("#<%=TaskGrid.FindControl("ucTaskGrid").FindControl("grvTaskGrid").FindControl("grvGrid").ClientID%>");
        }

        function getFilterControl() {
            try {
                return $("#<%=TaskGrid.FindControl("ucTaskGridFilter").FindControl("pnlFilter").ClientID%>");
            } catch (e) {
                return 0;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
  
      <%-- Se agrega el script en esta parte por una cuestion de resolucion de namespaces en AJAX --%>
    <script type="text/javascript">
        //se agrega este handler para mostrar el loading mientras cambia de etapa
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);

        function beginRequestHandler() {
            ShowLoadingAnimation();
        }

        function endRequestHandler() {
            hideLoading();
        }
    </script>
    <asp:Label runat="server" ID="lblNoWFVisible" Text="El usuario no posee ningun WorkFlow visible por favor contactese con el administrador de sistema"
        Visible="False"></asp:Label>
    <asp:UpdatePanel runat="server" ID="UpdTaskGrid" UpdateMode="conditional">
        <ContentTemplate>
            <div id="sidebar">
                <UC1:UCArbol runat="server" ID="Arbol" />
            </div>
            <div id="content" >
                <UC2:UCTask runat="server" ID="TaskGrid" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
