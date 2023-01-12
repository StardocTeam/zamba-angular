<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Filter.ascx.cs" Inherits="UserControls_Filter" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table>
    <tr>
        <td>
            <fieldset>
                <legend>Vencimiento</legend>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="0">No Filtrar Vencimiento</asp:ListItem>
                    <asp:ListItem Value="1">Tareas Vencidas</asp:ListItem>
                    <asp:ListItem Value="2">Tareas No Vencidas</asp:ListItem>
                </asp:DropDownList>
            </fieldset>
        </td>
        <td>
            <fieldset>
                <legend>Fecha Vencimiento </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Desde"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Hasta"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            <cc1:CalendarExtender ID="ceFechaHasta" runat="server" TargetControlID="txtTo">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="ceFechaDesde" runat="server" TargetControlID="txtFrom">
            </cc1:CalendarExtender>
            </fieldset>
            
        </td>
        <td>
            <fieldset>
                <legend>Estado</legend>
                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                    DataTextField="Name" DataValueField="Name" DataSourceID="odsEstados">
                    <asp:ListItem Selected="True">Todos Los Estados</asp:ListItem>
                </asp:DropDownList>
            </fieldset>
            <asp:ObjectDataSource ID="odsEstados" runat="server" SelectMethod="GetStates" TypeName="Business">
                <SelectParameters>
                    <asp:SessionParameter Name="wfid" SessionField="WfId" Type="String" />
                    <asp:SessionParameter Name="stepid" SessionField="StepId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
        <td>
            <fieldset>
                <legend>Usuario</legend>
                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                    DataSourceID="odsUsersList" DataTextField="Name" DataValueField="Name">
                    <asp:ListItem Selected="True">Todos Los Usuarios</asp:ListItem>
                    <asp:ListItem>[Ninguno]</asp:ListItem>
                </asp:DropDownList>
            </fieldset>
            <asp:ObjectDataSource ID="odsUsersList" runat="server" SelectMethod="GetAllUsers"
                TypeName="Business"></asp:ObjectDataSource>
        </td>
        <td>
            <fieldset>
                <legend>Iniciada</legend>
                <asp:RadioButton ID="RadioButton1" runat="server" Text="Si" AutoPostBack="True" GroupName="Iniciada" /><asp:RadioButton
                    ID="RadioButton2" runat="server" Text="No" AutoPostBack="True" GroupName="Iniciada" />
                <asp:RadioButton ID="RadioButton3" runat="server" Checked="True" Text="Todas" AutoPostBack="True"
                    GroupName="Iniciada" /></fieldset>
        </td>
    </tr>
</table>
