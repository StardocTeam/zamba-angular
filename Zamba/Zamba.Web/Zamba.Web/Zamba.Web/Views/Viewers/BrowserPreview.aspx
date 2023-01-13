<%@ Page Language="C#" AutoEventWireup="true" Inherits="BrowserPreview" EnableEventValidation="false" Codebehind="BrowserPreview.aspx.cs" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <meta charset="UTF-8" />
    <title title="Zamba Web"></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
<%--    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="apple-touch-icon-precomposed" href="img/icon.png" />
    <link rel="apple-touch-icon" href="img/icon.png" />
    <link rel="apple-touch-startup-image" href="img/splash.png" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link rel="apple-touch-icon" sizes="57x57" href="apple-icon-57x57.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="apple-icon-72x72.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="apple-icon-114x114.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="apple-icon-144x144.png" />
    <link rel="apple-touch-icon-precomposed" sizes="57x57" href="apple-icon-57x57-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="apple-icon-72x72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="apple-icon-114x114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="apple-icon-144x144-precomposed.png" />--%>

    <!-- Startup splash screen images -->
    <!-- iPhone 320x460 -->
   <%-- <link href="<?php echo get_stylesheet_directory_uri() . '/images/startup-iphone.png'; ?>" media="(device-width: 320px) and (device-height: 480px) and (-webkit-device-pixel-ratio: 1)" rel="apple-touch-startup-image">
    <!-- iPhone Retina 640x920 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-iphone-retina.png'; ?>" media="(device-width: 320px) and (device-height: 480px) and (-webkit-device-pixel-ratio: 2)">
    <!-- iPhone 5 640x1096 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-iphone5.png'; ?>" media="(device-width: 320px) and (device-height: 568px) and (-webkit-device-pixel-ratio: 2)">
    <!-- iPad Portrait 768x1004 -->
    <link href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-portrait.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: portrait) and (-webkit-device-pixel-ratio: 1)" rel="apple-touch-startup-image">
    <!-- iPad Landscape 748x1024 -->
    <link href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-landscape.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: landscape) and (-webkit-device-pixel-ratio: 1)" rel="apple-touch-startup-image">
    <!-- iPad Retina Portrait 1536x2008 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-retina-portrait.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: portrait) and (-webkit-device-pixel-ratio: 2)">
    <!-- iPad Retina Landscape 1496x2048 -->
    <link rel="apple-touch-startup-image" href="<?php echo get_stylesheet_directory_uri() . '/images/startup-ipad-retina-landscape.png'; ?>" media="(device-width: 768px) and (device-height: 1024px) and (orientation: landscape) and (-webkit-device-pixel-ratio: 2)">

    <link id="lnkWebIcon" rel="shortcut icon" runat="server" type="image/x-icon" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWeb.css" />--%>

    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <link href="../../Content/site.css" rel="stylesheet" />
        <%: Styles.Render("~/bundles/Styles/bootstrap")%>
      <%--  <%: Styles.Render("~/Content/bootstrap-responsive.css")%>--%>
        <%:Scripts.Render("~/bundles/jqueryCore") %>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
        
        <script src="../../Scripts/app/view/zamba.view.js"></script>

    </asp:PlaceHolder>
</head>

<body>
        <form runat="server">
        <div class="container-fluid">


    <div class="jumbotron">
        <div class="col-xs-offset-3 col-xs-4">
    <button id="BtnLoadPage" type="button" role="button" class="btn btn-primary" onclick="loadOriginalFromWeb();">Cargar Adjunto</button>
  <div class="alert alert-info" role="alert" id="LoadingDiv"></div>
</div>

    </div>
   </div>
      </form>
</body>
</html>