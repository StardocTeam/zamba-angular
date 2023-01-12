<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVC_Demo.Models.Pelicula>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Nueva Pelicula
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Nueva Pelicula</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Datos de la pelicula</legend>
            <p>
                <label for="Titulo">Titulo:</label>
                <%= Html.TextBox("Titulo") %>
                <%= Html.ValidationMessage("Titulo", "*") %>
            </p>
            <p>
                <label for="Fecha">Fecha:</label>
                <%= Html.TextBox("Fecha") %>
                <%= Html.ValidationMessage("Fecha", "*") %>
            </p>
            <p>
                <label for="Puntaje">Puntaje:</label>
                <%= Html.TextBox("Puntaje") %>
                <%= Html.ValidationMessage("Puntaje", "*") %>
            </p>
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Volver al listado", "Listar") %>
    </div>

</asp:Content>

