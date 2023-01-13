<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marsh.Bussines.FormularioBussines>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Seguridad e Higiene
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <iframe src="/download/<%=((Marsh.Bussines.FormularioBussines)Model).File.Trim() %>" width="515px" height="455px" style="border:1px solid #000"></iframe>

</asp:Content>