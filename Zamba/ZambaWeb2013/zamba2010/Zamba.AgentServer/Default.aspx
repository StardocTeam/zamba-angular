<%@ Page Title="Zamba - Control de Licencias" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Zamba.AgentServer._Default" %>
    <%@ Register Src="~/Controls/UCClients.ascx" TagPrefix="Clients" TagName="List" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Stardoc - Zamba Control Panel
    </h2>
    <p>
 <Clients:List ID="ClientList" runat="server" />

    </p>
</asp:Content>
