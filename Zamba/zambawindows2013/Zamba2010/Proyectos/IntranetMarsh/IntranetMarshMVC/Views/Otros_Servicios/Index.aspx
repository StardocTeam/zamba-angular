<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Otros servicios</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">

    <script type="text/javascript" src="../../Scripts/otros_servicios.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="494" height="28" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494"><div align="left" class="titulo">Servicios</div></td>
    </tr>
</table>

<br />

<div id="div_form_pedido">

    <table width="495" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="476">
                <div align="left">
                    <p class="texto">
                        <span class="titulo3"><strong>Otros Servicios  </strong></span>
                        <br />
                        <br />
                        Complete el formulario, el mismo ser&aacute; enviado a RRHH para su revisi&oacute;n. <br />
                        <br />
                    </p>
                </div>
            </td>
        </tr>
    </table>


    <% using (Ajax.BeginForm("EnviarSolicitud", new AjaxOptions() { OnBegin = "ajaxValidate", OnSuccess = "OtrosServicios_Resultado", HttpMethod = "POST" })) { %>

        <table width="495" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td align="left">
                    Servicio solicitado:
                </td>
                <td>
                   <select id="idservicio" name="idservicio" class="texto">
                    <%
                    foreach (var serv in (IEnumerable<Marsh.Bussines.ServicioBussines>)ViewData["servicios"])
                    {
                         %>
                        <option value="<%=serv.Id %>"><%=serv.Descripcion%></option>
                         <%
                    }
                    %>
                    </select> 
                </td>
            </tr>
            <tr>
                <td width="139" class="texto" align="left">
                    Nombre y Apellido
                </td>
                <td width="348">
                    <select id="usuario" name="usuario" class="texto">
                        <option value="">Seleccione un usuario ...</option>
                    <%
                    foreach (var user in (IEnumerable<Marsh.Bussines.UsuarioBussines>)ViewData["usuarios"])
                    {
                        if ((string)(user.NombreApellido).Trim() != "")
                        {
                         %>
                        <option value="<%=(string)user.NombreApellido.Trim() %>"><%=user.NombreApellido%></option>
                         <%
                        }
                    }
                    %>
                    </select>            
                </td>
            </tr>
            <tr>
                <td width="139" valign="top" class="texto" align="left">
                    Mensaje
                </td>
                <td>
                    <div align="left">
                       <textarea name="mensaje" id="mensaje" cols="50" rows="6" class="texto"></textarea>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="submit" name="Submit" value="Enviar" />
                </td>
            </tr>
        </table>
    <%
    }
    %>

</div>

<div id="mensaje_envio_pedido" style="display:none"></div>

</asp:Content>