<%@ Control Language="C#" AutoEventWireup="false" CodeFile="~/UserControls/Login.ascx.cs"
    Inherits="Login" %>
<asp:Panel ID="pnlLog" runat="server">
    <table>
        <caption>
            Log In</caption>
        <tr>
            <td>
                <asp:Label ID="lbNombreUsuario" runat="server" AssociatedControlID="tbNombreUsuario">Nombre De Usuario:</asp:Label></td>
            <td>
                <asp:TextBox ID="tbNombreUsuario" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombreUsuario" runat="server" ControlToValidate="tbNombreUsuario"
                    ErrorMessage="Ingrese un Nombre de Usuario">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbPassword" runat="server" AssociatedControlID="tbContraseña">Contraseña:</asp:Label></td>
            <td>
                <asp:TextBox ID="tbContraseña" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContraseñaUsuario" runat="server" ControlToValidate="tbContraseña"
                    ErrorMessage="Ingrese una Contraseña">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="cbRememberMe" runat="server" Text="Workflow" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btLogIn" runat="server" Text="Log In" OnClick="LoginButton_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlCambioPassword" runat="server" Visible="False">
    <table>
        <caption>
            Cambio de Contraseña</caption>
        <tr>
            <td>
                <asp:Label ID="lbNuevaContraseña" runat="server" Text="Nueva Contraseña"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbContraseña1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbValidacionContraseña" runat="server" Text="Validación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbContraseña2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btCambiarPassword" runat="server" Text="Cambiar" OnClick="btCambiarPassword_Click" /><br />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ValidationSummary ID="vsErrores" runat="server" />
