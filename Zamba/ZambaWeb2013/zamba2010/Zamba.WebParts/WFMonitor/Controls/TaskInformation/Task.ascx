<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Task.ascx.cs" Inherits="Task" %>
<fieldset title="Tarea">
    <legend style="font: caption; font-size: small; color: navy">Tarea</legend>
    <div class="UserControlBody" style="color: navy; font-size: small">
        <asp:HiddenField ID="hfTaskId" runat="server" />
        <table id="tblTaskInformation" runat="server">
            <tr>
                <td>
                    <asp:Label ID="lbAsignedBy" runat="server" Text="Asignado por" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbAsignedByValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbAsignedTo" runat="server" Text="Asignado a" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbAsignedToValue" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbCheckIn" runat="server" Text="Check In" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbCheckInValue" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbExpirationDate" runat="server" Text="Fecha de Expiración" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbExpirationDateValue" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbIsExpired" runat="server" Text="Expirado" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbIsExpiredValue" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbState" runat="server" Text="Estado" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbStateValue" runat="server" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbTaskState" runat="server" Text="Estado Tarea" Font-Bold="true" />
                </td>
                <td>
                    <asp:Label ID="lbTaskStateValue" runat="server" Enabled="false" />
                </td>
            </tr>
        </table>
    </div>
    <div class="UserControlButtons">
     <%--   <asp:Button ID="btActualizar" runat="server" Text="Actualizar" OnClick="btActualizar_Click"
            BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            ToolTip="Actualizar información" Font-Names="Verdana" Font-Size="Small" ForeColor="#284775" />--%>
    </div>
</fieldset>
