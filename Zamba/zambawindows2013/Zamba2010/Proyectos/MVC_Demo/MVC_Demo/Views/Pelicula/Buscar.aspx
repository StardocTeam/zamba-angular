<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MVC_Demo.Models.Pelicula>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Buscar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Buscar</h2>
    
    Son las <%=DateTime.Now.ToString() %>
    <br />
    <br />

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <p>
                <label for="Titulo">T&iacute;tulo a buscar:</label>
                <%=Html.TextBox("Titulo", "") %>
                <%=Html.ValidationMessage("Titulo", "*") %>
            </p>
            <p>
                <input type="submit" value="Buscar" />
            </p>
        </fieldset>

    <% } %>
    
    <%
    if (Model != null)
    {
    %>
    
    <h2>Resultados</h2>
    
    <table id="resultados">
    
        <thead>
            <tr>             
                <th>
                    T&iacute;tulo
                </th>
                <th>
                    Fecha
                </th>
                <th>
                    Puntaje
                </th>
                <th></th>
            </tr>
        </thead>
    
        <tbody>

        <% foreach (var item in Model) { %>

            <tr id="tr-<%=item.Id%>">
                <td>
                    <%=Html.Encode(item.Titulo) %>
                </td>
                <td>
                    <%=Html.Encode(item.Fecha.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-AR")))%>
                </td>
                <td>
                    <%=Html.Encode(item.Puntaje) %>
                </td>
                <td>
                    <a id="editar"  href="#" rel="<%=item.Id%>">Editar</a> |
                    <a id="detalle" href="#" rel="<%=item.Id%>">Detalle</a>
                </td>
            </tr>

        <% } %>

        </tbody>
        
    </table>

    <% } %>

</asp:Content>