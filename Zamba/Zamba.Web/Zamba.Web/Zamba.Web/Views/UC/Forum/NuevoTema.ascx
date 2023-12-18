﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls.Forum.ControlsForumWebUserControl" Codebehind="NuevoTema.ascx.cs" %>

<style type="text/css">
    .style1
    {
        width: 290px;
    }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        
        $(".ajax__tab_tab").height(20);
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        setTimeout("parent.hideLoading();", 500);
        $(".ajax__tab_tab").height(20);
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
        <%--<asp:PostBackTrigger ControlID="btnAddAttach" /> Se comenta hasta que este arreglada la funcionalidad de adjuntar--%>
        <asp:PostBackTrigger ControlID="btnGuardarMensaje" />
        <asp:PostBackTrigger ControlID="btnCerrar" />
        <asp:PostBackTrigger ControlID="btnNotificarGuardar" />
    </Triggers>
    <ContentTemplate>
        <center>
            <div style="width:100%;margin:0px auto">
                <%-- <div style="width: 400; margin-right: 0px;" align="left"> --%>
                <asp:HiddenField ID="hdnSourceDocId" runat="server" />
                <asp:HiddenField ID="hdnSourceMessageId" runat="server" />
                <asp:HiddenField ID="hdnParentMessageId" runat="server" />
                <asp:HiddenField ID="hdnSourceDocTypeId" runat="server" />
                <div id="divForo" style="text-align: center; margin-bottom: 5px" runat="server"  class="col-xs-12">
                    <br />
<%--                    <asp:Label ID="lblMensajeNuevo" runat="server" Text="Asunto" Font-Underline="False" />--%>
                    <div style="text-align: center" runat="server" class="col-xs-6 col-xs-offset-3" >
                        <asp:TextBox ID="txtAsunto" runat="server"  class="form-control" MaxLength="250" Enabled="False" />
                    </div>
                    <br />
                   <%-- <div id="Div2" style="text-align: center" runat="server">
                        <asp:Label ID="lblMensajeNuevo0" runat="server" Text="Mensaje"  />
                    </div>--%>
                    <div id="Div1" style="text-align: center" runat="server" class="col-xs-6 col-xs-offset-3">
                        <asp:TextBox ID="txtMensaje" class="form-control " runat="server" Height="200px"  TextMode="MultiLine" />
                    </div>
                    <br />
                    <center>
<%--                    <div id="Div5" class="col-xs-12" style="text-align: center; background-color: #EDF7FE;" runat="server">
                        <div>
                             <asp:FileUpload ID="FileUpload1" runat="server" CssClass="col-xs-2"  Height="25px" EnableViewState="true"/>
                              <asp:Button ID="btnAddAttach" runat="server" OnClick="btnAddAttach_Click" class="btn btn-info" Text="Adjuntar"/>
                             
                        </div>
                        <table>
                            <tr>
                               <%-- <td style="white-space: nowrap; padding-top: 5px; padding-left: 10px" valign="top" >
                                    Adjuntos:&nbsp;
                                </td>--%> 
           <%--                     <td  style="height: 75px">
                                    <asp:GridView ID="gvAttachs" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="1" Font-Size="X-Small"
                                        AlternatingRowStyle-Font-Underline="false" HeaderStyle-Font-Underline="false"
                                        EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                        Font-Underline="False" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                        RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                        EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false" OnSelectedIndexChanging="gvAttachs_SelectedIndexChanging"
                                        DataKeyNames="AttachPath">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px" Font-Size="X-Small" />
                                        <PagerStyle Font-Underline="False" Wrap="False" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                                        <EditRowStyle Font-Underline="False" Wrap="False" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
                                        <EmptyDataRowStyle Font-Underline="False" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <div >
                            <tr>
                                <td >
                                    <asp:Label ID="lblAttachslength" runat="server" ForeColor="Red" Font-Bold="true"  Text="Restan 5120 KB para archivos adjuntos"></asp:Label>                                
                                </td>
                            </tr>
                            <tr>  
                                <td>
                                    <asp:Button ID="btnRemoveAttachs" runat="server" class="btn btn-info" Text="Quitar adjuntos" OnClick="ClearAttachments" />&nbsp;
                                </td>
                            </tr>
                        </div>
                        <asp:CheckBox ID="chkAddWebLink" runat="server" Text="Agregar link al documento" />
                        <br />
                    </div>--%>
                    </center>
                    <div id="Div4" style="text-align: center; padding-bottom: 5px" runat="server">
                        <asp:Label ID="lblErrors" runat="server" ForeColor="White" Visible="False"></asp:Label>
                    </div>
                    <div class="ActionsHeader">
                        <div class="fg-buttonset ui-helper-clearfix" style="width: 970px;">
                        <asp:LinkButton runat="server"  title="Responder y notificar"   Id="btnNotificarGuardar"    class="btn btn-primary"    OnClick="btnNotificarGuardar_Click" OnClientClick="parent.ShowLoadingAnimation();">Guardar</asp:LinkButton>
                            <asp:LinkButton runat="server"  Id="btnGuardarMensaje"      class="btn btn-default"    OnClick="btnGuardarMensaje_Click" OnClientClick="parent.ShowLoadingAnimation();" Visible="false" >Responder</asp:LinkButton>
                            <asp:LinkButton runat="server"  Id="btnParticipantes"      class="btn btn-default"    OnClick="btnParticipantes_Click" OnClientClick="parent.ShowLoadingAnimation();">Participantes</asp:LinkButton>
	                        <asp:LinkButton runat="server"  Id="btnCerrar"              class="btn btn-default"    OnClick="btnCerrar_Click" OnClientClick="parent.ShowLoadingAnimation();">Cancelar</asp:LinkButton>
                        </div>
                    </div>
                 </div>
                 <div id="divParticipantes" style="text-align: center; margin-bottom: 5px" runat="server" visible="false">
                    <div id="Div3" style="text-align: center" runat="server">
                        <b>
                            <asp:Label ID="LblNoAsignado" runat="server" Text="No Asignados" Font-Underline="False" />
                        </b>
                        <br />
                        <asp:Label ID="lblGroupNotAsig" runat="server" Text="Grupos" Font-Underline="False" />
                        <asp:ListBox ID="lstGroupNotAsig" runat="server" Width="97%" ></asp:ListBox>
                        <asp:Label ID="lblUserNotAsig" runat="server" Text="Usuarios" Font-Underline="False" />
                        <asp:ListBox ID="lstUserNotAsig" runat="server" Width="97%" ></asp:ListBox>
                        
                        <br />
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Asignados" Font-Underline="False" />
                        </b>
                        <br />
                        <br />
                        <center>
                            <asp:LinkButton runat="server" Id="LinkBtnAddPart" class="fg-button ui-state-default ui-corner-all" OnClick="btnAddPart_Click">Agregar</asp:LinkButton>                        
                            <asp:LinkButton runat="server" Id="LinkBtnRemPart" class="fg-button ui-state-default ui-corner-all" OnClick="btnRemPart_Click">Quitar</asp:LinkButton>                        
                        </center>
                        <br />
                        <br />
                        <asp:Label ID="lblGroupAsig" runat="server" Text="Grupos" Font-Underline="False" />
                        <asp:ListBox ID="lstGroupAsig" runat="server" Width="97%" ></asp:ListBox>
                        <asp:Label ID="lblUserAsig" runat="server" Text="Usuarios" Font-Underline="False" />
                        <asp:ListBox ID="lstUserAsig" runat="server" Width="97%" ></asp:ListBox>
                    </div>
                    <center>
                        <div id="Div6" style="text-align: center" runat="server">
                            <asp:LinkButton runat="server"  Id="btnClosePart" class="fg-button ui-state-default ui-corner-all" OnClick="btnClosePart_Click">Cerrar</asp:LinkButton>
                        </div>
                    </center>
                 </div>
            </div>
        </center>
    </ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID="btnAddAttach" /> Se comenta hasta que este arreglada la funcionalidad de adjuntar--%>
    </Triggers>
</asp:UpdatePanel>