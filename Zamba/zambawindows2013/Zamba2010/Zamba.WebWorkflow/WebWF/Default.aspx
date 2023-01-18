<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebWF_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
<tr>
<td>
    <asp:TreeView ID="TreeView1" runat="server">
    </asp:TreeView>
</td>
<td>
<table>
<tr>
<td>

</td>
</tr>
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</td>
</tr>
<tr>
<td>

</td>
</tr>
</table>
</td>
</tr>
</table>
</asp:Content>

