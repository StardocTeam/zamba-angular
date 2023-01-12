<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcGenericReport.ascx.cs" Inherits="WfReports_UserControls_UcGenericReport_UcGenericReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<rsweb:ReportViewer ID="rpvGenericReport" runat="server" ShowFindControls="False" ShowPageNavigationControls="False" Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="270px" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False">
    <LocalReport ReportPath="~/WfReports\Reports\Report.rdlc" />
</rsweb:ReportViewer>
