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

<div id="wrapper"class="toggled ">
<div id="divContent" style="width: 100%; height: 1500px; overflow: visible">
    
    <div id="sidebar-wrapper" class="scrollbarindices" >
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
            <td id="DivDoc" runat="server" style="overflow: visible; height: 500px;" ng-controller="DocumentViewerController">
                   <div id="page-content-wrapper">
                <div class="container-fluid">
                    <div class="row">
                       <div class="col-md-12 " id="iframeStyle" style="height: auto"  >
                        <div class="row">
                            <zamba-document-viewer class="iframeClassPDF" id="DocumentViewerIFrame"></zamba-document-viewer>
                        </div>
                    </div>
                </div>
  </div></div>
                

            </td>
        </tr>
    </table>
</div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        <% if (completarindice.Visible)
    { %>
        //Visibilidad del panel de índices
        var divDoc = $('#<%=DivDoc.ClientID %>');
        var panel2Indices = $('#<%=completarindice.ClientID %>' + '_Panel2');
        $("#btnShowHide").click(function () {
            SetIndexPnlVisibility(this, divDoc, panel2Indices);
        });

        //$("#ctl00_ContentPlaceHolder_ctl01_docTB_BtnShowIndexs").click(function () {
        //    BtnShowIndexs(this, divDoc, panel2Indices);
        //});
        

        <% }
    else
    { %>
        //$("#DivIndices").remove();
        $("#separator").remove();
        <% } %>

        setTimeout(function () { CheckDocLocation(); parent.hideLoading(); }, 2000);
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

    $(window).on("load", function () {
        var screenHeight = $(document).height() - $("#divButtons").outerHeight(true) - ($("#Header").outerHeight(true) * 2) - 20;
        $("#divContent").height(screenHeight);

        <% if (completarindice.Visible)
    { %>
        <%--var indicesHeight = $("#DivIndices").height();


        //Si el alto del panel de indices es mas grande que el alto del contenedor general, setea la altura y agrega el scroll vertical
        if (indicesHeight > screenHeight) {
            $("#DivIndices").height(screenHeight);
            $('#<%=completarindice.ClientID %>' + '_Panel2').css("overflow", "auto");
            $('#<%=completarindice.ClientID %>' + '_Panel2').height(screenHeight - 30);
        }--%>

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
    });
</script>
