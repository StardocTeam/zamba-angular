<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Marsh.Bussines.UsuarioBussines>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MARSH - Intranet - Desperfectos
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
	 <script type="text/javascript" src="../../Scripts/desperfectos.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="494" height="28" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494"><div align="left" class="titulo">Servicios</div></td>
    </tr>
</table>

<br />

<table width="495" border="0" cellpadding="4" cellspacing="1">
    <tr>
        <td width="476">
            <div align="left" >
                <p class="texto">
                    <span class="titulo3"><strong>Desperfectos</strong></span>
                </p>
            </div>
        </td>
    </tr>
</table>

<div id="div_form_desperfectos">

    <% using (Ajax.BeginForm("enviar", new AjaxOptions() { OnBegin = "ajaxValidate", OnSuccess = "Desperfectos_Resultado", HttpMethod = "POST" })) { %>

        <table width="495" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td colspan="2">                
                    Complete el formulario, el mismo ser&aacute; enviado a RRHH 
                    para su revisi&oacute;n. 
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
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
        </table>

        <br />

        <table width="495" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div align="left">                    
                        <label>
                            <span class="titulo3">
                                <strong>1.</strong>
                            </span>
                            <span class="texto">
                                Complete el lugar f&iacute;sico del desperfecto.
                            </span>
                            <br />
                            <br />
                        </label>
                        <label>
                            <textarea name="lugar" cols="70" rows="4" class="texto"></textarea>
                        </label>
                        <br />
                        <br />
                        <span class="titulo3">
                            <strong>2.</strong>
                        </span>
                        <span class="texto"> 
                            Describir el desperfecto
                            <br />
                            <br />
                            <label>
                                <textarea name="descripcion" cols="70" rows="4" class="texto"></textarea>
                            </label>
                        </span>
                        <br />
                        <br />
                        <label>
                            <input type="submit" name="Submit" value="Enviar" />
                        </label>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
       
    <% } %>

</div>

<div id="mensaje_envio_desperfecto" style="display:none"></div>

</asp:Content>