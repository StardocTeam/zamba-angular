<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Marsh.Bussines.NoticiaBussines>" %>
<%@ Import Namespace="IntranetMarshMVC.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - <%= Html.Encode(((Marsh.Bussines.NoticiaBussines)Model).Titulo)%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" src="/intranet/Scripts/noticias.js"></script>

    <table width="495" height="110" border="0" cellpadding="0" cellspacing="0" id="noticia">
        <tr>
            <td>
                <div class="noticia_categoria" style="float:left">
                    <%= Html.Encode(((Marsh.Bussines.NoticiaBussines)Model).Categoria)%>                    
                </div>
                <div class="noticia_categoria" style="float:right">
                    <%= Html.Encode(((Marsh.Bussines.NoticiaBussines)Model).Fecha.ToShortDateString())%> 
                </div>
            </td>
        </tr>
        <tr>
            <td width="495" valign="top">
                <div class="linktitnoticiaprincipal2">
                    <%= Html.Encode(((Marsh.Bussines.NoticiaBussines)Model).Titulo)%>
                </div>
                <br />
            </td>
        </tr>
        <tr>
            <td width="495" valign="top">
                <%
                string imagen = ((Marsh.Bussines.NoticiaBussines)Model).Imagen;

                if (imagen != "")
                {
                %>
                <div style="float:left; margin: 0px 15px 15px 0px">
                    <%=Html.Image(imagen, ((Marsh.Bussines.NoticiaBussines)Model).Titulo, 180, 120)%> 
                </div>
                <%
                }    
                %>
                <div class="texto">        
                    <%=((Marsh.Bussines.NoticiaBussines)Model).NoticiaLarga%>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>