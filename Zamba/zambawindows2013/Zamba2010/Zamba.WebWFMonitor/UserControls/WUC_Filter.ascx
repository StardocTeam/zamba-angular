<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_Filter.ascx.vb" Inherits="UserControls_WUC_Filter" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table  border="0" cellpadding="0" cellspacing="0"  >
    <tr>
        <td style="width: 192px; vertical-align: top; height: 22px;">
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Text="Vencimiento" ForeColor="Blue"></asp:Label></td>
        <td style="width: 3px; height: 22px;">
        </td>
        <td style="width: 2871px; vertical-align: top; height: 22px;">
            <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Text="Fecha Vencimiento"
                ForeColor="Blue" Width="184px"></asp:Label></td>
        <td style="width: 4281px; height: 22px;">
            <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Text="Estado" ForeColor="Blue"></asp:Label></td>
        <td style="vertical-align: top; height: 22px;">
        </td>
        <td style="width: 120450px; height: 22px;">
            <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Text="Usuario" ForeColor="Blue"></asp:Label></td>
        <td style="width: 120450px; vertical-align: top; height: 22px;">
        </td>
        <td style="vertical-align: top; width: 120450px; height: 22px;">
            <asp:Label ID="Label8" runat="server" Font-Names="Verdana" ForeColor="Blue" Text="Iniciada"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 192px; height: 15px;">
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Names="Verdana"
                Width="192px" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="0">No Filtrar Vencimiento</asp:ListItem>
                <asp:ListItem Value="1">Tareas Vencidas</asp:ListItem>
                <asp:ListItem Value="2">Tareas No Vencidas</asp:ListItem>
            </asp:DropDownList></td>
        <td style="width: 3px; height: 15px;">
        </td>
        <td style="width: 2871px; height: 15px;">
            <table>
                <tr>
                    <td style="height: 44px">
                        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="Small" Text="Desde"></asp:Label>
                    </td>
                    <td style="height: 44px">
                        <asp:TextBox ID="txtFrom" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td style="height: 44px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="Small" Text="Hasta"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </td>
        <td style="height: 15px">
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Width="184px"
                AppendDataBoundItems="True" DataTextField="Name" DataValueField="Name" Font-Names="Verdana" DataSourceID="odsEstados">
                <asp:ListItem Selected="True">Todos Los Estados</asp:ListItem>
            </asp:DropDownList>&nbsp;
        </td>
        <td style="width: 91px; height: 15px;">
        </td>
        <td style="width: 120450px; height: 15px;">
            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="184px"
                AppendDataBoundItems="True" DataSourceID="odsUsersList" DataTextField="Name"
                DataValueField="Name" Font-Names="Verdana">
                <asp:ListItem Selected="True">Todos Los Usuarios</asp:ListItem>
                <asp:ListItem>[Ninguno]</asp:ListItem>
            </asp:DropDownList></td>
        <td style="width: 120450px; height: 15px">
        </td>
        <td style="width: 1726450px; height: 15px">
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Si" AutoPostBack="True" Font-Names="Verdana"
                GroupName="Iniciada" /><asp:RadioButton ID="RadioButton2" runat="server" Text="No"
                    AutoPostBack="True" Font-Names="Verdana" GroupName="Iniciada" />
            <asp:RadioButton ID="RadioButton3" runat="server" Checked="True" Height="8px" Text="Todas"
                Width="80px" AutoPostBack="True" Font-Names="Verdana" GroupName="Iniciada" /></td>
    </tr>
    <tr>
        <td style="width: 192px; height: 15px">
            <cc1:CalendarExtender ID="ceFechaHasta" runat="server" TargetControlID="txtTo">
            </cc1:CalendarExtender>
        </td>
        <td style="width: 3px; height: 15px">
        </td>
        <td style="width: 2871px; height: 15px">
            <cc1:CalendarExtender ID="ceFechaDesde" runat="server" TargetControlID="txtFrom">
            </cc1:CalendarExtender>
        </td>
        <td style="height: 15px">
            <asp:ObjectDataSource ID="odsEstados" runat="server" SelectMethod="GetStates" TypeName="Business">
                <SelectParameters>
                    <asp:SessionParameter Name="wfid" SessionField="WfId" Type="String" />
                    <asp:SessionParameter Name="stepid" SessionField="StepId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
        <td style="width: 91px; height: 15px">
        </td>
        <td style="width: 120450px; height: 15px">
            <asp:ObjectDataSource ID="odsUsersList" runat="server" SelectMethod="GetAllUsers"
                TypeName="Business"></asp:ObjectDataSource>
        </td>
        <td style="width: 120450px; height: 15px">
        </td>
        <td style="width: 1726450px; height: 15px">
        </td>
    </tr>
</table>
