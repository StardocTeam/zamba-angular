<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IntranetMarsh.UsrLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Centro de Informacion Marsh</title>
    <link href="../Images/Default.css" rel="stylesheet" type="text/css" />
    <SCRIPT language=javascript 
src="http://www.marsh.com.ar/framework_js/marsh.js"></SCRIPT>

<SCRIPT language=javascript>chooseStyle();</SCRIPT>

<SCRIPT language=javascript 
src="http://www.marsh.com.ar/framework_js/marshpublic.js" 
type=text/javascript></SCRIPT>
<LINK href="http://www.marsh.com.ar/framework_css/spiffyCal_v2_1.css" 
type=text/css rel=stylesheet>
<SCRIPT language=JavaScript 
src="http://www.marsh.com.ar/framework_js/spiffyCal_v2_1.js"></SCRIPT>
<LINK href="http://www.marsh.com.ar/framework_css/public.css" type=text/css 
rel=stylesheet>
</head>

<script language="javascript" type="text/javascript">

    //function preventPreviousPage()
    //{
        // [Gaston] 
        // Si el usuario presiona el botón para volver a la página anterior no va a poder ir a la página anterior, sino que va a volver a la misma 
        // página de login. Esto se
        //if(history.length>0)
            //history.go(+1);
    //}
    
//    function getIPClient()
//    {
//        try
//        {
//            var ip = java.net.InetAddress.getLocalHost().getHostAddress();
//            hs = document.getElementById("vIPClient");
//            hs.value = ip;
//        }
//        catch(err)
//        {
//        }
//    }
    
//    function showuser()
//    {
//        var wshshell;
//        
//        try
//        {
//    	    wshshell=new ActiveXObject("WScript.Shell");
//        }
//        catch(err)
//        {
//        }
//        
//        if (wshshell!=null)
//        {
//    	    var username =wshshell.ExpandEnvironmentStrings("%username%");
//    	    hs = document.getElementById("vUserName");

//		    hs.value = username;
//		    otroHs = document.getElementById("lblLoginWindows");
//		    otroHs.innerHTML = 'Bienvenido ' + hs.value + ', haga click en el botón para ingresar con su usuario Windows, o puede elegir entrar con otro usuario de Zamba.';
//    		
//    		// [Gaston] 
//    		// Se obtiene el nombre de la computadora del usuario
//		    var computerName = wshshell.ExpandEnvironmentStrings("%computername%");
//		    // Se obtiene la variable vComputerName
//		    hs = document.getElementById("vComputerName")
//		    // Se asigna a la propiedad value de vComputerName el nombre de la computadora del usuario ...
//		    hs.value = computerName
//		    
//		    // ... para después utilizar dicho valor en el código del servidor haciendo lo siguiente:
//		    
//		    // HtmlInputHidden varComputerName;                                 Declaración de una variable de tipo HtmlInputHidden en el código del servidor
//		    // varComputerName = (HtmlInputHidden)FindControl("vUserName");     El contenido de vComputerName pasa a varComputerName
//		    // varComputerName.Value                                            Nombre de la computadora del usuario
//	    }
//    }
//    
//   function UserZambaLogin()
//    {
//    o1 = document.getElementById("chkWindowsLogIn");
//    o1.click();
//    o2 = document.getElementById("Submit1");
//    o2.fireEvent('OnClick');
//    alert('Hola Manola');
//    }
    
</script>

<!--
  <%--var wshshell="new" ActiveXObject=""("
  var="" username=wshshell.ExpandEnvironmentStrings("%username%"
  --%>-->
<body topmargin="0" id="loginBody" runat="server" >
<div align="center" style="margin:0px auto;height:100%;width:100%">
    <form id="form1" runat="server" defaultbutton="Submit2">
   <%-- <input id="vUserName" runat="server" type="hidden" />
   
    <input id="vIPClient" runat="server" type="hidden" />
    <input id="flagWindowsLogin" type="hidden" runat="server" />--%>
        <input id="vComputerName" runat="server" type="hidden" /> 
        <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
        <TBODY>
        <TR bgColor=#003366>
          <TD width=12 
          background=http://www.marsh.com.ar/framework_images/pshbg.gif><IMG 
            height=51 src="http://www.marsh.com.ar/framework_images/spacer.gif" 
            width=12></TD>
          <TD vAlign=center width=146 
          background=http://www.marsh.com.ar/framework_images/pshbg.gif><IMG 
            alt=Marsh 
            src="http://www.marsh.com.ar/framework_images/marsh_logo.gif"></TD>
          <TD class=M2cffffff vAlign=bottom 
          background=http://www.marsh.com.ar/framework_images/pshbg.gif>&nbsp; 

            <TABLE height="63%" cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TBODY>
              <TR>
                <TD align="left"><FONT face=verdana color=white 
                  size=3><B>&nbsp;&nbsp;Argentina </B></FONT></TD></TR>
              <TR>
                <TD align="left" style="padding-bottom:10px" class=M2cffffff vAlign=bottom>&nbsp;&nbsp;&nbsp; <asp:Label ID="DateLabel" runat="server" ForeColor="White" Font-Size="Small"></asp:Label> </TD></TR>
                <tr>
                <td>
                <div style="width:630px; text-align:center">
                <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
        <TBODY>
        <TR>
          <TD width=1 bgColor=#ffffff><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD width=120 bgColor=#336699><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
          width=120></TD>
          <TD width=1 bgColor=#ffffff><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD width=120 bgColor=#006600><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
          width=120></TD>
          <TD width=1 bgColor=#ffffff><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD width=120 bgColor=#666633><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
          width=120></TD>
          <TD width=1 bgColor=#ffffff><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD width=120 bgColor=#666666><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
          width=120></TD>
          <TD width=1 bgColor=#ffffff><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD width=120 bgColor=#660066><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
          width=120></TD>
          <TD width=1 bgColor=#660066><IMG height=1 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
        width=1></TD></TR>
        <TR vAlign=center>
          <TD width=1 bgColor=#ffffff><IMG height=20 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD align=middle width=120 bgColor=#336699 height=18><A 
            class=M2cffffff 
            href="http://www.marsh.com.ar/index.cfm?logout=ok">Inicio</A></TD>
          <TD width=1 bgColor=#ffffff><IMG height=20 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD align=middle width=120 bgColor=#006600><A class=M2cffffff 
            href="http://www.marsh.com.ar/auto.cfm?myurl=marsh/servicios.cfm">Servicios</A></TD>
          <TD width=1 bgColor=#ffffff><IMG height=20 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD align=middle width=120 bgColor=#666633><A class=M2cffffff 
            href="http://www.marsh.com.ar/auto.cfm?myurl=marshregional/latinoamerica.cfm">Marsh 
            América Latina</A></TD>
          <TD width=1 bgColor=#ffffff><IMG height=20 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD align=middle width=120 bgColor=#666666><A class=M2cffffff 
            href="http://www.marsh.com.ar/auto.cfm?myurl=marsh/nosotros_marsh.cfm">Nosotros</A></TD>
          <TD width=1 bgColor=#ffffff><IMG height=20 alt="" 
            src="http://www.marsh.com.ar/framework_images/spacer.gif" 
width=1></TD>
          <TD align=middle width=120 bgColor=#660066 colSpan=2><A 
            class=M2cffffff 
            href="http://www.marsh.com.ar/auto.cfm?myurl=marshregional/acerca_marsh.cfm">Acerca 
            de Marsh</A></TD></TR></TBODY></TABLE>
                </div>
                </td>
                </tr>
                </TBODY></TABLE></TD>
                
          <TD class=M2cffffff vAlign=bottom align=right 
          background=http://www.marsh.com.ar/framework_images/pshbg.gif 
          bgColor=#003366>&nbsp; 
            <TABLE height="63%" cellSpacing=0 cellPadding=0 width="100%" 
            border=0>
              <TBODY>
              <TR>
                <TD><FONT face=verdana color=white size=3><B>&nbsp;</B></FONT> 
                </TD></TR>
              <TR>
                <TD>&nbsp;</TD></TR></TBODY></TABLE></TD>
          <TD width=5 
          background=http://www.marsh.com.ar/framework_images/pshbg.gif><IMG 
            height=1 src="http://www.marsh.com.ar/framework_images/spacer.gif" 
            width=5></TD></TR></TBODY></TABLE>       
    <table id="Table1" border="0" runat="server" cellpadding="0" cellspacing="0" height="100%" width="100%">
        <tr style="padding: 0">
            <td>
                <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0" style="width:100%">
                    <tr>
                        <%--<td style="width: 159" valign="top">
                         
                        </td>--%>
                        <td valign="top" height="100%" style="width: auto">
                            <table id="Table3" border="0" runat="server" cellpadding="0" cellspacing="0" height="100%"
                                style="width:100%">
                              
                                <tr>
                                    <td valign="top" height="100%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                     
                                            <tr>
                                                <td valign="top" colspan="2">
                                                    <%--<div>4--%>
                                                        <table style="width:100%; height: 500;
                                                            background-color: transparent;">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;<%--<asp:ScriptManager runat="server" ID="sc">
                                                                    </asp:ScriptManager>--%><hr />
                                                                    <div align="center">
                                                                        <table>
                                                                            <tr>
                                                                                <td height="25px">
                                                                                    <asp:Label ID="lblLoginWindows" Font-Bold="false" Visible="true" runat="server" Font-Size="Medium" />
                                                                                    <td>
                                                                                        <asp:Label ID="lblError" runat="Server" EnableViewState="false" ForeColor="red"></asp:Label>
                                                                                        <asp:HyperLink NavigateUrl="javascript:UserZambaLogin()" ID="hylLoginWindows" Font-Bold="false"
                                                                                            Visible="false" runat="server" />
                                                                                    </td>
                                                                                    </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <hr />
                                                                    <div align="center" style="width:100%">
                                                                        <asp:Table ID="tblBtnIngresar" Width="25%" align="center" runat="server">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Button ID="Submit1" OnClick="Submit1_Click" Text="Ingresar" Font-Size="Medium"
                                                                                        BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                                                                        Font-Names="Verdana" ForeColor="#284775" runat="server" />
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
                                                                            <asp:TableRow ID="TableRow2" runat="server">
                                                                                <asp:TableCell ID="TableCell2" runat="server" Width="54%" Height="100px">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td height="41">
                                                                                                <div align="left">
                                                                                                    <strong><font face="Arial, Helvetica, sans-serif" size="2">
                                                                                                        <asp:Label runat="server" Text="Nombre de Usuario " ID="lblUserName">
                                                                                                        </asp:Label>
                                                                                                    </font></strong>
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
                                                                                            <td height="41">
                                                                                                <div align="left">
                                                                                                    <strong><font face="Arial, Helvetica, sans-serif" size="2">
                                                                                                        <asp:Label runat="server" Text="Clave de Acceso " ID="lblPassword">
                                                                                                        </asp:Label>
                                                                                                    </font></strong>
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
                                                                    </div>
                                                                    <table width="25%" align="center" cellspacing="20">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="Submit2" OnClick="Submit2_Click" Text="Iniciar Sesión" BackColor="#FFFBFF"
                                                                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                                                                    Font-Size="0.8em" ForeColor="#284775" runat="server" 
                                                                                    CausesValidation="False" EnableViewState="False" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lvlInvisible" runat="server" Height="40" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    <%--</div>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" colspan="3" height="100%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                   <td id="Td7" runat="server" class="BackBot" style="font-size:small" height="20px" width="100%" align="center">
                                        <%--<a href="">Acerca de Zamba</a><img id="Img4" runat="server" src="images/line1.gif"
                                            style="border: 0" align="middle" width="5" height="36" />--%>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td8" runat="server" background="../images/footbg.gif" height="59">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</div>
</body>
</html>
