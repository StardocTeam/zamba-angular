<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Marsh.Bussines.UsuarioBussines>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Comunicandonos
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
	 <script type="text/javascript" src="../../Scripts/comunicandonos.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="494" height="28" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494" align="left" class="titulo">
            Servicios
        </td>
    </tr>
</table>

<br />

<table width="495" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="476" align="left">
            <p class="texto">
                <span class="titulo3">
                    <strong>Comunic&aacute;ndonos </strong>
                </span>
            </p>
        </td>
    </tr>
</table>

<br />
<br />

<div id="div_form_comunicandonos">

    <% using (Ajax.BeginForm("enviar", new AjaxOptions() { OnBegin = "ajaxValidate", OnSuccess = "Comunicandonos_Resultado", HttpMethod = "POST" })) { %>

        <table width="495" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td width="139" class="texto">
                    <div align="left">Nombre y Apellido</div>
                </td>
                <td width="348">
                    <div align="left">
                        <select id="usuario" name="usuario" class="texto">
                            <option value="">Seleccione un usuario ...</option>
                        <%
                            foreach (Marsh.Bussines.UsuarioBussines user in ViewData.Model)
                            {
                                if ((string)user.NombreApellido.Trim() != "")
                                {
                             %>
                            <option value="<%=(string)user.NombreApellido.Trim() %>"><%=user.NombreApellido%></option>
                             <%
                                }
                            }
                        %>
                        </select>
                    </div>
                </td>
            </tr>  
            <tr>
                <td width="139" valign="top" class="texto" align="left">
                    Mensaje
                </td>
                <td align="left">
                    <textarea name="mensaje" cols="50" rows="6" class="texto"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <input type="submit" name="Submit" value="Enviar" />
                </td>
            </tr>
        </table>
        
    <% } %>
    
</div>

<div id="mensaje_envio"></div>

</asp:Content>