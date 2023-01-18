<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Views_UC_Grid_CustomFilterControlMarshArt" Codebehind="CustomFilterControlMarshArt.ascx.cs" %>
<script type="text/javascript">

    function ClearFilters() {
        $(".filtersfield").val("");
        return true;

    }

    $(document).ready(function () {

        $("#ContentPlaceHolder_TaskGrid_ucTaskGridFilter_ImageButton1").click(ShowLoadingAnimation);
        $("#ContentPlaceHolder_TaskGrid_ucTaskGridFilter_ImageButton2").click(ShowLoadingAnimation);

    });
</script>
<asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
    <table id="filterTable">
        <tr>
            <td>
                Apellido:
            </td>
            <td>
                <input class="filtersfield" type="text" id="txtapellido" runat="server" />
            </td>
            <td>
                CUIL
            </td>
            <td>
                <input class="filtersfield" type="text" id="txtcuil" runat="server" />
            </td>
            <td>
                Poliza
            </td>
            <td>
                <input class="filtersfield" type="text" id="txtpoliza" runat="server" style="width:60px" />
            </td>
            <td>
                Cliente
            </td>
            <td>
                <input class="filtersfield" type="text" id="txtcliente" runat="server" />
            </td>
            <td>
                <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Content/Images/Toolbars/search.png"
                    OnClick="btnFilter_Click" />
            </td>
            <td>
                <asp:ImageButton ID="btnClear" runat="server" ImageUrl="~/Content/Images/Toolbars/close_16.png"
                    Height="16" OnClientClick="return ClearFilters();" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
