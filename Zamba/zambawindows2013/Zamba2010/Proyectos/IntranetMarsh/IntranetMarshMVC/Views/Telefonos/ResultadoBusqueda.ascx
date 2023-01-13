<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<br />
<br />

<table width="500" border="0" cellspacing="0" cellpadding="5">
    <tr>
        <td bgcolor="#F0F0F0" align="left">
            <span class="titulo3">
                <strong>Resultado de la b&uacute;squeda</strong>
            </span>
        </td>
    </tr>
</table>

<%
if ((int)ViewData["resultados_cant"] > 0)
{
%>
<table width="500" border="0" cellspacing="1" cellpadding="3">
    <tr>
        <td width="205" height="25" bgcolor="#F3F3F3" class="titulo2" align="left">Nombre</td>
        <td width="82"  class="titulo2 fondo_gris_claro" align="left">Interno</td>
        <td width="140" class="titulo2 fondo_gris_claro" align="left">Sector</td>
        <td width="70"  class="titulo2 fondo_gris_claro" align="left">Im&aacute;gen</td>
    </tr>
</table>

<table width="500" border="0" cellspacing="1" cellpadding="3">
    <%
    string imagen;
    int id = 0;
    
    foreach (Marsh.Bussines.UsuarioBussines user in (IEnumerable<Marsh.Bussines.UsuarioBussines>)ViewData["resultados"]) 
    {
        imagen = user.Imagen;
        id++;
    %>
    <tr>
        <td width="205" class="texto" align="left"   valign="top"><%=user.Apellido%>&nbsp;<%=user.Nombre%></td>
        <td width="82"  class="texto" align="left"   valign="top"><%=user.Interno%></td>
        <td width="140" class="texto" align="left"   valign="top"><%=user.Sector%></td>
        <td width="70"  class="texto" align="center" valign="top">
        <%
            if (imagen == String.Empty)
            {
        %>
            <img src="../imgs/icono-tel.jpg" width="16" height="16" alt=""/>
        <%
            }
            else
            {
        %>
            <ul class="hoverbox">
                <li>
                    <a href="#"><img src="<%=imagen%>" alt=""/><img src="<%=imagen%>" alt="" class="preview" /></a>
                </li>
            </ul>          
        <%
            }
        %>  
        </td>
    </tr>
    <tr>
        <td colspan="4" height="1px" style="height: 1px; border-top: 1px solid #F3F3F3; padding-bottom: 2px;"></td>
    </tr>
    <% 
    } 
    %>
</table>
<br />
<%
    if ((int)ViewData["pagina_actual"] > 1)
    {
%>     
    <div style="float:left">
        <a href="javascript:IrPaginaContactos(<%=(int)ViewData["pagina_actual"] - 1%>);" rel="<%=(int)ViewData["pagina_actual"] - 1%>" id="link_mas_contactos" class="linkmenu_izq">
            &laquo; Anterior
        </a>
    </div> 
<%
    }
    if ((int)ViewData["total_paginas"] > (int)ViewData["pagina_actual"])
    {
%> 
    <div style="float:right" style="padding-right: 20px">
        <a href="javascript:IrPaginaContactos(<%=(int)ViewData["pagina_actual"] + 1%>);" rel="<%=(int)ViewData["pagina_actual"] + 1%>" id="link_mas_contactos" class="linkmenu_izq">
            Siguiente &raquo;
        </a>
    </div>
<%
    }
    // Si esta en la ultima pagina pasa a la letra siguiente
    if ((int)ViewData["total_paginas"] == (int)ViewData["pagina_actual"])
    {
%> 
    <div style="float:right" style="padding-right: 20px">
        <a href="javascript:SiguienteBusqueda();" rel="" id="A1" class="linkmenu_izq">
            Siguiente &raquo;
        </a>
    </div>
<%   
    } 
}
else
{
%>
    <br />
    <span class="titulo2">No se han encontrado resultados.</span>
<%
}
%>

<script>
    addFuncionalidad();
</script>