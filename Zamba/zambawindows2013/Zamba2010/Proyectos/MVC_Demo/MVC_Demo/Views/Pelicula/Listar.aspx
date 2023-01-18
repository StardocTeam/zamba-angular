<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MVC_Demo.Models.Pelicula>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Listar
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listado de peliculas</h2>
    
    <% if (Model != null) { %>     

    <table id="resultados">
    
        <thead>
            <tr>
                <th></th>               
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
        
        <%
        Random r = new Random();   
        %>
        
        <% foreach (MVC_Demo.Models.Pelicula Peli in Model) { %>

    
            <tr id="tr-<%=Peli.Id%>">
                <td>
                    <img id="photo" src="../../Photos/thumb<%=r.Next(1, 9)%>.jpg" alt="" />
                </td>            
                <td>
                    <%=Html.Encode(Peli.Titulo)%>
                </td>
                <td>
                    <%=Html.Encode(Peli.Fecha.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("en-AR")))%>
                </td>
                <td>
                    <%=Html.Encode(Peli.Puntaje)%>
                </td>
                <td>
                    <a id="detalle" href="#" rel="<%=Peli.Id%>">Detalle</a><br /><br />
                    <a id="editar"  href="#" rel="<%=Peli.Id%>">Editar</a><br />                    
                    <a id="borrar"  href="#" rel="<%=Peli.Id%>">Borrar</a>
                </td>
            </tr>
    
         <% } %>
         
        </tbody>
    </table>
    
    <% } %>

    <p>
        <a id="nueva" href="#">Nueva pelicula</a>
    </p>

</asp:Content>