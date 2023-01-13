<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCTaskToExpire.ascx.cs" Inherits="UCTaskToExpire" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<body class="ms-informationbar">
    <%--<asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>--%>
            <table style="margin: 0px 0px 0px 0px">
                <tr class="ms-topnav">
                    <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
                        <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
                        <asp:Label ID="lblTitle" Text="Vencimiento de tareas" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:Label runat="server" ID="lblWorkflow" Text="Workflow:" Font-Size="XX-Small" />
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:DropDownList ID="cmbWorkflow" runat="server" AutoPostBack="true" Font-Size="XX-Small" Width="130px" OnSelectedIndexChanged="cmbWorkflow_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:Label runat="server" ID="LblHoursToExpire" Text="Horas restantes para expirar:" Font-Size="XX-Small" />
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:TextBox ID="txtHoursToExpire" runat="server" Width="35px" Font-Size="XX-Small"></asp:TextBox><asp:Button ID="btnBuscar" runat="server" Text="Aplicar" OnClick="btnBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:RadioButton ID="rdbByStep" AutoPostBack="true" runat="server" Checked="True" Font-Size="XX-Small" OnCheckedChanged="rdbByStep_CheckedChanged" Text="Por Etapa" />
                        <asp:RadioButton ID="rdbByUser" AutoPostBack="true" runat="server" Font-Size="XX-Small" OnCheckedChanged="rdbByUser_CheckedChanged" Text="Por Usuario" />
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        | &nbsp;<asp:RadioButton ID="rdbVerGrafico" AutoPostBack="true" runat="server" Checked="True" Font-Size="XX-Small" Text="Ver Gráfico" OnCheckedChanged="rdbVerGrafico_CheckedChanged" />
                        <asp:RadioButton ID="rdbVerTabla" AutoPostBack="true" runat="server" Font-Size="XX-Small" Text="Ver Tabla" OnCheckedChanged="rdbVerTabla_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
                <tr height="100px">
                    <td style="margin: 0px 0px 0px 0px">
                        <rsweb:ReportViewer ID="rpvTaskToExpireByWorkflow" runat="server" ShowFindControls="False" ShowPageNavigationControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="270px" ShowDocumentMapButton="False" ShowExportControls="False" ShowRefreshButton="False" ShowZoomControl="False">
                            <LocalReport ReportPath="WfReports\Reports\rptTaskToExpire.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsTaskToExpire_dtTaskToExpire" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red" Visible="False">No se encontraron Tareas</asp:Label>
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        <rsweb:ReportViewer ID="rpvGrdTaskToExpireByWorkflow" runat="server" ShowFindControls="False" ShowPageNavigationControls="False" Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="270px" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False">
                            <LocalReport ReportPath="WfReports\Reports\RptGrdTaskToExpire.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
            <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</body>
