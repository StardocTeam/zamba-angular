<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCSendMail.ascx.cs" Inherits="WCSendMail" %>

<script type="text/javascript">

    function Cerrar_Click() {
        window.close();
    }

    $(document).ready(function () {
        $("#<%=btnSendMail.ClientID %>").click(function () {
            ShowLoadingAnimation();
        });
    });

    $(window).load(function () {
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
                    <div style="padding: 5px;" class= "ui-state-default ui-corner-top" >       
                        <div id="DivPrincipal" runat="server">
                            <table style="border:0;width:100%">
                                <tr>
                                    <td style="white-space: nowrap;width:80px">
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
                                </tr>
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
                                       <asp:Label ID="lblAttachsLenght" runat="server"  ForeColor="Red" Font-Bold="true"
                                        Font-Size="X-Small" Text="Restan 5120 KB para archivos adjuntos"></asp:Label>
                                    </td>
                                </tr>
                                -->
                                <tr>
                                    <td style="white-space: nowrap; padding-top: 15px;">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <!--<br /> 
                                        <asp:CheckBox ID="chkAddOriginalDocument" runat="server" Font-Bold="False" 
                                            Font-Size="X-Small" Font-Strikeout="False" Text="Adjuntar documento original" />                     
                                        <br /> 
                                        !-->
                                        <asp:CheckBox ID="chkAddWebLink" runat="server" Font-Bold="False" 
                                            Font-Size="X-Small" Font-Strikeout="False" Checked="true" Text="Agregar link al documento" />  
                                        <br />
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSendMail" runat="server" Text="Enviar" OnClick="btnSendMail_Click" class="ui-state-default ui-corner-top" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancelMail" runat="server" Text="Cerrar" OnClick="btnCancelMail_Click" class="ui-state-default ui-corner-top" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel id="pnlErrors" runat="server" Visible="false">
                                        <asp:Label ID="lblErrors" runat="server" Font-Size="Small" Font-Names="Tahoma" Text="El envio se ha realizado con exito">
                                        </asp:Label>
                                        <br />
                                        <input type="button" id="btnCerrar" onclick="javascript:Cerrar_Click();" value="Cerrar" class= "ui-state-default ui-corner-top"/>
                                    </asp:Panel>
                                </td>
                                <td>
                                &nbsp;</td>
                            </tr>
                        </table> 
                    </div>
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
                                       <asp:Label ID="lblAttachsLenght" runat="server"  ForeColor="Red" Font-Bold="true"
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
