<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDocTypeIndexsForInsert.ascx.cs" Inherits="Views_UC_Index_ucDocTypeIndexsForInsert"  EnableViewState="false" %>

<asp:Panel ID="pnlListadoIndices" runat="server" width="550px">
    <fieldset title="" class="Fielset-controles-UC" enableviewstate="false">
        <h5>
             Paso 2. Ingrese los siguientes atributos
            <%--<span style=""><asp:LinkButton runat="server" ID="lnkClearIndexs" OnClick="btnClearIndexs_Click">Limpiar</asp:LinkButton></span>--%>
        </h5>
        <div class="insertAttributesPanel" id="divAttributes">
            <asp:HiddenField runat="server" ID="hddocId" />
            <asp:HiddenField runat="server" ID="hdDTId" />
            <asp:Label runat="server" ID="lbTaskId" Visible="false"  />
            <asp:Panel ID="Panel2" runat="server" EnableViewState="false">                                       
                <asp:Table ID="tblIndices" runat="server" CellPadding="0" EnableViewState="false" />                        
            </asp:Panel>
        </div>
    </fieldset>
</asp:Panel>

<script type="text/javascript">
    $(document).ready(function () {
        
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
        SetValidationsAction("<%=this.SaveButtonName %>");
    });
</script>