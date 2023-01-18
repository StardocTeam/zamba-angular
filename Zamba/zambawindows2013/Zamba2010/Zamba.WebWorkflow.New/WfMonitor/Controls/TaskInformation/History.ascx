<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="History"
    EnableTheming="true" %>
<fieldset title="Historial">
    <legend style="font: caption; font-size: small; color: navy">Historial</legend>
    <div class="UserControlBody">
        <table>
            <tr>
                <td>
                    <asp:HiddenField runat="server" ID="hfTaskId" />
                    <asp:GridView ID="gvHistory" Font-Names="Times New Roman" Font-Size="Small" runat="server"
                        AllowPaging="True" OnPageIndexChanging="gvHistory_PageIndexChanging" BackColor="White"
                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="5">
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
        </table>
    </div>
    <br>
    <%--<div class="UserControlButtons">
        <asp:Button ID="btRefresh" runat="server" Text="Actualizar" OnClick="btRefresh_Click" BackColor="#EFF3FB" 
        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ToolTip="Actualizar Historial"
        Font-Names="Verdana" Font-Size="Small" ForeColor="#284775" />
    </div>--%>
</fieldset>
