<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="TestDocumentsRelated.aspx.cs" Inherits="Controls_Related_Prueba" Title="Untitled Page" %>

<%@ Register TagPrefix="ZControls" TagName="DocRelated" Src="~/Controls/Related/WCDocumentsRelated.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;&nbsp;&nbsp;
    <table>
        <tr>
            <td>
                <asp:Label ID="lblIdDocument" runat="server" Text="Id Document" />
                <asp:TextBox ID="txtIdDocument" runat="server" />
            </td>
            <td>
                <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" 
                    onclick="btnAceptar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <zcontrols:docrelated runat="server" ID="DocRelated1" />
            </td>
        </tr>
    </table>
</asp:Content>
