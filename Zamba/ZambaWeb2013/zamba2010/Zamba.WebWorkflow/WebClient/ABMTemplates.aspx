<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="ABMTemplates.aspx.cs" Inherits="WebClient_ABMTemplates" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table>

        <tr>

            <td>

                <asp:Panel runat="server">
                
                    <asp:Table runat="server" Height="559px" style="margin-top: 0px" Width="315px">
                    
                        <asp:TableRow>
                        
                            <asp:TableCell>
                            
                                <asp:Table runat="server">
                            
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTemplates" runat="server" Text="Templates"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                            
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label4" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="lblTemplate" runat="server" Text="Template"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label5" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtPath" width="226px" runat="server"></asp:TextBox>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label2" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="lblName" runat="server" Text="Nombre"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label8" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                   
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtName" width="226px" runat="server"></asp:TextBox>
                                        </asp:TableCell>
                                     </asp:TableRow> 
                                   
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label6" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="lblDescription" runat="server" Text="Descripción"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label10" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                   
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtDescription" width="226px" runat="server"></asp:TextBox>
                                        </asp:TableCell>
                                     </asp:TableRow> 
                                   
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label ID="Label11" runat="server" Text="1" ForeColor="White"></asp:Label>
                                        </asp:TableCell>
                                     </asp:TableRow>
                                    
                                     <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Button ID="btnAdd" runat="server" Text="Aceptar" OnClick="btnAdd_Click"/>
                                            <asp:Button ID="btnEdit" runat="server" Text="Actualizar" OnClick="btnEdit_Click"/>
                                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" OnClick="btnDelete_Click"/>
                                        </asp:TableCell>
                                     </asp:TableRow>
                            
                                </asp:Table>
                            
                            </asp:TableCell>
                            
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label12" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label13" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label14" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label15" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label16" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label17" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label18" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label19" runat="server" Text="1" ForeColor="White"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                                            
                    </asp:Table>
                
                </asp:Panel>

            </td>

            <td>
            
                <asp:Panel ID="Panel2" runat="server">
                    <asp:ListBox ID="lstTemplates" runat="server" Height="567px" Width="471px" 
                        onselectedindexchanged="lstTemplates_SelectedIndexChanged" 
                        AutoPostBack="True"></asp:ListBox>
                </asp:Panel>
                
            </td>

        </tr>
        
        <tr>
        
            <ajaxToolkit:ModalPopupExtender ID="PopUp" runat="server" TargetControlID="btnAdd" PopupControlID="PopupPanel" 
                                            CacheDynamicResults="true">
            </ajaxToolkit:ModalPopupExtender>
        
        </tr>

    </table>
    
    <asp:Panel ID="PopupPanel" runat="server" Style="display: none" BackColor="#FF6699" Width="191px">
    
            <asp:Table ID="Table1" runat="server">
            
           <asp:TableRow>
                <asp:TableCell>
                    <asp:FileUpload ID="ppfuUploadFile" Text="Buscar" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
                
             <asp:TableRow>
                <asp:TableCell>
                        <asp:Label runat="server" Id="pplblName" Text="Nombre"></asp:Label> 
                </asp:TableCell>
              </asp:TableRow>
              
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="ppTxtName" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Id="pplblDescription" Text="Descripción"></asp:Label> 
                </asp:TableCell>
            </asp:TableRow>
              
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="pptxtDescription" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="ppbtnAddTemplate" runat="server" Text="Aceptar" OnClick="btnAddTemplate_click"/>
                </asp:TableCell>
            </asp:TableRow>
                
            </asp:Table>
                            
    </asp:Panel>

</asp:Content>


