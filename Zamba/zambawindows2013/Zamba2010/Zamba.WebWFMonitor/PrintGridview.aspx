<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrintGridview.aspx.vb" Inherits="PrintGridview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tareas</title>
    <link type="text/css" href="App_Themes/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvTareas" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                EmptyDataText="NO SE ENCONTRARON TAREAS" EnableViewState="False" GridLines="Horizontal"
                PageSize="15" CellPadding="4">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                <HeaderStyle CssClass="HeaderStyle" Font-Underline="False" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
