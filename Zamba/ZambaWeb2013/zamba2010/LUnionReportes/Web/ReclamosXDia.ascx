<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReclamosXDia.ascx.cs"
    Inherits="Web.ReclamosXDia" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="design">
    <span class="title2">Denuncias por mes</span>
</div>
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Vista" MultiPageID="RadMultiPage1"
    SelectedIndex="0" Align="Justify" ReorderTabsOnSelect="true" Width="450px" OnClientTabSelected="ResizeIF">
    <Tabs>
        <telerik:RadTab Text="Gr&aacute;fico" >
        </telerik:RadTab>
        <telerik:RadTab Text="Grilla" >
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="pageView"
    Width="345px">
    <telerik:RadPageView ID="RadPageView1" runat="server">
        <div id="PrintChart" runat="server">
            <div onclick="Imprimir_Click1('<%=Chart1.ClientID  %>')" style="height: 15pt; cursor: pointer; position:absolute; padding-left:10px;padding-top:10px">
                <img id="lnkImprimir" src="Content/Images/print.png" alt="Imprimir documento"
                style="height: 16px" />
                <asp:Label ID="lblImprimir" Text="" runat="server" ToolTip="Imprimir documento"
                Height="90%"></asp:Label>
            </div>
        </div>
        <asp:Chart ID="Chart1" runat="server" DataSourceID="ZSQL2" BackColor="White" BorderlineDashStyle="Solid"
                BackGradientStyle="LeftRight" BorderColor="#B54001" BorderlineWidth="2" Height="300"
                Width="450" OnDataBound="Chart1_DataBound">
                <Titles>
                     <asp:Title Name="NoDataFound" Font="Trebuchet MS, 14" Text="No se encontraron resultados" ForeColor="#1a3b69" TextOrientation="Horizontal" ShadowColor="Gray" ShadowOffset="3" TextStyle="Shadow" Visible="false"></asp:Title>                        
                </Titles> 
                <Series>
                <asp:Series Name="Series1" ChartType="Pie" XValueMember="Fecha Denuncia" YValueMembers="Cantidad Denuncias">
                </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="#404040" BackSecondaryColor="White"
                        BackColor="Transparent" ShadowColor="OldLace" BorderWidth="1">
                        <Area3DStyle Enable3D="true" Rotation="0" Perspective="30" Inclination="50" IsRightAngleAxes="true"
                            WallWidth="0" IsClustered="false" PointDepth="50" PointGapDepth="200" LightStyle="Realistic" />
                        <Position X="1" Y="5" Height="90" Width="90" />
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        <asp:SqlDataSource ID="ZSQL2" runat="server" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
            SelectCommand="select top 12 sum([Cantidad Denuncias]) as [Cantidad Denuncias], [Fecha Denuncia] from (SELECT top 99.99 PERCENT COUNT(1) AS [Cantidad Denuncias], CONVERT(char(7),  R.i26427, 111) AS [Fecha Denuncia] from wfdocument inner join  doc_i26037 I on wfdocument.doc_id = I.doc_id INNER JOIN doc_I26027 R ON R.i26260 = I.i26260 GROUP BY R.i26427) AS Q group by [Fecha Denuncia]  order by [Fecha Denuncia] desc">
        </asp:SqlDataSource>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server" Width="450">
    <asp:CheckBox ID="CheckBox2" Text="Ignorar paginado en exportacion" runat="server">
            </asp:CheckBox>
        <telerik:RadGrid runat="server" ID="RadGrid1" DataSourceID="ZSQL3" AllowSorting="True"
            AllowFilteringByColumn="True" Width="98%" ClientSettings-Resizing-AllowResizeToFit="false"
            GroupingEnabled="true" BorderStyle="None" CellSpacing="0" GridLines="None" AllowPaging="true"
            ShowGroupPanel="True" Skin="Windows7" PageSize="20" OnItemCommand="RadGrid1_ItemCommand">
            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
            <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
            </ClientSettings>
            <GroupingSettings ShowUnGroupButton="true" />
            <ExportSettings IgnorePaging="true" OpenInNewWindow="true" HideStructureColumns="true">
                <Pdf PageHeight="210mm" PageWidth="297mm" DefaultFontFamily="Arial Unicode MS"
                    PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm" />
            </ExportSettings>
            <MasterTableView Width="100%" CommandItemDisplay="Top">
                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                    ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToCsvButton="false" ShowExportToPdfButton="true"
                    ExportToExcelText="Exportar a Excel" ExportToPdfText="Exportar a PDF" ExportToWordText="Exportar a Word"/>
            </MasterTableView>
        </telerik:RadGrid>
        <asp:SqlDataSource ID="ZSQL3" runat="server" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
            SelectCommand="select top 1000 DI.i26247 as [Nro de Siniestro], DI.i26260 as [ID Reclamo], HG.descripcion as [Tipo Beneficiario], RS.descripcion as [Responsable Siniestro], REPLACE(CONVERT(VARCHAR,DI.i26421,3),'/','/') as [Fecha Denuncia], REPLACE(CONVERT(VARCHAR,DI.i26201,3),'/','/') as [Fecha Siniestro], SLST.descripcion AS PRODUCTOR, HG.descripcion as [Hecho Generador] from doc_t26027 INNER JOIN wfdocument on wfdocument.doc_id=doc_t26027.doc_id INNER JOIN doc_i26027 DI on doc_t26027.doc_id=DI.doc_id INNER JOIN doc_I26029 P ON DI.i26475 = P.i26475 INNER JOIN slst_s26478 RS ON DI.i26478 = RS.codigo INNER JOIN slst_s26312 HG ON DI.i26312 = HG.codigo INNER JOIN slst_s26324 TB ON DI.i26312 = TB.codigo LEFT JOIN slst_s26233 SLST ON SLST.codigo = P.i26233 INNER JOIN wfstep ON wfstep.step_id=wfdocument.step_id order by DI.i26421 desc"></asp:SqlDataSource>
    </telerik:RadPageView>
</telerik:RadMultiPage>
