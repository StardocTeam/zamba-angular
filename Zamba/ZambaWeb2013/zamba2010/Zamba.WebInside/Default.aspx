<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" MasterPageFile="~/Masterpage.Master" %>

<asp:Content ID="Content4" ContentPlaceHolderID="header_css" runat="Server">
    <title>Zamba</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="header_js" runat="Server">

</asp:Content>
<asp:Content ID="ContentPlaceHolderHome" runat="server" ContentPlaceHolderID="ContentPlaceHolderHome">
<div style="font-size:14px;">
<h2>Zamba Software</h2>
    <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Cliente/ZambaCliente.htm">Cliente</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="~/administrador/ZambaAdministrador.htm">Administrador</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" 
            NavigateUrl="~/reportes/publish.htm">Reportes</asp:HyperLink>
</div>
</asp:Content>