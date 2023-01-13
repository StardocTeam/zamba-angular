<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="IntranetMarshMVC.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="../../Scripts/noticias.js"></script>

<table width="494" height="28" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494"><div align="left" class="titulo">Noticias Marsh </div></td>
    </tr>
</table>

<br />

<%
if (Model != null)
{
    int i = 0;
    string noticia_corta = "";
    string noticia_corta_completa = "";
    
    foreach (var noticia in (IEnumerable<Marsh.Bussines.NoticiaBussines>)ViewData["noticias"])
    {
        if (i == 0)
        {
    %>
    
    <table width="495" height="130" border="0" cellpadding="0" cellspacing="0" id="noticia">
        <tr>
            <td valign="top" style="padding-right: 15px">
                <div align="left">
                    <%=Html.Image(noticia.Imagen, noticia.Titulo, 180, 120) %>                     
                </div>
            </td>
            <td valign="top">
                <div align="left">
                    <a id="A1" href="javascript:LeerNoticia(<%=noticia.Id%>)" rel="<%=noticia.Id%>" class="linktitnoticiaprincipal">
                        <%=noticia.Titulo%>
                    </a>
                    <br />
                    <span class="texto">
                        <%=noticia.NoticiaCorta%>
                    </span>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />    
    <%
            i++;
        }
        else
        {
            noticia_corta = noticia.NoticiaCorta;
            noticia_corta_completa = noticia_corta;

            if (noticia_corta != null)
                if(noticia_corta.Length > 100)
                    noticia_corta = noticia_corta.Substring(0, 100) + "...";
    %>
    <table width="495" border="0" cellpadding="0" cellspacing="0" id="noticia">
        <tr>
            <td width="90" valign="top">
                <div align="left">
                    <%=Html.Image(noticia.Imagen, noticia.Titulo, 80, 50) %> 
                </div>
            </td>         
            <td width="405" valign="top" align="left">
				<a id="link_leer_noticia" href="javascript:LeerNoticia(<%=noticia.Id%>)" rel="<%=noticia.Id%>" class="linktitnoticia" onmouseover="Tip('<%=noticia_corta_completa%>', BGCOLOR, '#E8F3FE', BORDERWIDTH, 1, BORDERCOLOR, '#6E9BBA', WIDTH, 405, OPACITY, 100, FIX, [this, 0, 0],DELAY,0,PADDING,10)" onmouseout="UnTip();">
					<%=noticia.Titulo%>
				</a>
				<br />
				<span class="texto">
					<%=noticia_corta%>
				</span>					
            </td>
        </tr>
    </table>
    <br />
    <br />
    <%
        }
    }        

    if ((int)ViewData["total_paginas"] > (int)ViewData["pagina_actual"])
    {
    %>
    <br />
    <div align="right" style="padding-right: 20px">
        <a href="#" rel="<%=(int)ViewData["pagina_actual"] + 1%>" id="link_noticias" class="linkmenu_izq">
            [+] Noticias
        </a>        
    </div>
    <%
    }   
} 
%>
</asp:Content>