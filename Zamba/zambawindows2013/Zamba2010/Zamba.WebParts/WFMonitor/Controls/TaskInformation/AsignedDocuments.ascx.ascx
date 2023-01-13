<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AsignedDocuments.ascx.ascx.cs"
    Inherits="AsignedDocuments" %>
<fieldset title="Documentos Asociados">
    <legend style="font: caption; font-size: medium; color: navy">Documentos asignados</legend>
    <div class="UserControlBody">
        <asp:HiddenField runat="server" ID="hfTaskId" />
        <asp:GridView ID="gvAsociatedDocuments" runat="server" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                Font-Size="Small">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="True" />
        </asp:GridView>
    </div>
    <div class="UserControlButtons">
        <asp:Button ID="btRefresh" runat="server" Text="Actualizar" OnClick="btRefresh_Click"
            BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" Font-Size="Small" ForeColor="#284775"  ToolTip="Actualizar Listado"/>
    </div>
</fieldset>
