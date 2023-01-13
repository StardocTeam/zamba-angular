<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocumentacionFaltante.ascx.cs"
    Inherits="Web.DocumentacionFaltante" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="design">
    <span class="title2">Documentacion faltante</span>
</div>
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Vista" MultiPageID="RadMultiPage1"
    SelectedIndex="0" Align="Justify" ReorderTabsOnSelect="true" Width="450px" OnClientTabSelected="ResizeIF">
    <Tabs>
        <telerik:RadTab Text="Gr&aacute;fico 1">
        </telerik:RadTab>
        <telerik:RadTab Text="Gr&aacute;fico 2">
        </telerik:RadTab>
        <telerik:RadTab Text="Grilla">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="pageView"
    Width="345px">
    <telerik:RadPageView ID="RadPageView1" runat="server">
        <div id="PrintChart1" runat="server">
            <div onclick="Imprimir_Click1('<%=Chart1.ClientID  %>')" style="height: 15pt; cursor: pointer; position:absolute; padding-left:10px;padding-top:10px">
                <img id="lnkImprimir" src="Content/Images/print.png" alt="Imprimir documento" style="height: 16px" />
                <asp:Label ID="lblImprimir" Text="" runat="server" ToolTip="Imprimir documento"
                    Height="90%"></asp:Label>
            </div>
        </div>
        <asp:Chart ID="Chart1" runat="server" DataSourceID="ZSQL" BackColor="White" BorderlineDashStyle="Solid"
            BackGradientStyle="LeftRight" BorderColor="#B54001" BorderlineWidth="2" Height="300"
            Width="450" OnDataBound="Chart1_DataBound" OnClientTabSelected="ResizeIF">
            <Titles>
                <asp:Title Name="NoDataFound1" Font="Trebuchet MS, 14" Text="No se encontraron resultados" ForeColor="#1a3b69" TextOrientation="Horizontal" ShadowColor="Gray" ShadowOffset="3" TextStyle="Shadow" Visible="false"></asp:Title>                        
            </Titles> 
            <Series>
                <asp:Series Name="Series1" ChartType="Pie" XValueMember="Productor" Font="Trebuchet MS, 7" YValueMembers="Cantidad"
                     IsValueShownAsLabel="false" Palette="BrightPastel">
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
        <asp:SqlDataSource ID="ZSQL" runat="server" OnSelected="zsql_selected" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
            SelectCommand="SELECT COUNT(1) AS Cantidad, SLST.descripcion AS PRODUCTOR FROM wfdocument INNER JOIN doc_i26027 R ON wfdocument.doc_id = R.doc_id INNER JOIN doc_I26029 P ON R.i26475 = P.i26475 LEFT JOIN slst_s26233 SLST ON SLST.codigo = P.i26233 LEFT JOIN slst_s26314 SLST_A ON SLST_A.codigo = R.i26314 LEFT JOIN doc_i26033 D ON D.i26260 = R.i26260 AND D.I26322 IN ('Denuncia del Asegurado', 'Denuncia Administrativa Asegurado') AND D.i26488 ='Original' AND IsNull(D.I26512, 0) = 0 LEFT JOIN doc_t26033 ON D.doc_id = Doc_t26033.doc_id AND doc_t26033.doc_file <> '' WHERE D.I26322 IS NULL AND step_id IN (26002,26003,26004,26005) AND R.i26178 = 0 AND SLST.codigo IN (SELECT DISTINCT top 30 codigoproductor FROM (SELECT top 100 percent SLST.codigo AS codigoproductor, R.i26201 FECHA FROM wfdocument INNER JOIN doc_i26027 R ON wfdocument.doc_id = R.doc_id INNER JOIN doc_I26029 P ON R.i26475 = P.i26475 LEFT JOIN slst_s26233 SLST ON SLST.codigo = P.i26233 LEFT JOIN slst_s26314 SLST_A ON SLST_A.codigo = R.i26314 LEFT JOIN doc_i26033 D ON D.i26260 = R.i26260 AND D.I26322 IN ('Denuncia del Asegurado', 'Denuncia Administrativa Asegurado') AND D.i26488 ='Original' AND IsNull(D.I26512, 0) = 0 LEFT JOIN doc_t26033 ON D.doc_id = Doc_t26033.doc_id AND doc_t26033.doc_file <> '' WHERE D.I26322 IS NULL AND step_id IN (26002,26003,26004,26005) AND R.i26178 = 0 ORDER BY R.i26201 ASC ) AS x ) GROUP BY SLST.descripcion">
        </asp:SqlDataSource>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server">
        <div id="PrintChart2" runat="server">
            <div onclick="Imprimir_Click1('<%=Chart2.ClientID  %>')" style="height: 15pt; cursor: pointer; position:absolute; padding-left:10px;padding-top:10px">
                <img id="Img1" src="Content/Images/print.png" alt="Imprimir documento" style="height: 16px" />
                <asp:Label ID="Label1" Text="" runat="server" ToolTip="Imprimir documento"
                    Height="90%"></asp:Label>
            </div>
        </div>
        <asp:Chart ID="Chart2" runat="server" DataSourceID="ZSQL2" BackColor="#F3DFC1" BorderlineDashStyle="Solid"
            BackGradientStyle="LeftRight" BorderColor="#B54001" BorderlineWidth="2" Height="300"
            Width="450" OnDataBound="Chart2_DataBound">
            <Titles>
                <asp:Title Name="NoDataFound2" Font="Trebuchet MS, 14" Text="No se encontraron resultados" ForeColor="#1a3b69" TextOrientation="Horizontal" ShadowColor="Gray" ShadowOffset="3" TextStyle="Shadow" Visible="false"></asp:Title>                        
            </Titles> 
            <Series>
                <asp:Series Name="Series1" ChartType="Pie" XValueMember="Productor" YValueMembers="Cantidad"
                    IsValueShownAsLabel="false" Palette="BrightPastel" Font="Trebuchet MS, 8">
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
            SelectCommand="SELECT COUNT(1) as Cantidad, SLST.descripcion AS PRODUCTOR from wfdocument inner join  doc_i26037 I on wfdocument.doc_id = I.doc_id  INNER JOIN doc_I26027 R ON R.i26260 = I.i26260 INNER JOIN doc_I26029 P ON R.i26475 = P.i26475 inner join doc_t26037 on I.doc_id = Doc_t26037.doc_id  LEFT JOIN slst_s26233 SLST ON SLST.codigo = P.i26233 LEFT JOIN slst_s26314 SLST_A ON SLST_A.codigo = I.i26314    where (doc_t26037.doc_file = '' or I.i26488 <> 'Original')  and IsNull(I.I26512, 0) = 0  and  step_id in (26036,26037,33071,33072)  and R.i26178 = 0 GROUP BY SLST.descripcion">
        </asp:SqlDataSource>
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView3" runat="server">
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
            SelectCommand="SELECT SLST.descripcion AS PRODUCTOR, ISNULL(R.i26247,0) AS [NRO DE SINIESTRO], REPLACE(CONVERT(VARCHAR,R.i26201,3),'/','/') AS 'FECHA SINIESTRO', SLST_A.descripcion AS [ASEGURADO], R.i26174 AS [NRO POLIZA], R.i26210 AS [TIPO PATENTE], R.i26309 AS PATENTE, R.i26208 AS MARCA, R.i26245 AS MODELO, R.i26308 AS SUBMODELO from wfdocument inner join  doc_i26027 R on wfdocument.doc_id = R.doc_id INNER JOIN doc_I26029 P ON R.i26475 = P.i26475  LEFT JOIN slst_s26233 SLST ON SLST.codigo = P.i26233  LEFT JOIN slst_s26314 SLST_A ON SLST_A.codigo = R.i26314  left join doc_i26033 D on D.i26260 = R.i26260 and D.I26322 in ('Denuncia del Asegurado', 'Denuncia Administrativa Asegurado')   and  D.i26488='Original'  and IsNull(D.I26512, 0) = 0   left join doc_t26033   on D.doc_id = Doc_t26033.doc_id and doc_t26033.doc_file <> '' where D.I26322 is null and step_id in (26002,26003,26004,26005) and R.i26178 = 0 order by R.i26201 ASC">
        </asp:SqlDataSource>
    </telerik:RadPageView>
</telerik:RadMultiPage>
