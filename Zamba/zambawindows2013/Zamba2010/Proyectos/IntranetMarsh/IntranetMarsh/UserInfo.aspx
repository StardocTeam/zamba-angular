<%@ Page Language="C#" MasterPageFile="~/IntraMasterPage.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="IntranetMarsh.UserInfo" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<form id="form1" runat="server">--%>
    <table style="width:579px; height:430px">
    <tr>
    <td>
    <div id="divtitle" 
        
            
            style="width:603px; background-color:#BA6915; font-size:small; font-weight: bold; font-family: Arial; color: #FFFFFF;">
        Información de Usuario</div>
    <asp:Table ID="tblAbc" runat="server" BackColor="White" Width="601px">
        <asp:TableRow>        
    <asp:TableCell Width="60px" BackColor="#406967">
    <asp:LinkButton id="todos" runat="server" Text="Todos"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
        
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="A" runat="server" Text="A"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="B" runat="server" Text="B"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="C" runat="server" Text="C"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="D" runat="server" Text="D"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >   
    <asp:LinkButton id="E" runat="server" Text="E"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="F" runat="server" Text="F"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="G" runat="server" Text="G"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="H" runat="server" Text="H"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="I" runat="server" Text="I"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>    
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="J" runat="server" Text="J"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    
    
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="K" runat="server" Text="K"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="L" runat="server" Text="L"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
   
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="M" runat="server" Text="M"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="N" runat="server" Text="N"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
   <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="O" runat="server" Text="O"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="P" runat="server" Text="P"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click"  />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="Q" runat="server" Text="Q"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="R" runat="server" Text="R"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="S" runat="server" Text="S"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="T" runat="server" Text="T"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="U" runat="server" Text="U"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="V" runat="server" Text="V"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="W" runat="server" Text="W"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="X" runat="server" Text="X"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="Y" runat="server" Text="Y"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    <asp:TableCell Width="20px" BackColor="#406967" >
    <asp:LinkButton id="Z" runat="server" Text="Z"  BorderStyle="None" ForeColor="White" Font-Underline="true" OnClick="btnTodos_click" />
    </asp:TableCell>
    
    </asp:TableRow> 
    </asp:Table>
    
    <asp:Table ID="tbBusqueda" runat="server" Font-Size="Small" 
        BackColor="#40698F" Width="603px" ForeColor="White"  >
    
    <asp:TableRow >
    <asp:TableCell>        
    <asp:label ID="lblApellido" runat="server" Text="Apellido: "></asp:label>
    </asp:TableCell>
    <asp:TableCell>
    <asp:label ID="lblNombre" runat="server" Text="Nombre: "></asp:label>
    </asp:TableCell>
    <asp:TableCell>
     <asp:Label ID="lblDepartamento" runat="server" Text="Sector"></asp:Label>     
    </asp:TableCell>  
    </asp:TableRow>
    
    <asp:TableRow>
    <asp:TableCell>
    <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    </asp:TableCell>
    <asp:TableCell>
    <asp:DropDownList ID="drlstDepartamento" runat="server"></asp:DropDownList>
    </asp:TableCell>
    
    <asp:TableCell >
    <asp:Button ID="btnSearch" runat="server" Text="Buscar" onclick="btnSearch_Click" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>  
   
   
  
    
    
    

     <div style="padding-left:3px;height:345px;width:600px; overflow:auto">
     <asp:GridView ID="gvQuienEsQuien" runat="server" CellPadding="4" 
        Font-Size="Small" 
        SelectedRowStyle-Font-Underline="True" PagerSettings-PreviousPageText="<" 
        PagerSettings-NextPageText=">" PagerSettings-FirstPageText="|<" 
        PagerSettings-LastPageText=">|" PageSize="20" 
        SelectedRowStyle-BorderColor="#FF3300" SelectedRowStyle-BorderStyle="Solid" 
        SelectedRowStyle-Font-Italic="True" 
        OnSelectedIndexChanged="gvQuienEsQuien_SelectedIndexChanged" 
        ForeColor="#333333" onrowcreated="gvQuienEsQuien_RowCreated1" Width="600px" 
             AllowSorting="True" Height="145px" onsorting="gvQuienEsQuien_Sorting">
        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />

        <PagerSettings FirstPageText="|&lt;" LastPageText="&gt;|" NextPageText="&gt;" PreviousPageText="&lt;"></PagerSettings>

         <Columns>
             <asp:CommandField SelectText="Ver" ShowSelectButton="True" />
         </Columns>

        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" 
             Font-Italic="True" />
        <HeaderStyle BackColor="#174067" Font-Bold="True" ForeColor="White" />
         <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /></asp:GridView>
        </div>
        </td>
        </tr>
        </table>
<%--    </form>--%>
   
</asp:Content>
