<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomFilterControl.ascx.cs"
    Inherits="Views_UC_Grid_CustomFilterControl" %>
<script type="text/javascript">

    function ClearFilters() {
        $(".filtersfield").val("");
        return true;

    }

    //$(document).ready(function () {

    //    $("#ctl00_ContentPlaceHolder_TaskGrid_ucTaskGridFilter_ImageButton1").click(ShowLoadingAnimation);
    //    $("#ctl00_ContentPlaceHolder_TaskGrid_ucTaskGridFilter_ImageButton2").click(ShowLoadingAnimation);

    //});
</script>
<asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
    <div  id="filterTable">
    <div class="trClientData">
    <div class="col-md-2  text-center">
      Razon Social  
      <input class="form-control input-sm filtersfield" type="text" id="txtrazonsocial" runat="server" style="border-radius:7px;" />
        </div>
    <div class="col-md-2  text-center">
      Calle  
      <input class="form-control input-sm filtersfield" type="text" id="txtcalle" runat="server" style="border-radius:7px;"/>
        </div>
    <div class="col-md-2  text-center">
      Numero  
      <input class="form-control input-sm filtersfield" type="text" id="txtnro" runat="server" style="border-radius:7px;"/>
        </div>
    <div class="col-md-2  text-center">
      Codificacion  
      <input class="form-control input-sm filtersfield" type="text" id="txtcodificacion" runat="server" style="border-radius:7px;"/>
        </div>
    <div class="imgbuscador col-xs-offset-5 col-md-offset-0 col-md-2">
    <%--  <asp:ImageButton ID="btnFilter" runat="server" ImageUrl="~/Content/Images/Toolbars/search.png"
       OnClick="btnFilter_Click" />--%>

             <asp:LinkButton ID="btnFilter" runat="server" CssClass="fa fa-check fa-2x" OnClick="btnFilter_Click" style="color:#0397FB">
             </asp:LinkButton>
           
              <asp:LinkButton ID="btnClear" runat="server" CssClass="col-xs-offset-1 fa fa-times fa-2x" style="color:#A7A7A7"
              OnClientClick="return ClearFilters();" OnClick="btnClear_Click" />
     </div>
        </div>
        </div>
   <%-- <table id="filterTable">
        <tr class ="trClientData">
            <td>
                Razon Social  
            </td>
            <td>
                <input class="form-control input-sm filtersfield" type="text" id="txtrazonsocial" runat="server" />
            </td>
            <td>
                 Calle 
            </td>
            <td>
                <input class="form-control input-sm filtersfield" type="text" id="txtcalle" runat="server" />
            </td>
            <td>
                Nro 
            </td>
            <td>
                <input class="form-control input-sm filtersfield" type="text" id="txtnro" runat="server" />
            </td>
            <td>
                Codificacion 
            </td>
            <td>
                <input class="form-control input-sm filtersfield" type="text" id="txtcodificacion" runat="server" />
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
    </table>--%>
</asp:Panel>
