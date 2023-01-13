<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCSendMail" Codebehind="WCSendMail.ascx.cs" %>

    <link rel="stylesheet" type="text/css" href="../Content/styles/normalize.css" />
    <link rel="stylesheet" type="text/css" href="../Content/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../Content/themes/base/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap-theme.css" />
<%--    <link rel="stylesheet" type="text/css" href="../Content/HomeWidget.css" />--%>
    <link rel="stylesheet" type="text/css" href="../Content/toastr.css" />
    <link rel="stylesheet" type="text/css" href="../Scripts/ng-embed/ng-embed.min.css" />
    <link rel="stylesheet" type="text/css" href="../Content/partialSearchIndexs.css?v=248" />
    <link rel="stylesheet" type="text/css" href="../GlobalSearch/search/Filters.css" />
    <link rel="stylesheet" type="text/css" href="../Content/bootstrap.min.css" />

    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
<script src="../Scripts/jquery-2.2.2.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.min.js"></script>
 

<script type="text/javascript">

    function Cerrar_Click() {
        window.close();
    }

    $(document).ready(function () {
        $("#<%=btnSendMail.ClientID %>").click(function () {
            ShowLoadingAnimation();
        });
    });

    $(window).on("load",function () {
        hideLoading();
    });

</script>
<%--<br />
<div align="center" >
<ajaxToolKit:TabContainer ID="TabContainer" runat="server" AutoPostBack="true"  
    ActiveTabIndex="0" Width="800px">
    <ajaxToolKit:TabPanel ID="TabDocumento" runat="server" HeaderText="Enviar por email">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                <form id="loginForm" method="post" class="form-horizontal" action="none">
                    <div style="padding: 5px;" >       
                        <div id="DivPrincipal" runat="server">
                            <div id="ContPrincipal" runat="server" class="marginform">
                                <div >
                                    <label class="marginlabel"> Para:</label>
                                    <asp:TextBox ID="txtEmailTo" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                        <%--<span class="help-block"></span>--%>
                                </div>
                                <div>
                                    <label class="marginlabel"> CC:</label>
                                    <asp:TextBox ID="txtCC" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <label class="marginlabel"> CCO:</label>
                                    <asp:TextBox ID="txtCCO" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <label class="marginlabel"> Asunto:</label>
                                    <asp:TextBox ID="txtAsunto" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                </div>
                                    <div>
                                    <label class="marginlabel"> Mensaje:</label>
                                    <asp:TextBox ID="txtMessageBody" runat="server" Height="97" TextMode="MultiLine"
                                                Width="100%" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                        <asp:CheckBox ID="chkAddWebLink" runat="server" Font-Bold="False" 
                                                Font-Size="X-Small" Font-Strikeout="False" Checked="true" Text="Agregar link al documento" CssClass="LinkDocument" />  
                                </div>
                              <%--  <div title="Se enviará con correo de usuario.">
                                        <asp:CheckBox ID="chkGemericOrUserMessage" runat="server" Font-Bold="False" 
                                                Font-Size="X-Small" Font-Strikeout="False" Checked="true" Text="Agregar Remitente" CssClass="LinkDocument" />  
                                </div>--%>
                           </div>


                            <%--<table style="border:0;width:100%">
                                <tr>
                                    <td style="white-space: nowrap;width:80px" class="DivPrincipalabel">
                                       Para:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailTo" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;width:80px">
                                       CC:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCC" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;width:80px">
                                       CCO:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCCO" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        Asunto:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAsunto" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; padding-top: 5px;">
                                       Mensaje:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMessageBody" runat="server" Height="170" TextMode="MultiLine"
                                            Width="500"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <!--
                                <tr>
                                    <td style="white-space: nowrap; padding-top: 5px;" valign="top" align="right">
                                        Adjuntar:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="250px"/>
                                        <asp:Button ID="btnAddAttach" runat="server" Font-Size="Small" OnClick="btnAddAttach_Click"
                                            Text="Agregar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle">
                                        Adjuntos:
                                    </td>
                                    <td style="height: 75" valign="middle">
                                        <asp:GridView ID="gvAttachs" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
                                            AlternatingRowStyle-Font-Underline="false" HeaderStyle-Font-Underline="false"
                                            EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                            Font-Underline="False" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                            RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                            EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false"  
                                            onselectedindexchanging="gvAttachs_SelectedIndexChanging" 
                                            DataKeyNames="AttachPath">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px"
                                                Font-Size="X-Small" />
                                            <PagerStyle Font-Underline="False" Wrap="False" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                Font-Size="X-Small" />
                                            <EditRowStyle Font-Underline="False" Wrap="False" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
                                            <EmptyDataRowStyle Font-Underline="False" />
                                        </asp:GridView>
                                    </td>
                                    
                                    <td style="vertical-align:bottom">
                                    <p style="font-size: x-small">
                                            <font color="navy">Click para quitar</font></p>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="2">
                                       <asp:Label ID="lblAttachslength" runat="server"  ForeColor="Red" Font-Bold="true"
                                        Font-Size="X-Small" Text="Restan 5120 KB para archivos adjuntos"></asp:Label>
                                    </td>
                                </tr>
                                -->
                                <tr>
                                    <%--<td style="white-space: nowrap; padding-top: 15px;">
                                        &nbsp;
                                    </td>--%>
                                    <td>
                                        <!--<br /> 
                                        <asp:CheckBox ID="chkAddOriginalDocument" runat="server" Font-Bold="False" 
                                            Font-Size="X-Small" Font-Strikeout="False" Text="Adjuntar documento original" />                     
                                        <br /> 
                                        !-->
                                       
                                        <div class="btnform col-md-6">
                                            <asp:Button ID="btnSendMail" runat="server" Text="Enviar" OnClick="btnSendMail_Click" class="btn btn-default btnsep" />
                                            <%--<<%--asp:Button ID="btnCancelMail" runat="server" Text="Cerrar" OnClick="btnCancelMail_Click" class="btn btn-default" />--%>
                                            <input type="button" id="btnCerrar" onclick="javascript: Cerrar_Click();" value="Cerrar" class="btn btn-default">
                                     
                                              
                                                        <asp:Panel id="pnlErrors" runat="server" Visible="false">
                                                            <asp:Label ID="lblErrors" runat="server" Font-Size="Small" Font-Names="Tahoma" Text="El envio se ha realizado con exito">
                                                            </asp:Label>
                                                            <br />
                                                            <%--<input type="button" id="btnCerrar" onclick="javascript: Cerrar_Click();" value="Cerrar" class= "btn btn-default"/>--%>
                                                        </asp:Panel>
                                                   

                                                </div>    
                                        
                                    </td>
                                </tr>
                            <%--</table>--%>
                        </div>
                        
                      
                    </div>
     <%--               </form>
                    <div id="idcerrar" style="padding: 5px; display: none">       
                       <table >
                            <tbody><tr>
                                <td>
                                    <div id="Arbol_pnlErrors">
	
                                        <span id="Arbol_lblErrors" style="font-family:Tahoma;font-size:Small;">Envio de mail cancelado</span>
                                        <br>
                                        <input type="button" id="btnCerrar" onclick="javascript: Cerrar_Click();" value="Cerrar" class="btn btn-default">
                                    </div>
                                </td>
                                <td>
                                &nbsp;</td>
                            </tr>
                        </tbody></table> 
                    </div>--%>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAddAttach" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </ajaxToolKit:TabPanel>  --%>
    
  <%--  <asp:TabPanel runat="server" HeaderText="Enviar por email" ID="TabDocumento"><ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="padding: 5px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErrors" runat="server" Visible="false"  
                                        Font-Size="Small" Font-Names="Tahoma" Text="El envio se ha realizado con exito"></asp:Label>
                                </td>
                                <td>
                                &nbsp;</td>
                            </tr>
                        </table>        
                        <div id="DivPrincipal" runat="server">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                <!--
                                <tr>
                                    <td style="white-space: nowrap;" valign="top" width="120">
                                        <p style="font-size: small">
                                           Usuario Actual:
                                        </p>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCurrentUserName" runat="server" Width="500" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;" valign="top">
                                        Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrentUserMail" runat="server" Width="500" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                -->
                                <tr>
                                    <td style="white-space: nowrap;" valign="top" align="right" width="80">
                                       Para:
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtEmailTo" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;" valign="top" align="right">
                                        Asunto:
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtAsunto" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; padding-top: 5px;" valign="top" align="right">
                                       Mensaje:
                                    </td>
                                    <td  height="170">
                                        <asp:TextBox ID="txtMessageBody" runat="server" Height="170" TextMode="MultiLine"
                                            Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; padding-top: 5px;" valign="top" align="right">
                                        Adjuntar:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="250px" />
                                        <asp:Button ID="btnAddAttach" runat="server" Font-Size="Small" OnClick="btnAddAttach_Click"
                                            Text="Adjuntar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                     <br /> <br />
                                        Adjuntos:
                                    </td>
                                    <td style="height: 75" >
                                        <br /> <br />
                                        <asp:GridView ID="gvAttachs" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
                                            AlternatingRowStyle-Font-Underline="false" HeaderStyle-Font-Underline="false"
                                            EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                            Font-Underline="False" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                            RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                            EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false"  
                                            onselectedindexchanging="gvAttachs_SelectedIndexChanging" 
                                            DataKeyNames="AttachPath">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px"
                                                Font-Size="X-Small" />
                                            <PagerStyle Font-Underline="False" Wrap="False" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                Font-Size="X-Small" />
                                            <EditRowStyle Font-Underline="False" Wrap="False" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
                                            <EmptyDataRowStyle Font-Underline="False" />
                                        </asp:GridView>
                                       <asp:Label ID="lblAttachslength" runat="server"  ForeColor="Red" Font-Bold="true"
                                        Font-Size="X-Small" Text="Restan 5120 KB para archivos adjuntos"></asp:Label>
                                    </td>
                                    <!--
                                    <td style="vertical-align:bottom">
                                    <p style="font-size: x-small">
                                            <font color="navy">Click para quitar</font></p>
                                    </td>
                                    -->
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap; padding-top: 15px;">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <br /> 
                                        <asp:CheckBox ID="chkAddOriginalDocument" runat="server" Font-Bold="False" 
                                            Font-Size="X-Small" Font-Strikeout="False" Text="Adjuntar documento original" />                                     
                                        <br /> 
                                        <asp:CheckBox ID="chkAddWebLink" runat="server" Font-Bold="False" 
                                            Font-Size="X-Small" Font-Strikeout="False" Text="Agregar link al documento" />
                                        <br />
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSendMail" runat="server" Text="Enviar" OnClick="btnSendMail_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancelMail" runat="server" Text="Cerrar" OnClick="btnCancelMail_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAddAttach" />
                </Triggers>
            </asp:UpdatePanel>
      
        
</ContentTemplate>
</asp:TabPanel>--%>
<%--</ajaxToolKit:TabContainer>
</div>
<br />
<br />
<hr />--%>

<%--<script>
    $(document).ready(function () {
        console.log("estoy aca");
        $("#loginForm").validate({
            rules: {
                "txtEmailTo": {
                    required: true,
                }
                , "txtCC": {
                    required: true,
                }
                
            }
            , messages: {
                "txtEmailTo": {
                    required: "Debe ingresar un tipo de cliente"
                }
                , "txtCC": {
                    required: "Debe ingresar sus Rif"
                }

            }
        });
    });
</script>--%>
