<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserHistory.aspx.cs" Inherits="UserHistory"
    MasterPageFile="~/ZambaMaster.master" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:GridView ID="gvUserHistory" runat="server" CellPadding="4" ForeColor="#333333"
            GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px"
                Font-Size="X-Small" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
            <PagerSettings Position="TopAndBottom" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" Font-Size="X-Small" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                Font-Size="X-Small" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
<%--    </form>
</body>
</html>--%>