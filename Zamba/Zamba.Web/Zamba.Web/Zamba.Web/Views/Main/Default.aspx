<%@ Page Language="C#" AutoEventWireup="false" Inherits="Default" Culture="es-ES" UICulture="es-ES"
    MasterPageFile="~/Masterpage.Master" CodeBehind="Default.aspx.cs" %>

<%@ Register Src="~/Views/UC/WF/Arbol.ascx" TagName="UCArbol" TagPrefix="zarb" %>
<%@ Register Src="~/Views/UC/Task/TaskGrid.ascx" TagName="UCTask" TagPrefix="zgrd" %>
<%--<%@ Register Src="~/Views/UC/Index/DocTypesIndexs.ascx" TagName="DocTypesIndexs" TagPrefix="zind" %>--%>
<%--<%@ Register TagPrefix="ZControls" TagName="DocTypes" Src="~/Views/UC/DocTypes/DocTypes.ascx" %>--%>
<%@ Register Src="~/Views/UC/Grid/UcNewsGrid.ascx" TagName="UCNews" TagPrefix="znws" %>
<%@ Register Src="~/Views/UC/WF/UCWFExecution.ascx" TagName="UCWFs" TagPrefix="zucwf" %>


<%--<%@ Register Src="~/Views/Main/HomeWidget.ascx" TagName="UCHomeWidget" TagPrefix="zhwg" %>--%>


<asp:Content ID="Content4" ContentPlaceHolderID="header_css" runat="Server">
    <title>Zamba</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="header_js" runat="Server">
</asp:Content>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder">

    <div id="MainTabberContainer">
        <div id="MainTabber">
            <asp:HiddenField ID="viewsNews" runat="server" />
            <asp:HiddenField ID="viewInsert" runat="server" />
            <asp:HiddenField ID="hdnLastTaskIds" runat="server" />
            <asp:HiddenField ID="hdnUserId" runat="server" />
            <asp:HiddenField ID="hdnConnectionId" runat="server" />
            <%-- Barra navBar --%>
            <nav id="MasterHeader" class="navbar  navbar-fixed-top navbarHeadColor" data-ng-cloak style="height: 30px">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#MainNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a href="#" class="navbar-brand clientlogo" onclick="OpenHome();"></a>

                </div>

                <div class="collapse navbar-collapse" id="MainNavbar">
                    <ul id="mainMenu" class="nav navbar-nav" style="display: inline-block;">
                        <li id="liHome" class="navigation-item"><a href="#tabhome" id="btntabhome">Inicio</a></li>

                        <li id="liNews" class="navigation-item"><a href="#tabnew" id="tabnews">Novedades</a></li>
                        <li id="liTabSearch" class="navigation-item"><a href="#tabsearch" id="btnTabSearch">Búsqueda</a></li>

                        <li id="liResults" style="display: none;"><a href="#tabresults">Resultados</a></li>
                        <li><a href="#tabInsert" class="navigation-item" id="liInsert">Insertar</a></li>
                        <li id="liTasks" style="display: none;"><a id="anchorTabTasks" href="#tabtasks">Tareas</a></li>
                        <li id="liTabRule" style="display: none;"><a href="#tabRules" id="rule">Rule</a></li>
                    </ul>
                    <ul id="userActionHeader" class="nav navbar-nav dropdown" data-toggle="dropdown ">
                        <li id="dropdown-header">

                            <a class="dropdown-toggle" id="dropCabezera" data-toggle="dropdown " href="#">Acciones<i class="caret"></i></a>
                            <ul class="dropdown-menu " aria-labelledby="dropdown-header" id="pnlHeaderButtons" runat="server">
                            </ul>
                        </li>
                    </ul>
                    <ul>
                        <li id="Advfilter2" class="adv2  col-xs-2">
                            <img class="favAdvSearch" />
                            <div class="input-group" style="padding-top: 5%;">
                                <input class="advancedSearchBox form-control input-xs hidden-sm col-xs-4" style="float: right;" placeholder="Buscar...">
                                <!--<span class="input-group-btn">
                           <img src="../../Content/Images/icon.png" class="favAdvSearch" aria-hidden="true" style="margin-left: 10px; width: 16px; height: 16px;" title="Abrir busqueda minimizada" /></img>
                             <a id="LupaSearch" class="visible-sm hidden-md" style="background: none;">
                               <i class="fa fa-search fa-1x" style="color: white; position: relative" aria-hidden="true"></i>
                             </a>
                         </span>-->
                            </div>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li class="dropdown" id="tblUsuario">
                            <a href="#" id="UsuarioDrop" class="dropdown-toggle" data-toggle="dropdown">
                                <span class="glyphicon glyphicon-user icon-color " style="padding-right: 5px;"></span>
                                <p class="label label-primary user-button-bgcolor hidden-sm " runat="server" id="lblUsuarioActual2">
                                </p>
                                <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu" id="dropdown-user">
                                <li>
                                    <p class="label label-primary user-button-bgcolor visible-sm" runat="server" id="lblUsuarioActual3"></p>
                                </li>
                                <li><a id="btnMapaSitioCab" href="#" style="text-decoration: none; color: Navy;" onclick="RedirectToSiteMap();">Mapa de sitio</a></li>
                                <li><a href="#" style="text-decoration: none; color: Navy;" onclick="ShowChangePassDlg();">Cambiar contraseña </a></li>
                                <li><a href="#" style="text-decoration: none; color: Navy;" onclick="$('#openModalTimeout').modal();$('#openModalTimeout').draggable();">Bloquear página </a></li>
                                <li><a id="btnHelpCab" href="#" style="text-decoration: none; color: Navy;" onclick="btnHelpCab_Click();">Ayuda</a></li>
                                <li><a id="btnReleaseNotes" href="https://docs.google.com/document/d/1qVetyXq5IfNR_4gVkCR4GkA3_4lizL3A2WoFVfG0WUY/pub?embedded=true" target="_blank" style="text-decoration: none; color: Navy;">Release Notes</a></li>
                                <li><a href="../Security/Logout.aspx" style="text-decoration: none; color: Navy" id="logOutSite">Cerrar sesión </a></li>
                                <li class="divider"></li>
                                <li><span style="font-size: 10px;">Zamba Web Ver. <% =ConfigurationManager.AppSettings["ZambaVersion"].ToString() %></span></li>
                                <li class="divider"></li>
                                <li>
                                    <button type="submit" runat="server" class="btn btn-primary btn-xs" style="text-decoration: none; color: Navy;" onserverclick="CleanCache">Borrar Cache</button></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </nav>
            <%-- Fin Barra navBar --%>
            <%--   <div id="tabhome" style="display: none;" class="MainTabberTabs TabSpace">--%>
            <%--<div class="row">
                    <div id="Advfilter3" class="centerDiv">
                        <div class="col-md-3"></div>
                        <div class="col-md-6 adv3">
                            <input class="advancedSearchBox form-control input-md" placeholder="Buscar..." />
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <asp:Panel ID="pnlHomeButtons" CssClass="home-tab container" runat="server">
                </asp:Panel>--%>

            <%--<div>
                    <%--                 <iframe src="HomeWidget.aspx" id="homewidget"  style="height: 600px;width:100%; border: none"> </iframe>--%>
            <%-- </div>--%>


            <%--</div>--%>
            <div id="tabnew" style="display: none;" class="MainTabberTabs TabSpace">
                <div>
                    <div class="row">
                        <div class="col-md-12" data-ng-controller="GridListController">
                            <%--   <asp:UpdatePanel runat="server" ID="UpdNews" UpdateMode="conditional">
                                <ContentTemplate>
                                    <znws:UCNews runat="server" ID="ucNewsGrid" />
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <%--                            <my-shared-scope> </my-shared-scope>--%>

                            <grid-news />

                        </div>
                    </div>
                </div>
            </div>

            <div id="tabsearch" style="display: block;" class="MainTabberTabs TabSpace">
                <div id="EntitiesCtrl" data-ng-controller="maincontroller" class="container-fluid" style="height: 100%;">
                    <div class="row">
                        <div class="col-md-12">
                            <input id="txtMensajes" runat="server" style="display: none;" />
                        </div>
                    </div>

                    <!-- <div id="globalSearchContent" style="display: none">
                    </div>-->

                    <div id="searchWrapper" class="row" style="height: 100%;">
                        <div id="treeWrapper" class="col-xs-3 col-sm-6 col-md-3 EntitiesTree slideSidePanel">
                            <nav class="navbar " id="spnToolbar" style="margin-bottom: 0; min-height: 0">
                                <span id="expandAllNodes" class="btn btn-xs btnBlue btnLightblue" title="Expandir árbol" data-placement="right" data-toggle="tooltip">
                                    <i class="fa fa-plus-square-o"></i>
                                </span>
                                <span id="collapseAllNodes" class="btn btn-xs btnBlue btnLightblue" title="Contraer árbol" data-placement="right" data-toggle="tooltip">
                                    <i class="fa fa-minus-square-o"></i>
                                </span>

                                <span id="selectAllNodes" class="btn btn-xs btnBlue btnLightblue" title="Seleccionar todo" data-placement="right" data-toggle="tooltip">
                                    <i class="fa fa-check-square-o"></i>
                                </span>
                                <span id="unSelectNodes" class="btn btn-xs btnBlue btnLightblue" title="Deseleccionar todo" data-placement="right" data-toggle="tooltip">
                                    <i class="fa fa-square-o"></i>
                                </span>

                                <span id="showSelectNodes" class="btn btn-xs btnBlue btnLightblue" title="Mostrar seleccionados" data-placement="right" data-toggle="tooltip">
                                    <i class="fa fa-star"></i>
                                </span>
                                <input id="filterText" value="" class="k-textbox" placeholder="Buscar" style="height: 25px; width: 80px" />
                            </nav>
                            <div class="searchtreeview" id="treeview"></div>
                            <div id="toogleTree" onclick="toogleTree(this);">
                                <span class="glyphicon glyphicon-chevron-left"></span>
                            </div>
                        </div>
                        <div class="col-xs-9 col-sm-8 col-md-9 searchsearchbox slideMainPanel" id="dvSearchBox" style="padding: 0px;">
                            <div class="row" style="display: none">
                                <div class="col-md-12" onclick="advanceSearch=true">
                                    <label>Buscar</label>
                                    <input id="TxtTextSearch" />
                                </div>
                            </div>
                            <div class="row" id="barratop">
                                <div id="barraTopBtns" class="col-md-9 col-md-offset-2 col-xs-offset-2 ">
                                    <a id="btnCloseSidePanel" class="btn btn-md" style="display: none;">
                                        <span id="imgBtnCloseSidePanel" class="glyphicon glyphicon-menu-left btnToggleSidePanel"></span>
                                    </a>
                                

                                    <a id="btnbusqueda" class="btn btn-sm" data-ng-click="DoSearch()" data-ng-if="Search.DoctypesIds.length > 0">
                                        <span class="glyphicon glyphicon-search white"></span><span class="white" style="padding-left: 5px;">Buscar</span>
                                    </a>
                                    <a id="btnbusqueda2" class="btn btn-sm" onclick="CleanAllInputs()" data-ng-if="Search.DoctypesIds.length > 0">
                                        <span class="glyphicon glyphicon-trash white"></span><span class="white" style="padding-left: 5px;">Limpiar</span>
                                    </a>
                                </div>
                                <div class="col-md-3">
                                    <div id="NoResults" runat="server" visible="false" class="NoResults">
                                        <asp:LinkButton ID="lnkNoResults" runat="server" ForeColor="Red">NO SE ENCONTRARON RESULTADOS</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                         
                                        <div class="row" id="dvDocTypesIndexs" style="margin: 0;">
                                            <div class="col-md-12" style="margin: 10px;">
                                                <search-indexslist></search-indexslist>
                                                  

                                            </div>
                                        </div>
                                  

                            <div class="row" data-ng-if="Search.DoctypesIds.length > 0 && Search.Indexs.length > 5">
                                <div class="col-md-9 col-md-offset-3 col-xs-offset-3">
                                    <a class="btn btn-primary btn-xs" data-ng-click="DoSearch()">
                                        <span class="glyphicon glyphicon-search white"></span><span class="white">Buscar</span>
                                    </a>
                                    <div class="row" data-ng-if="Search.DoctypesIds.length > 0">
                                        <div class="col-md-9 col-md-offset-3 col-xs-offset-3">
                                            <a class="btn btn-primary btn-xs" data-ng-click="DoSearch()">
                                                <span class="glyphicon glyphicon-search white"></span><span class="white">Buscar</span>
                                            </a>
                                            <a class="btn btn-primary btn-xs" data-ng-click="CleanSearch()">
                                                <span class="glyphicon glyphicon-trash white"></span><span class="white">Limpiar</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <script type="text/javascript">
                    <%--script para reconocer cuando se cierra el browser y cerrar sesion adecuadamente--%>
                    //29/07/11: se agrega el manejo de eventos asíncronos, para los UpdatePanel por ejemplo.
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args) {
                        $('.EntryIndex').keypress(function (event) {
                            if (event.which == 13) {
                                $("#ContentPlaceHolder_btnSearch").click();
                                event.preventDefault();
                            }
                        });
                        FixFocusError();

                        var screenHeight = GetHeight() - $("#lblMsg").width() - 95;
                        $("#gridContent").height(screenHeight);
                        loadCliksInRows();

                        //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
                        //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
                        $("input[type=text]").focus(function () {
                            if (!$(this).hasClass("hasDatepicker")) {
                                $(this).select();
                            }
                        });


                        //document.getElementById('homewidget').contentWindow.LoadWidget(GetUID());
                    }

                    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
                    function initializeRequestHandler(sender, args) {
                        FixFocusError();
                    }

                    function mostrarMensaje() {
                        if ($("#<%=txtMensajes.ClientID %>").val() != "") {
                            alert($("#<%=txtMensajes.ClientID %>").val());
                            $("#<%=txtMensajes.ClientID %>").val("");
                        }
                    }
                </script>
            </div>

            <div id="tabresults" class="tabresults-Frm-Default MainTabberTabs TabSpace" style="display: none; overflow: auto;">

                <div id="ResultsCtrl" data-ng-controller="resultscontroller" class="container-fluid">

                    <div class="container-fluid">
                        <div class="row">

                            <div class="col-md-12">
                                <%--  <search-results></search-results>--%>
                                <results-grid></results-grid>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div id="tabInsert" style="display: none" class="MainTabberTabs TabSpace">
                <iframe src="about:blank" runat="server" id="insertIframe" style="width: 100%; height: 100%;"></iframe>
            </div>



            <div id="tabtasklist" style="padding: 0; display: none" class="MainTabberTabs TabSpace">
                <style type="text/css">
                    .ui-resizable-handle {
                        display: none !important;
                    }
                </style>

                <asp:Label runat="server" ID="lblNoWFVisible" Text="El usuario no posee ningun WorkFlow visible por favor contactese con el administrador de sistema"
                    Visible="False"></asp:Label>
                <asp:UpdatePanel runat="server" ID="UpdTaskGrid" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="sidebar" class="col-xs-12 col-sm-4 col-md-3 slideSidePanel" style="margin: 0; padding: 0;">
                            <zarb:UCArbol runat="server" ID="Arbol" />
                        </div>

                        <div id="content" class="col-xs-12 col-sm-8 col-md-9 grillaMini slideMainPanel" style="margin: 0; padding: 0;">
                            <zgrd:UCTask runat="server" ID="TaskGrid" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="tabtasks" style="padding: 0; overflow-y: hidden; display: none" class="MainTabberTabs">
                <div id="TasksDiv" style="border: none; height: 100%;" class="container-fluid">
                    <ul id="TasksDivUL" class="nav nav-tabs" style="background-image: none; background-color: transparent; border-color: transparent; border: none">
                    </ul>
                </div>
                <div id="NoTaskDiv">
                    <span class="EmptyContentMessage">Usted no posee tareas abiertas</span>
                </div>
                <div id="Contents">
                </div>
            </div>
            <div id="tabRules" style="padding: 0; overflow-y: hidden; background-color: #f7f7f7; display: none" class="MainTabberTabs">
                <button class="btn">testtt</button>

                <%--   <zucwf:UCWFs runat="server" ID="RuleView" />    --%>
            </div>
        </div>
    </div>
    <%--Modal bootstrap DIV "Abrir tareas cerradas inesperadamente"--%>

    <div class="modal fade" id="openTasks" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"></h4>
                </div>

                <div class="modal-body">
                    <p id="subTitleModal" />
                    <p class="tareasDiv" style="margin-left: 50px;" />
                    <p id="questionModal" />
                </div>

                <div class="modal-footer" style=" padding: 7px">
                    <a class="btn btn-danger btn-ok">Abrir</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="openModalIF" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content" id="openModalIFContent">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" id="closeModalIF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <button type="button" class="close" onclick="OpenModalIF.fullscreen(this);"><span aria-hidden="true">&#9633;</span></button>
                    <h5 class="modal-title" id="modalFormTitle"></h5>
                </div>
                <div class="modal-body" id="modalFormHome">
                    <div id="modalDivBody">
                    </div>
                    <iframe id="modalIframe" runat="server" target="_parent"></iframe>
                </div>
            </div>
        </div>
    </div>


    <%--<div id="BPLetras" style="background-color:white; height:5000px; width:5000px;" >

        
                <div data-ng-controller="appController" style=" float: inherit;">
                    <div class="modal-content" style="min-height: 100px;background-color:#5387bd">
                        <div class="modal-header" style="padding: 2px;background-color:white;border-radius: 10px;">
                           <div class="GSTxtInModalHeader">   
                                 </div> 
                            <div style="float: right;">                   --%>
    <%--<a href="#" data-toggle="tooltip" style="margin-right:5px;text-decoration:none" data-placement="bottom" class="glyphicon glyphicon-wrench remove-all-icon " id="advSearchConfig" onclick="showGSConfig();" title="Configuracion"></a> --%>
    <%--<a href="#" data-toggle="tooltip"  style="margin-right:5px;text-decoration:none" data-placement="bottom" class="glyphicon glyphicon-question-sign remove-all-icon " id="advSearchHelp" onclick="showGSHelp();" title="Ayuda"></a>--%>
    <%--                                 <a href="#" id="advSearchSave" title="Guardar busqueda" role="button"><span class="remove-all-icon glyphicon glyphicon-minus"></span></a>--%>
    <%--                                <a href="#" data-toggle="tooltip"  style="margin-right:5px;text-decoration:none" data-placement="bottom" class="glyphicon glyphicon glyphicon-minus " id="advSearchSave" title="Minimizar Ventana"></a>

                                <a href="#" data-toggle="tooltip" style="margin-right:5px;text-decoration:none" data-placement="bottom" class="glyphicon glyphicon-remove " data-dismiss="modal" id="advSearchClose" title="Cerrar busqueda"></a>
                            </div>
                         <div class="filterCount dropdown" >
                                    <button class="btn  btn-xs dropdown-toggle" style="color:black" type="button" id="filterCountDiv" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" value="25">
                                        Traer <b>25</b> registros
                                    <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu" aria-labelledby="dropdownMenu1" style="z-index: 99999;font-size: 12px;">
                                        <li><a href="#" data-value="10">Traer <b>10</b> registros</a></li>
                                        <li><a href="#" data-value="25 action">Traer <b>25</b> registros</a></li>
                                        <li><a href="#" data-value="50">Traer <b>50</b> registros</a></li>
                                        <li><a href="#" data-value="100">Traer <b>100</b> registros</a></li>
                                    </ul>
                                </div>
                        </div>
                        <div class="modal-body" style="padding: 0;">
                            <zamba-search style="background-color: white !important;" id="zambasearchcontrol" ng-model="searchParams"
                                parameters="availableSearchParams"
                                placeholder="Buscar...">
                                </zamba-search>--%>

    <%--redirecciona al modal--%>
    <%--                            <script>
                                function GetNextUrl(index) {
                                    return angular.element($('#zambasearchcontrol')).scope().GetNextUrl(index);

                                }
                            </script>
                        </div>
                    </div>
                </div>
              



</div>--%>




    <asp:HiddenField ID="hdnFLinesCount" runat="server" />
    <asp:HiddenField ID="hdnFRefresh" runat="server" />
    <asp:HiddenField ID="hdnFView" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="footer_js" runat="Server">

<%-- <link rel="stylesheet" href="../../scripts/kendouistyles/kendo.common.min.css" />
    <link rel="stylesheet" href="../../scripts/kendouistyles/kendo.rtl.min.css" />--%>
<%--    <link rel="stylesheet" href="../../scripts/kendouistyles/kendo.silver.min.css" />--%>
<%--    <link rel="stylesheet" href="../../scripts/kendouistyles/kendo.mobile.all.min.css" />--%>

<%--    <script src="../../scripts/kendoui/js/kendo.all.min.js"></script>--%>

    <script src="../../Scripts/app/Grids/KendoGrid.js?v=256"></script>
    <script src="../../Scripts/app/Grids/Grids.js"></script>

    <%--Obtencion de front end data--%>
    <script type="text/javascript">
        function getInitTabparams() {
            return String(window.location.search);
        }
        function viewInsertAspx() {
            return $("#<%=viewInsert.ClientID %>").val();
        }
        function getFLCount() {
            return $("#<%=hdnFLinesCount.ClientID %>").val();
                    }
                    function getRfInterval() {
                        return $("#<%=hdnFRefresh.ClientID %>").val();
                    }
                    function getFView() {
                        return $("#<%=hdnFView.ClientID %>").val();
                    }
                    function getPageTheme() {
                        return "<%=Page.Theme%>";
                    }
                    function getGrid() {
                        return $("#<%=TaskGrid.FindControl("ucTaskGrid").FindControl("grvTaskGrid").FindControl("grvGrid").ClientID%>");
                    }
                    function getFilterControl() {
            <%--return $("#<%=TaskGrid.FindControl("ucTaskGridFilter").FindControl("pnlFilter").ClientID%>");--%>
        }

        $(document).ready(function () {


        });
    </script>
    <%   if (HttpContext.Current.IsDebuggingEnabled)
        { %>
    <script type="text/javascript" src='../../Scripts/Default.aspx.js'></script>
    <%  }
        else
        { %>
    <script type="text/javascript" src='../../Scripts/Default.aspx.min.js'></script>
    <%  } %>
    <%--//se agrega este handler para mostrar el loading mientras cambia de etapa--%>
    <%--  <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler);
    </script>--%>




</asp:Content>




