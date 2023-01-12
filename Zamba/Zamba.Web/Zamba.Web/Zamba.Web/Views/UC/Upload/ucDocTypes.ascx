<%@ Control Language="C#" AutoEventWireup="true" Inherits="ucDocTypes" Codebehind="ucDocTypes.ascx.cs" %>



<asp:Panel ID="pnlListadoIndices" runat="server" width="550px" ScrollBars="Auto">
    <fieldset title="" class="Fielset-controles-UC" enableviewstate="true" style="padding:5px;">
        <h4  class="noprint"><strong>Selecciona la entidad</strong> </h4>
        <div class="UserControlBody" >
            <asp:DropDownList runat="server" ID="DocTypes" onselectedindexchanged="DocTypes_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" Width="485px"></asp:DropDownList>
        </div>
    </fieldset>
</asp:Panel>

