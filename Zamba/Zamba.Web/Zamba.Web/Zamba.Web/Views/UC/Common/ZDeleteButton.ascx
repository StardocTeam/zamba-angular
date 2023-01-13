<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Views_UC_Common_ZDeleteButton" CodeBehind="ZDeleteButton.ascx.cs" %>


<script type="text/javascript">
    function getBtnDelete() {
        //se comento ya que por el momento no tiene utilidad, muestra un boton delete segun los permisos
        <%--return $("#<%=btnDelete.ClientID %>");--%>
    }
</script>
<%--<div id="btnDelete" runat="server" onclick="ShowDeleteConfirmation();" style="display: inline; height: 20px; width: 80px; padding-left: 10px">--%>
    <%-- float: left;--%>
    <%--<img src="../../Content/Images/Toolbars/delete2.png" style="height: 16px" />--%>
   <%-- <div class="btn btn-default btn-xs" style="cursor: pointer; vertical-align: middle; color: #004e98">--%>
        <%-- height: 20px; --%>
<%--        <span class="glyphicon glyphicon-remove"></span>
        <span>Eliminar
        </span>
    </div>
</div>--%>
<div>
    <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label>
</div>
<div id="divDeleteConfirmation" style="display: none; float: left; width: 600px; padding-left: 10px">
    <span id="lblQuestion" style="font-size: x-small; color: #8B0000">¿Está seguro que desea
        eliminar el documento?</span>
    <div style="display: inline">
        <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Aceptar" Style="width: 100px"
            OnClientClick="ShowLoadingAnimation();" />
        <input type="button" value="Cancelar" style="width: 100px" onclick="HideDeleteConfirmation();" />
    </div>
</div>
