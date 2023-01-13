<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TasksList.ascx.cs" Inherits="TasksList" %>
<%@ Register Src="~/WfMonitor/Controls/TaskInformation/TaskInformation.ascx"  TagPrefix="uc" TagName="info" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<fieldset title="Listado de Tareas">
    <legend class="srch-Title">Listado de Tareas</legend>
    <div class="UserControlBody">
        <table width="100%" style="position: static; height: auto; width: auto">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ForeColor="Navy" ID="lbNoTasks" runat="server" CssClass="ms-informationbar" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="lnkAll" runat="server" Text="Seleccionar todas" OnClick="lnkAll_Click"
                        ToolTip="Seleccionar todas las tareas" />
                </td>
                <td align="right">
                    <asp:LinkButton ID="lnkNone" runat="server" Text="Seleccionar ninguna" OnClick="lnkNone_Click"
                        ToolTip="Deseleccionar todas las tareas" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%; color: navy; text-align: left;" align="center" valign="middle"
                    colspan="2" width="40px" height="20px">
                    <asp:GridView ID="gvTasks" OnPageIndexChanging="gvTasks_PageIndexChanging" OnSelectedIndexChanged="gvTasks_SelectedIndexChanged"
                        Width="100%" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="2">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <EmptyDataRowStyle Wrap="True" />
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Ver" ShowSelectButton="True" ItemStyle-Font-Size="X-Small">
                                <ItemStyle Font-Size="X-Small" />
                                <ControlStyle BackColor="LightSteelBlue" Font-Bold="False" Font-Size="X-Small" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbSelected" runat="server" ToolTip="Seleccionar Tarea" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="False" />
                            <asp:BoundField Visible="False" />
                            <asp:BoundField HeaderText="Nombre" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Expirado">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <RowStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btUpdate" runat="server" OnClick="btUpdate_Click" Text="Actualizar"
                        BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" ToolTip="Actualizar Listado" />
                </td>
                <td align="right">
                    <asp:Button ID="btView" runat="server" Text="Seleccionar tareas" BackColor="#EFF3FB"
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                        Font-Size="X-Small" ForeColor="#284775" ToolTip="Selecciona las tareas para realizar acciones sobre las mismas"
                        OnClick="btView_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="TaskInfo" runat="server">
    </div>
</fieldset>
