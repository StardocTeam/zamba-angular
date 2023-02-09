<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Insert_Insert"
    UICulture="Auto" Culture="Auto" EnableViewState="true" EnableEventValidation="false" CodeBehind="Insert.aspx.cs" %>



<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Views/UC/Upload/ucUploadFile.ascx" TagName="ucUploadFile" TagPrefix="uc1" %>
<%@ Register Src="~/Views/UC/Upload/ucDocTypes.ascx" TagName="ucDocTypes" TagPrefix="uc2" %>
<%@ Register Src="~/Views/UC/Index/ucDocTypeIndexsForInsert.ascx" TagName="ucDocTypesIndexs" TagPrefix="uc3" %>


<html>
<head id="Head2" runat="server">

    <asp:PlaceHolder runat="server">

        <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />

        <script src="../../Scripts/jquery-3.1.1.min.js"></script>

        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <script src="../../Scripts/Zamba.js?v=169"></script>

        
        <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css">
        <%--     <link rel="stylesheet" type="text/css" href="../../Content/site.css"/>--%>
        <!-- JS -->

        <%--        <script src="../../Scripts/jquery-1.12.4.js"></script>--%>
        <script src="../../Scripts/jquery-1.9.1.js"></script>
        <script src="../../Scripts/jquery-ui.js"></script>
        <link href="../../Content/themes/base/jquery-ui.css" rel="stylesheet" />


        <%: Styles.Render("~/bundles/Styles/masterblankStyles")%>
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <link href="../../Content/site.css" rel="stylesheet" />
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
        <%: Scripts.Render("~/scripts/dropzone.js") %>
        <%: Scripts.Render("~/bundles/bootbox") %>
        <link href="../../Content/dropzone.min.css" rel="stylesheet" />
        <link href="../../Content/partialSearchIndexs.css" rel="stylesheet" />
        <script src="../../Scripts/sweetalert.min.js"></script>
        <script src="../../Scripts/ListaV2.js?v=168"></script>


        <script src="../../Scripts/angular.min.js"></script>
        <script src="../../Scripts/angular-messages.js"></script>
        <script src="../../Scripts/angular-xeditable-0.8.1/js/xeditable.js"></script>
        <script src="../../Scripts/angular-sanitize.min.js"></script>
        <script src="../../Scripts/angular-animate.min.js"></script>
        <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>

        <script src="../../Scripts/angular-filter/angular-filter.min.js"></script>

        <script src="../../app/Grid/JS/ui-bootstrap-tpls-1.2.4.js"></script>

        <script src="../../app/zapp.js?v=168"></script>

        <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

        <script src="../../app/insert/insertcontroller.js?v=168"></script>
        <script src="../../app/insert/insertservices.js?v=168"></script>
        <script src="../../Scripts/zamba.associated.js?v=227"></script>

<%--        <script src="../../app/insert/history/historycontroller.js?v=168"></script>
        <script src="../../app/insert/history/historyservices.js?v=168"></script>--%>


        <style>
            /* latin */
            @font-face {
                font-family: 'Libre Barcode 39';
                font-style: normal;
                font-weight: 400;
                src: local('Libre Barcode 39 Regular'), local('LibreBarcode39-Regular'), url(https://fonts.gstatic.com/s/librebarcode39/v8/-nFnOHM08vwC6h8Li1eQnP_AHzI2G_Bx0g.woff2) format('woff2');
                unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD;
            }

            @font-face {
                font-family: 'IDAutomationHC39M';
                src: local('IDAutomationHC39M'), url(../../fonts/IDAutomationHC39M.woff);
            }

            .codebar39 {
                font-family: 'Libre Barcode 39', cursive;
                font-size: 48px;
            }

            @media print {

                /* ESTILOS SOLO PARA LA IMPRESION */
                body {
                    writing-mode: rl;
                    -webkit-print-color-adjust: exact;
                }

                .ng-scope > zamba-associated > nav {
                    display: none;
                }

                #printer {
                    display: none;
                }

                @page {
                    size: auto;
                    /* auto es el valor inicial */
                    margin:0;
                    margin-left: 25px;
                    margin-top: 2px;
                    margin-bottom: auto;
                }

                .noprint,
                .noprint * {
                    display: none !important;
                }

                .AllTop {
                    position: relative;
                    margin-top: -75px;
                }

                .insertAttributesPanel {
                    height: 100% !important;
                    overflow-y: hidden !important;
}
            }

            .texto-vertical-2 {
                writing-mode: vertical-lr;
                transform: rotate(180deg);
            }
        </style>


    </asp:PlaceHolder>
    <title>Insertar documento</title>
</head>

<body ng-app="app" ng-controller="insertController" ng-cloak>

    <input type="hidden" id="insertMode" value="insert" />
    <!--Iframe Modal para la Ejecucion de Reglas. Dentro Carga el y que tiene el Switch para Executar cada Regla.-->
    <div class="modal fade" id="openModalIFWF" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="overflow-y: hidden;">
        <div class="modal-dialog">
            <div class="modal-content" id="openModalIFContentWF" style="padding: 0">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" id="closeModalIFWF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>

                    <h4 class="modal-title" id="modalFormTitleWF"></h4>
                </div>
                <div class="modal-body" id="modalFormHomeWF" style="padding: 0; height: 350px">
                    <div id="EntryRulesContent" style="z-index: 997; top: 0; right: 120px;">
                        <iframe id="WFExecForEntryRulesFrame" src="~/Views/WF/WFExecutionForEntryRules.aspx" style="height: 700px;" runat="server"></iframe>
                        <%--                            <iframe id="printFrame" src="" style="display:none;"></iframe>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="InsertIframeBody" class="col-lg-12 body-Frm-Views-Insert">

        <div class="col-lg-12">
            <form id="aspnetForm" runat="server" class="form" enctype="multipart/form-data">
                <asp:HiddenField ID="hdnUserId" runat="server" />
                <asp:HiddenField ID="hdnConnectionId" runat="server" />
                <div id="dialoginfoetapa" style="display: none" title="Zamba Software">
                    <asp:Label ID="lblpopup" runat="server" Text="Contendra el texto de la etapa"></asp:Label>
                </div>

                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Release">
                </asp:ScriptManager>

                <asp:Panel runat="server" ID="pnlDatos">
                    <div class="row div-Frm-Views-Insert" style="overflow: visible; margin-top: 10px; border-radius: 4px; width: auto; height: auto;">
                        <div class="col-md-6 col-xs-12" id="test">
                            <div class="form-group row"></div>
                            <div class="noprint">
                                <button id="InsertOptionBtn" type="button" class="btn btn-primary" onclick="InsertOptionClick();" ng-click="insertMode = true">Archivos</button>
                                <button id="BarcodeOptionBtn" type="button" class="btn btn-default" onclick="BarcodeOptionClick();" ng-click="insertMode = false">Caratulas</button>
                                <a id="BtnClearIndex" class="btn btn-sm btn-outline-primary ng-scope" onclick="clearFilter()">
                                    <span class="glyphicon glyphicon-trash"></span><span class="hidden-xs" style="padding-left: 5px;">Limpiar</span>
                                </a>
                            </div>
                            <div class="form-group row"></div>

                            <div id="BarcodePanel" ng-show="insertMode == false">
                                <div class="form-group row">
                                    <div class="col-xs-6">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="input-group">
                                                    <!--Nro Guia-->
                                                    <span class="input-group-addon" style="font-weight: bold !important; border: none; background-color: transparent; box-shadow: none">Caratula:</span>
                                                    <input type="text" id="nroBC" ng-model="barcodeId" size="1" class="form-control input-sm" disabled style="font-weight: 600 !important; background-color: white !important; border: none; background-color: transparent; box-shadow: none" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-11 col-xs-offset-1">
                                                <span runat="server" id="currentUserName"></span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-11 col-xs-offset-1">
                                                <span id="currentTime"></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 pull-right">
                                        <input id="nroBC39" ng-model="barcodeId" class="form-control input-sm dataType length" style="font-family: 'IDAutomationHC39M'; font-size: 15pt; text-align: center; height: 75px; background: none; border: none; box-shadow: none;"
                                            readonly="readonly">
                                    </div>
                                </div>

                            </div>


                            <div>
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <uc2:ucDocTypes runat="server" ID="ucDocTypes" EnableViewState="false" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="padding-top: 10px; overflow-x: auto; position: relative; left: 5px;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <uc3:ucDocTypesIndexs runat="server" ID="ucDocTypesIndexs" EnableViewState="false" />

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                        </div>
                        <div class="col-md-6 col-xs-12 ">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="DropPanel">
                                <ContentTemplate>

                                    <div class="form-group row"></div>
                                    <div style="padding-top: 10px;">
                                        <uc1:ucUploadFile runat="server" ID="ucUploadFile" EnableViewState="false" />
                                        <asp:Label ID="lblMsj" runat="server" Visible="false" CssClass="error" Style="padding: 10px 0px 10px 0px"></asp:Label>
                                    </div>



                                </ContentTemplate>

                            </asp:UpdatePanel>

                            <div class="form-group row"></div>
                            <div class="form-group row"></div>

                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="NavPanel">
                                <ContentTemplate>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="nav nav-bar noprint">

                                <asp:LinkButton runat="server" Text="Insertar" ID="lnkInsertar" OnClientClick="SetInserting('Insertando archivos...');" OnClick="lnkInsertar_clic" ng-show="insertMode == true" 
                                    CssClass="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> " />

                                <asp:LinkButton runat="server" Text="Replicar" ID="lnkReplicar" OnClick="lnkReplicar_clic" ng-show="insertMode == true " disable
                                    CssClass="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> " />

                                <button type="button" id="lnkGenerarCaratula"  data-ng-click="generateBC();" data-ng-show="insertMode == false"
                                    class="btn btn-success btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> ">
                                    Generar Caratula</button>

                                <button type="button" id="lnkReimprimirCaratula" data-ng-click="PrintBC();"  data-ng-show="insertMode == false && barcodeId > 0"
                                    class="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> ">
                                    Reimprimir Caratula</button>

                                <button type="button" id="lnkReplicarCaratula" data-ng-click="ReplicarBC();" data-ng-show="insertMode == false && barcodeId > 0"
                                    class="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Procesando </div> ">
                                    Replicar Caratula</button>

                                <asp:LinkButton ID="lnkRefresh" CssClass="btn btn-success btn-sm" OnClick="lnkRefresh_click" Text="Refrescar" runat="server">Cancelar</asp:LinkButton>


<%--                                <button type="button" id="lnkhistory" data-ng-click="showHistory()" data-ng-show="insertMode == false" data-ng-controller="historycontroller"
                                    class="btn btn-primary btn-sm">
                                    <i class="fa fa-clock"></i>
                                </button>--%>



                                <asp:LinkButton runat="server" Text="Cancelar" ID="lnkCancelar" OnClientClick="CloseInsert();" Visible="false"
                                    CssClass="btn btn-primary btn-sm" OnClick="lnkCancel_clic" />
                            </div>
                            <div class="row">
                                <span id="insertInfo" class="insertInfo"></span>
                                <asp:Label ID="lblInsertado" runat="server" Visible="false" Style="top: 20px; left: 120px; position: relative;"
                                    Font-Size="Medium" Font-Bold="true" Text="El documento ha sido insertado con exito"></asp:Label>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
            </form>
        </div>


    </div>
    <style>
        .loader {
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            width: 30px;
            height: 30px;
            -webkit-animation: spin 2s linear infinite; /* Safari */
            animation: spin 2s linear infinite;
        }

        /* Safari */
        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        #pnlDatos {
            margin-top: 48px;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#nroBC39").val("(" + $("#nroBC").val() + ")");
            display_ct();
            var scope = angular.element($("#InsertIframeBody")).scope();
            if ($("#insertMode").val() == "BC") {
                scope.insertMode = false;
            }
            else {
                scope.insertMode = true;
            }
        });

        function InsertOptionClick() {
            $("#BarcodeOptionBtn").removeClass('btn-primary');
            $("#BarcodeOptionBtn").addClass('btn-default');
            $("#InsertOptionBtn").removeClass('btn-default');
            $("#InsertOptionBtn").addClass('btn-primary');
            $("#insertMode").val("insert");
            $("#DropPanel").show();
        };
        function BarcodeOptionClick() {
            $("#InsertOptionBtn").removeClass('btn-primary');
            $("#InsertOptionBtn").addClass('btn-default');
            $("#BarcodeOptionBtn").removeClass('btn-default');
            $("#BarcodeOptionBtn").addClass('btn-primary');
            $("#insertMode").val("BC");
            $("#DropPanel").hide();
        };


        function display_c() {
            var refresh = 1000; // Refresh rate in milli seconds
            mytime = setTimeout('display_ct()', refresh)
        };

        function display_ct() {
            var x = new Date()
            document.getElementById('currentTime').innerHTML = x.toLocaleString();
            display_c();
        };


        function generateBC() {

        };

        function EnableInsert() {
            var scope = angular.element($("#InsertIframeBody")).scope();
                scope.FileAdded = true; 
        };

        function SetInserting(text) {
            $("#insertInfo").innerHTML = text;
        };

        function SetInsertOK() {
            $("#insertInfo").innerHTML = "Archivos Insertados.";

        };
        function clearFilter() {

            var countIndex = $(".insertAttribute").length;
            if (countIndex > 0) {
                for (var i = 0; i < countIndex; i++) {
                    if ($($(".insertAttribute")[i].nodeName).selector == "SELECT") {
                        $($(".insertAttribute")[i]).val("Seleccionar");
                    }
                    if ($($(".insertAttribute")[i].nodeName).selector == "INPUT" || $($(".insertAttribute")[i].nodeName).selector == "TEXTAREA" ) {
                        $($(".insertAttribute")[i]).val("");
                    }
                }

            }

        }
    </script>


</body>
</html>
<script type="text/javascript">
    function OpenTask(url, taskid, taskname) {
        window.opener.OpenTask(url, taskid, taskname);
        closeSelf();
    }

    function InsertSucces() {



    }

    function closeSelf() {
        window.close();
    }
    function maxlength(tb, max) {
        return (tb.value.length < max);
    }

    function FixAndPosition(objDlg) {
        objDlg.css("top", "100px");
        objDlg.css("position", "absolute");
        var zIndexMax = getMaxZ();
        $(objDlg).css({ "z-index": Math.round(zIndexMax) });
    }

      <%--      function OpentInsertingDialog() {
                $("#<%=pnlDatos.ClientID%>").hide();
                $("#divInserting").show();
                window.setInterval(function () { $("#btnForceClose").show(); }, 10000);

                if (parent != this)
                    parent.ShowLoadingAnimation();
            }

            function CloseInsertingDialog() {
                t = setTimeout("hideLoading();", 500);
            }--%>

         <%--   function hideLoading() {
                $("#divInserting").hide();
                $("#<%=pnlDatos.ClientID%>").show();
            $("#btnForceClose").hide();--%>

            //if (parent != this)
            //    parent.hideLoading();
            //}



            //function ModifyHeight() {
            //    var height = getHeightScreen();

        <%-- Height es 0 cuando viene del thick box, por ende le mando el alto justo y lo calculo por el 60% --%>
            //if (height < 1) {
            //    height = 572;
            //    height = height * 0.6;
            //} else {

            //    height = height * 0.5;
            //}

            //$("#divAttributes").height(height);
            //}
    <%-- Se coloco para esconder los datapicker ya que se superponian --%>
    $(document).ready(function () {
        setTimeout(function () {
            $("input").click(function () {
                $("#ui-datepicker-div").remove();
            })


        }, 100);
    });

    $('.btn').on('click', function () {
        var $this = $(this);
        $this.button('loading');
        setTimeout(function () {
            $this.button('reset');
        }, 3000);

    });
</script>


