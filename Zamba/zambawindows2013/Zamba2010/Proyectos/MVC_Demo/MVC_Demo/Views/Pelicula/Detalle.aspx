<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVC_Demo.Models.Pelicula>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detalle
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Detalle</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Titulo:
            <%= Html.Encode(Model.Titulo) %>
        </p>
        <p>
            Fecha:
            <%= Html.Encode(Model.Fecha.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-AR") )) %>
        </p>
        <p>
            Puntaje:
            <%= Html.Encode(Model.Puntaje) %>
        </p>
    </fieldset>
    <p>
        <a id="editar"  href="#" rel="<%=Model.Id%>">Editar</a> |
        <a id="listado"  href="#">Listado</a>
    </p>

</asp:Content>

