<%@ Control Language="C#" AutoEventWireup="false" CodeFile="TaskGrid.ascx.cs" Inherits="TaskGrid" %>
<%@ Register src="../Grid/ucTaskGrid.ascx" tagname="ucTaskGrid" tagprefix="ZControls" %>
<%@ Register src="../Grid/CustomFilterControl.ascx" tagname="ucTaskGridFilter" tagprefix="uc1" %>
<%@ Register src="../Grid/CustomFilterControlMarshArt.ascx" tagname="ucTaskGridFilterMarsh" tagprefix="uc1" %>
        

    <asp:Label ID="lblMsg" runat="server" Text="No se ha seleccionado ninguna etapa"></asp:Label>
<% if (Page.Theme.ToLower() == "aysadiseno") {%>
    <uc1:ucTaskGridFilter ID="ucTaskGridFilter" runat="server" />
<% } %>

<% if (Page.Theme.ToLower() == "marsh") {%>
  <uc1:ucTaskGridFilterMarsh ID="ucTaskGridFilterMarsh" runat="server" />
<% }  %>

    <ZControls:ucTaskGrid ID="ucTaskGrid" runat="server" />
