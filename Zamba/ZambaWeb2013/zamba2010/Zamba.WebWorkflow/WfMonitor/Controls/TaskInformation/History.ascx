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
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <RowStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#D6EBFC" Font-Bold="True" ForeColor="#336699" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</fieldset>
