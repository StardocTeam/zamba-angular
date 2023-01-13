<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Views_Client_Reintegros_Main" Codebehind="Main.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header_css" Runat="Server">




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header_js" Runat="Server">

    <script type="text/javascript">
function Redirect(page)
{
    var pagina = '../Main/Default.aspx?tabRedirect='+page;
    //alert(pagina);
    document.location.href = pagina;
}


function InsertFormModal(formid) {
    var url = "../WF/DocInsertModal.aspx?formid=" + formid + "&userid=" + GetUID();
    ShowIFrameModal("Insertar documentos", url, 550, 960);
    StartObjectLoadingObserverById("IFDialogContent");
}


$(document).ready(function() {
      //      $("#dialoginsertarform").dialog({
     //         bgiframe: true, autoOpen: false, height: 100, modal: true
    //       });
    $("#dialoginsertarform").dialog({
        bgiframe: true,
        modal: true,
        autoOpen: false,
        height: 170,
        buttons:
            {
                'Aceptar': function() {
                    $(this).dialog('close');
                    InsertFormModal($('#<%=ddltipodeform.ClientID%>').val(), true);
                },
                'Cancelar': function() {
                    $(this).dialog('close');
                }
            }
    });

});



</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">


    <div class="TablePrincipal-MainAysa">
    <div style=" padding-top:10px; text-align: center">
    
            <%--<input id="btnAltaEntidad" type="button" onclick="jQuery('#dialoginsertarform').dialog('open'); return false" />--%>
            <input id="btnAltaEntidad" type="button" onclick="InsertFormModal('33324')" class="btnAltaEntidad" />
            <input id="btnBusqueda-Aysa" type="button" onclick="Redirect('Busqueda')"/>
            <input id="btnListados-Aysa" type="button" onclick="Redirect('Tareas')"/>
            <input id="btnModificacion-Aysa" type="button" />
    </div>
    </div>
    <div class="footer-MainAysa">
        <div>
            Resolución Optima 1024 x 768 o superior,        <br />
            Se recomienda utilizar Internet Explorer 8.0 o superior para obtener mayor compatibilidad
        </div>
    </div>
    
    <div id="dialoginsertarform" style="display:none" title="Seleccione un formulario">
    <center>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="ddltipodeform" runat="server" ToolTip="Seleccione el formulario deseado" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>   
        </table>
    </center>
</div>

</asp:Content>

