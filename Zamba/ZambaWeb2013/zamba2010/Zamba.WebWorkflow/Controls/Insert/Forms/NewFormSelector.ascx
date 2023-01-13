<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewFormSelector.ascx.cs"
    Inherits="Controls_Insert_Forms_NewFormSelector" %>
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Seleccione un Formulario: " Font-Names="Arial"
                ForeColor="Navy" Font-Size="X-Small"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="cmbVirtualForm" runat="server" 
                Height="42px" Width="194px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Button ID="btnInsert" runat="server" Text="Visualizar" 
            onclick="btnInsert_Click" /> 
    </td>
    </tr>
    </table>