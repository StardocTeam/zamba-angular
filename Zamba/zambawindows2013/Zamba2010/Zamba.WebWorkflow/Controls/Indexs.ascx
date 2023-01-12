<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Indexs.ascx.cs" Inherits="Indexs" %>
<fieldset title="Listado de Indices" class="FieldSet">
    <legend class="Legend">Listado de Indices</legend>
    <div class="UserControlBody" style="font-size:xx-small;">
        <asp:HiddenField runat="server" ID="hdTaskId" />
        <asp:HiddenField runat="server" ID="hdDTId" />
        <asp:Label runat="server" ID="lbTaskId" Visible="false" CssClass="Label" />
        <asp:Panel ID="Panel1" runat="server"  ScrollBars="auto" Height="200 px" >
            <asp:Table ID="tblIndices" runat="server" CssClass="Table" />
        </asp:Panel>
    </div>
</fieldset>
