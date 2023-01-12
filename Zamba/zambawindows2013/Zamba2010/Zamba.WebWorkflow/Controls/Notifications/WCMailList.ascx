<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCMailList.ascx.cs" Inherits="Controls_Notifications_WebUserControl" %>


<table>
<tr>

<td>
<asp:ListBox runat="server" ID="lstMails" Height="386px" Width="207px"></asp:ListBox>
</td>
<td style="text-align:center">
<table>
<tr style="vertical-align:middle;text-align:center">
<td >
<asp:Button ID ="btnAdd" runat="server" Text=">>" Height="35px" Width="46px" 
        onclick="btnAdd_Click"/>
</td>
</tr>
<tr>
<td>
<asp:Button ID ="btnRemove" runat="server" Text="<<" Height="35px" Width="46px" 
        onclick="btnRemove_Click"/>
</td>
</tr>
</table>
</td>

<td>
<asp:ListBox runat="server" ID="lstAddedMails" Height="386px" Width="207px"></asp:ListBox>
</td>
</tr>

<tr>
<td>
</td>
<td>
<asp:Button runat="server" ID="btnOk" Text="Aceptar" onclick="btnOk_Click" />
</td>
<td>
</td>
</tr>
</table>