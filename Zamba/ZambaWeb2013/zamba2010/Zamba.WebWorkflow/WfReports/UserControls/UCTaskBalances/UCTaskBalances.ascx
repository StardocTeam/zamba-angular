<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCTaskBalances.ascx.cs" Inherits="UCTaskBalances" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<%--<asp:Scriptager>--%>
<body class="ms-informationbar">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <table style="margin: 0px 0px 0px 0px">
                <tr>
                    <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
                        <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
                        <asp:Label ID="lblTitle" Text="Distribución de tareas" runat="server" />
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
                <tr runat="server" id="RowSteps">
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:Label runat="server" ID="lblEtapas" Text="Etapa:" Font-Size="XX-Small" />
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:DropDownList ID="cmbEtapas" runat="server" AutoPostBack="true" Font-Size="XX-Small" OnSelectedIndexChanged="DropEtapas_SelectedIndexChanged" Width="145px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="margin: 0px 0px 0px 0px">
                        <asp:RadioButton ID="rdbByWf" AutoPostBack="true" runat="server" Checked="True" Font-Size="XX-Small" OnCheckedChanged="rdbByWf_CheckedChanged" Text="Por Workflow" /><asp:RadioButton ID="rdbByStep" AutoPostBack="true" runat="server" Font-Size="XX-Small" OnCheckedChanged="rdbByStep_CheckedChanged" Text="Por Etapa" />&nbsp;
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        |
                        <asp:RadioButton ID="rdbVerGrafico" AutoPostBack="true" runat="server" Checked="True" Font-Size="XX-Small" Text="Ver Gráfico" OnCheckedChanged="rdbVerGrafico_CheckedChanged" /><asp:RadioButton ID="rdbVerTabla" AutoPostBack="true" runat="server" Font-Size="XX-Small" Text="Ver Tabla" OnCheckedChanged="rdbVerTabla_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
                <tr height="100px">
                    <td style="margin: 0px 0px 0px 0px">
                        <rsweb:ReportViewer ID="rpvTasksBalance" runat="server" ShowFindControls="False" ShowPageNavigationControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="270px" ShowDocumentMapButton="False" ShowExportControls="False" ShowRefreshButton="False" ShowZoomControl="False">
                            <LocalReport ReportPath="WfReports\Reports\rptTasksBalances.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                        &nbsp;
                        <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red" Visible="False">No se encontraron Tareas</asp:Label>
                    </td>
                    <td style="margin: 0px 0px 0px 0px">
                        <rsweb:ReportViewer ID="rpvGrdTasksBalance" runat="server" ShowFindControls="False" ShowPageNavigationControls="False" Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="270px" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False">
                            <LocalReport ReportPath="WfReports\Reports\rptGrdTasksBalances.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
            <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />
  <%--      </ContentTemplate>
    </asp:UpdatePanel>--%>
</body>
