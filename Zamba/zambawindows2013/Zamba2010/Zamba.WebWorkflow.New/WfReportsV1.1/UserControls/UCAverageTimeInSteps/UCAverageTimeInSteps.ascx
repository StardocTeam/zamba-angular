<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCAverageTimeInSteps.ascx.cs"
    Inherits="UCAverageTimeInSteps" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<body class="ms-informationbar">
    <table style="margin: 0px 0px 0px 0px">
        <tr>
            <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
                <asp:Label ID="lblInvisibleTitle" Width="15" runat="server" />
                <asp:Label ID="lblTitle" Text="Tiempos Promedio" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:Label runat="server" ID="lblWorkflow" Text="Workflow:" Font-Size="XX-Small" />
            </td>
            <td style="margin: 0px 0px 0px 0px">
                <asp:DropDownList ID="cmbWorkflow" runat="server" AutoPostBack="true" Font-Size="XX-Small"
                    Width="130px" OnSelectedIndexChanged="cmbWorkflow_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="RowSteps">
            <td>
                <asp:Label runat="server" ID="lblEtapas" Text="Etapa:" Font-Size="XX-Small" />
            </td>
            <td style="margin: 0px 0px 0px 0px">
                <asp:DropDownList ID="cmbEtapas" runat="server" AutoPostBack="true" Font-Size="XX-Small"
                    OnSelectedIndexChanged="DropEtapas_SelectedIndexChanged" Width="130px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="margin: 0px 0px 0px 0px">
                <asp:RadioButton ID="rdbWorkflow" AutoPostBack="true" runat="server" Checked="True"
                    Font-Size="XX-Small" OnCheckedChanged="rdbWorkflow_CheckedChanged" Text="Por Workflow" /><asp:RadioButton
                        ID="rdbByStep" AutoPostBack="true" runat="server" Font-Size="XX-Small" OnCheckedChanged="rdbByStep_CheckedChanged"
                        Text="Por Etapa" />&nbsp;
            </td>
        </tr>
    </table>
    <table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
        <tr height="100px">
            <td style="margin: 0px 0px 0px 0px">
                <asp:Label ID="lblNoData" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                    Visible="False">No se encontraron Tareas</asp:Label>
                     <asp:GridView runat="server" ID="gvTime" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false"  >
                         <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                         <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                         <Columns>
                             <asp:BoundField DataField="AverageTime_Source" HeaderText="Nombre" />
                             <asp:BoundField DataField="AverageTime" HeaderText="Tiempo" />
                         </Columns>
                         <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                         <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                         <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                         <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick" />
</body>