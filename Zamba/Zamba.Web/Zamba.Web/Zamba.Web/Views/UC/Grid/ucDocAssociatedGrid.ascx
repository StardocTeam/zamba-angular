<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Grid_ucDocAssociatedGrid" Codebehind="ucDocAssociatedGrid.ascx.cs" %>

<div id="DocAssociatedGridContent"style="margin-top:25px;margin-left:25px" class="gridContainer">
    <asp:GridView ID="grdDocAssociated" runat="server"
        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" HeaderStyle-BackColor="#337ab7" HeaderStyle-ForeColor="White" HeaderStyle-CssClass="header-center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
         OnRowDataBound="grdDocAssociated_RowDataBound">
        <RowStyle CssClass="RowStyleAsoc" Wrap="false" Height="32px" />
        <EmptyDataRowStyle CssClass="RowStyleAsoc" Wrap="false" />
        <PagerStyle CssClass="PagerStyle" />
               
    </asp:GridView>

    <asp:Label ID="lblMsg" runat="server" Visible="False"></asp:Label>
    <style type="text/css">
        th[scope] {
            padding: 10px;
        }
    </style>
</div>

<script>
    function GrillaEstilos(){
        $(".header-center > th").css("text-align", "center");
        $(".header-center > th").css("padding", "5px");
    }
</script>