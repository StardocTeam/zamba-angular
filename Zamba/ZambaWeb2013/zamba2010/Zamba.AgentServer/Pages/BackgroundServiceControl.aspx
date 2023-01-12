<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackgroundServiceControl.aspx.cs" Inherits="Zamba.AgentServer.Pages.BackgroundServiceControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblinfo" runat="server" Text=""></asp:Label>

    <asp:Button ID="btnStart" runat="server" Text="Start Service" 
        onclick="btnStart_Click" />

</asp:Content>
