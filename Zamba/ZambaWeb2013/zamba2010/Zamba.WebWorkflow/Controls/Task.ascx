<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Task.ascx.cs" Inherits="Task" %>
<fieldset title="Tarea" class="FieldSet">
    <legend class="Legend">Tarea</legend>
    <div class="UserControlBody" style="color: navy; font-size: small">
        <asp:HiddenField ID="hfTaskId" runat="server" />
        <asp:Panel ScrollBars="auto" Height="200 px"  >
            <table id="tblTaskInformation" runat="server" class="Table"  style="font-size:xx-small;">
                <tr>
                    <td>
                        <asp:Label ID="lbAsignedBy" runat="server" Text="Asignado por" Font-Bold="true" CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbAsignedByValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbAsignedTo" runat="server" Text="Asignado a" Font-Bold="true" CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbAsignedToValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbCheckIn" runat="server" Text="Check In" Font-Bold="true" CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbCheckInValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbExpirationDate" runat="server" Text="Fecha de Expiración" Font-Bold="true"
                            CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbExpirationDateValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbIsExpired" runat="server" Text="Expirado" Font-Bold="true" CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbIsExpiredValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbState" runat="server" Text="Estado" Font-Bold="true" CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbStateValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbTaskState" runat="server" Text="Estado Tarea" Font-Bold="true" CssClass="Label" />
                    </td>
                    <td>
                        <asp:Label ID="lbTaskStateValue" runat="server" Enabled="false" CssClass="Label" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</fieldset>
