<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcGenericReport.ascx.cs" Inherits="WfReports_UserControls_UCGenericReport_UcGenericReport" %>
<asp:HiddenField id="hdQuery" runat="server" />
<table style="margin: 0px 0px 0px 0px">
    <tr>
        <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
            <asp:Label ID="lblTitle" Text="" runat="server" />
        </td>
    </tr>
</table>
<table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
    <tr height="100px">
        <td style="margin: 0px 0px 0px 0px">
            <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red" Visible="False" Text="No se encontraron Tareas" />
            <asp:GridView runat="server" ID="gvGeneric" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="true" AllowPaging="True" onpageindexchanging="gvGeneric_PageIndexChanging">
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>

