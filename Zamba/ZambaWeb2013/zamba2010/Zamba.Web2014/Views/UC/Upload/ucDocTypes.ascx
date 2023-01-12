<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDocTypes.ascx.cs" Inherits="ucDocTypes" %>

<asp:Panel ID="pnlListadoIndices" runat="server" width="550px" ScrollBars="Auto">
    <fieldset title="" class="Fielset-controles-UC" enableviewstate="true" style="padding:5px;">
        <h5>Paso 1. Seleccione el entidad</h5>
        <div class="UserControlBody" >
            <asp:DropDownList runat="server" ID="DocTypes" onselectedindexchanged="DocTypes_SelectedIndexChanged" AutoPostBack="true" Width="520px"></asp:DropDownList>
        </div>
    </fieldset>
</asp:Panel>

