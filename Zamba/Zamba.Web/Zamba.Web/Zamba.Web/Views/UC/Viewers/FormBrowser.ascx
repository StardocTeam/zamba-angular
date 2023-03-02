<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Viewers_FormBrowser" EnableViewState="true" CodeBehind="FormBrowser.ascx.cs" %>
<%@ Register TagPrefix="ind" TagName="CompletarIndices" Src="~/Views/UC/Index/DocTypesIndexs.ascx" %>
<%@ Register TagPrefix="tb" TagName="DocumentToolbar" Src="~/Views/UC/Viewers/DocToolbar.ascx" %>

<asp:HiddenField ID="hdnIsShowing" runat="server" />
<asp:HiddenField ID="DocId" runat="server" />
<nav id="navToolbar" runat="server" class="navbar navbar-toggleable-sm navbar-default navbar-fixed-top fixed-top-3 " role="navigation" style="min-height: 0; border: none; border-radius: 0; margin: 0;">
    <div class="Toolbar" id="Toolbar-div" style="overflow: visible;">
        <%--              <a class="navbar-brand tasklogo" href="#"></a>--%>

        <asp:Label runat="server" ID="lblClose" onclick="CloseMe()" Style="cursor: pointer;" Text="Cerrar" Visible="false"> </asp:Label>
        <asp:UpdatePanel ID="uppnlToolbarDetailActions" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <tb:DocumentToolbar ID="docTB" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</nav>

<style>
    .mini-submenu {
        display: none;
        width: 42px;
        padding-left: 30px;
    }

        .mini-submenu:hover {
            cursor: pointer;
        }

    #slide-submenu {
        cursor: pointer;
    }

    #toogleTree {
        width: 20px;
        height: 100%;
        top: 0;
        right: 0;
        background-color: lightgray;
        position: absolute;
        border: 1px;
        z-index: 9;
        border-style: outset;
        cursor: pointer;
    }

    .sidebarcolumn {
        margin: 0;
        padding: 0;
        background-color: #eeeeee;
    }

    .fixed-top-2 {
        margin-top: 38px;
    }
</style>


<div id="wrapper" class="toggled ">
    <div class="sidebar" id="DivIndices" runat="server" style="display: none">
        <%-- <div class="sidebarcolumn col-sm-3 col-md-3">--%>

        <%--  <div id="toogleTree" onclick="toogleTree(this);">
                <span class="glyphicon glyphicon-chevron-left" style="margin-top: 200px;"></span>
            </div>--%>
        <%--    </div>--%>



        <!-- Sidebar -->
        <div id="sidebar-wrapper" class="scrollbarIndices hidden-xs" style="overflow-x: hidden">
            <ul class="sidebar-nav force-overflow">
                <asp:UpdatePanel ID="uppnDetailViewer" runat="server">
                    <ContentTemplate>
                        <ind:CompletarIndices ID="completarindice" runat="server" EnableViewState="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </ul>
        </div>

    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="">
        <div class="">
            <div class="row">
                <div class="col-lg-12">
                    <div id="documentViewerController" ng-controller="DocumentViewerController">
                        <div id="DivDoc" runat="server">
                            <asp:PlaceHolder ID="frmHolder" runat="server"></asp:PlaceHolder>
                            <div>
                                <asp:Label runat="server" Text="" ID="lblDoc" Visible="false"></asp:Label>
                            </div>
                            <div id="divViewer Divform">
                                <asp:Literal runat="server" ID="docViewer" Mode="PassThrough" EnableViewState="true"></asp:Literal>
                            </div>
                            <div id="frmViewer" runat="server"></div>
                        </div>
                    </div>
                </div>

                <div id="divMessage" title="Zamba Software" style="display: none; height: 70px; overflow: visible; align-content: center; margin-top: 30px">
                    <span>No se pueden realizar cambios en los atributos</span>
                    <br />
                    <input type="button" value="OK" id="btnCloseMsg" onclick="$('#divMessage').dialog('close');" style="width: 40pt" />
                </div>

                <div id="divValidationFail" title="Zamba Software" style="display: none; height: 70px; overflow: visible; align-content: center">
                    <span>Algunos campos no superaron las validaciones.<br />
                        Por favor revise los campos incorrectos.</span>
                    <br />
                    <input type="button" value="OK" id="Button1" onclick="$('#divValidationFail').dialog('close');" style="width: 40pt" />
                </div>

                <%-- Hidden para marcar si la regla consulta o no --%>
                <input id="hdnRuleActionType" type="text" style="display: none" name="hdnRuleActionType" />

            </div>
        </div>
    </div>
    <!-- /#page-content-wrapper -->

</div>





<script type="text/javascript">

    $(document).ready(function () {

        if ($('#rowTaskHeader').length) {
            $('#ctl00_ContentPlaceHolder_ucTaskDetail_ctl00_docTB_BtnClose').hide();
        }

        ExecuteFormReadyActions();
        //$(".btn-openNewTab").hide();

        <% if (completarindice.Visible)
    { %>
        //Visibilidad del panel de índices
        var divDoc = $('#<%=DivDoc.ClientID %>');
        var docContainer = $('#<%=frmViewer.ClientID %>');
        $("#btnShowHide").click(function () {
            SetIndexPnlVisibility(this, divDoc, docContainer);
        });
        $("#ctl00_ContentPlaceHolder_ctl01_docTB_BtnShowIndexs").click(function () {
            SetIndexPnlVisibility(this, divDoc, docContainer);
        });

        <% }
    else
    { %>
        $("#DivIndices").remove();
        <% } %>

    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        FixFocusError();
        <% if (completarindice.Visible)
    { %>
        var divDoc = $('#<%=DivDoc.ClientID %>');
        var docContainer = $('#<%=frmViewer.ClientID %>');
        $("#btnShowHide").click(function () {
            SetIndexPnlVisibility(this, divDoc, docContainer);
        });
        $("#ctl00_ContentPlaceHolder_ctl01_docTB_BtnShowIndexs").click(function () {
            SetIndexPnlVisibility(this, divDoc, docContainer);
        });

        //Al termina de hacer un postback de Ajax, acomodamos el documento para que se visualice bien.
        var a = $(window).width() - $("#DivIndices").width() - 5;
        $("#separator").animate({ width: $("#DivIndices").width() }, "slow");
        $('#<%=DivDoc.ClientID %>').animate({ width: a }, "slow");
        <% } %>
    }

    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
    function initializeRequestHandler(sender, args) {
        FixFocusError();
    }

    function ReloadFrame() {

        var iframe = document.frames ? document.frames["ctl00_ContentPlaceHolder1_formBrowser"] : $("#ContentPlaceHolder1_formBrowser");
        var ifWin = iframe.contentWindow || iframe;
        var url = ifWin.location;

        ifWin.location.reload();
        ifWin.location = url;
    }

    function CloseMe() {

        parent.CloseModal();
    }

    $(function () {
        $('.mini-submenu').fadeIn();
        $('#DivDoc').removeClass('col-md-9');
        $('#DivDoc').addClass('col-md-12');
    });


    <% if (completarindice.Visible)
    { %>
    $(function () {
        $('#slide-submenu').on('click', function () {
            $(this).closest('.sidebarcolumn').fadeOut('slide', function () {
                $('.mini-submenu').fadeIn();
                $('#DivDoc').removeClass('col-md-9');
                $('#DivDoc').addClass('col-md-12');
            });
        });

        $('.mini-submenu').on('click', function () {
            $(this).next('.sidebarcolumn').toggle('slide');
            $('.mini-submenu').hide();
            $('#DivDoc').removeClass('col-md-12');
            $('#DivDoc').addClass('col-md-9');
        });
    })
    <% } %>

    function toogleTree(_this) {
        var collapse = $("#ContentPlaceHolder_ucTaskDetail_ctl00_uppnDetailViewer").data("collapse") == true || false;
        if (collapse === false) {
            $("#contentDiv").data("width", $("#contentDiv").css("width")).css("width", "100%");
            $(".vsplitter").hide();
            $("#toogleTree").css("right", "inherit")
                .children("span").removeClass("glyphicon glyphicon-chevron-left")
                .addClass("glyphicon glyphicon-chevron-right");
            $("#ContentPlaceHolder_ucTaskDetail_ctl00_uppnDetailViewer").children().not("#toogleTree").hide();
            $(_this).css("left", "0");
        }
        else {
            $("#contentDiv").css("width", $("#contentDiv").data("width"));
            $(".vsplitter").show();
            $("#toogleTree").css("right", "0")
                .children("span").removeClass("glyphicon glyphicon-chevron-right")
                .addClass("glyphicon glyphicon-chevron-left");
            $("#ContentPlaceHolder_ucTaskDetail_ctl00_uppnDetailViewer").children().not("#toogleTree").show();
            $(_this).css("left", "auto");
        }
        $("#ContentPlaceHolder_ucTaskDetail_ctl00_uppnDetailViewer").data("collapse", !collapse);
    }


</script>
