<%@ Page Language="C#" MasterPageFile="~/IntraMasterPage.Master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="IntranetMarsh.Home" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset runat="server" id="News" style="width:590px;height:430px">
<div style="padding-left:4px;padding-top:10px">
<center>
<table>
<tr>
<td bgcolor="#176915" style="width:400px">
<div style="padding-left:20px">
<asp:Label ID="lbTitle" runat="server" Font-Bold="False" ForeColor="White" 
        Text="NOTICIAS DE MARSH"></asp:Label>
</div>
</td>
<td bgcolor="#009900" style="width:7px">
</td>
<td bgcolor="#33CC33" style="width:7px">
</td>
</tr>
</table>
</center>
</div>
<%--<center>--%>
<div style="padding-left:10px;margin-top:10px;width:572px; height:380px;overflow:auto">
<asp:Table runat="server" ID="NewsTable">
</asp:Table>
</div>
<%--</center>--%>
  <div id="ShowNews" runat="server" visible="false" style=" background-color:Activeborder;border:solid 1px black;position:absolute;width:400px;height:350px; top:20%; left:30%; vertical-align:middle;font-family: 'Arial'; font-size:small; font-weight:normal; font-style: normal; font-variant: normal; text-transform: none; color: #FF0000">
  <table width="100%">
  <tr>
  <td style="padding-top:15px;padding-left:10px;padding-right:10px" align="center" valign="top">
  <div style="height:280px;overflow:auto;width:100%">
  <asp:Label ID="NewsTitle" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="#3333cc" Font-Underline="true"></asp:Label>
  <br />
  <div style="text-align:left;padding-top:15px">
  <asp:Label ID="NewsMsg" runat="server" Font-Size="Small" ForeColor="Black"></asp:Label>
  </div>
  </div>
  </td>
  </tr>
  </table>
    <br />
    <div style="text-align:center">
  <asp:Button ID="btnCloseNews" runat="server" Text="Cerrar" />
  </div>
  </div>
</fieldset>
</asp:Content>
