<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prueba.aspx.cs" Inherits="Prueba" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<form id="form1" runat="server">
<asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
<asp:updatepanel id="UpdPnlMultiView" runat="server" updatemode="Conditional">
    
    <ContentTemplate>
    
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="View1" runat="server">
            
                <asp:Button runat="server" Text="Pasar a vista 2" onclick="Unnamed1_Click"  ></asp:Button>
            
            </asp:View>
            
            <asp:View ID="View2" runat="server">
            
                <asp:Table ID="Table1" runat="server" Width="259px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblSubject" runat="server" Text="Asunto" Font-Underline="False" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox ID="txtSubject" runat="server" Height="25px" Width="300" MaxLength="32" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblBody" runat="server" Text="Mensaje" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox ID="txtBody" runat="server" Height="150px" Width="300" TextMode="MultiLine" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
<%--            <asp:Button ID="btnNotifyAndSave" runat="server" Text="Notificar y Guardar" />
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeCollapsePanel" runat="server" TargetControlID="pnlSendMail"
                CollapsedSize="0" ExpandedSize="800" Collapsed="True" ExpandControlID="btnNotifyAndSave"
                CollapseControlID="btnNotifyAndSave" AutoCollapse="False" AutoExpand="False"
                ExpandDirection="Vertical">
            </ajaxToolkit:CollapsiblePanelExtender>--%>
            <asp:Panel ID="pnlSendMail" runat="server">
                <table width="1" cellpadding="2" cellspacing="0" border="0">
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <asp:Label ID="lblCurrentUserName" runat="server" Text="Usuario Actual:" Visible="true"></asp:Label>
                            <%--                                                            <p style="font-size: x-small">
                                                                <font color="navy">Usuario Actual:</font></p>--%>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCurrentUserName" runat="server" Width="300" ReadOnly="True" Visible="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <asp:Label ID="lblCurrentUserMail" runat="server" Text="Email:" Visible="true"></asp:Label>
                            <%--                                                            <p style="font-size: x-small">
                                                                <font color="navy">Email:</font></p>--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCurrentUserMail" runat="server" Width="300" ReadOnly="True" Visible="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;" valign="top">
                            <asp:Label ID="lblEmailTo" runat="server" Text="Email a Enviar:" Visible="true"></asp:Label>
                            <%--                                                            <p style="font-size: x-small">
                                                                <font color="navy">Email a Enviar:</font></p>--%>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtEmailTo" runat="server" Width="300" Visible="true"></asp:TextBox>
                        </td>
                    </tr>
<%--                    <tr>
                        <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" Visible="true" />
                    </tr>--%>
                    <tr>
                        <td style="height: 75">
                            <asp:GridView ID="gvAttachs" runat="server" Visible="true" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                Font-Size="X-Small" AlternatingRowStyle-Font-Underline="false" HeaderStyle-Font-Underline="false"
                                EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
                                Font-Underline="False" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
                                RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
                                EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false" 
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
                            <asp:Label ID="lblAttachsLenght" runat="server" ForeColor="Red" Font-Bold="true"
                                Font-Size="X-Small" Text="restan 5120 KB para archivos adjuntos" Visible="true"></asp:Label>
                        </td>
                        <td style="vertical-align: bottom">
                            <asp:Label ID="lblClickForRemove" runat="server" ForeColor="Red" Font-Bold="true"
                                Font-Size="X-Small" Text="Click para quitar" Visible="true"></asp:Label>
                            <%--                                                            <p style="font-size: x-small">
                                                                <font color="navy">Click para quitar</font></p>--%>
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
                                        <asp:Button ID="btnSendMail" runat="server" Text="Enviar" OnClick="btnSendMail_Click"
                                            Visible="true" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelMail" runat="server" Text="Cerrar" OnClick="btnCancelMail_Click"
                                            Visible="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblErrors" runat="server" Visible="true" ForeColor="Red" Font-Bold="true"
                    Width="400" Size="X-Small" Text="El mail se envio con éxito"></asp:Label>
            </asp:Panel>
            </tr>
            
            <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" Visible="true" 
                                  text_Add = "Agregar archivo" text_Delete="Eliminar archivo" text_Uploading="Subiendo archivo" text_X="Ocultar"
             />
            
        </ContentTemplate>
    </asp:UpdatePanel>
            </asp:View>
        
        </asp:MultiView>
    
    </ContentTemplate>
   
    </asp:updatepanel>
</form>
