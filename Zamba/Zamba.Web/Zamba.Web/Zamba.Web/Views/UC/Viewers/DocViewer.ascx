<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Viewers_DocViewer" EnableViewState="false" CodeBehind="DocViewer.ascx.cs" %>
<%@ Register TagPrefix="ind" TagName="CompletarIndices" Src="~/Views/UC/Index/DocTypesIndexs.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="tb" TagName="DocumentToolbar" Src="~/Views/UC/Viewers/DocToolbar.ascx" %>
<%@ Reference Control="~/Views/UC/Viewers/ImageViewer.ascx" %>


<div id="divButtons" style="overflow: visible;">
    <asp:UpdatePanel ID="uppnlToolbarDetailActions" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <tb:DocumentToolbar ID="docTB" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Label ID="lblDoc" runat="server" Text=""></asp:Label>
</div>

<div id="wrapper" class="toggled ">
    <div id="divContent" style="width: 100%;  overflow: visible">

        <div id="sidebar-wrapper" class="scrollbarindices hidden-xs">
            <asp:UpdatePanel ID="Panel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:UpdatePanel ID="uppnDetailViewer" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <ind:CompletarIndices ID="completarindice" runat="server" EnableViewState="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <table style="width: 100%; height: 100%">
            <tr style="vertical-align: top">




                <td id="separator" style="width: 5px"></td>
                <td id="DivDoc" runat="server" style="overflow: visible; height: 250px;">
                    <div id="page-content-wrapper">
                        <div class="container-fluid" ng-controller="DocumentViewerController" id="documentViewerController">
                            <div class="row">
                                <div class="col-md-12 " id="iframeStyle" style="height: auto">
                                    <div class="row">
                                        <zamba-document-viewer class="iframeClassPDF" id="DocumentViewerIFrame"></zamba-document-viewer>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </td>
            </tr>
        </table>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        var UrlParams;
        var flag;

        if (flag = (parent.name != "TAGGESTION")) {
            UrlParams = getUrlParametersFromIframe();
        } else {
            UrlParams = parent.getUrlParametersFromIframe();
        }

        $("#closeModalDoShowForm").on("click", function () {
            parent.window.location.reload();
        });

        <% if (completarindice.Visible)
    { %>
        //Visibilidad del panel de índices
        var divDoc = $('#<%=DivDoc.ClientID %>');
        var panel2Indices = $('#<%=completarindice.ClientID %>' + '_Panel2');
        $("#btnShowHide").click(function () {
            SetIndexPnlVisibility(this, divDoc, panel2Indices);
        });

        <% }
    else
    { %>
        $("#separator").remove();
        <% } %>

        function getUrlParameters(url) {
            if (url != undefined) {
                var pairs = url.split("?")[1].split("&");
                var res = {}, i, pair;
                for (i = 0; i < pairs.length; i++) {
                    pair = pairs[i].toLowerCase().split('=');
                    if (pair[1])
                        res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
                }
                return res;
            } else {
                var pairs = window.location.search.substring(1).split(/[&?]/);
                var res = {}, i, pair;
                for (i = 0; i < pairs.length; i++) {
                    pair = pairs[i].toLowerCase().split('=');
                    if (pair[1])
                        res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
                }
                return res;
            }
        }

        function getUrlParametersFromIframe() {
            var Url = $("#iframeDoShowForm").attr("src");
            return getUrlParameters(Url);
        }

        setTimeout(function () { CheckDocLocation(); parent.hideLoading(); }, 2000);

        if (UrlParams.hasOwnProperty("previewmode")) {
            if (UrlParams.previewmode != undefined) {
                var isTrueSet = (UrlParams.previewmode === 'true');

                if (isTrueSet)
                    HideBtnsOnPreviewMode();
            }
        } else {
            $("#modalDoShowForm").css("display", "none");
            flag ? showVerticalScrollBar() : parent.showVerticalScrollBar();
        }
    });

    function CheckDocLocation() {
        var docFrame = null;
        if (docFrame != null) {
            var content = docFrame.contentWindow.document.mimeType || docFrame.contentWindow.document.contentType;
            if (content === undefined) {
                parent.toastr.info('El documento ha sido mostrado por fuera de Zamba. De no poder encontrarlo, verifique si este no fue descargado por su navegador.');
            }
        }
    }

    function HideBtnsOnPreviewMode() {
        $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_btnEmail").hide();
        $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_btnAttach").hide();
        $("#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_BtnShowIndexs").hide();
        $("#ctl00_ContentPlaceHolder_ucTaskHeader_UACCell").hide();
        $("#liMails").hide();
        $("#liHistory").hide();
        $("#liAsociated").hide();
        $("#BtnFinalizar").hide();
        $("#BtnIniciar").hide();
        $("#liCerrar").hide();

    }

    $(window).on("load", function () {
        var screenHeight = $(document).height() - $("#divButtons").outerHeight(true) - ($("#Header").outerHeight(true) * 2) - 20;
        $("#divContent").height(screenHeight);

        <% if (completarindice.Visible)
    { %>

        <% } %>
    });

    // Felipe; Este codigo es para poner el titulo de la pagina, se toma del nombre de la tarea, solicitud de  MARSH
    $(document).ready(function () {
        if ($("#ctl00_ContentPlaceHolder_ctl01_docTB_lbltitulodocumento")[0] != undefined) {
            var nameTitle = $("#ctl00_ContentPlaceHolder_ctl01_docTB_lbltitulodocumento")[0].innerHTML;
            if (nameTitle != "" & nameTitle != null) {
                window.document.title = nameTitle;
            }
        }
        var DocviewerScope = angular.element($("#DocumentViewerIFrame")).scope();

        $(window).bind("resize", function () {
            DocviewerScope.ResizeIframe();
        });
    });
</script>
