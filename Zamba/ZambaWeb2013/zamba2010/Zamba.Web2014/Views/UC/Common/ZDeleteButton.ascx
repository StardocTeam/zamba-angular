<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ZDeleteButton.ascx.cs"
    Inherits="Views_UC_Common_ZDeleteButton" %>


<script type="text/javascript">
    function getBtnDelete() {
        return $("#<%=btnDelete.ClientID %>");
    }
</script>
<div id="btnDelete" runat="server" onclick="ShowDeleteConfirmation();" style="display: inline;
    float: left; height: 20px; width: 80px; padding-left: 10px">
    <img src="../../Content/Images/Toolbars/delete2.png" style="height: 16px" />
    <span style="height: 20px; cursor: pointer; vertical-align: middle; color: #004e98">
        Eliminar</span>
</div>
<div>
    <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label></div>
<div id="divDeleteConfirmation" style="display: none; float: left; width: 600px;
    padding-left: 10px">
    <span id="lblQuestion" style="font-size: x-small; color: #8B0000">¿Está seguro que desea
        eliminar el documento?</span>
    <div style="display: inline">
        <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Aceptar" Style="width: 100px"
            OnClientClick="ShowLoadingAnimation();" />
        <input type="button" value="Cancelar" style="width: 100px" onclick="HideDeleteConfirmation();" />
    </div>
