<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Grid_UcNewsGrid" Codebehind="UcNewsGrid.ascx.cs" %>
<div style="margin: 50px 5px 5px 10px">
    <asp:Label runat="server" ID="lblZeroNews" Text="No hay novedades disponibles" Visible="false" Font-Size="Medium"></asp:Label>
</div>
<div class="gridContainer">
    <asp:GridView ID="grdNews" runat="server" AllowPaging="False" AllowSorting="False"
        AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-condensed newsGrid" GridLines="None">
        <RowStyle CssClass="RowStyleTasks" Wrap="false" />
        <EmptyDataRowStyle CssClass="EmptyRowStyle" Wrap="false" />
        <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
    </asp:GridView>
</div>
<script>
    $(function () {
        $('.newsGrid tbody tr').click(function () {
            $(this).remove();
        });
        $('.newsGrid tbody tr td a').click(function () {
            $(this.parentNode.parentNode).remove();
        });
    });
</script>

