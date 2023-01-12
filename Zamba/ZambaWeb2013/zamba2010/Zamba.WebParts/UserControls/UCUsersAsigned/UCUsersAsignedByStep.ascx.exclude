<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCUsersAsignedByStep.ascx.cs"
    Inherits="UserControls_UCUsersAsigned_UCUsersAsignedByStep" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<table>
    <tr>
        <td style="width: 5px">
            <asp:Label runat="server" ID="lblWorkflow" Text="Workflow:" Font-Size="XX-Small" />
        </td>
        <td>
            <asp:DropDownList ID="cmbWorkflow" runat="server" AutoPostBack="true" Width="145px"
                OnSelectedIndexChanged="cmbWorkflow_SelectedIndexChanged" Font-Size="XX-Small">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 5px">
            <span style="font-size: 9pt">Etapa:</span></td>
        <td>
            <asp:DropDownList ID="cmbEtapas" AutoPostBack=true runat="server" Width="145px" Font-Size="XX-Small" OnSelectedIndexChanged="cmbEtapas_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td>
            <asp:RadioButton ID="rdbVerGrafico" AutoPostBack =true runat="server" Checked="True" Font-Size="XX-Small"
                Text="Ver Gráfico" OnCheckedChanged="rdbVerGrafico_CheckedChanged" />
            <asp:RadioButton ID="rdbVerTabla" AutoPostBack=true runat="server" Font-Size="XX-Small" Text="Ver Tabla" OnCheckedChanged="rdbVerTabla_CheckedChanged" /></td>
    </tr>
</table>
<table>
    <tr>
        <td id=1>
            <rsweb:ReportViewer ID="rpvUsersAsigned" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="400px" ShowFindControls="False" ShowPageNavigationControls="False" Width="400px">
                <LocalReport ReportPath="Reports\rptUsersAsigned.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </td>
        <td id=2>
            <rsweb:ReportViewer ID="rpvGrdUsersAsigned" runat="server" ShowFindControls="False"
                ShowPageNavigationControls="False" Font-Names="Verdana" Font-Size="8pt" Height="400px"
                Width="400px">
                <LocalReport ReportPath="Reports\RptGrdUsersAsigned.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
