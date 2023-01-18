<%@ Page Language="C#" AutoEventWireup="false" Inherits="Diagrama1"  MasterPageFile="~/Masterpage.Master" EnableViewState="false" Codebehind="Diagrama.aspx.cs" %>
<%@ Register Src="~/Views/UC/wf/Arbol.ascx" TagName="UCMenu" TagPrefix="UC1" %>
<%@ Register Src="~/Views/UC/wf/Diagrama.ascx" TagName="UCDiagrama" TagPrefix="UC4" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder2" runat="server">  
    <UC1:UCMenu runat="server" ID="arbol" />
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <UC4:UCDiagrama runat="server" ID="diagrama" />
</asp:Content>