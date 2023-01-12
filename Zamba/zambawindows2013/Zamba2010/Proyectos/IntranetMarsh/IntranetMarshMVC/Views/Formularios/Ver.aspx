<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marsh.Bussines.FormularioBussines>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Formularios y solicitudes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="titulo3" style="float:left">
        <strong><%=((Marsh.Bussines.FormularioBussines)Model).Titulo.Trim() %></strong>
    </div>
    <div class="titulo3" style="float:right">
        <a href="javascript:history.go(-1);" class="linkmenu_izq">&laquo; Volver</a>
    </div>    
    <br />
    <br />
    <span class="texto">
        <%=((Marsh.Bussines.FormularioBussines)Model).Descripcion%>
    </span>    
    <br />
    <br />
    <iframe src="file:///<%=((Marsh.Bussines.FormularioBussines)Model).File.Trim() %>" width="515px" height="400px" style="border:1px solid #000"></iframe>

</asp:Content>
