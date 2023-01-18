<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true"
    CodeFile="Results.aspx.cs" Inherits="WebClient_Results_Results" Title="Zamba Web Resultados"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ZControls" TagName="Results" Src="~/Controls/Core/WCResults.ascx" %>
<%--<%@ Register TagPrefix="ZControls" TagName="NewResults" Src="~/Controls/Core/NewWCResults.ascx" %>--%>
<%@ Register TagPrefix="ZControls" TagName="Indexs" Src="~/Controls/Core/WCIndexs.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Asociated" Src="~/Controls/Asociated/WCDocumentsAsociated.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Forum" Src="~/Controls/Forum/WCForum.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="OfficeViewer" Src="~/Controls/Viewer/WCOfficeviewer.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="DocRelated" Src="~/Controls/Related/WCDocumentsRelated.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="InsertFindDocument" Src="~/Controls/Insert/WCInsert.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="InsertNewOffice" Src="~/Controls/Insert/NewOffice/NewOfficeSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Forms" Src="~/Controls/Insert/Forms/NewFormSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Templates" Src="~/Controls/Insert/Templates/TemplatesListSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="SendMail" Src="~/Controls/Notifications/WCSendMail.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="MailList" Src="~/Controls/Notifications/WCMailList.ascx" %>
<%--<%@ Register TagPrefix="MyUC" TagName="NuevoMensaje" Src="~/Controls/Forum/NuevoTema.ascx" %>--%>
<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<%@ Register TagPrefix="MyUC" TagName="NuevoMensaje" Src="~/Controls/Forum/NuevoTema.ascx" %>--%>
    <input id="asd" runat="server" type="hidden" />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <center>
                <asp:Table runat="server" ID="tblUpdate" BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699"
                    Height="100%">
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell1" runat="server">AGUARDE UN MOMENTO POR FAVOR</asp:TableCell></asp:TableRow>
                </asp:Table>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="padding-left: 8px">
        <table style="width: 1000px; height: 500px;">
            <tr>
                <td valign="top" style="width: 350px;">
                    <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="350px">
                        <Panes>
                            <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server" Width="350px">
                                <Header>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <a href="" class="accordionLink">Informacion de Documento</a>
                                            </td>
                                            <td align="right">
                                                <img height="10px" width="10px" alt="Expandir/Contraer" src="../../images/arrow_up_green.ico" />
                                                <br />
                                                <img height="10px" width="10px" alt="Expandir/Contraer" src="../../images/arrow_down_green.ico" />
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </Header>
                                <Content>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div style="padding-left: 2px">
                                                <asp:Label runat="server" ID="CurrentTab" Visible="false" />
                                                <asp:Label runat="server" ID="Messages" Visible="false" />
                                                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="400px" Width="330px" ActiveTabIndex="0">
                                                    <ajaxToolkit:TabPanel runat="server" ID="PanelIndexs" HeaderText="Indices" Width="330px" EnableViewState="true">
                                                        <ContentTemplate>
                                                            <ZControls:Indexs runat="server" ID="IndexsControl" EnableViewState="true" />
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel runat="server" ID="PanelForo" HeaderText="Foro" Width="330px">
                                                        <ContentTemplate>
                                                            <ZControls:Forum runat="server" ID="ForumControl" />
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <%--</ajaxToolkit:TabPanel>--%>
                                                    <%--  <ajaxToolkit:TabPanel runat="server" ID="PanelAsociated" HeaderText="Asociados" Width="330px">
                                                        <ContentTemplate>
                                                            <ZControls:Results ID="AsociatedResults" runat="server" OnOnSelectedResultChanged="ResultsGrid_OnSelectedResultChanged"
                                                                OnOnReloadValues="ResultsGrid_ReloadValues" />
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>--%>
                                                    <ajaxToolkit:TabPanel runat="server" ID="Tabhistorial" HeaderText="Historial" Width="330px">
                                                        <ContentTemplate>
                                                            <%--<ZControls:DocRelated runat="server" ID="DocRelatedControl" />--%>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                </ajaxToolkit:TabContainer>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </Content>
                            </ajaxToolkit:AccordionPane>
                            <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server" Width="340px">
                                <Header>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <a href="" class="accordionLink">Favoritos</a>
                                            </td>
                                            <td align="right">
                                                <img height="10px" width="10px" alt="Expandir/Contraer" src="../../images/arrow_up_green.ico" />
                                                <br />
                                                <img height="10px" width="10px" alt="Expandir/Contraer" src="../../images/arrow_down_green.ico" />
                                            </td>
                                            <td style="width: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </Header>
                                <Content>
                                </Content>
                            </ajaxToolkit:AccordionPane>
                        </Panes>
                    </ajaxToolkit:Accordion>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="UpdPnlMultiView" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View1" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><ContentTemplate><ajaxToolkit:TabContainer runat="server" ID="TabContResults" Height="560px" Width="650px"
                                                ActiveTabIndex="0"><ajaxToolkit:TabPanel runat="server" HeaderText="Resultados" ID="Resultados"><HeaderTemplate>Resultados</HeaderTemplate><ContentTemplate><asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"><ContentTemplate><div id="Div1" style="width: 500px" runat="server"><div><asp:Table ID="ToolbarTable" runat="server" Style="background-position: left top;
                                                                            background-image: url('file:///D:/Zamba2008/Zamba.WebWorkflow/../../images/impresora.png');
                                                                            background-repeat: repeat-y; background-attachment: fixed;"><asp:TableRow><asp:TableCell> <asp:ImageButton ID="imgbtnExportarExcel" runat="server" Height="37px" ImageUrl="~/images/excel.jpg"
                                                                                        ToolTip="Exportar a Excel" Width="45px" /></asp:TableCell><asp:TableCell> <asp:HyperLink ID="imgbtnImprimir" Target="_blank" runat="server" Height="36px" ImageUrl="~/images/PrintToolbar.png"
                                                                                        ToolTip="Imprimir documento actual" NavigateUrl="~/WebClient/Results/PrintDocument.aspx"></asp:HyperLink></asp:TableCell><asp:TableCell> <asp:HyperLink ID="imgbtnEnviarPorMail" Target="_blank" runat="server" Height="42px"
                                                                                        ImageUrl="~/images/MailToolbar.png" ToolTip="Enviar por mail documento actual"
                                                                                        Width="47px" NavigateUrl="~/WebClient/Results/SendMail.aspx" /></asp:TableCell><asp:TableCell> <asp:HyperLink ID="imgbtnIncorporarDocumento" runat="server" Height="37px" Width="42px"
                                                                                        ToolTip="Incorporar nuevo documento a la carpeta" Target="_parent" ImageUrl="~/images/folder_add.png"
                                                                                        NavigateUrl="~/WebClient/Results/Results.aspx"></asp:HyperLink></asp:TableCell></asp:TableRow></asp:Table></div><ZControls:Results runat="server" ID="ResultsGrid" OnOnSelectedResultChanged="ResultsGrid_OnSelectedResultChanged"
                                                                        OnOnReloadValues="ResultsGrid_ReloadValues" /></div></ContentTemplate></asp:UpdatePanel></ContentTemplate></ajaxToolkit:TabPanel><ajaxToolkit:TabPanel runat="server" HeaderText="Asociados" ID="TabPanel5"><ContentTemplate><div id="Div2" style="width: 500px" runat="server"><ZControls:Results ID="AsociatedResults" runat="server" OnOnSelectedResultChanged="ResultsGrid_OnSelectedResultChanged"
                                                                OnOnReloadValues="ResultsGrid_ReloadValues" /></div></ContentTemplate></ajaxToolkit:TabPanel></ajaxToolkit:TabContainer></ContentTemplate></asp:UpdatePanel>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSubject" runat="server" Text="Asunto" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSubject" runat="server" Height="25px" Width="475" MaxLength="32"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMessage" runat="server" Text="Mensaje" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtMessage" runat="server" Height="150px" Width="475" TextMode="MultiLine" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Guardar" 
                                                            Style="height: 26px" Font-Size="Small" Height="16px" Width="65px" onclick="btnSave_Click" 
                                                     />
                                                <asp:Button ID="btnNotifyAndSave" runat="server" Text="Notificar y Guardar" 
                                                    />
                                                <asp:Button  ID="btnCancel" runat="server" Text="Cancelar" 
                                                             Style="height: 26px" Font-Size="Small" Height="16px" 
                                                    onclick="btnCancel_Click"/> 
                                            </td>
                                        </tr>
                                    </table>
                                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeCollapsePanel" runat="server" TargetControlID="pnlSendMail"
                                        CollapsedSize="0" ExpandedSize="800" Collapsed="True" ExpandControlID="btnNotifyAndSave"
                                        CollapseControlID="btnNotifyAndSave" AutoCollapse="False" AutoExpand="False"
                                        ExpandDirection="Vertical"></ajaxToolkit:CollapsiblePanelExtender>
                                    <asp:Panel ID="pnlSendMail" runat="server">
                                     <table width="1" cellpadding="2" cellspacing="0" border="0">
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCurrentUserMail" runat="server" Text="Email:" Visible="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCurrentUserMail" runat="server" Width="300" ReadOnly="True" Visible="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap;" valign="top">
                                                <asp:Label ID="lblCurrentUserName" runat="server" Text="Usuario Actual:" Visible="true"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCurrentUserName" runat="server" Width="300" ReadOnly="True" Visible="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="white-space: nowrap;" valign="top">
                                                <asp:Label ID="lblEmailTo" runat="server" Text="Email a Enviar:" Visible="true"></asp:Label>
                                                <asp:Button ID="btnMails" runat="server" onclick="btnMails_Click" Text="..." />
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtEmailTo" runat="server" Width="300" Visible="true"></asp:TextBox>
                                            </td>
                                        </tr
                                        <tr>
                                        <tr>
                                            <td style="height: 75;">
                                                <asp:GridView ID="gvAttachs" runat="server" 
                                                    AlternatingRowStyle-Font-Underline="false" AlternatingRowStyle-Wrap="false" 
                                                    AutoGenerateColumns="False" BackColor="White" BorderColor="White" 
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="AttachPath" 
                                                    EditRowStyle-Font-Underline="false" EditRowStyle-Wrap="false" 
                                                    EmptyDataRowStyle-Font-Underline="false" Font-Size="X-Small" 
                                                    Font-Underline="False" FooterStyle-Font-Underline="false" 
                                                    HeaderStyle-Font-Underline="false" PagerStyle-Font-Underline="false" 
                                                    PagerStyle-Wrap="false" RowStyle-Font-Underline="false" RowStyle-Wrap="false" 
                                                    SelectedRowStyle-Wrap="false" Visible="true">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="X-Small" 
                                                        ForeColor="White" />
                                                    <RowStyle BackColor="#F7F6F3" BorderStyle="None" BorderWidth="1px" 
                                                        Font-Size="X-Small" ForeColor="#333333" />
                                                    <PagerStyle Font-Underline="False" Wrap="False" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="X-Small" 
                                                        ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="X-Small" 
                                                        ForeColor="White" HorizontalAlign="Center" />
                                                    <EditRowStyle Font-Underline="False" Wrap="False" />
                                                    <AlternatingRowStyle BackColor="White" Font-Size="X-Small" 
                                                        ForeColor="#284775" />
                                                    <EmptyDataRowStyle Font-Underline="False" />
                                                </asp:GridView>
                                                <asp:Label ID="lblAttachsLenght" runat="server" Font-Bold="true" 
                                                    Font-Size="X-Small" ForeColor="Red" 
                                                    Text="restan 5120 KB para archivos adjuntos"></asp:Label>
                                            </td>
                                            <td style="vertical-align: bottom">
                                                <p style="font-size: x-small">
                                                    <font color="navy">Click para quitar</font></p>
                                            </td>
                                            <tr>
                                                <td style="white-space: nowrap; padding-top: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnSendMail" runat="server" onclick="btnSendMail_Click" 
                                                                    Text="Enviar" Visible="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <p style="font-size: x-small">
                                                        <b>Nota:</b> los detalles ingresados en esta pagina <b>no</b> seran almacenados 
                                                        o usados para ningun otro proposito mas que para el envio de este mail</p>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                        <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" MaxFiles="10" showDeletedFilesOnPostBack="true" File_RenameIfAlreadyExists="true" Directory_CreateIfNotExists="true" runat="server" Visible="true" text_Add="Agregar archivo"
                                                              text_Delete="Eliminar archivo" text_Uploading="Subiendo archivo" text_X="Ocultar" />
                                        <asp:Label ID="lblErrors" runat="server" Visible="false" ForeColor="Red" Font-Bold="true"
                                                   Font-Size="X-Small" Text=""></asp:Label>
                                    </asp:Panel>
                                </asp:View>
                                
                              <asp:View ID="view3" runat="server">
                                <asp:Panel ID="pnlMailList" runat="server">
                                    <ZControls:MailList ID="ucMailList" runat="server" />
                                </asp:Panel>
                              </asp:View>                              
                            </asp:MultiView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table style="width: 1000px; height: auto;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <ZControls:OfficeViewer runat="server" ID="OfficeViewer" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel runat="server" ID="UpPop" UpdateMode="Conditional">
    <ContentTemplate>
                    <div>
                    <asp:Panel ID="pnlPopUpModal" runat="server" Visible="false">
                        <div style="width: 100%; height: 100%; background-color: Silver; z-index: 100; position: fixed;
                            top: 0; left: 0; " > <%--filter:alpha(opacity=50);--%>
                        </div>
                        <div style="z-index: 1000; top: 30%; background-color: White; border: solid 1px black;
                            position: fixed; width: 250px; left: 40%;">
                            <table style="width: 94%">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; height: 50px; width: 240px; text-align: center; padding-left: 1px;">
                                        <asp:label runat="server" Text="¿Desea guardar los cambios realizados?"></asp:label></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <center>
                                        <asp:Button runat="server" ID="btnAccept" Visible="true" Text="Aceptar" OnClick="btnAccept_Click" />
                                        <asp:Button runat="server" ID="btnCancelChg" Visible="true" Text="Cancelar" OnClick="btnCancelChg_Click" />
                                        </center>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                <div>
                    <asp:Panel ID="pSendMailok" runat="server" Visible="false">
                        <div style="width: 100%; height: 100%; background-color: Silver; z-index: 100; position: fixed;
                            top: 0; left: 0; " > <%--filter:alpha(opacity=50);--%>
                        </div>
                        <div style="z-index: 1000; top: 30%; background-color: White; border: solid 1px black;
                            position: fixed; width: 250px; left: 40%;">
                            <table style="width: 94%">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; height: 25px; width: 240px; text-align: center; padding-left: 1px;">
                                        <asp:label ID="Label1" runat="server" Text="El mail se envío con éxito"></asp:label></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <center>
                                        <asp:Button runat="server" ID="btnSendMailOk" Visible="true" Text="Aceptar" OnClick="btnSendMailOk_Click" />
                                        </center>
                                    </td>
                                </tr>
                             
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                
                
        
        
</asp:Content>
