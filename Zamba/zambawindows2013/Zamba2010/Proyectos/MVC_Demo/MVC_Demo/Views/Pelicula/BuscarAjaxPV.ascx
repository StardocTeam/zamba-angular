<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MVC_Demo.Models.Pelicula>>" %>

    <h2>Resultados</h2>
    
   <table id="resultados">
        <thead>
            <tr>                        
                <th>
                    Titulo
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
                    <a id="detalle" href="#" rel="<%=item.Id%>">Detalle</a> |
                    <a id="borrar"  href="#" rel="<%=item.Id%>">Borrar</a>
                </td>
            </tr>
    
         <% } %>
         
        </tbody>
    </table>