<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Indexs.ascx.cs" Inherits="Indexs" %>
<%@ Register TagPrefix="MyUcs" TagName="Error" Src="~/RequestAction/Controls/ErrorHandler.ascx" %>
<fieldset title="Listado de Indices" class="FieldSet">
    <legend class="Legend">Listado de Indices</legend>
    <MyUcs:Error ID="UcError" runat="server" />
    <div class="UserControlBody" style="font-size: xx-small;" id="pnlIndexes" runat="server">
        <asp:HiddenField runat="server" ID="hdTaskId" />
        <asp:Label runat="server" ID="lbTaskId" Visible="false" CssClass="Label" />
        <asp:Panel ID="Panel1" runat="server" ScrollBars="auto" Height="200 px">
            <asp:Table ID="tblIndices" runat="server" CssClass="Table" style="font-size: xx-small;"/>
        </asp:Panel>
    </div>
</fieldset>
