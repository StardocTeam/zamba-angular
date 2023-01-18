<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcGraphic.ascx.cs" Inherits="UcGraphic" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<CR:CrystalReportViewer ID="crvGraphic" runat="server" AutoDataBind="true" /><br />
<asp:Button ID="btGraphic" runat="server" Text="Cargar Grafico" OnClick="btGraphic_Click" />