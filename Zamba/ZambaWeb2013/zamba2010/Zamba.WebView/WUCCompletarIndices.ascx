<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/WUCCompletarIndices.ascx.cs" Inherits="WebUserControl" %>
<asp:GridView ID="gvCompletarIndice" runat="server" AutoGenerateColumns="False" CellPadding="4" 
    GridLines="None" Font-Size="X-Small" 
    ForeColor="#333333" 
    onpageindexchanging="gvCompletarIndice_PageIndexChanging" PageSize="9">
    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
    <RowStyle BackColor="#EFF3FB" />
    <Columns>
        <asp:BoundField HeaderText="ID_indice" Visible="False" />
        <asp:BoundField HeaderText="Nombre" />
        <asp:TemplateField HeaderText="Valor">
            <ItemTemplate>
                <asp:TextBox ID="txtIndex_value" runat="server"></asp:TextBox>
             
            </ItemTemplate>
            <HeaderStyle Font-Size="X-Small" />
        </asp:TemplateField>
    </Columns>
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" 
        Wrap="False" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#2461BF" Font-Size="X-Small" />
    <AlternatingRowStyle BackColor="White" />
    
</asp:GridView>
<asp:HiddenField ID="hdnDoctypeIdRecibe" runat="server" />

