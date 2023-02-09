<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Grid_ucDocAssociatedGrid" Codebehind="ucDocAssociatedGrid.ascx.cs" %>

<div id="DocAssociatedGridContent" class="gridContainer">
    <asp:GridView ID="grdDocAssociated" runat="server"
        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None"
         OnRowDataBound="grdDocAssociated_RowDataBound">
        <RowStyle CssClass="RowStyleAsoc" Wrap="false" />
        <EmptyDataRowStyle CssClass="RowStyleAsoc" Wrap="false" />
        <PagerStyle CssClass="PagerStyle" />
        <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>        
    </asp:GridView>
    <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
</div>
