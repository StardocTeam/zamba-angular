<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Formularios y solicitudes
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
     <script type="text/javascript" src="../../Scripts/formularios.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="494" height="28" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494">
            <div align="left" class="titulo">Formularios y solicitudes </div>
        </td>
    </tr>
</table>

<br />

<%
if (Model != null)
{
    string txt_buscar = "";
    string categ_buscar = "";
    string sel = "";
    string rel;
    string id;
    
    if (Request.QueryString["buscar"] != null)
        txt_buscar = Request.QueryString["buscar"].ToString();

    if (Request.QueryString["categ"] != null)
        categ_buscar = Request.QueryString["categ"].ToString();    
%>
<table width="491" border="0" cellpadding="0" cellspacing="1">
    <tr>
        <td colspan="2">
            <div align="left">
                <span class="titulo3">
                    <strong>Instrucciones</strong>
                </span>
                <br />
                <br />
                <span class="titulo3"><strong>1.</strong></span> Descargue el formulario requerido<br />
                <span class="titulo3"><strong>2.</strong></span> Imprima y complete los datos requeridos<br />
                <span class="titulo3"><strong>3.</strong></span> Env&iacute;e el formulario a la oficina de RRHH<br />
            </div>
        </td>
    </tr>    
    <tr>
        <td colspan="2" style="padding-bottom: 10px">
            <br /><br />
            <div align="left">
                <span class="titulo3">
                    <strong>Buscar formularios</strong>
                </span>
            </div>
        </td>
    </tr>
    <tr>
        <td width="45">Buscar:</td>
        <td>
            <input type="text" id="buscar_form" class="texto" value="<%=txt_buscar %>"/>
            <span class="linkmenu_izq">
                <a href="#" id="lnk_buscar_forms" class="linkmenu_izq">&raquo;</a>
            </span>            
        </td>
        <td>Filtrar:</td>
        <td>
            <select id="categoria_form" class="texto">
                <option value="">Todas</option>                
                <%                
                foreach (var form in (IEnumerable<string>)ViewData["categorias"])
                {
                    if (form.ToString() == categ_buscar)
                        sel ="SELECTED";
                    else
	                    sel = "";
                %>
                <option value="<%=form.ToString()%>" <%=sel%>><%=form.ToString()%></option>
                <%
                }
                %>
            </select>
            <span class="linkmenu_izq">
                <a href="#" id="lnk_filtrar_forms" class="linkmenu_izq">&raquo;</a>
            </span>
        </td>        
    </tr>    
</table>

<br />
<br /> 
<% 
int i = 0;
foreach (var form in (IEnumerable<Marsh.Bussines.FormularioBussines>)ViewData["formularios"])
{
	i++;
%> 
<table width="491" border="0" cellpadding="4" cellspacing="1">
    <tr>
        <td width="405" class="titulo3">
            <div align="left">
                <strong>
                    .&nbsp;
                    <a id="link_ver_form" href="file:///<%=form.File%>" rel="<%=form.Id%>" class="linktitnoticia">
                        <%=form.Titulo%>
                    </a>
                </strong>
            </div>
            <span class="texto">
                <%=form.Descripcion %>
            </span>             
        </td>
    </tr>
</table>
<%
}

if(i == 0)
{
%>
	<span class="titulo2">No se han encontrado resultados.</span>
<%
}
%>

<br />
<br />
<br />    

<%
    string href;
    
    if ((int)ViewData["pagina_actual"] > 1)
    {
        if (Request.QueryString["buscar"] != null)
        {
            href = "/buscar/?pagina=" + (((int)ViewData["pagina_actual"]) - 1) + "&buscar=" + Request.QueryString["buscar"].ToString() + "&categ=" + Request.QueryString["categ"].ToString();            
        }
        else if (Request.QueryString["categ"] != null)
        {
            href = "/filtrar/?pagina=" + (((int)ViewData["pagina_actual"]) - 1) + "&categ=" + Request.QueryString["categ"].ToString();
        }            
        else
        {
            href = "/pagina/?pagina=" + (((int)ViewData["pagina_actual"]) - 1).ToString();
        }
%>
    <div style="float:left">
        <a href="javascript:ir_a_formulario('<%=href %>');"class="linkmenu_izq">
            &laquo; Anterior 
        </a>
    </div> 
<%
    }
    
    if ((int)ViewData["total_paginas"] > (int)ViewData["pagina_actual"])
    {
        if (Request.QueryString["buscar"] != null)
        {
            href = "/buscar/?pagina=" + (((int)ViewData["pagina_actual"]) + 1) + "&buscar=" + Request.QueryString["buscar"].ToString() + "&categ=" + Request.QueryString["categ"].ToString();           
        }
        else if (Request.QueryString["categ"] != null)
        {
            href = "/filtrar/?pagina=" + (((int)ViewData["pagina_actual"]) + 1) + "&categ=" + Request.QueryString["categ"].ToString();
        }
        else
        {
            href = "/pagina/?pagina=" + (((int)ViewData["pagina_actual"]) + 1).ToString();
        }      
%> 
    <div style="float:right" style="padding-right: 20px">
        <a href="javascript:ir_a_formulario('<%=href %>');"class="linkmenu_izq">
            Siguiente &raquo;
        </a>
    </div>
<%
    }   
}
%>

</asp:Content>