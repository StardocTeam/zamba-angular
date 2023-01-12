<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Indexs.ascx.cs" Inherits="Indexs" %>
<fieldset title="Listado de Indices">
    <legend style="font: caption; font-size: small; color: navy">Listado de Indices</legend>
    <div class="UserControlBody">
        <asp:HiddenField runat="server" ID="hdTaskId" />
        <asp:Label runat="server" ID="lbTaskId" Visible="false" ForeColor="Navy" />
        <asp:Table ID="tblIndices" runat="server" ForeColor="Navy" Font-Size="Small" />
    </div>
</fieldset>
