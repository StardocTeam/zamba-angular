<%@ Page Language="C#" AutoEventWireup="false" Inherits="Notificatios_SendMails" Codebehind="SendMails.aspx.cs" %>
<%@ Register Src="~/Notifications/WCSendMail.ascx" TagName="UCSendMails" TagPrefix="UC1" %>
<%@ Import Namespace="System.Web.Optimization" %>
<html>
<head id="Head2" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../Content/Styles/ZambaUIWebTables.css" />

     <%:Scripts.Render("~/bundles/jqueryCore") %>
     <script src="../Scripts/jquery-2.2.2.js"></script> 
     <script src="../Scripts/jquery.blockUI.js" type="text/javascript"></script>  
       

    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>
    <style type="text/css">
        body
        {
            font-family: Tahoma, Verdana;
            font-size: 11px;
            font-weight: normal;
        }
        
        input, select, textarea, button, iframe
        {
            border: 1px solid #AAAAAA;
        }
        input[type=checkbox]
        {
            border: 0px;
        }
        a, img, imglink
        {
            text-decoration: none;
            border: 0;
        }
        table
        {
            empty-cells: show;
        }
        .MailHeader
        {
            background: url(../Content/Images/hpbrp.gif) #5c9ccc repeat-x;
            border: solid 1px #4297d7;
            width: 100%;
            height: 40px;
            border-collapse: collapse;
        }
        .TextHeader
        {
            font-size: 20px;
            text-align: right;
        }
    </style>
    <title>Enviar documento por Mail</title>
    <script>
        function ShowLoadingAnimation() {
            if ($(".blockUI").length == 0) {
                $.blockUI({ message: '<h1>Enviando mail...</h1><br>Por favor aguarde y no cierre la ventana.<br>' });
            }
        }
        function hideLoading() {
            if ($(".blockUI").length > 0)
                $.unblockUI();
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server" enctype="multipart/form-data" >      
  
                <UC1:UCSendMails runat="server" ID="Arbol" class="actionsHeader" />
           
    </form>
</body>
</html>
