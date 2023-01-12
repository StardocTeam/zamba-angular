<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AsignedDocuments.ascx.ascx.cs"
    Inherits="AsignedDocuments" %>
<fieldset title="Documentos Asociados" class="FieldSet">
    <legend class="Legend">Documentos asignados</legend>
    <div class="UserControlBody">
        <asp:HiddenField runat="server" ID="hfTaskId" />
        <asp:GridView ID="gvAsociatedDocuments" runat="server" BackColor="White" BorderColor="#CCCCCC"
            BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="True" />
        </asp:GridView>
    </div>
</fieldset>
