<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Viewers_DocToolbar" CodeBehind="DocToolbar.ascx.cs" %>


<link rel="stylesheet" type="text/css" href="../../Content/font-awesome.min.css" />
<link rel="stylesheet" type="text/css" href="../../Content/themes/base/jquery-ui.css" />
<link rel="stylesheet" type="text/css" href="../../Content/bootstrap-theme.css" />
<link rel="stylesheet" type="text/css" href="../../Content/HomeWidget.css" />
<link rel="stylesheet" type="text/css" href="../../Content/toastr.css" />
<link rel="stylesheet" type="text/css" href="../../Scripts/ng-embed/ng-embed.min.css" />
<link rel="stylesheet" type="text/css" href="../../GlobalSearch/search/searchbox.css" />
<link rel="stylesheet" type="text/css" href="../../Content/bootstrap.min.css" />



<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.common.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.rtl.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.silver.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.mobile.all.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.flat.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.default.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.default.mobile.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.dataviz.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.dataviz.default.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.common-material.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.material.min.css" />
<link rel="stylesheet" href="../../scripts/kendoui/styles/kendo.material.mobile.min.css" />

<link rel="stylesheet" type="text/css" href="../../Content/styles/normalize.css" />

<script>


    zambaApplication = "ZambaSearch";
</script>

<%--<script src="../../Scripts/jquery-2.2.2.min.js"></script>--%>
<script src="../../Scripts/jquery-ui.min.js"></script>
<script src="../../Scripts/angular.min.js"></script>
<script src="../../Scripts/angular-route.min.js"></script>
<script src="../../Scripts/angular-cookies.min.js"></script>
<script src="../../Scripts/angular-sanitize.min.js"></script>
<script src="../../Scripts/angular-animate.min.js"></script>
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
<script src="../../Scripts/handlebars-v2.0.0.js"></script>
<%--<script src="../../../Scripts/Zamba.js"></script>--%>
<%--<script src="../../GlobalSearch/search/searchbox-loader.js"></script>--%>
<%--<script src="../../../Scripts/app/view/Zamba.Chat.js"></script>--%>
<script src="../../Scripts/ckeditor/ckeditor.js"></script>

<asp:HiddenField ID="hdnShowHistoryTab" runat="server" />
<asp:HiddenField ID="hdnShowForumTab" runat="server" />
<asp:HiddenField ID="hdnShowAsociatedTab" runat="server" />
<asp:HiddenField ID="hdnShowMailsTab" runat="server" />
<asp:HiddenField ID="hdnLastTab" runat="server" />
<asp:HiddenField ID="hdnCurrentFormID" runat="server" />
<asp:HiddenField ID="hdnDocId" runat="server" />
<asp:HiddenField ID="hdnFilePath" runat="server" />
<asp:HiddenField ID="hdnDocTypeId" runat="server" />
<asp:HiddenField ID="hdnWfstepid" runat="server" />
<asp:HiddenField ID="hdnDocExt" runat="server" />
<asp:HiddenField ID="hdnImprimir" runat="server" />
<asp:HiddenField ID="hdnDocContainer" runat="server" />

<style>
    .ToolbarButtons {
        height: 26px !important;
        margin: 0 5px 0 5px;
    }

    .fixed-top-2 {
        margin-top: 36px;
    }

    .btn-group-xs > .btn, .btn-xs {
        font-size: 15px !important;
    }

    .ClassBtnCollapse {
        background-color: #337ab7 !important;
        border-color: #ccc !important;
    }

    .classglyphicon {
        color: #fafafa !important;
    }

    /*DropZone*/

    .upload-container {
        position: relative;
    }

        .upload-container input {
            border: 1px solid #92b0b3;
            background: #f1f1f1;
            outline: 2px dashed #92b0b3;
            outline-offset: -10px;
            padding: 100px 0px 100px 250px;
            text-align: center !important;
            width: 500px;
        }

            .upload-container input:hover {
                background: #ddd;
            }

        .upload-container:before {
            position: absolute;
            bottom: 50px;
            left: 245px;
            color: #3f8188;
            font-weight: 900;
        }

    .upload-btn {
        margin-left: 300px;
        padding: 7px 20px;
    }

    #file_upload {
        width: 96%;
        padding: 50px 25%;
        margin-left: 15px;
        border-color: rgb(207, 213, 255);
    }

    .modalControl {
        margin-bottom: 5px !important;
    }

    .rowFooter {
        margin-bottom: 0px !important;
    }

    .addLinkMarginBottom {
        margin-bottom: 0px !important;
    }

    .dropZoneMarginTop {
        margin-top: 5px !important;
        margin-bottom: 0px !important;
    }
</style>


<div id="Toolbar" class="btn-toolbar navbar navbar-toggleable-xs navbar-default navbar-fixed-top fixed-top-2 " role="toolbar" style="min-height: 35px; height: 35px">

    <%-- <button class="navbar-toggler" type="button" data-toogle="collapse" data-target="#navbarToolBar" aria-controls="navbarToolBar" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>--%>

    <div class="collapse navbar-collapse" id="navbarSupportedContent" style="background-color: white; margin-top: 5px">
        <ul class="navbar-nav mr-auto" style="list-style: none;">

            <li class="nav-item">
                <button type="submit" id="BtnClose" runat="server" class="btn btn-primary btn-xs BtnClose" style="margin-left: 8px; display: none" tooltip="Cerrar"
                    onclick="CloseCurrentTask()" tabindex="-1">
                    Cerrar             
                </button>
            </li>
            <li class="nav-item ">
                <button id="BtnShowIndexs" title="Mostrar datos" type="button" runat="server" class="btn btn-primary btn-xs ToolbarButtons BtnShowIndexsClass" onclick="OcultarScrolIE()" href="#menu-toggle">
                    <span class="glyphicon glyphicon-indent-left ToolbarText" style="margin-right: 5px;"></span>Datos
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnRefresh" title="Refrescar" type="button" runat="server" class="btn btn-primary btn-xs ToolbarButtons" onclick="Refresh_C();">
                    <span class="glyphicon glyphicon-refresh ToolbarText" style="margin-right: 5px;"></span>Actualizar
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnEmail" title="Enviar mail" type="button" runat="server" class="btn btn-primary btn-xs ToolbarButtons" onclick="Email_Click()">
                    <span class="glyphicon glyphicon-envelope ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button style="display: none" id="btnAttach" title="Adjuntar" type="button" runat="server" class="btn btn-sucess btn-xs ToolbarButtons" onclick="IncorporarDoc_Click()">
                    <span class="glyphicon glyphicon-paperclip ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnOpenNewTab" style="display: none;" title="Abrir en nueva pestaña" type="button" runat="server" class="btn btn-sucess ToolbarButtons btn-xs btn-openNewTab" onclick="View_Doc()">
                    <span class="glyphicon glyphicon-new-window ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnPrint" style="display: none;" title="Imprimir" type="button" runat="server" class="ToolbarButtons btn btn-primary btn-xs" onclick="Imprimir_Click()">
                    <span class="glyphicon glyphicon-print ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnFav" style="display: none" type="button" runat="server" class="ToolbarButtons btn btn-primary btn-xs" onclick="TaskOptions.SetFav(this);">
                    <span class="glyphicon glyphicon-heart ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnImportant" style="display: none" type="button" runat="server" class="ToolbarButtons btn btn-primary btn-xs" onclick="TaskOptions.SetImportant(this);">
                    <span class="glyphicon glyphicon-star ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button id="btnAddNews" title="Agregar novedad" style="display: none;" type="button" runat="server" class="ToolbarButtons btn btn-sucess btn-xs" onclick="TaskOptions.AddNews(this);">
                    <span class="glyphicon glyphicon-plus ToolbarText"></span>
                </button>
            </li>

            <li class="nav-item ">
                <button id="btndoc" title="Descargar documento" type="button" class="ToolbarButtons btn btn-info btn-xs" onclick="DownloadFile()">
                    <span class="glyphicon glyphicon-download-alt ToolbarText" style="margin-right: 5px;"></span>Descargar
                </button>
            </li>

            <li class="nav-item ">
                <button id="liForum" type="button" class="ToolbarButtons btn btn-info btn-xs ToolbarText disabled" style="display: none">Foro</button>
                <!--onclick="ShowForum()"-->
            </li>

            <li class="nav-item ">
                <button id="liChat" type="button" class="ToolbarButtons btn btn-info btn-xs ToolbarText" onclick="ShowTaskChat()" style="display: none">Chat</button>
            </li>

            <li class="nav-item ">
                <button id="liAsociated" type="button" class="ToolbarButtons btn btn-info btn-xs ToolbarText" onclick="ShowAsociated()" style="display: none">Asociados</button>
            </li>

            <li class="nav-item ">
                <button id="liMails" type="button" class="ToolbarButtons btn btn-info btn-xs ToolbarText" onclick="ShowMailHistory()" style="display: none">Mails enviados</button>
            </li>

            <li class="nav-item ">
                <button id="liHistory" type="button" class="ToolbarButtons btn btn-info btn-xs ToolbarText" onclick="ShowDocHistory()" style="display: none">Historial</button>
            </li>

            <li class="nav-item ">
                <div id="btnCollapse" class="btn btn-default btn-xs ClassBtnCollapse" style="display: none" onclick="OcultarIframe(); ">
                    <div class="glyphicon glyphicon-chevron-up ToolbarText classglyphicon" style="height: 16px"></div>
                </div>
            </li>
            <li class="nav-item ">
                <%--                <div id="DivContainer" class="col-md-12 col-md-offset-12 col-sm-offset-7 col-sm-12 ">--%>
                <div id="lbltitulodocumento" runat="server" style="font-weight: bold; padding-left: 10px"></div>
                <%--                </div>--%>
            </li>
        </ul>

    </div>
</div>

<div id="DivColapseContent">
    <iframe id="tabContent" style="margin-top: 70px; height: 1400px; width: 100%; max-height: 500px; display: none"></iframe>
</div>

<div id="ModalMail" class="modal fade" style="position: -ms-page; margin-top: 40px; margin-left: -185px; padding-right: 200px;" role="dialog" data-backdrop="false">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content" style="width: 800px;">
            <form name="formMail">
                <div class="modal-body">
                    <div class="form-group modalControl row">
                        <label class="col-sm-1 control-label">Para</label>
                        <div class="col-sm-11">
                            <input class="form-control EmailInput" name="for" placeholder="Para">
                        </div>
                    </div>

                    <div class="form-group modalControl row">
                        <label class="col-sm-1 control-label">CC</label>
                        <div class="col-sm-11">
                            <input class="form-control EmailInput" name="cc" placeholder="Cc">
                        </div>
                    </div>

                    <div class="form-group modalControl row">
                        <label class="col-sm-1 control-label">CCO</label>
                        <div class="col-sm-11">
                            <input class="form-control EmailInput" name="cco" placeholder="Cco">
                        </div>
                    </div>

                    <div class="form-group modalControl row">
                        <label class="col-sm-1 control-label">Asunto</label>
                        <div class="col-sm-11">
                            <input class="form-control EmailInput" name="subject" placeholder="Asunto">
                        </div>
                    </div>

                    <div class="form-group">
                        <textarea name="messageBody" class="form-body EmailInput" style="width: 100%; height: 100px; resize: none; border: 1px solid #ccc !important; border-radius: 3px;" id="editor" rows="10" cols="80"></textarea>
                    </div>

                    <div class="form-group row addLinkMarginBottom">
                        <div class="col-sm-9">
                            <input type="checkbox" name="addListLinks"><label id="LabelAddListLinks">Agregar link a documento </label>
                        </div>

                        <div class="col-sm-3" align="right">
                            <span class="loadersmall" style="display: none"></span>
                            <input id="btnMailZipSubmit" class="btn btn btn-default" type="button" onclick="SendEmail()" value="Enviar" style="background-color: rgb(66, 189, 62); color: white;">
                            <button id="btnMailZipMailClose" type="button" class="btn btn-default cancelMailZipButton" onclick="removeAttrFromFor();" data-dismiss="modal" style="background-color: #337ab7; color: white">Cerrar</button>
                        </div>
                    </div>

                    <div class="form-group row dropZoneMarginTop">
                        <div id="DropZone" class="upload-container">
                            <input type="file" id="file_upload" multiple />
                        </div>
                    </div>

                </div>
            </form>
        </div>
    </div>
</div>

<div class="modal" data-backdrop="static" data-keyboard="false" id="ModalSearchDropzone" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content" id="ModalSearchContent1" style="position: relative !important; margin-top: 50px !important">

            <div class="modal-header">
                <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>--%>
                <%--                <h4 class="modal-title" id="modalFormTitle">Selección de {{Search.selectedIndex.Name}}</h4>--%>
            </div>
            <div class="input-group">
                <input id="filtrar" type="text" class="form-control SearchStyle" placeholder=" Buscar..." style="margin-top: 10px; margin-left: 10px; width: 572px;">
            </div>
            <div class="modal-body" id="modalFormdropzone">
                <ul id="listaID" class="buscar">
                </ul>

            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    const fileInput = document.querySelector('#file_upload');
    var CollectionFiles = [];

    fileInput.addEventListener('change', (e) => {
        let files = e.target.files;
        let reader = new FileReader();
        let file;

        for (let i = 0; i < files.length; i++) {
            (function (file) {
                var reader = new FileReader();
                reader.onload = (file) => {
                    CollectionFiles.push({ "FileName": files[i].name, "Base64": reader.result.replace('data:', '').replace(/^.+,/, '').toString() });
                }
                reader.readAsDataURL(file)
            })(files[i]);
        }

    });


    function OcultarIframe() {

        $('#DivColapseContent').css("display", "none");
        $('#btnCollapse').css("display", "none");
    }

    function MostrarIframe() {

        $('#DivColapseContent').css("display", "block");
    }

    $(document).ready(function () {
        CKEDITOR.replace('editor');
        jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();

        var IndexPanelVisible = PermitsForIndexPanel();
        if (!IndexPanelVisible) {
            $('.BtnShowIndexsClass').css("display", "none");
        }

        $(".BtnShowIndexsClass").click(function (e) {
            e.preventDefault();
            $("#wrapper").toggleClass("toggled");
        });

        TitleTarea();
        var isIE11 = !!navigator.userAgent.match(/Trident.*rv\:11\./);
        if (isIE11) {
            $("#ctl00_ContentPlaceHolder_ctl01_docTB_btnPrint").css("display", "none");
        };

        // $("button").tooltip();
        var showForumTab = $("#<%=hdnShowForumTab.ClientID %>").val().toLowerCase();
        var showHistoryTab = $("#<%=hdnShowHistoryTab.ClientID %>").val().toLowerCase();
        var showAsociatedTab = $("#<%=hdnShowAsociatedTab.ClientID %>").val().toLowerCase();
        var showMailsTab = $("#<%=hdnShowMailsTab.ClientID %>").val().toLowerCase();

        if (showHistoryTab != "true") {
            $("#liHistory").hide();
            $("#divHistory").hide();
        }
        else {
            $("#liHistory").show();
            $("#divHistory").show();
        }

        if (showForumTab != "true") {
            $("#divForum").hide();
            $("#liForum").hide();
        }
        else {
            $("#liForum").show();
        }

        if (showAsociatedTab != "true") {
            $("#divAsociated").hide();
            $("#liAsociated").hide();
        }
        else {
            $("#liAsociated").show();
        }
        if (showMailsTab != "true") {
            $("#divMails").hide();
            $("#liMails").hide();
        }
        else {
            $("#liMails").show();
        }

        var path = document.getElementById('<%=hdnFilePath.ClientID %>').value;
        if (path.length == 0) {
            $("#divOpenNewTab").hide();
        }

        var url = window.location.href;

        if (url.indexOf("TaskViewer") >= 0)
            $("#liCerrar").css("display", "block");

        //if (url.indexOf("TaskViewer") >= 0)
        //    $("#liIniciar").css("display", "block");

        if (url.indexOf("DocViewer") >= 0)
            $("#Toolbar").find(".BtnClose").css("display", "block");


        (function ($) {

            $('#filtrar').keyup(function () {

                var rex = new RegExp($(this).val(), 'i');
                $('.buscar li').hide();
                $('.buscar li').filter(function () {
                    return rex.test($(this).text());
                }).show();

            })

        }(jQuery));


    });

    function TitleTarea() {

        var a = $('#DivContainer').children(0).text();
        var b = $('#DivContainer').children(0).text();
        var newString = a.substr(0, 30);
        $('#DivContainer').children(0).text(newString);
        // $('#DivContainer')[0].setAttribute("title", b);
    }

    function Collapse(collapse) {
        if (collapse) {
            $("#tabContent").attr('src', '');
            ShowAsociated();
            $("#btnCollapse").hide();
            $("#tabContent").hide('fast');
        } else {
            $("#btnCollapse").show();
            $("#tabContent").show('fast');
            ShowLoadingAnimation();
        }
    }

    /*function SetTabContentHeight() {
        $("#tabContent").css("height", $("#rowTaskDetail").outerHeight(true) - $("#Toolbar").outerHeight(true) - 10);
    }*/

    function ShowForum() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskForum.aspx?ResultID=<%=DocId %>&DocTypeId=<%=DocTypeId%>&currentUserID=<%=CurrentUserID%>');
        MostrarIframe();
        SetTabContentHeight();
       <%-- var scope = angular.element(document.getElementById("ForumPanel")).scope();
        scope.GetMessages(<%=DocId%>);--%>
    }

    function ShowAsociated() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskAsociated.aspx?ResultID=<%=DocId %>&DocTypeId=<%=DocTypeId%>');
        MostrarIframe();
        SetTabContentHeight();
        //.load(function () {
        //    parent.loadCliksInRows();
        //});
    }

    function ShowDocHistory() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskHistory.aspx?ResultID=<%=DocId %>');
        MostrarIframe();
        SetTabContentHeight();
    }

    function ShowMailHistory() {
        Collapse(false);
        $("#tabContent").attr('src', '../WF/TaskDetails/TaskMailhistory.aspx?ResultID=<%=DocId %>');
        MostrarIframe();
        SetTabContentHeight();

    }




    function Imprimir_Click() {

        var pending = true;
        var office = document.getElementById('<%=hdnImprimir.ClientID %>').value;

        try {


            if (window.frames['formBrowser'] != null) {
                window.frames['formBrowser'].focus();
                window.frames['formBrowser'].print();
                window.frames['formBrowser'].close();
                pending = false;

            } else if ($('#divViewer').length) {
                PrintDiv($('#divViewer').html());
                pending = false;
            }
        }
        catch (e) {
            //  alert(e.description);
        }

        if (pending) {
            if (print == "true") {
                try {
                    var destinationURL = "../Tools/ToolPrint.aspx?" + localStorage.queryStringAuthorization + "&docid=<%=DocId %>&doctypeid=<%=DocTypeId%>";
                    var newwindow = window.open(destinationURL, '_blank', 'width=620,height=580,left=' + (screen.width - 600) / 2 + ',top=' + (screen.height - 580) / 2 + ',directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=no,toolbar=no');
                }
                catch (e) {
                    //alert(e.description);
                }
            }
            else {
                $("#<%=btnPrint.ClientID %>").hide();
                $("#docToolbarUl").append("<li id='legend' style='display:none;'>La Impresión del documento se realiza externamente (en caso de no visualizarse la barra de impresion, guarde el documento o utilice click secundario->Imprimir)”</li>");
                $("#legend").show("3000");
            }
        }
    }

    function PrintDiv(divHtml) {
        var mywindow = window.open('', '_blank', 'height=400,width=600');
        var html = '<html><head><title>impresion</title></head><body>' + divHtml + '</body></html>';
        mywindow.document.write(html);
        mywindow.document.close();
        mywindow.print();
        mywindow.close();

        return true;
    }

    function CheckIsIE() {
        if (navigator.appName.toUpperCase() == 'MICROSOFT INTERNET EXPLORER') {
            return true;
        }
        else {
            return false;
        }
    }


    function removeAttrFromFor() {
        $('#ModalMail').find("input[name = 'for']").removeAttr('required');
        $('#file_upload').val("");
    }

    var LocalNextRuleIds;
    var LocalMailPathVariable;
    function Email_Click(Subject, Body, To, AttachLink, SendDocument, NextRuleIds, MailPathVariable, CC, CCO) {
        document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").hidden = false;
        document.querySelector("#ModalMail").querySelector("#LabelAddListLinks").hidden = false;

        var mailContainer = $("#ModalMail");

        mailContainer.modal();
        mailContainer.find("input[name = 'for']").attr("required", "true");

        mailContainer.find('input[name="for"]').val(To);
        mailContainer.find('input[name="cc"]').val(CC);
        mailContainer.find('input[name="cco"]').val(CCO);
        mailContainer.find('input[name="subject"]').val(Subject);

        var Link = AttachLink != undefined ? (AttachLink.toLowerCase() === 'true') : false;
        var FlagSendDocument = SendDocument != undefined ? SendDocument.toLowerCase() : false;

        LocalNextRuleIds = NextRuleIds != undefined ? NextRuleIds : "";
        LocalMailPathVariable = MailPathVariable != undefined ? MailPathVariable : "";

        if (Link && FlagSendDocument) {
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").checked = true;
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").disabled = true;
        } else if (!Link && FlagSendDocument) {
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").hidden = true;
            document.querySelector("#ModalMail").querySelector("#LabelAddListLinks").hidden = true;
        } else if (Link && !FlagSendDocument) {
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").checked = true;
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").disabled = true;
        } else if (!Link && !FlagSendDocument) {
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").checked = false;
            document.querySelector("#ModalMail").querySelector("input[name='addListLinks']").disabled = false;
        }

        mailContainer.find('textarea[name="messageBody"]').val(Body);
    }



    //____________________
    var ValMessage;
    function SendEmail() {
        var docId = document.getElementById('<%=hdnDocId.ClientID %>').value;
        var doctypeId = document.getElementById('<%=hdnDocTypeId.ClientID %>').value;
        var mailContainer = $("#ModalMail");
        var MailValidation = true;
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

        var ObjFor = mailContainer.find('input[name="for"]').val().replaceAll(';', ',');
        var ObjCc = mailContainer.find('input[name="cc"]').val().replaceAll(';', ',');
        var ObjCco = mailContainer.find('input[name="cco"]').val().replaceAll(';', ',');

        if (ObjFor == undefined) {
            ObjFor = "";
        }

        if (ObjCc == undefined) {
            ObjCc = "";
        }

        if (ObjCco == undefined) {
            ObjCco = "";
        }

        ValMessage = "";

        MailValidation = Val_contenido(ObjFor, reg, MailValidation, "Para");
        MailValidation = Val_contenido(ObjCc, reg, MailValidation, "Cc");
        MailValidation = Val_contenido(ObjCco, reg, MailValidation, "Cco");

        if (MailValidation == false) {
            toastr.error("Error: Corrija las advertencias.");
        } else {
            $(".loadersmall").css("display", "block");
            $("#btnMailZipSubmit").hide();
            $("#btnMailZipMailClose").hide();

            if (!doctypeId)
                doctypeId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocTypeId").value;

            if (!docId)
                docId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocId").value;

            var IdInfo = {};
            IdInfo.Docid = parseInt(docId);
            IdInfo.DocTypeid = parseInt(doctypeId);

            var attachsIds = [];
            attachsIds.push(IdInfo);

            var addLinks = mailContainer.find('input[name="addListLinks"]').prop("checked");

            var emaildata = {};
            emaildata.Idinfo = attachsIds;

            emaildata.MailTo = mailContainer.find('input[name="for"]').val();
            emaildata.CC = $('input[name="cc"]').val() == undefined ? "" : $('input[name="cc"]').val();
            emaildata.CCO = $('input[name="cco"]').val() == undefined ? "" : $('input[name="cco"]').val();
            emaildata.Subject = $('input[name="subject"]').val() == undefined ? "" : $('input[name="subject"]').val();
            emaildata.MessageBody = document.getElementById("cke_1_contents").children[0].contentDocument.children[0].childNodes[1].innerHTML;
            
            emaildata.AddLink = addLinks

            emaildata.Base64StringArray = CollectionFiles;

            var NextRuleIds = LocalNextRuleIds;
            emaildata.MailPathVariable = LocalMailPathVariable;

            emaildata.Userid = GetUID();
            emaildata.FromType = false;

            var hasFile = getIFAnyTaskHasFile(emaildata);

            //if (hasFile || addLinks) {
            $.ajax({
                type: "POST",
                url: location.origin.trim() + getRestApiUrl() + "/api/Email/SendEmail",
                data: JSON.stringify(emaildata),
                contentType: "application/json; charset=utf-8",

                success:
                    function (data, status, headers, config) {
                        ModalView(data);

                        CollectionFiles = [];
                        emaildata.MessageBody = "";
                        document.getElementById("cke_1_contents").children[0].contentDocument.children[0].childNodes[1].innerHTML = "";
                        emaildata.Subject = "";
                        $('#file_upload').val("");

                        if (NextRuleIds != undefined && NextRuleIds != null && NextRuleIds != '') {
                            var scope = angular.element($("#taskController")).scope();
                            scope.Execute_ZambaRule(NextRuleIds, GetDOCID());
                        }
                    },
                error:
                    function (data, status, headers, config) {
                        ModalView_Error(data);
                    }
            });
            //} else {
            //    ModalView_NoFile();
            //}
        }
    }

    function Val_contenido(List_Destinatarios, reg, Validado, field) {
        if (List_Destinatarios != undefined) {
            if (List_Destinatarios != "") {
                List_Destinatarios.split(",").forEach(function (elem, index) {
                    if (Validado) {
                        if (elem.trim() != "") {
                            if (reg.test(elem.trim()) == false) {
                                console.log("Iteracion erronea: " + elem.trim());
                                ValMessage += "• " + field + ": Hay caracteres o correo no valido.\n";
                                Validado = false;
                            }
                        } else {
                            ValMessage += "• " + field + ": Ha escrito doble coma o una coma al final.\n";
                            Validado = false;
                        }
                    }
                });
            }
        }
        return Validado;
    }

    //Oculta el modal para enviar un correo asi como tambien resetea las tareas seleccionadas.
    function ModalView(data) {
        console.log(data);
        if (data == false) {
            toastr.error("Error al enviar Email");
            $("#btnMailZipSubmit").show();
            $("#btnMailZipMailClose").show();
            $("#ModalSendZip").modal('toggle');
            $(".loadersmall").css("display", "none")
            $(".EmailInput").val("");
        } else {
            toastr.success("Email enviado con exito");
            $(".loadersmall").css("display", "none");
            $("#btnMailZipSubmit").show();
            $("#btnMailZipMailClose").show();
            $(".EmailInput").val("");
            removeAttrFromFor();

            $("#ModalMail").modal('toggle');
        }
    }

    //Muestra un mensaje de error y deja el modal abierto para reparametrizar.
    function ModalView_Error(error) {
        console.log(error);
        NotifyError(error);
        $(".loadersmall").css("display", "none");
        $("#btnMailZipSubmit").show();
        $("#btnMailZipMailClose").show();
    }

    //Valida y notifica la existencia de un error.
    function NotifyError(error) {
        if (error.data != undefined) {
            if (error.data.InnerException != undefined) {
                toastr.error(error.data.ExceptionMessage + ": " + error.data.InnerException.ExceptionMessage);
            } else {
                toastr.error(error.data.ExceptionMessage);
            }
        }
        else if (error.responseJSON != undefined) {
            if (error.responseJSON.InnerException != undefined) {
                toastr.error(error.responseJSON.ExceptionMessage + ": " + error.responseJSON.InnerException.ExceptionMessage);
            } else {
                toastr.error(error.responseJSON.ExceptionMessage);
            }
        }
        else {
            toastr.error("Error no capturado al enviar mensaje.");
        }
    }

    //Muestra un mensaje de error y deja el modal abierto para reparametrizar.
    function ModalView_NoFile() {
        //TO DO: Eliminar Metodo
        //toastr.warning("Ninguna de las tareas posee archivo asociado.", "No se ha enviado el mail");
        //$(".loadersmall").css("display", "none");
        //$("#btnMailZipMailClose").prop('disabled', false);
        //$("#btnMailZipSubmit").prop("disabled", false);
    }


    //function addAllCompletepath(pathList) {
    //    var url = location.origin.trim() +"/Zamba.Web";
    //    for (var path in pathList) {
    //        pathList[path] = url + pathList[path] + "<br>";
    //    } 
    //} 

   <%-- function getListOfLinks() {
        var docIdList = [];
        var docId = document.getElementById('<%=hdnDocId.ClientID %>').value;
        var allPathList = [];
        docIdList.push(parseInt(docId));
        $.ajax({
            type: "POST",
            url: location.origin.trim() + "/ZambaWeb.RestApi/api/Email/getListOfLinks",
            data: JSON.stringify(docIdList),
            contentType: "application/json; charset=utf-8", 
            async: false,
            success:
            function (data, status, headers, config) {
                allPathList = data;
            }
        });
        return allPathList;
    }--%>



    function getIFAnyTaskHasFile(mailData) {
        var hasFile = false;
        $.ajax({
            "async": false,
            "url": location.origin.trim() + getRestApiUrl() + "/api/Email/getIFAnyTaskHasFile",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(mailData),
            "success":
                function (data, status, headers, config) {
                    hasFile = data;
                },
            "error":
                function (data, status, headers, config) {
                    console.log(data);
                }
        });
        return hasFile;
    }




    //-----------------------------------------------------

    function Refresh_Click() {
        try {
            parent.RefreshCurrentTab();
        }
        catch (e) {
            //  alert(e.description);
        }
    }

    function IncorporarDoc_Click() {
        try {
            var doctypeId = document.getElementById('<%=hdnDocTypeId.ClientID %>');
            var docId = document.getElementById('<%=hdnDocId.ClientID %>');

            if (!doctypeId)
                doctypeId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocTypeId");

            if (!docId)
                docId = document.getElementById("ctl00_ContentPlaceHolder_TabContainer_TabDocumento_ucDocViewer_hdnDocId");

            var destinationURL = "../../Views/Insert/Insert.aspx?docid=" + docId.value + "&doctypeid=" + doctypeId.value + "&isview=true&entitychange=true";
            $('#IFDialogContent').unbind('load');
            parent.ShowInsertAsociated(destinationURL);
        }
        catch (e) {
            console.log(e.message);
            //alert(e.description);
        }
    }


    function DownloadFile() {
        var doctypeId = $("[id$=hdnDocTypeId]").val();
        var docId = $("[id$=hdnDocId]").val();

        var scope = angular.element(document.getElementById("DivDoc")).scope();
        if (scope == null)
            scope = angular.element(document.getElementById("ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_DivDoc")).scope();
        if (scope == null)
            scope = angular.element(document.getElementById("ctl00_ContentPlaceHolder_ctl01_DivDoc")).scope();

        scope.DownloadFile(GetUID(), doctypeId, docId);

<%--            var url = "../../Services/GetDownloadFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&UserID=" + <%=CurrentUserID%> + "&ConvertToPDf=false";


            $.ajax({
                type: "POST",
                url: serviceBase + "/search/GetDocFile",
                data: JSON.stringify(valueParams),
                contentType: "application/json; charset=utf-8",
                crossDomain: true,
                async: false,
                headers: { "Authorization": tokenSearchId },
                success: function (data) {
                    result = data;
                },
                error: function (ex) {
                    console.log(ex.responseJSON.Message);
                }
            });--%>

        //            window.open(url, '_blank');
    }

    $(document).ready(function () {
        var doctypeId = $("[id$=hdnDocTypeId]").val();
        var docId = $("[id$=hdnDocId]").val();
        localStorage.removeItem('url');

        var url = "../../Services/GetDocFile.ashx?DocTypeId=" + doctypeId + "&DocId=" + docId + "&" + localStorage.queryStringAuthorization + "&ConvertToPDf=true";
        //var ruta = $(location).attr('href', url)
        //window.location.href = url;
<%--        $("#tabContent").attr('src', '../WF/TaskDetails/TaskMailhistory.aspx?ResultID=<%=DocId %>');--%>
        localStorage.setItem('url', url);

        //var url = localStorage.getItem('url');
        //try {
        //    $("#previewDocSearch")[0].contentWindow.OpenUrl(url, -1);
        //}
        //catch (error) {
        //    $("#previewDocSearch").attr("src", url);
        //}
        //alert(url);
    });


    $('#dZFUpload0').hasClass('dz-started');



    function getRestApiUrl() {
        return '<%=ConfigurationManager.AppSettings["RestApiUrl"] %>';
    }

    function PermitsForIndexPanel() {
        var response = null;
        var genericRequest = {
            Params: {}
        };

        $.ajax({
            type: "POST",
            url: serviceBase + '/search/GetPermitsForIndexPanel',
            data: JSON.stringify(genericRequest),

            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                }
        });
        return response;
    }

    function OcultarScrolIE() {

        setTimeout(function () {
            if ($("#wrapper")[0].className == "toggled") {
                $("#sidebar-wrapper").css("overflow-y", "hidden");
            } else {
                $("#sidebar-wrapper").css("overflow-y", "auto");
            }
        }, 2000);


    }


</script>
