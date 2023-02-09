<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Index_ucDocTypeIndexsForInsert"  ValidateRequestMode="Disabled" EnableViewState="false" Codebehind="ucDocTypeIndexsForInsert.ascx.cs" %>



<asp:Panel ID="pnlListadoIndices" runat="server" width="550px" ng-app="app">
    <fieldset id="insertFieldset" title="" class="Fielset-controles-UC" enableviewstate="false" >
        <h4  class="noprint"><strong>
             Ingresa los datos
            </strong>
            <%--<span style=""><asp:LinkButton runat="server" ID="lnkClearIndexs" OnClick="btnClearIndexs_Click">Limpiar</asp:LinkButton></span>--%>
        </h4>
        <div class="insertAttributesPanel" id="divAttributes">
            
            <asp:HiddenField runat="server" ID="hddocId" />
            <asp:HiddenField runat="server" ID="hdDTId" />
            <asp:Label runat="server" ID="lbTaskId" Visible="false"  />
          
            <asp:Panel ID="Panel2" runat="server" EnableViewState="false">                                       
                <asp:Table Class="table" ID="tblIndices" runat="server" cellspacing="0" EnableViewState="false"/>                        
            </asp:Panel>

        </div>
    </fieldset>
</asp:Panel>

<script type="text/javascript">
    $(document).ready(function () {
        
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
        SetValidationsAction("<%=this.SaveButtonName %>");
    });


    function GetIndexs() {


    };
</script>