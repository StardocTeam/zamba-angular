<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocHistory.aspx.cs" Inherits="DocHistory"
    MasterPageFile="~/ZambaMaster.master" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
    <%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblDocHistory" runat="server" Font-Bold="True" Font-Italic="True"
        Font-Size="Medium" Text="Historial de Documento:"></asp:Label>
    <asp:GridView ID="gvDocHistory" runat="server">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px"
            Font-Size="X-Small" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
        <PagerSettings Position="TopAndBottom" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" Font-Size="X-Small" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
            Font-Size="X-Small" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
    </asp:GridView>
</asp:Content>
