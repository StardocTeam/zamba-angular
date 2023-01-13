<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Tarjetas Comerciales
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">

    <script type="text/javascript" src="../../Scripts/tarjetas.js"></script>
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
    
<div id="div_form_pedido">    
    
    <table width="495" border="0" cellpadding="0" cellspacing="1">
        <tr>
            <td width="476" align="left">
                <p class="texto">
                    <span class="titulo3">
                        <strong>Tarjetas comerciales</strong>
                    </span>
                    <br />
                    <br />
                    Complete el formulario, el mismo ser&aacute; enviado a 
                    RRHH para su revisi&oacute;n. 
                    <br />
                    <br />
                </p>
            </td>
        </tr>
    </table>

    <% using (Ajax.BeginForm("GuardarSolicitud", new AjaxOptions() { OnBegin = "ajaxValidate", OnSuccess = "Tarjetas_Resultado", HttpMethod = "POST" }))  { %>
    
    <table width="495" border="0" cellpadding="2" cellspacing="0">
        <tr>
            <td width="139" class="texto" align="left">
                Nombre y Apellido
            </td>
            <td width="348">
                <select id="usuario" name="usuario" class="texto" onchange="javascript:DatosUsuario();">
                    <option value="">Seleccione un usuario ...</option>
                <%
                foreach (var user in (IEnumerable<Marsh.Bussines.UsuarioBussines>)ViewData["usuarios"])
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
                <img id="img_loading" src="../../imgs/loading_small.gif" width="18" height="18" />           
            </td>
        </tr>
        <tr>
            <td width="139" class="texto" align="left">
                Cargo
            </td>
            <td align="left">
                <input name="cargo" id="cargo" type="text" class="texto" size="50" maxlength="50" disabled/>
            </td>
        </tr>
        <tr>
            <td width="139" class="texto" align="left">
                Sector
            </td>
            <td align="left">
                <input name="sector" id="sector" type="text" class="texto" size="50" maxlength="50" disabled/>
            </td>
        </tr>
        <tr>
            <td width="139" class="texto" align="left">
                Tel&eacute;fonos de contacto
            </td>
            <td align="left">
                <input name="telefono" id="telefono" type="text" class="texto" size="50" maxlength="50" disabled/>
            </td>
        </tr>
        <tr>
            <td width="139" class="texto" align="left">
                Mail de contacto
            </td>
            <td align="left">
                <input name="email" id="email" type="text" class="texto" size="50" maxlength="50" disabled/>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left"> 
            <br />
                <input type="submit" name="submit" id="submit" value="Enviar" disabled/>
            </td>
        </tr>
    </table>
    
    <% } %>

</div>

<div id="mensaje_envio_pedido" style="display:none"></div>

</asp:Content>