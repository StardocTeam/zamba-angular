<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_TestWS_WSTestPage" Codebehind="WSTestPage.aspx.cs" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title>Zamba - Prueba de servicio web</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span>Zamba - Prueba de servicio web</span>
    </div>
    <div>
        <span>Url del servicio: </span><asp:Label ID="lblUrl" runat="server"></asp:Label>
    </div>
    <div>
        <span>Credenciales: </span><asp:Label ID="lblCredentials" runat="server"></asp:Label>
    </div>
    <div style="display:inline">
        <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="Probar WS" />
    </div>
    <div style="display:inline">
        <asp:Button ID="btnAppConfigLocation" runat="server" OnClick="btnAppConfigLocation_Click" Text="Ruta carpeta config" />
    </div>
    <div style="display:inline">
        <asp:Button ID="btnLogLocation" runat="server" OnClick="btnLogLocation_Click" Text="Ruta carpeta log" />
    </div>
    <div>
        <span>Resultado: </span><asp:Label ID="lblResult" runat="server" Text="En espera"></asp:Label>
    </div>
    </form>
</body>
</html>
