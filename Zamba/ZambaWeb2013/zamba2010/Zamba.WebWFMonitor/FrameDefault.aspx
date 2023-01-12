<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrameDefault.aspx.vb" Inherits="FrameDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FRAMES</title>

    <script type="text/javascript" src="Scripts/querystring.js"></script>

</head>

<script type="text/javascript">
var p = "page";
</script>

<%--<frameset rows="500,*" frameborder="NO" border="0" framespacing="0" cols="*"> 
  <frame name="menu" scrolling="YES" src =  "Monitor.aspx">
  <frame name="mainFrame" contenteditable="true" src= ""> 
</frameset>--%>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
//function GetURL() { //if (String(Request.QueryString("URL")) == "undefined") //{return
"Monitor.aspx";} //else //{return Request.QueryString("URL");} //} 