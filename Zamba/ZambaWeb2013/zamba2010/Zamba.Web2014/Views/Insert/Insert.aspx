<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Insert.aspx.cs" Inherits="Views_Insert_Insert"
UICulture="Auto" Culture="Auto" EnableViewState="true" EnableEventValidation="false" %>

<%@ Import Namespace="System.Web.Optimization" %>
<%@ Register Src="~/Views/UC/Upload/ucUploadFile.ascx" TagName="ucUploadFile" TagPrefix="uc1" %>
<%@ Register Src="~/Views/UC/Upload/ucDocTypes.ascx" TagName="ucDocTypes" TagPrefix="uc2" %>
<%@ Register Src="~/Views/UC/Index/ucDocTypeIndexsForInsert.ascx" TagName="ucDocTypesIndexs"
    TagPrefix="uc3" %>
<html>
<head id="Head2" runat="server">
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/Styles/masterblank")%>
        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/bundles/Styles/ZStyles")%>
        <%: Styles.Render("~/Content/css")%>
        <%: Styles.Render( "~/bundles/Styles/bootstrap")%>
        <%: Styles.Render("~/Content/bootstrap-responsive.css")%>
        <%: Tools.GetJqueryCoreScript(HttpContext.Current.Request)%>
        <%: Scripts.Render( "~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/jqueryAddIns") %>
        <%: Scripts.Render("~/bundles/ZScripts") %>
        <%: Scripts.Render("~/bundles/bootstrap") %>
</asp:PlaceHolder>
    <title>Insertar documento</title>
</head>
<body id="InsertIframeBody" class="body-Frm-Views-Insert">
    <div>
    <form id="aspnetForm" runat="server" class="form" enctype="multipart/form-data"> 
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <div id="dialoginfoetapa" style="display: none" title="Zamba Software">
        <asp:Label ID="lblpopup" runat="server" Text="Contendra el texto de la etapa"></asp:Label>
    </div>
    <ajaxToolKit:ToolkitScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        <Services>
            <asp:ServiceReference Path="~/Services/IndexsService.asmx" />
        </Services>
    </ajaxToolKit:ToolkitScriptManager>
    <asp:Panel runat="server" ID="pnlDatos">
        <div class="div-Frm-Views-Insert">
            <div style="width: 560px; padding: 0px;" id="test">
                <div>
                    <uc2:ucDocTypes runat="server" ID="ucDocTypes" EnableViewState="false" />
                </div>
                <div style="padding-top:10px; overflow-x: auto;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <uc3:ucDocTypesIndexs runat="server" ID="ucDocTypesIndexs" EnableViewState="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="padding-top:10px">
                    <uc1:ucUploadFile runat="server" ID="ucUploadFile" EnableViewState="false" />
                    <asp:Label ID="lblMsj" runat="server" Visible="false" CssClass="error" Style="padding: 10px 0px 10px 0px"></asp:Label>
                </div>
            </div>
            <br />
            <div>
                <asp:LinkButton runat="server" Text="Insertar" ID="lnkInsertar" OnClick="lnkInsertar_clic"
                    CssClass="btn btn-success btn-sm" OnClientClick="OpentInsertingDialog();" />
                <asp:LinkButton runat="server" Text="Cancelar" ID="lnkCancelar" OnClientClick="CloseInsert();"
                    class="btn btn-danger btn-sm" OnClick="lnkCancel_clic" />
            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblInsertado" runat="server" Visible="false" Style="top: 200px; left: 110px;
        position: absolute;" Font-Size="Medium" Font-Bold="true" Text="El documento ha sido insertado con exito"></asp:Label>
    <div id="divInserting" style="display: none; text-align: center" title="Zamba">
        <h2>Insertando documento...</h2>
        <button id="btnForceClose" style="display:none">Cerrar</button>
    </div>
    </form>
    </div>

<script type="text/javascript">
    function OpenTask(url, taskid, taskname) {
        window.opener.OpenTask(url, taskid, taskname);
        closeSelf();
    }
    function closeSelf() {
        window.close();
    }
    function maxLenght(tb, max) {
        return (tb.value.length < max);
    }

    function FixAndPosition(objDlg) {
        objDlg.css("top", "100px");
        objDlg.css("position", "absolute");
        var zIndexMax = getMaxZ();
        $(objDlg).css({ "z-index": Math.round(zIndexMax) });
    }

    function OpentInsertingDialog() {
        $("#<%=pnlDatos.ClientID%>").hide();
        $("#divInserting").show();
        window.setInterval(function () { $("#btnForceClose").show(); }, 10000);

        if (parent != this)
            parent.ShowLoadingAnimation();
    }

    function CloseInsertingDialog() {
        t = setTimeout("hideLoading();", 500);
    }

    function hideLoading() {
        $("#divInserting").hide();
        $("#<%=pnlDatos.ClientID%>").show();
        $("#btnForceClose").hide();

        if (parent != this)
            parent.hideLoading();
    }

    function ModifyHeight() {
        var height = getHeightScreen();

        <%-- Height es 0 cuando viene del thick box, por ende le mando el alto justo y lo calculo por el 60% --%>
        if (height < 1) {
            height = 572;
            height = height * 0.6;
        } else {
            <%-- De otra forma, lo calculo por el 50% --%>
            height = height * 0.5;
        }

        $("#divAttributes").height(height);
    }
    <%-- Se coloco para esconder los datapicker ya que se superponian --%>
    $(document).ready(function () {
        setTimeout(function(){
            $(".ui-datepicker-trigger").hide();
        }, 100);
           
    });

</script>
</body>
</html>
