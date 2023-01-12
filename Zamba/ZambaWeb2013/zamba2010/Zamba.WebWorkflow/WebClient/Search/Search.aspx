<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="WebClient_Search_Search" Title="Zamba Web Busqueda" UICulture="Auto" Culture="Auto" %>

<%@ Register TagPrefix="ZControls" TagName="DocTypes" Src="~/Controls/Core/WCDocTypesl.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Indexs" Src="~/Controls/Core/WCIndexs.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4
        {
            width: 153px;
        }
        .style5
        {
            width: 338px;
        }
        .style6
        {
            width: 153px;
            height: 26px;
        }
        .style7
        {
            width: 338px;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
          <progresstemplate>
          <center>
          <asp:Table runat="server" ID="tblUpdate" BackColor="#D6EBFC" font-bold="True" forecolor="#336699"
          Height="100%">
          
          <asp:TableRow ID="TableRow1" runat="server">
           <asp:TableCell ID="TableCell1" runat="server">AGUARDE UN MOMENTO POR FAVOR</asp:TableCell></asp:TableRow>
            </asp:Table>
            
            </center>
                        
           </progresstemplate>
           </asp:UpdateProgress>

  <asp:UpdatePanel ID= "updatepanel1" runat="server" UpdateMode="Conditional" >
  <ContentTemplate>
  <div id="NoResults" runat="server" visible="false" style=" background-color:Activeborder;border:solid 1px black;position:absolute;width:45%; top:70%; left:30%; height: 25px; text-align:center; vertical-align:middle;font-family: 'Arial Black'; font-size: large; font-weight: bold; font-style: normal; font-variant: normal; text-transform: none; color: #FF0000; text-decoration: underline overline">
    <asp:LinkButton ID="lnkNoResults" runat="server" ForeColor="Red" onclick="LinkButton1_Click">NO SE ENCONTRARON RESULTADOS</asp:LinkButton>
  </div>
    <table style="width: 1000px; height: 500px;">
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 350px; height: 400px" valign="top">
                <ZControls:DocTypes runat="server" ID="DocTypesControl" />
            </td>
            <td style="width: 650px; height: 400px" valign="top" style="background-color:#D6EBFC">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <ZControls:Indexs runat="server" ID="IndexsControl" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            <fieldset class="FieldSet" style="background-color:#CCCCCC">
                <table>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblTextSearch" runat="server" Text="B�squeda de Texto" 
                                Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="txtTextSearch" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                             <asp:ImageButton runat="server" ID="btnSearch" Height="22px" 
                                OnClick="btnSearch_Click" Width="132px" ImageUrl="~/images/p_menubutton1.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblSearchNotesAndForum" runat="server" 
                                Text="B�squeda de Notas y Foro" Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:TextBox ID="txtSearchNotesAndForum" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnClearIndexs" Text="Limpiar índices" height="22" Width="132" OnClick="btnClearIndexs_Click" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="style6">
                            <asp:Label ID="lblSearchDocumentary" runat="server" Text="B�squeda Documental" 
                                Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:TextBox ID="txtSearchDocumentary" runat="server" Width="350px" 
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="style4">
                            
                        </td>
                        <td class="style5">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300" Text="No se encontraron documentos" Visible="False"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                </fieldset>
            </td
        </tr>
    </table>
  </ContentTemplate>  
</asp:UpdatePanel>
</asp:Content>
