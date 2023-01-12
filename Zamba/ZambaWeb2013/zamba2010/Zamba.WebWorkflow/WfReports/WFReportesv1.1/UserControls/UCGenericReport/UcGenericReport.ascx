<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcGenericReport.ascx.cs"
    Inherits="WfReports_UserControls_UCGenericReport_UcGenericReport" %>
<asp:HiddenField ID="hdQuery" runat="server" />
<%--<table style="margin: 0px 0px 0px 0px">
    <tr>
        <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
            <asp:Label ID="lblTitle" Text="" runat="server" />
        </td>
    </tr>
</table>--%>
<table style="margin: 0px 0px 0px 0px">
    <tr class="ms-topnav">
        <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
            <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
            <asp:Label ID="lblTitle" Text="" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="margin: 0px 0px 0px 0px">
            <%--            <asp:RadioButton ID="rdbByStep" AutoPostBack="true" runat="server" Checked="True"
                Font-Size="XX-Small" OnCheckedChanged="rdbByStep_CheckedChanged" Text="Por Etapa" />
            <asp:RadioButton ID="rdbByUser" AutoPostBack="true" runat="server" Font-Size="XX-Small"
                OnCheckedChanged="rdbByUser_CheckedChanged" Text="Por Usuario" />--%>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="XX-Small" 
                ontextchanged="RadioButtonList1_TextChanged" RepeatDirection="Horizontal" 
                AutoPostBack="True" 
                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                <asp:ListItem Value="Tabla" Selected="True">Ver Tabla</asp:ListItem>
                <asp:ListItem Value="Grafico">Ver Grafico</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td style="margin: 0px 0px 0px 0px">
            &nbsp;</td>
    </tr>
</table>
<table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
    <tr height="100px">
        <td style="margin: 0px 0px 0px 0px; vertical-align:top">
            <%--            <rsweb:ReportViewer ID="rpvTaskToExpireByWorkflow" runat="server" ShowFindControls="False"
                ShowPageNavigationControls="False" ShowPrintButton="False" ShowPromptAreaButton="False"
                Font-Names="Verdana" Font-Size="8pt" Height="250px" Width="270px" ShowDocumentMapButton="False"
                ShowExportControls="False" ShowRefreshButton="False" ShowZoomControl="False">
                <LocalReport ReportPath="WfReports\Reports\rptTaskToExpire.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                            Name="dsTaskToExpire_dtTaskToExpire" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>--%>
            <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                Visible="False" Text="No se encontraron Tareas" />
            <asp:GridView runat="server" ID="gvGeneric" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="true" AllowPaging="True" OnPageIndexChanging="gvGeneric_PageIndexChanging">
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <%--<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                Visible="False">No se encontraron Tareas</asp:Label>--%>
        </td>
        <td style="margin: 0px 0px 0px 0px">
            <asp:Image ID="Image1" runat="server" Height="268" Width="402" />
        </td>
    </tr>
</table>
<asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />
<%--<table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
    <tr height="100px">
        <td style="margin: 0px 0px 0px 0px">
            <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red" Visible="False" Text="No se encontraron Tareas" />
            <asp:GridView runat="server" ID="gvGeneric" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="true" AllowPaging="True" onpageindexchanging="gvGeneric_PageIndexChanging">
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </td>
    </tr>
</table>--%>