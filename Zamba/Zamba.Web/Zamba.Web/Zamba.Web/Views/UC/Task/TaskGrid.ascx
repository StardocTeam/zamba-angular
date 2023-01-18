<%@ Control Language="C#" AutoEventWireup="false" Inherits="TaskGrid" CodeBehind="TaskGrid.ascx.cs"  %>
<%@ Register Src="../Grid/ucTaskGrid.ascx" TagName="ucTaskGrid" TagPrefix="ZControls" %>
<%@ Register Src="../Grid/CustomFilterControl.ascx" TagName="ucTaskGridFilter" TagPrefix="uc1" %>
<%@ Register Src="../Grid/CustomFilterControlMarshArt.ascx" TagName="ucTaskGridFilterMarsh" TagPrefix="uc1" %>


<asp:Label ID="lblMsg" runat="server" Text="No se ha seleccionado ninguna etapa"></asp:Label>

<% if (Page.Theme !=null && Page.Theme.ToLower() == "marsh")
    {%>
<uc1:ucTaskGridFilterMarsh ID="ucTaskGridFilterMarsh" runat="server" />
<% }  %>

<ZControls:ucTaskGrid ID="ucTaskGrid" runat="server" />