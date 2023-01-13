<%@ Page Language="C#" AutoEventWireup="false" CodeFile="Default.aspx.cs" Inherits="_Default"
    MasterPageFile="~/Masterpage.Master" %>

<%@ Register Src="~/Views/UC/WF/Arbol.ascx" TagName="UCArbol" TagPrefix="zarb" %>
<%@ Register Src="~/Views/UC/Task/TaskGrid.ascx" TagName="UCTask" TagPrefix="zgrd" %>
<%@ Register Src="~/Views/UC/Index/DocTypesIndexs.ascx" TagName="DocTypesIndexs" TagPrefix="zind" %>
<%--<%@ Register TagPrefix="ZControls" TagName="DocTypes" Src="~/Views/UC/DocTypes/DocTypes.ascx" %>--%>
<%@ Register Src="~/Views/UC/Grid/UcNewsGrid.ascx" TagName="UCNews" TagPrefix="znws" %>

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
            <nav id="headerNav">
                <a id="headerMenu" class="ui-state-default ui-corner-top" href="#" style="display: none">Menú</a>
                <ul id="mainMenu" class="ui-tabs-nav mainMenu">
                    <li id="liHome" style="border: none"><a href="#tabhome" id="btntabhome">Home</a></li>
                    <li id="liNews" style="border: none"><a href="#tabnew" id="tabnews">Novedades</a></li>
                    <li style="border: none"><a href="#tabsearch">Busqueda</a></li>
                    <li style="border: none"><a href="#tabresults">Resultados</a></li>
                    <li style="border: none"><a href="#tabInsert" id="liInsert">Insertar</a></li>
                    <li style="border: none"><a id="tasklist" href="#tabtasklist">Listados</a></li>
                    <li style="border: none"><a href="#tabtasks">Tareas</a></li>
                </ul>
            </nav>
            <div id="tabhome" style="display: none" class="MainTabberTabs TabSpace">
                <div class="row">
                    <div id="Advfilter3" class="centerDiv">
                        <%--Home--%>
                        <div class="col-md-3"></div>
                        <div class="col-md-6 adv3">
                            <input class="advancedSearchBox" placeholder="Buscar...">
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
                <asp:Panel ID="pnlHomeButtons" CssClass="home-tab" runat="server">
                </asp:Panel>
            </div>
            <div id="tabnew" style="display: none" class="MainTabberTabs TabSpace">
                <div class="container-fluid" style="margin-left: 10px">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="UpdNews" UpdateMode="conditional">
                                <ContentTemplate>
                                    <znws:UCNews runat="server" ID="ucNewsGrid" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabsearch" style="display: none" class="MainTabberTabs TabSpace" ng-app="searchApp">
                <div id="EntitiesController" class="container-fluid" ng-controller="EntitiesController">
                    <div class="row">
                        <div class="col-md-12">
                            <input id="txtMensajes" runat="server" style="display: none;" />
                        </div>
                    </div>
                    <div id="searchWrapper" class="row">
                        <div class="col-md-3">

                            <div class="searchtreeview" id="treeview"></div>

                        </div>

                        <div class="col-md-9 searchsearchbox" id="dvSearchBox">
                            <div class="row" runat="server" id="pnlWordSearch" style="margin-bottom: 5px">
                                <div class="col-md-9">
                                    <span>
                                        <asp:Label ID="LblTextSearch" runat="server" Text="Buscar"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="TxtTextSearch" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--   <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-md-9 col-md-offset-3">

                                    <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-primary btn-xs" OnClick="btnSearch_Click">
                                                        <span class="glyphicon glyphicon-search white"></span><span class="white"> Buscar</span>
                                    </asp:LinkButton>
                                    <%--<asp:LinkButton ID="btnClearIndexs" runat="server" class="btn btn-info btn-xs" OnClick="btnClearIndexs_Click">
                                                        <span class="white">Limpiar</span>
                                            </asp:LinkButton>--%>
                                    <asp:CheckBox runat="server" ID="chkSearchByTask" Text="Buscar en tareas" CssClass="check-box-by-task" />
                                </div>
                                <div class="col-md-3">
                                    <div id="NoResults" runat="server" visible="false" class="NoResults">
                                        <asp:LinkButton ID="lnkNoResults" runat="server" ForeColor="Red">NO SE ENCONTRARON RESULTADOS</asp:LinkButton>
                                    </div>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300" Text="No se encontraron documentos"
                                        Visible="False"></asp:Label>
                                </div>
                            </div>
                            <%--        </ContentTemplate>
                            </asp:UpdatePanel>--%>


                            <div class="row" id="dvDocTypesIndexs">
                                <div class="col-md-12">
                                    <searchindexslist></searchindexslist>

                                    <%--                                    <asp:UpdatePanel ID="UpdatePanel2" name="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <zind:DocTypesIndexs ID="DocTypesIndexs" runat="server" EnableViewState="True" />
                                            <h2 style="text-align: center">
                                                <asp:Label ID="lblErrorIndex" Width="100%" runat="server" Text="No se han seleccionado documentos que puedan mostrar indices" Visible="false" ForeColor="Red"></asp:Label>
                                            </h2>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    //Manejo de header JS
                    $(function () {
                        var pull = $('#headerMenu');
                        menu = $('#headerNav ul');
                        menuHeight = menu.height();

                        $(pull).on('click', function (e) {
                            e.preventDefault();
                            menu.slideToggle();
                        });
                    });

                    $("#headerNav li").on('click', function (e) {
                        if ($(window).width() < 500) {//(matchMedia('only screen and (max-width: 500px)').matches){
                            e.preventDefault();
                            menu.slideToggle();
                        }
                    });

                    $(window).resize(function () {

                        var w = $(window).width();
                        if (w > 320 && menu.is(':hidden')) {
                            menu.removeAttr('style');
                        }
                        resizeHeaderMenu();
                    });

                    function resizeHeaderMenu() {
                        var $pull = $('#headerMenu');
                        if ($(window).width() < 500) {//(matchMedia('only screen and (max-width: 500px)').matches){
                            $("#mainMenu").css("display", "none");
                            $pull.css("display", "block");
                        }

                        else
                            $pull.css("display", "none");
                    }

    <%--script para reconocer cuando se cierra el browser y cerrar sesion adecuadamente--%>

                    //29/07/11: se agrega el manejo de eventos asíncronos, para los UpdatePanel por ejemplo.
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args) {
                        $('.EntryIndex').keypress(function (event) {
                            if (event.which == 13) {
                                $("#ctl00_ContentPlaceHolder_btnSearch").click();
                                event.preventDefault();
                            }
                        });
                        FixFocusError();
                        SetValidationsAction("<%=btnSearch.ClientID %>");

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
                    }

                    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
                    function initializeRequestHandler(sender, args) {
                        FixFocusError();
                    }

                    $(document).ready(function () {
                        ExecuteFormReadyActions();
                        $('.EntryIndex').keypress(function (event) {
                            if (event.which == 13) {
                                $("#ctl00_ContentPlaceHolder_btnSearch").click();
                                event.preventDefault();
                            }
                        });
                        mostrarMensaje();

                        SetValidationsAction("<%=btnSearch.ClientID %>");
                        resizeHeaderMenu();
                    });

                    function mostrarMensaje() {
                        if ($("#<%=txtMensajes.ClientID %>").val() != "") {
                            alert($("#<%=txtMensajes.ClientID %>").val());
                            $("#<%=txtMensajes.ClientID %>").val("");
                        }
                    }
                </script>
            </div>


            <div id="tabresults" class="tabresults-Frm-Default MainTabberTabs TabSpace" style="display: none">
                <%-- Se agrega el script en esta parte por una cuestion de resolucion de namespaces en AJAX --%>
                <script type="text/javascript">
                </script>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblMsg" runat="server" CssClass="EmptyContentMessage" Visible="True"
                                        Text="La grilla de resultados es mostrada al realizar una búsqueda."></asp:Label>
                                    <div id="gridContent" class="gridContainer gridContainer-Frm-Results" style="overflow: auto; width: 100%">
                                        <asp:GridView ID="grdResultados" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grdResultados_OnPageIndexChanging"
                                            AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" HeaderStyle-CssClass="FixedHeader" Style="overflow: auto">
                                            <RowStyle CssClass="RowStyleResults" Wrap="false" />
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" Wrap="false" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" Wrap="false" />
                                            <HeaderStyle CssClass="HeaderStyle" Wrap="false" />
                                            <EditRowStyle CssClass="EditRowStyle" Wrap="false" />
                                            <AlternatingRowStyle CssClass="AltRowStyleResults" Wrap="false" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabInsert" style="display: none" class="MainTabberTabs TabSpace">
                <iframe src="about:blank" runat="server" id="insertIframe" style="width: 100%"></iframe>
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
                        <div id="sidebar" class="col-md-3">
                            <zarb:UCArbol runat="server" ID="Arbol" />
                        </div>

                        <div id="content" class="col-md-9">
                            <zgrd:UCTask runat="server" ID="TaskGrid" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="tabtasks" style="height: 100%; display: none" class="MainTabberTabs">
                <div id="TasksDiv" style="border: none">
                    <ul id="TasksDivUL" style="background-image: none; background-color: transparent; border-color: transparent; border: none">
                    </ul>
                </div>
                <div id="NoTaskDiv">
                    <span class="EmptyContentMessage">Usted no posee tareas abiertas</span>
                </div>
                <div id="Contents">
                </div>
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

                <div class="modal-footer">
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
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="modalFormTitle"></h4>
                </div>
                <div class="modal-body" id="modalFormHome">
                    <div id="modalDivBody">
                    </div>
                    <iframe id="modalIframe" runat="server" target="_parent"></iframe>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hdnFLinesCount" runat="server" />
    <asp:HiddenField ID="hdnFRefresh" runat="server" />
    <asp:HiddenField ID="hdnFView" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="footer_js" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ModifyHeight();
            <%--//InitializeFeeds();--%>

        });

        function ModifyHeight() {
            var height = GetHeight();

            $("#tabnew").css("height", height);
            $("#<%=insertIframe.ClientID %>").css("height", $("#tabInsert").height());
            $("#insertIframe").css("height", $("#tabInsert").height());
            $("#insertIframe").css("width", 600);

            //$("#tabInsert").css("height", height);
            <%--$("#<%=insertIframe.ClientID %>").css("height", height);--%>
        }

        function GetHeight() {
            var masterHeader = $("#MasterHeader").css("display") == "none" ? 0 : $("#MasterHeader").height();
            var barraCabecera = $("#barra-Cabezera").css("display") == "none" ? 0 : $("#barra-Cabezera").innerHeight();
            var mainmenu = $("#mainMenu").css("display") == "none" ? 0 : $("#mainMenu").innerHeight();
            var headersHeight = masterHeader + barraCabecera + mainmenu;
            return screen.availHeight - headersHeight - 30
        }
    </script>

    <script type="text/javascript">
        function getInitTab() {
            var params = String(window.location.search);
            var aux1 = new Array();
            var page;

            initTab = null;
            if (params.length > 0) {
                params = params.substr(1);
                aux1 = params.split("=");

                if (aux1[0] == "tabRedirect") {
                    page = aux1[1];
                    switch (page) {
                        case "Novedades":
                            initTab = "tabnew";
                            break;
                        case "Busqueda":
                            initTab = "tabsearch";
                            break;
                        case "Tareas":
                            initTab = "tabtasklist";
                            break;
                    }
                }
            }

            return initTab;
        }

        var initaltab = "tabhome";
        $(function () {
            ShowLoadingAnimation();
            $("#TasksDiv").zTabs({});
            var viewNews = $("#ctl00_ContentPlaceHolder_viewsNews").val();
            var viewInsert = $("#<%=viewInsert.ClientID %>").val();            

            if (viewNews != null) {
                if (viewNews.toLowerCase() == "true") {
                    initaltab = "tabnew";
                    $("#liNews").css("display", "block");
                }
                else {
                    <%-- todo: una vez terminado la busqueda, volver a descomentar esta linea de codigo
                    //initaltab = "tabhome";--%>
                    // initaltab = "tabtasklist";
                    $("#liNews").css("display", "none");
                }
            }
            else {
                // initaltab = "tabtasklist";
                $("#liNews").css("display", "none");
            }

            if (viewInsert.toLowerCase() == "true") {
                $("#liInsert").css("display", "block");
            }
            else {
                $("#liInsert").css("display", "none");
            }

            var tab = getInitTab();
            if (tab) {
                initaltab = tab;
            }

            $("#MainTabber").zTabs({
                //selected: initaltab,
                ajaxOptions: {
                    error: function (xhr, status, index, anchor) {
                    }
                }, //23/08/11: Cuando se selecciona la tab de WF se refresca la grilla.
                // loading spinner
                beforeSend: function () {
                    ShowLoadingAnimation();
                },
                complete: function () {
                    $('.MainTabberTabs').show();
                    $('#mainMenu').tabs('option', 'visibility', 'visible');
                },
                activate: function (event, ui) {
                    //Antes de seleccionar nada, se elimina el tab dummy anteriormente creado.
                    if ($("#DummyTab")) {
                        $('#MainTabber').zTabs("remove", "DummyTab");
                    }

                    var tabText = $(ui.newTab[0]).text().trim();

                    switch (tabText) {
                        case "Insertar":
                            RefreshGeneralGrid(tabText, "../Insert/Insert.aspx", false);
                            break;
                    }
                }
            });

            if ($("#DummyTab")) {
                $('#MainTabber').zTabs("remove", "DummyTab");
            }
            if (viewInsert.toLowerCase() == "false") {
                $('#MainTabber').zTabs("remove", "tabInsert");
            }

            $('#MainTabber').zTabs("select", initaltab);
        });

        function RefreshGeneralGrid(tabName, url, oneLoad) {
            var WFIframe;

            switch (tabName) {
                case "Insertar":
                    WFIframe = $("#ctl00_ContentPlaceHolder_insertIframe");
                    break;
            }
             <%-- Primero se setea en en página en blanco para que tome el refresco.--%>
            if (WFIframe != null) {
                if (WFIframe[0] != null) {
                    if (oneLoad && WFIframe[0].contentWindow.location != "about:blank")
                        return;
                    ShowLoadingAnimation();
                    WFIframe[0].contentWindow.location = "about:blank";

                    try {
                        WFIframe[0].contentWindow.location = "about:blank";
                        WFIframe[0].contentWindow.location = url;
                        StartObjectLoadingObserver(WFIframe[0]);
                    }
                    catch (e) {
                        if (WFIframe.context != null) {

                            WFIframe.context.location = "about:blank";
                            WFIframe.context.location = url;
                        }
                    }
                }
            }
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

    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            <%-- Que se actualice la grilla cuando se presiona tab "Listados" --%>
            $('#tasklist').click(function () {
                document.getElementById('ctl00_ContentPlaceHolder_Arbol_RefreshClick').click();
                hideLoading();
            });

            <%-- Se agrega el script en esta parte por una cuestion de resolucion de namespaces en AJAX --%>
            <%--Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
            Con esto se deberia solucionar el problema del "bloqueo de los textbox".--%>
            $("input[type=text]").focus(function () {
                $(this).select();
            });
        });

        function getGrid() {
            return $("#<%=TaskGrid.FindControl("ucTaskGrid").FindControl("grvTaskGrid").FindControl("grvGrid").ClientID%>");
        }

        function getFilterControl() {
            return $("#<%=TaskGrid.FindControl("ucTaskGridFilter").FindControl("pnlFilter").ClientID%>");
        }

        <%--//se agrega este handler para mostrar el loading mientras cambia de etapa--%>
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler);

        function beginRequestHandler() {
            ShowLoadingAnimation();
        }
    </script>

</asp:Content>

