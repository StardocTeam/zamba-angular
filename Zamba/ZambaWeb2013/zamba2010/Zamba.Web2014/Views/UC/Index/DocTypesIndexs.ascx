<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocTypesIndexs.ascx.cs" Inherits="Controls.Indexs.DocTypesIndexs" %>
<%@ Import Namespace="Zamba.Core" %>


<asp:HiddenField runat="server" ID="hdIndexId" />

<asp:Panel ID="pnlListadoIndices" runat="server" Height="100%" ScrollBars="none">
    <fieldset title="Listado de Atributos"  enableviewstate="true" style="margin-top:30px; padding-left:30px; padding-right:10px;"  >
        <table title="Listado de Atributos" style="width: 100%">
            <tr>
                <td style="width: 55%">
                    <div onclick="Clean_Click()">
                        <asp:LinkButton ID="btnCleanIndexs" runat="server" Height="20px" Font-Overline="false"
                            CssClass="labelDescHeader" ToolTip="Deshacer Cambios" Visible="false">
                            <asp:ImageButton ID="btnCleanImage" runat="server" Height="16px" />
                            <span style="cursor: pointer">Deshacer Cambios</span>
                        </asp:LinkButton>
                    </div>
                </td>
                <td style="width: 35%">
                    <asp:LinkButton ID="btnSaveChanges" runat="server" Height="20px" Font-Overline="false"
                        CssClass="labelDescHeader" ToolTip="Guardar cambios realizados" Visible="false"
                        OnClick="SaveIndexChanges_Click">
                        <asp:ImageButton ID="btnSaveImage" runat="server" Height="16px" />
                        <span style="cursor: pointer">Guardar</span>
                    </asp:LinkButton>
                </td>
                <td style="width: 10%" align="right">
                    <asp:Panel ID="btnShowHideContainer" runat="server" Visible="true" align="right">
                        <div id="btnShowHide" class="Collapse" align="right">
                        </div>
                    </asp:Panel>
                </td>
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
            <asp:Panel ID="Panel2" runat="server" Height="70%" ScrollBars="None" EnableViewState="false">                                       
                <asp:Table ID="tblIndices" runat="server" CellPadding="5" EnableViewState="true" CellSpacing="3" />                        
            </asp:Panel>
        </div>
    </fieldset>
</asp:Panel>
<asp:Panel ID="pnlSustitutionList" runat="server" Height="100%" Scr0ollBars="Auto" Visible="false">
    <fieldset title="Listado de Atributos" class="Fielset-Control-DocTypesIndex" enableviewstate="true">
        <legend class="Legend" style="border-style: none; color:#0045D6"><b>Seleccionar Indice</b></legend>
            <table style="width: 100%;" class="Table-SustitutioList-Search">
                <tr>
                    <td>
                        <div style="overflow: auto; height: 300px; width: 600px; text-align: left; padding: 5px;">
                            <asp:GridView ID="gvSustitutionList" runat="server"  
                                OnSelectedIndexChanged="gvSustitutionList_SelectedIndexChanged" OnSorting="OnSorting"                                
                                CellPadding="3" BackColor="White" BorderColor="White" width="98%" AllowSorting="true" >
                                <Columns>
                                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" SelectText="Seleccionar" ItemStyle-Width="100px"/>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-top: 15px">
                        <asp:Button runat="server" ID="btnCancel" Visible="true" Text="Cancelar" OnClick="btnCancel_Onclick" />
                    </td>
                </tr>
            </table>
    </fieldset>            
</asp:Panel>


<script type="text/javascript">
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

    $(document).ready(function () {
        
        //Se modifica el validate, para que muestre los errores por debajo y sin imagen
        GenerateAutoValidations()
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        setTimeout("parent.hideLoading();", 1000);
        GenerateAutoValidations();
    }

    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler)
    function initializeRequestHandler(sender, args) {
        ShowLoadingAnimation();
        //GenerateAutoValidations();
    }

    //Se define esta funcion para validar codigo
    //generado a traves de eventos por AJAX
    function GenerateAutoValidations() {
        $('#aspnetForm').validate({
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