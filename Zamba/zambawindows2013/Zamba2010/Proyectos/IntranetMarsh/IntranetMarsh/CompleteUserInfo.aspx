<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompleteUserInfo.aspx.cs"
     MasterPageFile="~/IntraMasterPage.Master" Inherits="IntranetMarsh.WebForm1" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--     <form id="form1"   runat="server">--%>
            <div style="width: 100%; background-color: #EFEFEF;">
        <asp:Label ID="lblTitle" runat="server" Text="Información Personal de: " Font-Bold="True"
            Font-Italic="True" Font-Size="Large"></asp:Label>
 </div>         
 
 <div style="background-color:#EAF4FF; width:100%; height:100%">
         <asp:Table ID="tblContainer" runat="server" Width="100%" BackColor="#EAF4FF" Height="100%">
         <asp:TableRow>
                  <asp:TableCell VerticalAlign=Top>
         <asp:Image ID="Image1" runat="server" />
         
         </asp:TableCell>
         
         <asp:TableCell HorizontalAlign="Right">
         
         <asp:Table ID="tblUserAdminInfo" runat="server" Height="100%"
             BackColor="#EAF4FF" Font-Size="Small" >
        
            <asp:TableRow >
                <asp:TableCell HorizontalAlign="Right" >
                    <asp:Label ID="lblNombres"  runat="server" Text="Nombre/s: "></asp:Label>                 
                </asp:TableCell>
                
                <asp:TableCell>    
                    <asp:TextBox ID="txtNombres" runat="server" Width="250px"></asp:TextBox>
               </asp:TableCell>
               </asp:TableRow    >
                 
                 <asp:TableRow   HorizontalAlign="Right" >   
                <asp:TableCell>
                     <asp:Label ID="lblApellido" runat="server" Text="Apellido/s: " ></asp:Label>
                </asp:TableCell>  
                
                <asp:TableCell>   
                     <asp:TextBox ID="txtApellido" runat="server" Width="250px"></asp:TextBox>
               </asp:TableCell> 
            </asp:TableRow    >
            
            
            <asp:TableRow   HorizontalAlign="Right" >
            <asp:TableCell>
                <asp:Label ID="lblTelefono" runat="server" Text="Telefono: "></asp:Label>
              </asp:TableCell>




                <asp:TableCell>
                    <asp:TextBox ID="txtTelefono" runat="server" Width="250px"></asp:TextBox>
                    </asp:TableCell>
                  </asp:TableRow    > 
                    
                    <asp:TableRow  HorizontalAlign="Right"  >
                    <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell>
                      <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
                      </asp:TableCell>
                   </asp:TableRow    >
                   
                   <asp:TableRow   HorizontalAlign="Right" >
                   <asp:TableCell>
                    <asp:Label ID="lblPuesto" runat="server" Text="Puesto: "></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell>
                    <asp:TextBox ID="txtPuesto" runat="server" Width="250px"></asp:TextBox>
            </asp:TableCell>
            </asp:TableRow    >
       
       <asp:TableRow  HorizontalAlign="Right"  >
       <asp:TableCell>
                    <asp:Label ID="lblIntEmpresa" runat="server" Text="Int. Empresa: "></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell>
                    <asp:TextBox ID="txtIntEmpresa" runat="server" Width="250px"></asp:TextBox>
                </asp:TableCell>
                </asp:TableRow    >
                
                
                <asp:TableRow  HorizontalAlign="Right"  >
                <asp:TableCell>
                    <asp:Label ID="lblInterno" runat="server" Text="Interno: " ></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell>
                    <asp:TextBox ID="txtInterno" runat="server" Width="250px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow    >
                
                
                <asp:TableRow  HorizontalAlign="Right"  >
                <asp:TableCell>
                    <asp:Label ID="lblSector" runat="server" Text="Sector: "></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell>
                    <asp:TextBox ID="txtSector" runat="server" Width="250px"></asp:TextBox>
            </asp:TableCell>
            </asp:TableRow    >
            
            <asp:TableRow  HorizontalAlign="Right"  >
            <asp:TableCell>    
                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa: "></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell>
                    <asp:TextBox ID="txtEmpresa" runat="server" Width="250px"></asp:TextBox>
                    </asp:TableCell>
            </asp:TableRow    >
            
            
            <asp:TableRow    HorizontalAlign="Right">
            <asp:TableCell>
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo: "></asp:Label>
                    </asp:TableCell>
                   
                    <asp:TableCell>
                    <asp:TextBox ID="txtTipo" runat="server" WIDTH="250px"></asp:TextBox>
                    </asp:TableCell>
            </asp:TableRow    >
                
            
            
        </asp:Table>
         
         </asp:TableCell>
         
         </asp:TableRow>
         
         </asp:Table> 
          
          
       
      
    </div>
<%--</form>--%>
</asp:Content> 