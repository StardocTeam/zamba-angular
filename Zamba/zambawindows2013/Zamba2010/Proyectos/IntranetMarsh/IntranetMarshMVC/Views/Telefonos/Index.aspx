<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Guia de telefonos
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
	 <script type="text/javascript" src="../../Scripts/telefonos.js"></script>
	 <style>
	    input
		{
			vertical-align: bottom;
		}
	</style>	 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="500" height="28" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494" align="left" class="titulo">
            Gu&iacute;a de contactos
        </td>
    </tr>
</table>

<br />

<table width="500" border="0" align="left" cellpadding="0" cellspacing="0">
  <tr>
  
    <td width="500" valign="top" align="left">
    
        <span class="titulo3">
            Buscar contacto por letra: 
        </span>
        
        <br />
        <br />    
        
        <table width="500" border="0" cellpadding="4" cellspacing="1">
            <tr>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('a', 1);" rel="a" class="linktitnoticia">A</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('b', 1);" rel="b" class="linktitnoticia">B</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('c', 1);" rel="c" class="linktitnoticia">C</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('d', 1);" rel="d" class="linktitnoticia">D</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('e', 1);" rel="e" class="linktitnoticia">E</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('f', 1);" rel="f" class="linktitnoticia">F</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('g', 1);" rel="g" class="linktitnoticia">G</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('h', 1);" rel="h" class="linktitnoticia">H</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('i', 1);" rel="i" class="linktitnoticia">I</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('j', 1);" rel="j" class="linktitnoticia">J</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('k', 1);" rel="k" class="linktitnoticia">K</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('l', 1);" rel="l" class="linktitnoticia">L</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('m', 1);" rel="m" class="linktitnoticia">M</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('n', 1);" rel="n" class="linktitnoticia">N</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('ñ', 1);" rel="&ntilde;" class="linktitnoticia">&Ntilde;</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('o', 1);" rel="o" class="linktitnoticia">O</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('p', 1);" rel="p" class="linktitnoticia">P</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('q', 1);" rel="q" class="linktitnoticia">Q</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('r', 1);" rel="r" class="linktitnoticia">R</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('s', 1);" rel="s" class="linktitnoticia">S</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('t', 1);" rel="t" class="linktitnoticia">T</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('u', 1);" rel="u" class="linktitnoticia">U</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('v', 1);" rel="v" class="linktitnoticia">V</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('w', 1);" rel="w" class="linktitnoticia">W</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('x', 1);" rel="x" class="linktitnoticia">X</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('y', 1);" rel="y" class="linktitnoticia">Y</a></td>
                <td class="fondo_gris_claro"><a id="lnk_letra" href="javascript:BuscarPorLetra('z', 1);" rel="z" class="linktitnoticia">Z</a></td>
            </tr>
        </table>
        
        <br />
        
        <% using (Ajax.BeginForm("buscar", new AjaxOptions() { OnBegin = "ajaxValidate", OnSuccess = "CargaDivBusqueda", HttpMethod = "POST" })) { %>
            <div align="left">
                <span class="titulo3">Buscar:</span>&nbsp;
                <input name="abuscar" id="abuscar" type="text" class="texto" size="35" />
                <input name="submit" id="submit" type="submit" class="texto" value="Buscar" />
                <input name="pagina" id="pagina" type="hidden" value="1" />
            </div>
            <span style="margin: 10px 0px 0px 50px" class="titulo2">
                <input type="radio" name="buscaren" value="apenom" checked>Apellido y Nombre
                <input type="radio" name="buscaren" value="sector">Sector
                <input type="radio" name="buscaren" value="interno">Interno
            </span>    
        <% } %>
        
        <div id="resultados_busqueda"></div>
 
    </td>
  </tr>
</table>
</asp:Content>