<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAverageTimeInSteps.ascx.cs"
    Inherits="UCAverageTimeInSteps" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                    <link href="../../../default.css" rel="stylesheet" type="text/css" />
                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<body class="ms-informationbar">
<table style="margin: 0px 0px 0px 0px">
    <tr>
        <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
            <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
            <asp:Label ID="lblTitle" Text="Tiempos Promedio" runat="server"  />
        </td>
    </tr>
    <tr>
        <td style="margin: 0px 0px 0px 0px">
            <asp:Label runat="server" ID="lblWorkflow" Text="Workflow:" Font-Size="XX-Small" />
        </td>
        <td style="margin: 0px 0px 0px 0px">
            <asp:DropDownList ID="cmbWorkflow" runat="server" AutoPostBack="true" Font-Size="XX-Small"
                Width="130px" OnSelectedIndexChanged="cmbWorkflow_SelectedIndexChanged" 
                ToolTip="Listado de Work Flows que puede visualizar">
            </asp:DropDownList></td>
    </tr>
    <tr runat="server" id="RowSteps">
        <td>
            <asp:Label runat="server" ID="lblEtapas" Text="Etapa:" Font-Size="XX-Small" />
        </td>
        <td style="margin: 0px 0px 0px 0px">
            <asp:DropDownList ID="cmbEtapas" runat="server" AutoPostBack="true" Font-Size="XX-Small"
                OnSelectedIndexChanged="DropEtapas_SelectedIndexChanged" Width="130px" 
                ToolTip="Muestra las etapas del Work Flow seleccionado">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="margin: 0px 0px 0px 0px">
            <asp:RadioButton ID="rdbWorkflow" AutoPostBack="true" runat="server" Checked="True"
                Font-Size="XX-Small" OnCheckedChanged="rdbWorkflow_CheckedChanged" 
                Text="Por Workflow" ToolTip="Muestra los tiempos promedio por Work Flow" />
            <asp:RadioButton
                    ID="rdbByStep" AutoPostBack="true" runat="server" Font-Size="XX-Small" OnCheckedChanged="rdbByStep_CheckedChanged"
                    Text="Por Etapa" ToolTip="Muestra los tiempos promedios por etapa" />&nbsp;</td>
        <td style="margin: 0px 0px 0px 0px">
            |
            <asp:RadioButton ID="rdbVerGrafico" AutoPostBack="true" runat="server" Checked="True"
                Font-Size="XX-Small" Text="Ver Gráfico" 
                OnCheckedChanged="rdbVerGrafico_CheckedChanged" 
                ToolTip="Muestra el gráfico de los tiempos promedio del Work Flow seleccionado" />
            <asp:RadioButton ID="rdbVerTabla" AutoPostBack="true" runat="server" Font-Size="XX-Small"
                Text="Ver Tabla" OnCheckedChanged="rdbVerTabla_CheckedChanged" 
                ToolTip="Muestra una tabla con los tiempos promedio del Work Flow" /></td>
    </tr>
</table>
<table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
    <tr height="100px">
        <td style="margin: 0px 0px 0px 0px">
        
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>

            <rsweb:ReportViewer ID="rpvAverageTimeInSteps" runat="server" ShowFindControls="False"
                ShowPageNavigationControls="False" ShowPrintButton="False" ShowPromptAreaButton="False"
                Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="275px" ShowDocumentMapButton="False"
                ShowExportControls="False" ShowRefreshButton="False" ShowZoomControl="False">
                <LocalReport ReportPath="~/WfReports\Reports\rptAverageTimeInSteps.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsAverageTimeInSteps_dtAverageTimeInSteps" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            
            </ContentTemplate> 
  </asp:UpdatePanel> 
  
  
            <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                Visible="False">No se encontraron Tareas</asp:Label>
        </td>
        <td style="margin: 0px 0px 0px 0px">
            <rsweb:ReportViewer ID="rpvGrdAverageTimeInSteps" runat="server" ShowFindControls="False"
                ShowPageNavigationControls="False" Font-Names="Verdana" Font-Size="8pt" Height="250px"
                Width="275px" ShowExportControls="False" ShowPrintButton="False" ShowPromptAreaButton="False"
                ShowRefreshButton="False" ShowZoomControl="False">
                <LocalReport ReportPath="~/WfReports\Reports\RptGrdAverageTimeInSteps.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsAverageTimeInSteps_dtAverageTimeInSteps" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            &nbsp;
        </td>
    </tr>
</table>
<asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />
<%--</ContentTemplate>
</asp:UpdatePanel>--%>