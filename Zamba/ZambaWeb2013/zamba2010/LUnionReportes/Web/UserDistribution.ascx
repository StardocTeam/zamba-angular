<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDistribution.ascx.cs"
    Inherits="Web.UserDistribution" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="design">
    <span class="title2">Distribucion de Tareas</span>
    <div id="btnShowHideCombos" class="CombosHidden" style="float:right; background-image: url(./Content/Images/config.png); width:16px; height:16px;">
    </div>
</div>
<%--<asp:UpdatePanel runat="server" ID="udpud">
    <ContentTemplate>--%>
    <div id="combosTable" style="padding:3px;display:none; position:absolute;left:0px;top:17px;z-index:9999; background-color:#FFFFFE;">
    <table style="margin:2px; border: 1px solid black;">
            <tr>
                <td>
                        Proceso:
                </td>
                <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="WorkflowsSQL"
                            DataTextField="Name" DataValueField="work_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="WorkflowsSQL" runat="server" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
                            SelectCommand="Select 0 as work_id,'Seleccione Proceso' as Name UNION SELECT [work_id], [Name] FROM [WFWorkflow] ORDER BY [Name]">
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                        Etapa:
                </td>
                <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="WFStepsSQL"
                            DataTextField="Name" DataValueField="step_id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="WFStepsSQL" runat="server" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
                            SelectCommand="Select 0 as step_id,'Seleccione Etapa' as Name UNION SELECT [step_Id], [Name] FROM [WFStep] where work_id = @work_id  ORDER BY [Name]">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" DefaultValue="0" Name="work_id" PropertyName="SelectedValue"
                                    Type="Decimal" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </div>
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Vista" MultiPageID="RadMultiPage1"
            SelectedIndex="0" Align="Justify" ReorderTabsOnSelect="true" Width="450px" OnClientTabSelected="ResizeIF">
            <Tabs>
                <telerik:RadTab Text="Gr&aacute;fico">
                </telerik:RadTab>
                <telerik:RadTab Text="Grilla">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="pageView"
            Width="345px">
            <telerik:RadPageView ID="RadPageView1" runat="server">
                <div id="PrintChart" runat="server">
                <div onclick="Imprimir_Click1('<%=Chart1.ClientID  %>')" style="height: 15pt; cursor: pointer;
                     position: absolute; padding-left: 10px; padding-top: 10px">
                    <img id="Img1" src="Content/Images/print.png" alt="Imprimir documento" style="height: 16px" />
                    <asp:Label ID="Label1" Text="" runat="server" ToolTip="Imprimir documento"
                        Height="90%"></asp:Label>
                </div>
                </div>
                <asp:Chart ID="Chart1" runat="server" DataSourceID="ZSQL" BackColor="White" BorderlineDashStyle="Solid"
                    BackGradientStyle="LeftRight" OnDataBound="Chart1_DataBound" BorderColor="#B54001" BorderlineWidth="2" Height="300"
                    Width="450">
                    <Titles>
	                    <asp:Title Name="NoDataFound" Font="Trebuchet MS, 14" Text="No se encontraron resultados" ForeColor="#1a3b69" TextOrientation="Horizontal" ShadowColor="Gray" ShadowOffset="3" TextStyle="Shadow" Visible="false"></asp:Title>                        
                    </Titles> 
                    <Series>
                        <asp:Series Name="Series1" ChartType="Pie" XValueMember="Usuario" Font="Trebuchet MS, 8"
                            YValueMembers="Cantidad" IsValueShownAsLabel="false" Palette="BrightPastel">
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
                <asp:SqlDataSource ID="ZSQL" runat="server" ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>"
                    SelectCommand="SELECT COUNT(1) AS Cantidad, USRTABLE.NAME AS Usuario, DOC_TYPE.DOC_TYPE_NAME AS Entidad, WFStepStates.Name AS Estado, WFWorkflow.Name AS Proceso, WFStep.Name AS Etapa FROM USRTABLE RIGHT OUTER JOIN DOC_TYPE INNER JOIN WFStepStates INNER JOIN WFStep ON WFStepStates.Step_Id = WFStep.step_Id INNER JOIN WFWorkflow ON WFStep.work_id = WFWorkflow.work_id INNER JOIN WFDocument ON WFStepStates.Doc_State_ID = WFDocument.Do_State_ID ON DOC_TYPE.DOC_TYPE_ID = WFDocument.DOC_TYPE_ID ON USRTABLE.ID = WFDocument.User_Asigned WHERE ([WFWorkflow].[work_id] = @work_id) and ([WFStep].[step_id] = @step_id) GROUP BY USRTABLE.NAME, DOC_TYPE.DOC_TYPE_NAME, WFStepStates.Name, WFWorkflow.Name, WFStep.Name ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" DefaultValue="0" Name="work_id" PropertyName="SelectedValue"
                            Type="Decimal" />
                        <asp:ControlParameter ControlID="DropDownList2" DefaultValue="0" Name="step_id" PropertyName="SelectedValue"
                            Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server">
                <asp:CheckBox ID="CheckBox2" Text="Ignorar paginado en exportacion" runat="server">
                    </asp:CheckBox>
                <telerik:RadGrid runat="server" ID="RadGrid1" DataSourceID="ZSQL" AllowSorting="True"
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
            </telerik:RadPageView>

        </telerik:RadMultiPage>
        <script type="text/javascript">
            $("#btnShowHideCombos").click(function () {
                SetCombosPnlVisibility(this);
            });

            function SetCombosPnlVisibility(obj) {
                var table = document.getElementById("combosTable");
                if ($(obj).hasClass("CombosHidden")) {                        
                    $(table).show(100);
                    $(obj).addClass("CombosVisible");
                    $(obj).removeClass("CombosHidden");                                              
                }
                else {                        
                    $(table).hide(100);
                    $(obj).addClass("CombosHidden");
                    $(obj).removeClass("CombosVisible"); 
                }
            }
        </script>
  <%--  </ContentTemplate>
</asp:UpdatePanel>--%>
