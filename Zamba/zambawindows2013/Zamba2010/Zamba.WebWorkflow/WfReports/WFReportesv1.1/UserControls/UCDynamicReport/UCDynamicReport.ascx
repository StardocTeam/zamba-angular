<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDynamicReport.ascx.cs"
    Inherits="UCDynamicReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<link href="../../../default.css" rel="stylesheet" type="text/css" />

<body class="ms-informationbar">

     <table style="margin: 0px 0px 0px 0px">
        <tr>
            <td width="100%" class="ms-topnav" style="margin: 0px 0px 0px 0px" colspan="2">
                <asp:Label ID="lblTitle" Text="Reporte dinamico" runat="server"  />
            </td>
        </tr>
     </table>
     
     <table style="margin: 0px 0px 0px 0px" class="ms-informationbar">
        <tr>
            <td style="margin: 0px 0px 0px 0px">
            
                <asp:Label ID="lblNothingResults" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="Red"
                           Visible="False" Text="No se encontraron Resultados">
                </asp:Label>
                
                <div style="OVERFLOW: auto; HEIGHT:120px">
                    <asp:GridView runat="server" ID="gvDynamicReport" CellPadding="4" 
                                  ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" >
                                  <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                  <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                  <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                  <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                  <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                  <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                 </div>
                
            </td>          
        </tr>
    </table>
    
</body>


