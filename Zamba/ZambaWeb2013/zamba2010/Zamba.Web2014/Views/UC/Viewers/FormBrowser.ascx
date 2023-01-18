<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormBrowser.ascx.cs" Inherits="Views_UC_Viewers_FormBrowser" EnableViewState="true" %>
<%@ Register TagPrefix="ind" TagName="CompletarIndices" Src="~/Views/UC/Index/DocTypesIndexs.ascx" %>
<%@ Register TagPrefix="tb" TagName="DocumentToolbar" Src="~/Views/UC/Viewers/DocToolbar.ascx" %>

<asp:HiddenField ID="hdnIsShowing" runat="server" />

<nav id="navToolbar" runat="server" class="navbar navbar-default" role="navigation" style="margin-bottom:0px; min-height: 20px">
    <div class="Toolbar container-fluid" id="Toolbar-div" style="overflow: visible; padding:5px">
        <asp:Label runat="server" ID="lblClose" onclick="CloseMe()" Style="cursor: pointer;" Text="Cerrar" Visible="false"> </asp:Label>
        <asp:UpdatePanel ID="uppnlToolbarDetailActions" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <tb:DocumentToolbar ID="docTB" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</nav>

<style>
 .mini-submenu{
  display:none;  
 
  width: 42px;
  padding-left: 30px;
}

.mini-submenu:hover{
  cursor: pointer;
}

#slide-submenu{
  cursor: pointer;
}
</style>

<div>
    <div class="sidebar" id="DivIndices" runat="server">
        <div class="mini-submenu">
            <button type="button" class="btn btn-primary btn-xs"><i class="glyphicon glyphicon-chevron-right"></i></button>
        </div>

        <div class="sidebarcolumn col-sm-3 col-md-3 ">
            <span class="pull-right" id="slide-submenu">
                <button type="button" class="btn btn-primary btn-xs"><i class="glyphicon glyphicon-chevron-left"></i></button>
            </span>

            <asp:UpdatePanel ID="uppnDetailViewer" runat="server">
                <ContentTemplate>
                    <ind:CompletarIndices ID="completarindice" runat="server" EnableViewState="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="DivDoc" runat="server" style="height: 100%; overflow: visible;">
        <asp:PlaceHolder ID="frmHolder" runat="server"></asp:PlaceHolder>
        <div>
            <asp:Label runat="server" Text="" ID="lblDoc"></asp:Label>
        </div>
        <div id="divViewer" style="width: 100%; height: 100%; overflow: visible">
            <asp:Literal runat="server" ID="docViewer" Mode="PassThrough" EnableViewState="true"></asp:Literal>
        </div>
        <div id="frmViewer" runat="server" style="width: 100%; overflow: auto"></div>
    </div>
</div>

<div id="divMessage" title="Zamba Software" style="display:none; height:70px; overflow:visible; align-content:center">
    <span>No se pueden realizar cambios en los atributos</span>
    <br />
    <input type="button" value="OK" id="btnCloseMsg" onclick="$('#divMessage').dialog('close');" style="width:40pt" />
</div>

<div id="divValidationFail" title="Zamba Software" style="display:none; height:70px; overflow:visible;align-content:center">
    <span>Algunos campos no superaron las validaciones.<br />
        Por favor revise los campos incorrectos.</span>
    <br />
    <input type="button" value="OK" id="Button1" onclick="$('#divValidationFail').dialog('close');" style="width:40pt" />
</div>

<%-- Hidden para marcar si la regla consulta o no --%>
<input id="hdnRuleActionType" type="text" style="display: none" name="hdnRuleActionType" />


<script type="text/javascript">

    $(document).ready(function () {
        ExecuteFormReadyActions();
        $(".btn-openNewTab").hide();

        <% if(completarindice.Visible) { %>
        //Visibilidad del panel de índices
        var divDoc = $('#<%=DivDoc.ClientID %>');
        var docContainer = $('#<%=frmViewer.ClientID %>');
        $("#btnShowHide").click(function () {
            SetIndexPnlVisibility(this, divDoc, docContainer);
        });
        <% } else { %>
        $("#DivIndices").remove();
        <% } %>
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        FixFocusError();
        <% if(completarindice.Visible) { %>
        var divDoc = $('#<%=DivDoc.ClientID %>');
        var docContainer = $('#<%=frmViewer.ClientID %>');
        $("#btnShowHide").click(function () {
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
        var iframe = document.frames ? document.frames["ctl00_ContentPlaceHolder1_formBrowser"] : $("#ctl00_ContentPlaceHolder1_formBrowser");
        var ifWin = iframe.contentWindow || iframe;
        var url = ifWin.location;

        ifWin.location.reload();
        ifWin.location = url;
    }

    function CloseMe() {
        parent.CloseModal();
    }

    $(function(){
        $('.mini-submenu').fadeIn();
        $('#DivDoc').removeClass('col-md-9');
        $('#DivDoc').addClass('col-md-12');
    });


    <% if(completarindice.Visible) { %>
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



</script>
