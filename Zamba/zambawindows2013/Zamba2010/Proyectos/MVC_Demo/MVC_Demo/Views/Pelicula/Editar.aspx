<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MVC_Demo.Models.Pelicula>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Editar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Editar</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <%=Html.Hidden("id", Model.Id) %>
            <p>
                <label for="Titulo">Titulo:</label>
                <%=Html.TextBox("Titulo", Model.Titulo) %>
                <%=Html.ValidationMessage("Titulo", "*") %>
            </p>
            <p>
                <label for="Fecha">Fecha:</label>
                <%=Html.TextBox("Fecha", Model.Fecha.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-AR")))%>
                <%=Html.ValidationMessage("Fecha", "*") %>
            </p>
            <p>
                <label for="Puntaje">Puntaje:</label>
                <%=Html.TextBox("Puntaje", Model.Puntaje) %>
                <%=Html.ValidationMessage("Puntaje", "*") %>
            </p>
            <p>
                <input type="submit" value="Guardar" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <a id="listado"  href="#">Listado</a>
    </div>

</asp:Content>

