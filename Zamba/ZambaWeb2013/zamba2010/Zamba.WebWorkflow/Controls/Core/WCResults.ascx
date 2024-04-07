﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Controls/Core/WCResults.ascx.cs" Inherits="Controls_Core_WCResults" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtExtenders" Namespace="ExtExtenders" TagPrefix="ext" %>
<script  type="text/javascript">

function imprime(mon_document){
  print(mon_document);
}

</script>

<div>
    <asp:Table runat="server" ID="tablaFiltro" Width="500px" BackImageUrl="images/fondoPantalla.jpg" CellSpacing="5" CellPadding="3">
        <asp:TableRow Visible="false">
            <asp:TableCell HorizontalAlign="Left">
                <asp:Label ID="lblIndexToSearch" runat="server" Font-Size="Small" ForeColor="Navy" Text="Criterios de Búsqueda" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Visible="false">
            <asp:TableCell HorizontalAlign="Left">
                <asp:DropDownList ID="cmbFilter" Font-Size="X-Small" Width="172px" runat="server" OnSelectedIndexChanged="cmbFilter_SelectedIndexChanged" AutoPostBack="true" />
                <asp:DropDownList AutoPostBack="true" ID="cmbOperadores" runat="server" Font-Size="X-Small" Width="70px" OnSelectedIndexChanged="cmbOperadores_SelectedIndexChanged" />
                <asp:TextBox ID="txtFirstFilter" runat="server" Font-Size="X-Small" Width="70px" MaxLength="20" />
                <asp:Label ID="cmbOperadoresDate" runat="server" Font-Size="X-Small" Visible="false" Text=" AL " />
                <asp:TextBox ID="txtSecondFilter" runat="server" Font-Size="X-Small" Width="70px" Visible="false" MaxLength="20" />
                <asp:Button ID="btnShowList" Font-Size="x-Small" runat="server" Text="..." BackColor="#ffcc66" Width="25" OnClick="btnShowList_Click" />
                <asp:Button ID="btnFilter" runat="server" Text="Buscar" OnClick="btnFilter_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Label ID="OrdenarPor" runat="server" Text="Ordenar por: "></asp:Label>
    <asp:DropDownList ID="dplstColumName" runat="server" 
        ToolTip="Elija un valor de la lista por el cual ordenar">
    </asp:DropDownList>
    <asp:Button ID="btnAplicarOrden" runat="server" Font-Size="XX-Small" 
        onclick="btnAplicarOrden_Click" Text="Aplicar" 
        ToolTip="Aplica la seleccion para usarla como criterio de ordenamiento en la grilla" />
    <asp:Table runat="server" ID="tblAll" Width="620px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblTotal" runat="server" Font-Size="Small" ForeColor="Navy" /></asp:TableCell></asp:TableRow>
        <asp:TableRow Height="100%" VerticalAlign="Top">
            <asp:TableCell VerticalAlign="Top" Width="100%">
                <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
                    <ContentTemplate>
                    
    
                        <div style='overflow: auto; width: 635px; height: 460px;'>
                            <%--<ext:YuiGrid ID="ExtGrid" runat="server" AutoGenerateColumns="true" Width="100%" />--%>
                            <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small" OnSelectedIndexChanged="gvDocuments_SelectedIndexChanged" OnRowCreated="gvDocuments_RowCreated" OnRowDataBound="gvDocuments_OnRowDataBound" OnSorting="OnSorting" AlternatingRowStyle-Font-Underline="false" HeaderStyle-Font-Underline="false" EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false" Font-Underline="false" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false" RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false" EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvDocuments_PageIndexChanging" >
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px" Font-Size="X-Small" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
                                <PagerSettings Position="TopAndBottom" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" Font-Size="X-Small" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
                                <Columns>
                                
                                    <asp:CommandField ShowSelectButton="True" />
                                    
                                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("iva") %>'></asp:Label>
                        <%--<asp:Label ID="lblName" runat="server" Text='<%#Eval("") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>           
                
            
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblPageNumber" runat="server" ForeColor="Navy" Visible="false" />
                        <asp:Button ID="btnFirst" runat="server" Font-Size="X-Small" Text="<<" Width="25" OnClick="btnFirst_OnClick" Visible="false" />
                        <asp:Button ID="BtnBack" runat="server" Font-Size="X-Small" Text="<" Width="25" OnClick="BtnBack_OnClick" Visible="false" />
                        <asp:Button ID="BtnNext" runat="server" Font-Size="X-Small" Text=">" Width="25" OnClick="BtnNext_OnClick" Visible="false" />
                        <asp:Button ID="btnLast" runat="server" Font-Size="X-Small" Text=">>" Width="25" OnClick="btnLast_OnClick" Visible="false" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:TableCell></asp:TableRow>
    </asp:Table>
</div>