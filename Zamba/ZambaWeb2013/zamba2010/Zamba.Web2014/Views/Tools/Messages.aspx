<%@ Page Language="C#" MasterPageFile="~/MasterBlankPage.master" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Views_Tools_Messages" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header_css" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header_js" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <center>
        <div>&nbsp;
        </div>
        <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="Medium"></asp:Label>
        <div>&nbsp;
        </div>
        <asp:Button ID="btnClose" runat="server" Text="Cerrar" OnClick="btnClose_Click" />
        <div>&nbsp;
        </div>
    </center>
</asp:Content>

