<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TasksList.ascx.cs" Inherits="TasksList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<fieldset title="Listado de Tareas" class="FieldSet">
   <legend class="Legend">Listado de Tareas</legend>
   <div class="UserControlBody">
      <table>
         <tr>
            <td align="center">
               <asp:Label ID="lbNoTasks" runat="server" CssClass="Label" />
            </td>
         </tr>
         <tr>
            <td style="width: 100%; color: navy; height: 19px; text-align: left; font-size: small;"
               align="center" valign="middle">
               <asp:GridView ID="gvTasks" OnPageIndexChanging="gvTasks_PageIndexChanging" OnSelectedIndexChanged="gvTasks_SelectedIndexChanged"
                  runat="server" AllowPaging="True" AutoGenerateColumns="False" ForeColor="Navy"
                  CellPadding="4" GridLines="None" Width="100%" Font-Size="Small">
                  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="False" />
                  <EmptyDataRowStyle Wrap="True" />
                  <Columns>
                     <asp:CommandField ButtonType="Button" SelectText="Ver" ShowSelectButton="True" ControlStyle-CssClass="Button" />
                     <asp:BoundField Visible="False" />
                     <asp:BoundField Visible="False" />
                     <asp:BoundField HeaderText="Nombre" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                     </asp:BoundField>
                     <asp:BoundField HeaderText="Expirado">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                     </asp:BoundField>
                  </Columns>
                  <RowStyle BackColor="#EFF3FB" Wrap="True" />
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Left"
                     VerticalAlign="Middle" Wrap="False" />
                  <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Wrap="True" />
                  <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" Wrap="True" />
                  <AlternatingRowStyle BackColor="White" Wrap="True" />
               </asp:GridView>
            </td>
         </tr>
      </table>
   </div>
</fieldset>
