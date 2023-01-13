<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToolPrint.aspx.cs" Inherits="Views_Tools_ToolPrint"
    MasterPageFile="~/MasterPopUppage.Master" %>

<%@ MasterType TypeName="MasterPopUpPage" %>
<asp:Content ID="Content3" ContentPlaceHolderID="header_js" Runat="Server">	
<script type="text/javascript">
    function CheckIsIE() {
        if (navigator.appName.toUpperCase() == 'MICROSOFT INTERNET EXPLORER') {
            return true;
        }
        else {
            return false;
        }
    }
    var t;
    function Imprimir_Click() {
        try {
            if (CheckIsIE() == true) {
                document.formBrowser.focus();
                document.formBrowser.print();

            }
            else {
                window.frames['formBrowser'].focus();
                window.frames['formBrowser'].print();

            }
            if (t)
                clearTimeout(t);
        }
        catch (e) {
            alert(e.description);
        }
    }

    $(document).ready(function() {
        var winHeight = $(document).height() - 40;
        $("#<%=formBrowser.ClientID %>").height(winHeight);
        t = setTimeout("Imprimir_Click()", 1000);
    });
</script>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder">
    <%--<title>Documentos Asociados</title>--%>
    <table style="height: 100%; width: 100%;">
        <tr>
            <td style="background-color: Silver; border: solid 1px black; text-align: center">
                <div onclick="Imprimir_Click()" style="height: 100%; cursor: pointer; width: 100%;"
                    id="divImprimir">
                    <img id="lnkImprimir" src="../../Content/Images/print.png" alt="Imprimir documento"
                        style="height: 16px" />
                    <asp:Label ID="lblImprimir" Text="Imprimir documento" runat="server" ToolTip="Imprimir documento"
                        Height="90%"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <iframe id="formBrowser" runat="server" align="right" frameborder="0" name="formBrowser"
                        style="border: 1px solid black;width:100%;height:100%" title="" scrolling="yes">
                        El documento solicitado no pudo ser cargado. Esto puede deberse a que el mismo no
                        exista o que se haya producido un error al obtenerlo. Por favor, comuníquese con
                        el administrador del sistema. </iframe>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
