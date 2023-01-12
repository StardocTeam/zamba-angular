<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls.Indexs.DocTypesIndexs" Codebehind="DocTypesIndexs.ascx.cs" %>
<%@ Import Namespace="Zamba.Core" %>


<asp:HiddenField runat="server" ID="hdIndexId" />
<div class="">
<asp:Panel ID="pnlListadoIndices" runat="server" Height="100%" ScrollBars="none" >
    <fieldset title="Listado de Atributos"  enableviewstate="true" style="margin-top:10px; padding-left:10px; padding-right:10px; border: 0px !important;"  >
        <table title="Listado de Atributos" style="width: 17%;margin-bottom: 30px;margin-left: 84px;">
            <tr>
                 <td style="width: 55%;">
                    <asp:LinkButton ID="btnSaveChanges" runat="server" Height="32px" Font-Overline="false"
                        CssClass=" btn btn-info "  OnClick="SaveIndexChanges_Click" ToolTip="Guardar cambios realizados" Visible="true" style="-webkit-box-shadow: 2px 2px 5px #999;-moz-box-shadow: 2px 2px 5px #999;margin-right: 20px;">
                        <asp:ImageButton ID="btnSaveImage"  runat="server" Height="16px" Enabled="true" style="display:none !important" />
                        <span style="cursor: pointer">Guardar</span>
                    </asp:LinkButton>
                </td>
                <td style="width: 35%">
                    <div onclick="Reload_Click()">
                        <asp:LinkButton ID="btnCleanIndexs" runat="server" Height="32px" Font-Overline="false"
                            CssClass="btn btn-danger" ToolTip="Deshacer Cambios" Visible="true" style="-webkit-box-shadow: 2px 2px 5px #999;-moz-box-shadow: 2px 2px 5px #999;" >
                            <asp:ImageButton ID="btnCleanImage" runat="server" Height="16px" style="display:none !important" />
                            <span style="cursor: pointer">Deshacer</span>
                        </asp:LinkButton>
                    </div>
                </td>
               
               
              <%-- <td style="width: 10%" align="right">
                    <asp:Panel ID="btnShowHideContainer" runat="server" Visible="true" align="right">
                        <div id="btnShowHide" class="Collapse" align="right">
                        </div>
                    </asp:Panel>
                </td>--%>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSaveMsg" runat="server" class="Legend-Control-DocTypesIndex" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblSelectIndex" runat="server" text="No se seleccionó ningun documento" Font-Bold="true" CssClass="NoDocument" Font-Size="Small" Visible="false" style="margin:20px"></asp:Label>
        <div class="UserControlBody" >
            <asp:HiddenField runat="server" ID="hddocId" />
            <asp:HiddenField runat="server" ID="hdDTId" />
            <asp:Label runat="server" ID="lbTaskId" Visible="false" CssClass="Label" />
            <asp:Panel ID="Panel2" runat="server" Height="70%" ScrollBars="None" EnableViewState="false" Visible="true">                                       
                <asp:Table ID="tblIndices" runat="server" CellPadding="5" EnableViewState="false" CellSpacing="3" style="margin-left: 7px;" />                        
            </asp:Panel>
        </div>
    </fieldset>
</asp:Panel>
</div>
<asp:Panel ID="pnlSustitutionList" runat="server" Height="100%" Scr0ollBars="Auto" Visible="false">
    <fieldset title="Listado de Atributos" class="Fielset-Control-DocTypesIndex" enableviewstate="true" style="border: 0px !important;">
        <legend class="Legend" style="border-style: none; color:#777;margin-left: 10px; font-size: 18px; margin-bottom: 10px;"><b>Seleccionar Indice</b></legend>
            <table style="width: 100%;" class="Table-SustitutioList-Search">
                <tr>
                    <td>
                        <div style="overflow: auto; height: 90%; width: 36rem; text-align: left; padding: 5px;">
                            <asp:GridView ID="gvSustitutionList" runat="server"  
                                OnSelectedIndexChanged="gvSustitutionList_SelectedIndexChanged" OnSorting="OnSorting"                                
                                CellPadding="3"  BorderColor="#dde4ec" BackColor="#dde4ec" width="98%" AllowSorting="true" >
                                <Columns>
                                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" SelectText="Seleccionar" ItemStyle-Width="100px" ItemStyle-CssClass="margin-left= 10px"/>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding-top: 15px">
                        <asp:Button CssClass="btn btn-danger BtnCancelSombra" runat="server" ID="btnCancel" Visible="true" Text="Cancelar" OnClick="btnCancel_Onclick"  />
                    </td>
                </tr>
            </table>
    </fieldset>            
</asp:Panel>


<script type="text/javascript">

    $(function () {
        $(".datepickerIndices").datepicker();
    });

    function popupsust() {
        $('#pnlSustitutionList').dialog();
    }

    function Clean_Click() {
        try {
            parent.RefreshCurrentTab();
        }
        catch (e) {
            alert(e.description);
        }
    }

    function Reload_Click() {
        location.reload();
    }

    $(document).ready(function () {

        
        //Se modifica el validate, para que muestre los errores por debajo y sin imagen
       // GenerateAutoValidations()
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        setTimeout("parent.hideLoading();", 1000);
        //GenerateAutoValidations();
    }

    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
    function initializeRequestHandler(sender, args) {
        ShowLoadingAnimation();
       //GenerateAutoValidations();
    }

    //Se define esta funcion para validar codigo
    //generado a traves de eventos por AJAX
    function GenerateAutoValidations() {
        if ($('#aspnetForm').validate) $('#aspnetForm').validate({
            errorClass: "error2",
            errorElement: "div",
            wrapper: "div",
            errorPlacement: function (error, element) {
                element.parent().append(error);
                offset = element.offset();
                error.css('left', offset.left);
                error.css('top', offset.top + element.outerHeight());
            }
        });

        <%if (this.currentmode != WebModuleMode.Search) {%>

        //Se llama a la generacion de validaciones automaticas
        SetValidationsAction("<%=btnSaveChanges.ClientID %>");

        <%} %>
    }

</script>