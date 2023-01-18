<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCSendMail.ascx.cs" Inherits="Controls_WCSendMail" %>
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <div style="padding: 5px; border: solid black thin;">
            <div id="DivPrincipal" runat="server">
                <table width="1" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <p style="font-size: x-small">
                                <font color="navy">Usuario Actual:</font></p>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCurrentUserName" runat="server" Width="300" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <p style="font-size: x-small">
                                <font color="navy">Email:</font></p>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCurrentUserMail" runat="server" Width="300" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <p style="font-size: x-small">
                                <font color="navy">Email a Enviar:</font></p>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtEmailTo" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <p style="font-size: x-small">
                                <font color="navy">Asunto:</font></p>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtAsunto" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap; padding-top: 5px;" valign="top">
                            <p style="font-size: x-small">
                                <font color="navy">Texto del Mensaje:</font></p>
                        </td>
                        <td style="background-color: #FBEDBB; height: 170">
                            <asp:TextBox ID="txtMessageBody" runat="server" Height="170" TextMode="MultiLine"
                                Width="300" BackColor="#fbedbb"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap; padding-top: 5px;" valign="top">
                            <p style="font-size: x-small">
                                <font color="navy">Adjuntos:</font></p>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="217px" />
                            <asp:Button ID="btnAddAttach" runat="server" Font-Size="X-Small" OnClick="btnAddAttach_Click"
                                Text="adjuntar" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 75">
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
                           <asp:Label ID="lblAttachsLenght" runat="server"  ForeColor="Red" Font-Bold="true"
                            Font-Size="X-Small" Text="restan 5120 KB para archivos adjuntos"></asp:Label>
                        </td>
                        <td style="vertical-align:bottom">
                        <p style="font-size: x-small">
                                <font color="navy">Click para quitar</font></p>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap; padding-top: 15px;">
                            &nbsp;
                        </td>
                        <td align="left">
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
                    <tr>
                        <td valign="top" colspan="2">
                            <p style="font-size: x-small">
                                <b>Nota:</b> los detalles ingresados en esta pagina <b>no</b> seran almacenados
                                o usados para ningun otro proposito mas que para el envio de este mail</p>
                        </td>
                    </tr>
                </table>
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblErrors" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"
                            Font-Size="X-Small" Text="Envio realizado con Exito"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
<%--    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddAttach" />
    </Triggers>
</asp:UpdatePanel>
--%>