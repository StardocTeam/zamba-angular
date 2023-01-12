<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mariva Ingreso de Usuario</title>
</head>

<script language="javascript" type="text/javascript">

    function showuser()
    {
    var wshshell
    try{
    	wshshell=new ActiveXObject("WScript.Shell");
    }
    catch(err)
    {
    }
    if (wshshell!=null){
    	var username=wshshell.ExpandEnvironmentStrings("%username%");
    	hs = document.getElementById("vUserName");
		hs.value = username;
		otroHs = document.getElementById("lblLoginWindows");
		otroHs.innerHTML = 'Bienvenido ' + hs.value + ', haga click en el botón para ingresar con su usuario Windows, o puede elegir entrar con otro usuario de Zamba.';
	    }
    }
    
   function UserZambaLogin()
    {
    o1 = document.getElementById("chkWindowsLogIn");
    o1.click();
    o2 = document.getElementById("Submit1");
    o2.fireEvent('OnClick');
    alert('Hola Manola');
    }

</script>

<!--
  <%--var wshshell="new" ActiveXObject=""("
  var="" username=wshshell.ExpandEnvironmentStrings("%username%"
  --%>-->
<body topmargin="0" id="loginBody" runat="server">
    <form id="form1" runat="server" style="background-image: url(images/fondo3.JPG);">
        <font face="Arial, Helvetica, sans-serif" size="2">
            <input id="vUserName" runat="server" type="hidden" />
            <input id="flagWindowsLogin" type="hidden" runat="server" />
            <table style="width: 100%; height: 200; background-image: url(images/fondo3.JPG);
                background-color: transparent;">
                <tr>
                    <td bgcolor="#edecf4" style="vertical-align: bottom">
                        <img height="65" src="images/top.jpg" width="780" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ScriptManager runat="server" ID="sc">
                        </asp:ScriptManager>
                        <center>
                            <asp:Table Width="40%" runat="server" ID="tbl5" Height="78px">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server">
										<font size="4">
										<strong>Sistema
                            de Información Online</strong></font>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <hr />
                            <table>
                                <tr>
                                    <td height="25px">
                                        <asp:Label ID="lblLoginWindows" Visible="true" runat="server" />
                                                     <asp:Label ID="lblError" runat="Server" EnableViewState="false" ForeColor="red"></asp:Label>
                                            <asp:HyperLink NavigateUrl="javascript:UserZambaLogin()" ID="hylLoginWindows" 
                                                Visible="false" runat="server" />
                                        </td>
                                </tr>
                            </table>
                            <hr />
                            <center>
                                <asp:Table ID="tblBtnIngresar" Width="25%" align="center" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Button ID="Submit1" OnClick="Submit1_Click" Text="Ingresar" BackColor="#FFFBFF"
                                                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ForeColor="#284775"
                                                runat="server" Font-Size="Small" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkWindowsLogIn" Visible="true" Text="Ingresar con otro usuario"
                                                runat="server" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <asp:Table runat="server" ID="tbl2" Width="49%" Height="80px">
                                    <asp:TableRow runat="server">
                                        <asp:TableCell runat="server" Width="54%" Height="100px">
                                            <table>
                                                <tr>
                                                    <td height="41">
                                                        <div align="left">
                                                            <strong>
                                                                <asp:Label runat="server" Text="Nombre de Usuario " ID="lblUserName">
                                                                </asp:Label>
                                                            </strong>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" Width="185px" ID="txtUserName">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" Font-Bold="true" ForeColor="Red" Text=" (Requerido)" ID="RequiredFieldValidator1">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div align="left">
                                                            <strong>
                                                                <asp:Label runat="server" Text="Clave de Acceso " ID="lblPassword">
                                                                </asp:Label>
                                                            </strong>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" Width="185px" ID="txtPassword" TextMode="Password">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ForeColor="Red" Font-Bold="true" runat="server" Text=" (Requerido)" ID="RequiredFieldValidator2">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </center>
                            <table width="25%" align="center" cellspacing="20">
                                <tr>
                                    <td>
                                        <asp:Button ID="Submit2" OnClick="Submit2_Click" Text="Iniciar Sesión" BackColor="#FFFBFF"
                                            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ForeColor="#284775"
                                            runat="server" /></td>
                                </tr>
                            </table>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lvlInvisible" runat="server" Height="140"></asp:Label>
                        <center>
                            <span style="vertical-align: middle;">Copyright© 2007 Stardoc Argentina - Todos los
                                derechos reservados.
                                <br />Resolución Óptima 1024 x 768
                            </span>
                        </center>
                    </td>
                </tr>
            </table>
        </font>
    </form>
</body>
</html>