<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocViewer.ascx.cs" Inherits="Views_UC_Viewers_DocViewer"  EnableViewState="false"%>
<%@ Register TagPrefix="ind" TagName="CompletarIndices" Src="~/Views/UC/Index/DocTypesIndexs.ascx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="tb" TagName="DocumentToolbar" Src="~/Views/UC/Viewers/DocToolbar.ascx" %>
<%@ Reference Control = "~/Views/UC/Viewers/ImageViewer.ascx" %>

<div id="divButtons" style="overflow: visible; padding:5px">
    <asp:UpdatePanel ID="uppnlToolbarDetailActions" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <tb:DocumentToolbar ID="docTB" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblDoc" runat="server" Text=""></asp:Label>
</div>

<div id="divContent">
    <table style="width: 100%;">
        <tr style="vertical-align: top">
            <td id="DivIndices" style="position: absolute; left: 0px; top: 0px">
                <asp:UpdatePanel ID="uppnDetailViewer" runat="server">
                    <ContentTemplate>
                        <ind:CompletarIndices ID="completarindice" runat="server" EnableViewState="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td id="separator" style="width: 5px"></td>
            <td id="DivDoc" runat="server" style="overflow: visible;">
                <iframe id="formBrowser" runat="server" name="formBrowser"
                    style="border: 1px solid black; margin-left:30px; overflow:visible; width: 100%; min-height: 400px"
                    wmode="transparent" title=""></iframe>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        <% if(completarindice.Visible) { %>
            //Visibilidad del panel de índices
            var divDoc = $('#<%=DivDoc.ClientID %>');
            var panel2Indices = $('#<%=completarindice.ClientID %>' + '_Panel2');
            $("#btnShowHide").click(function () {
                SetIndexPnlVisibility(this, divDoc, panel2Indices);
            });
        <% } else { %>
            $("#DivIndices").remove();
            $("#separator").remove();
        <% } %>

        setTimeout(function () { CheckDocLocation(); parent.hideLoading(); }, 2000);
    });

    function CheckDocLocation() {
        var docFrame = document.getElementById('<%=formBrowser.ClientID %>');
        if (docFrame != null) {
            var content = docFrame.contentWindow.document.mimeType || docFrame.contentWindow.document.contentType;
            if (content === undefined) {
                parent.toastr.info('El documento ha sido mostrado por fuera de Zamba. De no poder encontrarlo, verifique si este no fue descargado por su navegador.');
            }
        }
    }

    $(window).load(function () {
        var screenHeight = $(document).height() - $("#divButtons").height() - $("#Header").height() - 20;
        $("#divContent").height(screenHeight);
        $("#DivDoc").height(screenHeight);
        $("#<%=formBrowser.ClientID%>").height(screenHeight);

        <% if(completarindice.Visible) { %>
            var indicesHeight = $("#DivIndices").height();
            var a = $('#<%=DivDoc.ClientID %>').width() - $("#DivIndices").width();

            //Si el alto del panel de indices es mas grande que el alto del contenedor general, setea la altura y agrega el scroll vertical
            if (indicesHeight > screenHeight) {
                $("#DivIndices").height(screenHeight);
                $('#<%=completarindice.ClientID %>' + '_Panel2').css("overflow", "auto");
                $('#<%=completarindice.ClientID %>' + '_Panel2').height(screenHeight - 30);
            }

            //Setea el ancho adecuado
            $('#<%=DivDoc.ClientID %>').width(a);
        <% } %>
    });
</script>
