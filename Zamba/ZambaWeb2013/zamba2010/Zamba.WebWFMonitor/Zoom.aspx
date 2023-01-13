<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Zoom.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/javascript">
<!--

//var Id = "<%=Image1.ClientID%>";
//alert(document.getElementsByName(Id).value); 
//alert(document.getElementsByTagName(Id).value); 


//alert(document.getElementById("<%=Image1.ClientID%>").value);
//alert(document.getElementById("<%=Image1.ClientID%>").ImageUrl);
//alert(document.getElementById("Image1").value);
//alert(document.getElementById("Image1").ImageUrl);
//document.getElementById("<%=Image1.ClientID%>").style.zoom='200%'";


function Button2_onclick() {
var Zoom = document.getElementById("<%=HiddenField1.ClientID%>").value - 5 + 15;
if (Zoom > 400) {Zoom = 400};
document.getElementById("<%=HiddenField1.ClientID%>").value = Zoom;
document.images.Image1.style.zoom= Zoom + "%";}

function Button1_onclick() {
var Zoom = document.getElementById("<%=HiddenField1.ClientID%>").value - 10;
if (Zoom < 10) {Zoom = 10};
document.getElementById("<%=HiddenField1.ClientID%>").value = Zoom;
document.images.Image1.style.zoom= Zoom + "%";}



function window_onload() {}

// -->
</script>
</head>
<body language="javascript" onload="return window_onload()">
    <form id="form1" runat="server">
    <div>
        <input id="Button2" type="button" value="+" language="javascript" onclick="return Button2_onclick()" />&nbsp;
        <input id="Button1" type="button" value="-" language="javascript" onload="this.style.zoom='30%'"  onclick="return Button1_onclick()" /><br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="Default3.aspx?Zoom=100" /><br />
        <br />
        &nbsp;<br />
        <br />
        <br />
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" Value="30" />
        </div>
    </form>
</body>
</html>
