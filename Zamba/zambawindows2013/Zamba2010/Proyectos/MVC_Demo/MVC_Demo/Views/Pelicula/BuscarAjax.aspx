<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MVC_Demo.Models.Pelicula>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Buscar con Ajax
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Buscar</h2>

    Son las <%=DateTime.Now.ToString() %>
    <br />
    <br />

    <% using (Ajax.BeginForm("BuscarAjax", new AjaxOptions() { OnSuccess = "LlenaResultadosBusqueda", HttpMethod = "POST" })) { %>

        <fieldset>
            <p>
                <label for="Titulo">T&iacute;tulo a buscar:</label>
                <%=Html.TextBox("Titulo", "")%>
                <%=Html.ValidationMessage("Titulo", "*")%>
            </p>
            <p>
                <input type="submit" id="enviar" value="Buscar" />
            </p>
        </fieldset>

    <% } %>
 
    <div id="resultados"></div>

</asp:Content>