<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="Insert.aspx.cs" Inherits="WebClient_Insert_Insert" Title="Zamba Web Insertar" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register TagPrefix="ZControls" TagName="Indexs" Src="~/Controls/Core/WCIndexs.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="InsertFindDocument" Src="~/Controls/Insert/WCInsert.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="InsertNewOffice" Src="~/Controls/Insert/NewOffice/NewOfficeSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Forms" Src="~/Controls/Insert/Forms/NewFormSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Templates" Src="~/Controls/Insert/Templates/TemplatesListSelector.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            width: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    <table>
    
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                                Height="200px" Width="351px">
                                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Archivo" 
                                    Width="330px">
                                    <HeaderTemplate>
                                        Archivo
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="padding-left: 15px;">
                                            <zcontrols:insertfinddocument id="UCInsertFindDocument" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" 
                                    HeaderText="Documento Office" Width="330px">
                                    <ContentTemplate>
                                        <div style="padding-left: 15px;">
                                            <zcontrols:insertnewoffice id="UCInsertNewOffice" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" 
                                    HeaderText="Formulario Virtual" Width="330px">
                                    <ContentTemplate>
                                        <div style="padding-left: 15px;">
                                            <zcontrols:forms id="InsertVirtualForm" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Plantilla" 
                                    Width="330px">
                                    <ContentTemplate>
                                        <div style="padding-left:65px;width:60px;height:200px;overflow:auto">
                                            <zcontrols:templates id="InsertTemplate" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>                            
                            <fieldset runat="server" style="height:230px;overflow: auto">                                
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ZControls:Indexs runat="server" ID="IndexsControl" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>                                
                            </fieldset>                            
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:100%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset runat="server" style="width: 624px; height: 475px">
                            <legend>Vista del Documento.</legend>
                            <table>
                                <tr>
                                    <td>
                                        <center>
                                            <iframe id="View" runat="server" width="614px" height="425px"></iframe>
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <div style="padding-left:447px">
                                    <asp:Button ID="SaveInsert" runat="server" Text="Insertar Documento" 
                                            onclick="SaveInsert_Click" />
                                    </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

