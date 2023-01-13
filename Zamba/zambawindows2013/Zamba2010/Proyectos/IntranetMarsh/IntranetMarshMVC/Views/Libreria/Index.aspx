<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 MARSH - Intranet - Pedido de libreria
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
	 <script type="text/javascript" src="../../Scripts/libreria.js"></script>
	 <script type="text/javascript" src="../../Scripts/jquery-spin.js"></script>
	 <style>
	    input
		{
			vertical-align: middle;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table width="494" height="35" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td width="494"><div align="left"><span class="titulo">Servicios</span></div></td>
    </tr>
</table>

<br />

<div id="div_form_pedido">

    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="495">
                <div align="left">
                        <span class="titulo3">
                            <strong>
                                Pedido de librer&iacute;a 
                            </strong>
                        </span>
						
						<span class="texto">
							<br />
							<br />
							Complete el formulario de env&iacute;o con los material de 
							librer&iacute;a requeridos. Recuerde 
							<br />
							que puede realizar m&aacute;s de un pedido. 
							<br />
							<br />
							S&oacute;lo deber&aacute; repetir la selecci&oacute;n de articulos. 
							Las mismas figuran abajo mediante agregado de &iacute;tem, una vez 
							finalizado la selecci&oacute;n, realice el env&iacute;o con el 
							bot&oacute;n 
							<br />
							<strong>
								Enviar pedido.
							</strong>
						</span>
                </div>
            </td>
        </tr>
    </table>

    <br />

    <table width="495" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="texto" width="140" valign="top">
                <div align="left">
                    <form id="frmusuario" name="frmusuario" style="margin-bottom:0;">
                        <span class="titulo3">
                            <strong>1.</strong>
                        </span>
                        <span class="texto">
                            Nombre y Apellido:
                        </span>
                </div>
            </td>
            <td>
                <div align="left">							
                        <select id="usuario_zamba" name="usuario_zamba" class="texto">
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
                    </form>
                </div>
            </td>
        </tr>    
        <tr>
            <td class="texto" width="140" valign="top">
                <div align="left">
                    <form action="javascript:Libreria_AgregarProducto()" id="ProductosLibreria" name="ProductosLibreria" style="margin-bottom:0;">
                        <br />                        
                        <span class="titulo3">
                            <strong>2.</strong>
                        </span>
                        <span class="texto">
                            Seleccionar art&iacute;culo:&nbsp;
                        </span>                        
                </div>
            </td>
            <td>
                <div align="left">			
					<select name="articulo" class="texto" id="articulo">
						<option value="">Elegir art&iacute;culo de librer&iacute;a...</option>
						<%
						foreach (var prod in (IEnumerable<Marsh.Bussines.ArticuloLibreriaBussines>)ViewData["productos"])
						{
						 %>
						<option value="<%=prod.Id %>"><%=prod.Articulo%></option>
						 <%
						}
						%>                        
					</select>                        
                </div>
            </td>
        </tr>           
        <tr>
            <td colspan="2">
                <div align="left">  
                    <br />                     
					<span class="titulo3">
						<strong>3.</strong>
					</span>
					<span class="texto">
						Elegir cantidad en paquetes o unidades del art&iacute;culo seleccionado
					</span>                    
                </div>
            </td>
        </tr>           
        <tr>
            <td valign="baseline" colspan="2">
                <div align="left" style="margin-left: 18px">  
					<br />                        
					<label for="cantidad">Cantidad:</label>                        
					<input type="input" class="texto" id="cantidad" value="0" style="width: 30px" />
					
					&nbsp;&nbsp;
					
					<label for="unidad">Unidad:</label>                        
					<select name="unidad" class="texto" id="unidad">
						<option value="unidades">Unidades</option>
						<option value="paquetes">Paquetes</option>                            
					</select>
				  
					&nbsp;  
				  
					<input id="btn_AgregarProductoLibreria" name="btn_AgregarProductoLibreria" type="submit" class="texto" value="Agregar"/>
				  </form>

				  <br />
				  <span class="lineadivision">&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;&middot;</span>                 
                </div>
            
                <div id="DetallePedido" style="display:none">
                
                    <% using (Ajax.BeginForm("EnviarPedido", new AjaxOptions() { OnBegin = "ajaxValidate", OnSuccess = "Libreria_Resultado", HttpMethod = "POST" })) { %>
                    
                        <table width="500" border="0" align="left" cellpadding="6" cellspacing="1">
                          <tr>
                            <td>
                                <input type="hidden" name="usuario" id="usuario" value="" />
                                <input type="hidden" name="cant_productos" id="cant_productos" value="0" />
                                <input type="hidden" name="aux_cont_productos" id="aux_cont_productos" value="0" />
                                <br />
                                <span class="titulo3">
                                    <strong>Pedido para enviar... </strong>
                                </span>
                                <br />                            
                            </td>
                          </tr>
                          <tr>
                            <td width="495">                    
                                <table id="detalle_pedido" width="100%" border="0" cellspacing="1" cellpadding="0">
                                    <thead>
                                        <tr>
                                            <td width="*" bgcolor="#FFFFFF" class="titulo3"><div align="left">Descripci&oacute;n</div></td>
                                            <td width="72" bgcolor="#FFFFFF" class="titulo3"><div align="left">Cantidad</div></td>
                                            <td width="72" bgcolor="#FFFFFF" class="titulo3"><div align="left">Unidad</div></td>
                                            <td width="20">&nbsp;</td>
                                        </tr> 
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                        </tr>                           
                                    </thead>
                                    <tbody>
                                        <tr><td></td></tr>
                                    </tbody>
                                </table>
                             </td>
                          </tr>
                          <tr>
                            <td>
                                <br />
                                <input id="btn_EnviarPedidoLibreria" name="btn_EnviarPedidoLibreria" type="submit" class="texto" value="Enviar pedido"/>
                            </td>
                          </tr>
                        </table>
                        
                    <% } %>
                
                </div>
                            
            </td>
        </tr>
    </table>

</div>

<div id="mensaje_envio_pedido" style="display:none"></div>

</asp:Content>