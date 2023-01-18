<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/ImageTemplate.ascx" TagPrefix="my" TagName="imageTemplate" %>
<%@ Register Assembly="SharpPieces.Web.Controls.Upload" Namespace="SharpPieces.Web.Controls"
    TagPrefix="piece" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload</title>
    <script type="text/javascript" src="js/lightbox.js"></script>


    <link href="css/lightbox.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <piece:Upload runat="server" ID="upload" InstantStart="False" BrowseText="Seleccionar"
            AllowDuplicates="false" Types="{'Imagenes': '*.jpg; *jpeg; *.gif; *.bmp'} " Trigger="Button1"
            OnFileReceived="upload_FileReceived" OnAllComplete="" />
    </div>
    <div style="width: 500px; background-color: White;">
        <ul class="photoupload-queue" id="photoupload-queue" />
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="Subir" />
    </div>
    <asp:ScriptManager ID="smMain" runat="server" />
    <asp:UpdateProgress ID="upgMain" AssociatedUpdatePanelID="upMain" runat="server">
        <ProgressTemplate>
            <img src="css/loading.gif" alt="Cargando" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upMain" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Table ID="tblImages" runat="server" />
            <asp:Button ID="btDelete" runat="server" Text="Borrar" OnClick="btDelete_Click" />
            <asp:Button ID="btRefreshImages" runat="server" OnClick="btRefreshImages_Click" Text="Actualizar Imagenes" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <input type="button" onclick="addLoadEvent(initLightbox);" value="Refresh" />
    </form>
</body>
</html>
