<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TasksList.ascx.cs" Inherits="TasksList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<fieldset title="Listado de Tareas">
    <legend style="font: caption; font-size: medium; color: navy">Listado de Tareas</legend>
    <div class="UserControlBody" >
        <table width="100%" height="100%">
            <tr>
                <td align="center">
                    <asp:Label ForeColor="Navy" Font-Size="Medium" ID="lbNoTasks" runat="server" />
                    <fieldset runat="server" id="fsSeleccionar" style="text-align: center; width: 90%">
                        <asp:RadioButtonList Font-Size="Small" AutoPostBack="true" ID="rblSeleccion" runat="server"
                            ToolTip="Modificar las Tareas seleccionados" Height="60%" Width="100%" ForeColor="Navy"
                            OnSelectedIndexChanged="rblSeleccion_SelectedIndexChanged" Font-Strikeout="False"
                            Font-Overline="False" RepeatDirection="Horizontal" CellPadding="2">
                            <asp:ListItem Value="0" Text="Todos" />
                            <asp:ListItem Value="1" Text="Ninguno" />
                            <asp:ListItem Value="2" Enabled="False" Text="Personal" />
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; color: navy; height: 19px; text-align: left; font-size: small;"
                    align="center" valign="middle" colspan="">
                    <asp:GridView ID="gvTasks" OnPageIndexChanging="gvTasks_PageIndexChanging" OnSelectedIndexChanged="gvTasks_SelectedIndexChanged"
                        runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="2"
                                Font-Size="Small">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <EmptyDataRowStyle Wrap="True" />
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Ver" ShowSelectButton="True">
                                <ItemStyle BackColor="White" BorderColor="White" />
                                <ControlStyle BackColor="LightSteelBlue" Font-Bold="False" ForeColor="White" Font-Size="x-Small" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbSelected" runat="server" AutoPostBack="true" OnCheckedChanged="chbSelected_CheckedChanged"
                                        ToolTip="Seleccionar Tarea" />
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
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" Font-Size="Small" />
                        <RowStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                            Font-Size="Small" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Font-Size="Small" />
                        <HeaderStyle BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699" HorizontalAlign="Center"
                            Font-Size="Small" />
                        <AlternatingRowStyle BackColor="#F7F7F7" Font-Size="Small" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btUpdate" runat="server" OnClick="btUpdate_Click" Text="Actualizar"
                        BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" ToolTip="Actualizar Listado" />
                        </td>
            </tr>
        </table>
    </div>
</fieldset>
