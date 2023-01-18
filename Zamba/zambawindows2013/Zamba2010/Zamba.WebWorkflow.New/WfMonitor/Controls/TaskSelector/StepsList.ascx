<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StepsList.ascx.cs" Inherits="StepsList" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<link href="../../../default.css" rel="stylesheet" type="text/css" />
<fieldset title="Listado de Etapas">
<legend class="srch-Title">Listado de Etapas</legend>
    <div class="UserControlBody">
        <table width="100%" height="100%">
            <tr>
                <td align="center">
                    <asp:Label ForeColor="Navy" Font-Size="Medium" ID="lbNoSteps" runat="server" 
                        CssClass="ms-informationbar" />
                    <asp:HiddenField ID="hdnSelectedRadioButton" runat="server" />
                    <fieldset runat="server" id="fsSeleccionar" style="text-align: center; width: 90%">
                        <asp:RadioButtonList Font-Size="Small" AutoPostBack="true" ID="rblSeleccion" runat="server" ToolTip="Modificar las Etapas seleccionadas"
                            Height="20%" Width="100%" ForeColor="Navy" OnSelectedIndexChanged="rblSeleccion_SelectedIndexChanged"
                            Font-Strikeout="False" Font-Overline="False" RepeatDirection="Horizontal" CellPadding="2">
                            <asp:ListItem Value="0" Text="Todos" />
                            <asp:ListItem Value="1" Text="Ninguno" />
                            <asp:ListItem Value="2" Enabled="False" Text="Personal" />
                        </asp:RadioButtonList></fieldset>
                  
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvSteps" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        OnPageIndexChanging="gvSteps_PageIndexChanging" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="2"
                                Font-Size="Small">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbSelected" runat="server" OnCheckedChanged="chbSelected_CheckedChanged" ToolTip="Seleccionar Etapa"
                                        AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="False" />
                            <asp:BoundField HeaderText="Nombre" />
                            <asp:BoundField HeaderText="Tareas" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                                <asp:BoundField HeaderText="Expiradas" >
                                    <ItemStyle HorizontalAlign="Right" />
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
                    <asp:Button ID="btUpdate" runat="server" Text="Actualizar" OnClick="btUpdate_Click" ToolTip="Actualizar Listado" 
                        BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="X-Small" ForeColor="#284775" />
                </td>
            </tr>
        </table>
    </div>
</fieldset>
