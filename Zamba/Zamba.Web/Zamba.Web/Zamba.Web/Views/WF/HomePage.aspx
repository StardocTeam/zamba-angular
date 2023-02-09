<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Zamba.Web.Views.UC.Home.HomePage" Async="true" EnableEventValidation="false" %>

<!DOCTYPE html>

<%@ Register Src="~/Views/UC/WF/UCWFExecution.ascx" TagName="UCWFExecution" TagPrefix="UC6" %>


<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <link href="../../Content/Styles/normalize.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../Content/Site.css" />
    <link rel="stylesheet" href="../../Content/bootstrap.min.css" />

    <script src="../../Scripts/jquery-3.1.1.min.js"></script>
    <script src="../../Scripts/jquery-ui.min.js"></script>

    <script src="../../Scripts/angular.min.js"></script>
    <script src="../../Scripts/angular-route.min.js"></script>
    <script src="../../Scripts/angular-cookies.min.js"></script>
    <script src="../../Scripts/angular-sanitize.min.js"></script>
    <script src="../../Scripts/angular-animate.min.js"></script>
    <script src="../../Scripts/angular-filter/angular-filter.min.js"></script>

    <script src="../../Scripts/angular-messages.js"></script>
    <script src="../../Scripts/angular-xeditable-0.8.1/js/xeditable.js"></script>


    <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/ui-bootstrap-tpls-0.12.0.min.js"></script>
    <script src="../../Scripts/bootstrap-waitingfor.js"></script>
    <script src="../../Scripts/modernizr-custom.js"></script>
    <script src="../../Scripts/bootbox.js"></script>
    <script src="../../Scripts/toastr.js"></script>
    <script src="../../Scripts/sweetalert.min.js"></script>

    <script src="../../Scripts/ui-bootstrap-tpls-0.12.0.min.js"></script>

    <script src="../../Scripts/moment.min.js"></script>
    <script src="../../Scripts/underscore.min.js"></script>
    <script src="../../Scripts/lodash.min.js"></script>
    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>
    <script src="../../GlobalSearch/services/authInterceptorService.js"></script>
    <script src="../../Scripts/typeahead.bundle.js"></script>
    <script src="../../Scripts/handlebars-v2.0.0.js"></script>
    <script src="../../Scripts/Token.js"></script>


    <script src="../../Scripts/Zamba.js?v=258"></script>
    <script src="../../Scripts/Zamba.Fn.js?v=235"></script>

    <script src="../../app/zapp.js?v=235"></script>
    <script src="../../app/i18n/i18n.js?v=235"></script>
    <script src="../../Scripts/bower-angular-translate-2.9.0.1/angular-translate.js"></script>
    <!--locale translate-->


    <script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>

    <script src="../../scripts/kendoui/js/kendo.all.min.js"></script>
    <script src="../../Scripts/app/Grids/KendoGrid.js?v=256"></script>


    <script src="../../app/Tasks/Controller/TaskController.js?v=235"></script>
    <script src="../../app/Tasks/Service/TaskService.js?v=235"></script>
    <script src="../../scripts/zamba.associated.js?v=248"></script>

    <%--<script src="../../Scripts/sweetalert2/sweetalert2.all.js"></script>
    <script src="../../Scripts/sweetalert2/sweetalert2.js"></script>
    <script src="../../Scripts/sweetalert2/sweetalert2.all.min.js"></script>--%>

    <title></title>
</head>

<body style="" data-ng-app="app">

    <form id="form" runat="server">


        <style>
            .ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable.ui-resizable {
                position: absolute !important;
                top: 23px !important;
                z-index: 6666 !important;
                background-color: white !important;
            }
        </style>

        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnPushNotification_player_id" runat="server" />
        <asp:HiddenField ID="hdnPushNotification_app_id" runat="server" />
        <asp:HiddenField ID="hdnConnectionId" runat="server" />

        <asp:HiddenField runat="server" ID="hidLastTab" />
        <asp:HiddenField runat="server" ID="HiddenTaskId" />
        <asp:HiddenField runat="server" ID="HiddenDocID" />
        <asp:HiddenField runat="server" ID="HiddenCurrentFormID" />

        <asp:LinkButton runat="server" ID="hdnsender" OnClick="hdnsender_Click" Text="TEST" Style="display: none" />

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>


        <UC6:UCWFExecution ID="UC_WFExecution" runat="server" height="200" width="200" />
        <h6 style="text-align: center; margin-top: 50px;" id="emptyMessage" runat="server"><b>No tenemos registros por ahora.</b>
            <br />
            <br />
            Aca encontraras las acciones y reportes definidos para tu perfil.</h6>


        <div id="pnl" class="container-fluid" runat="server">
        </div>

        <div id="MainHome">
        </div>

        <!--Iframe Modal para la Ejecucion de Reglas. Dentro Carga el y que tiene el Switch para Executar cada Regla.-->
        <%--  <div class="modal fade" id="openModalIFWF" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="overflow-y: hidden;">
            <div class="modal-dialog">
                <div class="modal-content" id="openModalIFContentWF" style="padding: 0">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" id="closeModalIFWF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar1</span></button>
                        <h4 class="modal-title" id="modalFormTitleWF"></h4>
                    </div>
                    <div class="modal-body" id="modalFormHomeWF" style="padding: 0; height: 590px">
                        <div id="EntryRulesContent" style="z-index: 997; top: 0; right: 120px;height:100%;width:100%">
                            <iframe id="WFExecForEntryRulesFrame" src="../../WF/WFExecutionForEntryRules.aspx" style="height: 100%;width:100%"  runat="server"></iframe>
                            <iframe id="printFrame" src="" style="display: none;"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div id="openModalIF" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="position: -ms-page; margin-top: 0px;width: 100%; height: 98%;">
            <div class="modal-dialog" style="width: 100%; height: 97%;">
                <div class="modal-content" id="openModalIFContent" style="width: 100%; height: 96%;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" id="closeModalIF"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                        <%--<button type="button" class="close" onclick="OpenModalIF.fullscreen(this);"><span aria-hidden="true">&#9633;</span></button>--%>
                        <h5 class="modal-title" id="modalFormTitle"></h5>
                    </div>
                    <div class="modal-body" id="modalFormHome">
                        <div id="modalDivBody" style="width: 100%; height: 98%;"></div>
                        <iframe id="modalIframe" runat="server" style="width: 98%; height: 92%;" frameborder="0" allowtransparency="true"></iframe>
                    </div>
                </div>
            </div>
        </div>


        <script type="text/javascript">
            function getValueFromWebConfig(key) {
                var pathName = null;
                $.ajax({
                    "async": false,
                    "crossDomain": true,
                    "url": "../../Services/ViewsService.asmx/getValueFromWebConfig?key=" + key,
                    "method": "GET",
                    "headers": {
                        "cache-control": "no-cache"
                    },
                    "success": function (response) {
                        if (response.childNodes[0].innerHTML == undefined) {
                            pathName = response.childNodes[0].textContent;
                        } else {
                            pathName = response.childNodes[0].innerHTML;
                        }

                    },
                    "error": function (data, status, headers, config) {
                        console.log(data);
                    }
                });
                return pathName;
            }



        </script>

        <script type="text/javascript">

            var thisDomain = window.location.protocol + "//" + window.location.host + "/" + window.location.pathname.split('/')[1];
            var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
            var zambaApplication = "Zamba";
            var URLServer = thisDomain + "/ZambaChat/";
            var urlGlobalSearch = thisDomain + "/Views/Search/";
            var URLServer = thisDomain + "/ZambaChat/";
            var ZCollLnk ='<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("ZCollLnk","https://localhost/zamba.web/zamba.collaboration/") %>';
            var zCollServer = '<%=Zamba.Core.ZOptBusiness.GetValueOrDefault("zCollServer","http://localhost/zamba.web/zambacollaborationserver/") %>';

        </script>


    </form>


</body>

</html>
