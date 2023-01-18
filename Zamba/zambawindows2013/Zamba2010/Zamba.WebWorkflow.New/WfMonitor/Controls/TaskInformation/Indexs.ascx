<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Indexs.ascx.cs" Inherits="Indexs" %>
<fieldset title="Listado de Indices">
    <legend style="font: caption; font-size: small; color: navy">Listado de Indices</legend>
    <div class="UserControlBody">
        <asp:HiddenField runat="server" ID="hdTaskId" />
        <asp:Label runat="server" ID="lbTaskId" Visible="false" ForeColor="Navy" />
        <asp:Table ID="tblIndices" runat="server" ForeColor="Navy" Font-Size="Small" />
    </div>
    <div class="UserControlButtons">
     <%--   <table>
            <tr>
                <td>
                    <asp:Button ID="btUndoChanges" runat="server" Text="Deshacer cambios" OnClick="btUndoChanges_Click"
                        ToolTip="Deshacer los cambios realizados a los Indices" Visible="false" BackColor="#EFF3FB"
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                        Font-Size="Small" ForeColor="#284775" /></td>
                <td>
                    <asp:Button ID="btSaveChanges" runat="server" Text="Guardar cambios" OnClick="btSaveChanges_Click"
                        ToolTip="Guardar los cambios realizados a los Indices" Visible="false" BackColor="#EFF3FB"
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                        Font-Size="Small" ForeColor="#284775" /></td>
                <td>
                    <asp:Button ID="btClearFields" runat="server" Text="Borrar todo" OnClick="btClearFields_Click"
                        ToolTip="Limpiar los Indices" Visible="false" BackColor="#EFF3FB" BorderColor="#CCCCCC"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="Small"
                        ForeColor="#284775" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btActualizar" runat="server" Text="Actualizar" OnClick="btActualizar_Click"
                        BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        ToolTip="Actualizar información" Font-Names="Verdana" Font-Size="Small" ForeColor="#284775" /></td>
            </tr>
        </table>--%>
    </div>
</fieldset>
