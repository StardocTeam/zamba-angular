<%@ Page Title="Zamba - Detalle de Licencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UCMHistory.aspx.cs" Inherits="Zamba.AgentServer.Pages.UCMHistory" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <style type="text/css">
          .dragRangeSlider .rslSelectedregion {
               cursor: pointer;
          }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
  
   <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
		<script type="text/javascript">
			   <!--

			$(document).ready(function () {

//                        CheckParams();

			});

			function OnResponseEnd(sender, args) {
			CheckParams();
			}

			function CheckParams() {
				loading();
		   
				if ($("#MainContent_HiddenParam").val() != null && $("#MainContent_HiddenParam").val() != 'Test') {
					var param = $("#MainContent_HiddenParam").val();

					document.getElementById("OutPut").innerHTML = param;

					GetUsers(param);
				}

				closeloading();
			}




				/* event for close the popup */
				$("#closediv").hover(
					function () {
						$('span.ecs_tooltip').show();
					},
					function () {
						$('span.ecs_tooltip').hide();
					}
				);

					$("#closediv").click(function () {
					disablePopup();  // function close pop up
				});

				$(this).keyup(function (event) {
					if (event.which == 27) { // 27 is 'Ecs' in the keyboard
						disablePopup();  // function close pop up
					}
				});


				/************** start: functions. **************/
				function loading() {
					$("div.loader").show();
				}
				function closeloading() {
					$("div.loader").fadeOut('normal');
				}


				function disablePopup() {
						$("#GridUsers").fadeOut("normal");
				}



			function GetUsers(param) {

if (param != null && param != '')
{
				var detailsurl = "http://localhost:56524/ws/ucmservice.svc/details/" + param;

				$("#UserList").empty();

				$.ajax({
					cache: false,
					type: "GET",
					async: false,
					dataType: "json",
					url: detailsurl,
					success: function (details) {

						$.each(details.DetailsResult, populateDropdown);

						$('div#GridUsers').show().css('top', 50).css('left', 950).appendTo('body');
						

					},
					error: function (xhr) {
						alert(xhr.responseText);
					}
				});

			}
		  
			
			}

			function Vergrafico() {

				if ($('#Grid').css('display') == 'none')
			{
				$('#Grid').show().fadeIn("slow");
				$('#Graph').hide().fadeOut("slow");
			   }
			else
				{
					$('#Grid').hide().fadeOut("slow");
					$('#Graph').show().fadeIn("slow");
			   }
			}
		  
			function log(message) {
				var log = $telerik.findElement(document, "log");
				log.innerHTML += message + "<br/>";
			}
			function GridCreated(sender, eventArgs) {
//                var grid = $find("<%=RadGrid1.ClientID %>");
//                //you can also use the sender argument keyword to reference the client grid object
//                log("ClientID of server-side grid object is: " + grid.get_element().id);
			}

			function RowResized(sender, eventArgs) {
//                var text = "";
//                text += "Row was resized";
//                text += ", Index: " + eventArgs.get_itemIndexHierarchical();
//                text += ", Height: " + $get(eventArgs.get_id()).offsetHeight;
//                document.getElementById("OutPut").innerHTML = text;
			}

			function RowClick(sender, eventArgs) {
//                var text = "";
//                text += "Row was clicked";
//                text += ", Index: " + eventArgs.get_itemIndexHierarchical();
//                document.getElementById("OutPut").innerHTML = text;
			}

			function RowDblClick(sender, eventArgs) {
//                var text = "";
//                text += "Row was double clicked";
//                text += ", Index: " + eventArgs.get_itemIndexHierarchical();
//                document.getElementById("OutPut").innerHTML = text;
		   
		   
			}

			function RowMouseOver(sender, eventArgs) {
//                var text = "";
//                text += "Mouse is over row";
//                text += ", Index: " + eventArgs.get_itemIndexHierarchical();
//                document.getElementById("OutPut").innerHTML = text;
			}

			function RowMouseOut(sender, eventArgs) {
//                var text = "";
//                text += "Mouse is out row";
//                text += ", Index: " + eventArgs.get_itemIndexHierarchical();
//                document.getElementById("OutPut").innerHTML = text;
			}

			function RowSelected(sender, eventArgs) {
				var text = "";
//                text += "Fecha Seleccionada: " + eventArgs.get_itemIndexHierarchical() + " was selected";

				var Cliente = eventArgs.getDataKeyValue("Cliente");

			   var Fecha = eventArgs.getDataKeyValue("Anio") +
						"/" + eventArgs.getDataKeyValue("Mes") +
						"/" + eventArgs.getDataKeyValue("Dia") +
						"/" + eventArgs.getDataKeyValue("Hora");

			   text += "<b>Fecha: </b>" + Fecha;

				document.getElementById("OutPut").innerHTML = text;

GetUsers( Cliente + "/" + Fecha);            
			   

				}

				// Populate drop-down box with JSON data (menu)
				populateDropdown = function () {
					$("#UserList").append('<li>' + this.Count + ': ' + this.Usuario + '-' + this.Tipo + '</li>');
				};



			function RowDeselected(sender, eventArgs) {
//                var text = "";
//                text += "Row with index: " + eventArgs.get_itemIndexHierarchical() + " was deselected";
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnMouseOut(sender, eventArgs) {
//                var text = "";
//                text += "Mouse is out column";
//                text += ", Index: " + eventArgs.get_gridColumn().get_element().cellIndex;
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnMouseOver(sender, eventArgs) {
//                var text = "";
//                text += "Mouse is over column";
//                text += ", Index: " + eventArgs.get_gridColumn().get_element().cellIndex;
//                document.getElementById("OutPut").innerHTML = text;
			}


			function ColumnClick(sender, eventArgs) {
//                var text = "";
//                text += "Column was clicked";
//                text += ", Index: " + eventArgs.get_gridColumn().get_element().cellIndex;
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnDblClick(sender, eventArgs) {
//                var text = "";
//                text += "Column was double clicked";
//                text += ", Index: " + eventArgs.get_gridColumn().get_element().cellIndex;
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnResized(sender, eventArgs) {
//                var text = "";
//                text += "Column was resized";
//                text += ", Index: " + eventArgs.get_gridColumn().get_element().cellIndex;
//                text += ", Width: " + eventArgs.get_gridColumn().get_element().offsetWidth;
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnSwapping(sender, eventArgs) {
//                var text = "Swapping columns with unique names: " + eventArgs.get_gridSourceColumn().get_uniqueName() + " and " + eventArgs.get_gridTargetColumn().get_uniqueName();
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnSwapped(sender, eventArgs) {
//                var text = "Columns with unique names: " + eventArgs.get_gridSourceColumn().get_uniqueName() + " and " + eventArgs.get_gridTargetColumn().get_uniqueName() + " were swapped";
//                document.getElementById("OutPut").innerHTML = text;
			}

			function ColumnContextMenu(sender, eventArgs) {
//                var text = "Context menu on column with index: " + eventArgs.get_gridColumn().get_element().cellIndex;
//                document.getElementById("OutPut").innerHTML = text;
			}

			function RowContextMenu(sender, eventArgs) {
//                var text = "Context menu on row with index: " + eventArgs.get_itemIndexHierarchical();
//                document.getElementById("OutPut").innerHTML = text;
			}


                function OnClientValueChanged(sender, args) {
                    // Show the tooltip only while the slider handle is sliding. In case the user simply clicks on the track of the slider to change the value
                    // the change will be quick and the tooltip will show and hide too quickly.
                    if (!isSliding) return;
                    var tooltip = $find("<%= RadToolTip1.ClientID %>");
                    setTimeout(function () {
                         UpdateToolTipText(tooltip, sender);
                    }, 30);
               }
 
               var isSliding = false;
               function OnClientSlideStart(sender, args) {
                    isSliding = true;
                    ShowRadToolTip(sender);
               }
 
               function OnClientSlide(sender, args) {
                    var tooltip = $find("<%= RadToolTip1.ClientID %>");
                    ResetToolTipLocation(tooltip);
               }
 
               function OnClientSlideRangeStart(sender, args) {
                    isSliding = true;
                    ShowRadToolTip(sender);
               }
 
               function OnClientSlideRange(sender, args) {
                    var tooltip = $find("<%= RadToolTip1.ClientID %>");
                    ResetToolTipLocation(tooltip);
               }
 
               function OnClientSlideEnd(sender, args) {
                    isSliding = false;
                    $find("<%= RadToolTip1.ClientID %>").hide();
               }
 
               function OnClientSlideRangeEnd(sender, args) {
                    isSliding = false;
                    $find("<%= RadToolTip1.ClientID %>").hide();
               }
 
               function ShowRadToolTip(slider) {
                    var tooltip = $find("<%= RadToolTip1.ClientID %>");
                    tooltip.set_targetControl($get("RadSliderSelected_" + slider.get_id()));
                    ResetToolTipLocation(tooltip);
                    setTimeout(function () {
                         UpdateToolTipText(tooltip, slider);
                    }, 30);
               }
 
               function ResetToolTipLocation(tooltip) {
 
                    if (!tooltip.isVisible()) {
                         setTimeout(function () {
                              tooltip.show();
                         }, 20);
                    }
                    else
                         tooltip.updateLocation();
               }
 
               function UpdateToolTipText(tooltip, slider) {
                    var div = document.createElement("div");
                    div.style.whiteSpace = "nowrap";
                    if (slider.get_itemType() == Telerik.Web.UI.SliderItemType.Item)
                         div.innerHTML = (slider.get_selectedItems()[0].get_text() + " / " + slider.get_selectedItems()[1].get_text());
                    else
                         div.innerHTML = (slider.get_selectionStart() + " / " + slider.get_selectionEnd());
 
                    tooltip.set_contentElement(div);
               }

			   -->
		</script>
	</telerik:RadCodeBlock>
	
	 <telerik:RadToolTip ID="RadToolTip1" runat="server" OffsetY="20" Position="TopCenter"
          ShowCallout="false" Height="45px" ShowEvent="FromCode" HideEvent="FromCode">
     </telerik:RadToolTip>

	  <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>

	<telerik:RadSkinManager ID="QsfSkinManager" runat="server" ShowChooser="false" />

	<telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All">

	</telerik:RadFormDecorator>

   <%-- <div id="DecorationZone">
				<div class="module" style="height: 22px; width: 92%">
				<asp:CheckBox ID="CheckBox1" runat="server" Text="Disable Paging?"></asp:CheckBox>
				<asp:CheckBox ID="CheckBox2" runat="server" Text="Add Second Worksheet?"></asp:CheckBox>
				<asp:CheckBox ID="CheckBox3" runat="server" Text="Apply Custom Styles?"></asp:CheckBox>
	
		</div>
	</div>--%>


	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	 <ClientEvents OnResponseEnd="OnResponseEnd" ></ClientEvents>
		 <AjaxSettings>
         
         <telerik:AjaxSetting AjaxControlID="PostBack1">
				<UpdatedControls>
					 <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="RadChart1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="DataList1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
					 <telerik:AjaxUpdatedControl ControlID="log" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="OutPut" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
            			<telerik:AjaxUpdatedControl ControlID="DropDownList1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
        			<telerik:AjaxUpdatedControl ControlID="DropDownList2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
         		  </UpdatedControls>
			</telerik:AjaxSetting>

			<telerik:AjaxSetting AjaxControlID="DropDownList1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="DropDownList2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
             		  </UpdatedControls>
			</telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="RadChart1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
					 <telerik:AjaxUpdatedControl ControlID="log" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="OutPut" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
	 <telerik:AjaxUpdatedControl ControlID="HiddenParam" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
     				  </UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadChart1">
				<UpdatedControls>
					 <telerik:AjaxUpdatedControl ControlID="log" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
			
				 <telerik:AjaxUpdatedControl ControlID="OutPut" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="HiddenParam" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
			
				  </UpdatedControls>
			</telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="RadSlider2">
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="lblSelectionStart2" UpdatePanelRenderMode="Inline">
                         </telerik:AjaxUpdatedControl>
                         <telerik:AjaxUpdatedControl ControlID="lblSelectionEnd2" UpdatePanelRenderMode="Inline">
                         </telerik:AjaxUpdatedControl>
                  		 <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="RadChart1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
				 <telerik:AjaxUpdatedControl ControlID="DataList1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                   </UpdatedControls>
               </telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
	</telerik:RadAjaxLoadingPanel>

	<asp:HiddenField ID="HiddenParam" runat="server" Value="Test" />

	<div style="float:right">
   
		<asp:DataList ID="DataList1" runat="server">
			<ItemTemplate>
				Maximo:
				<asp:Label ID="MaximoLabel" runat="server" Text='<%# Eval("Maximo") %>' />
&nbsp;
&nbsp;
				Minimo:
				<asp:Label ID="Label1" runat="server" Text='<%# Eval("Minimo") %>' />
&nbsp;
&nbsp;
				Promedio:
				<asp:Label ID="Label2" runat="server" Text='<%# Eval("Promedio") %>' />
			</ItemTemplate>
		  
		</asp:DataList>
	
 </div>
<div style="width:90%">
<table><tr><td>A&ntilde;o:</td><td><asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
    DataSourceID="YearDataSource" DataTextField="Year" 
    DataValueField="Year">
</asp:DropDownList></td><td>Mes:</td><td><asp:DropDownList ID="DropDownList2" 
        runat="server" AutoPostBack="True" 
    DataSourceID="MonthDataSource" DataTextField="Month" DataValueField="Month" 
        ondatabound="DropDownList2_DataBound">
</asp:DropDownList></td><td>Dias:</td><td>
 Desde el:
          <asp:Label ID="lblSelectionStart2" runat="server" Text="1"></asp:Label>
         dia, Hasta el:
          <asp:Label ID="lblSelectionEnd2" runat="server" Text="5"></asp:Label>
          <telerik:RadSlider runat="server" ID="RadSlider2" CssClass="dragRangeSlider" Width="450px"
               Height="45px" TrackPosition="TopLeft" IsSelectionRangeEnabled="true" MinimumValue="1"
               MaximumValue="30" SmallChange="1" LargeChange="5" SelectionStart="1" SelectionEnd="5"
               OnClientValueChanged="OnClientValueChanged" OnClientSlideStart="OnClientSlideStart"
               OnClientSlide="OnClientSlide" OnClientSlideEnd="OnClientSlideEnd" ShowDecreaseHandle="false"
               ShowIncreaseHandle="false" OnClientSlideRangeStart="OnClientSlideRangeStart"
               OnClientSlideRange="OnClientSlideRange" OnClientSlideRangeEnd="OnClientSlideRangeEnd"
               OnValueChanged="RadSlider2_ValueChanged" AutoPostBack="true" EnableDragRange="true"
               ItemType="Tick" EnableServerSideRendering="true">
          </telerik:RadSlider>
         
     </td></tr></table>
       
          
     
      <div style="float:left">
     

	   <asp:Button ID="PostBack1" Text="Actualizar" runat="server" 
            onclick="PostBack1_Click"></asp:Button>
	  &nbsp;  &nbsp;
  <asp:CheckBox ID="CheckBox1" runat="server" 
        oncheckedchanged="CheckBox1_CheckedChanged" Text="Diferenciar tipo de licencias" /> 
	
</div>
      <div style="float:right">
Mail: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
          &nbsp;  &nbsp;
           <asp:Button ID="BtnSendReportByMail" Text="Enviar Informe" runat="server" 
            onclick="BtnSendReportByMail_Click"></asp:Button>
  </div>
    <br />
   <p>Selecciona una Fecha en la grilla para ver los usuarios conectados en ese momento.</p>
	<span style="font-weight: bold;"></span><span id="OutPut" style="font-weight: bold;
			color: navy;"></span>
			 </div>


	<%-- <div style="float:left;width:90%;" id="Graph" >

		 <telerik:RadChart ID="RadChart1" runat="server" 
	 OnClick="RadChart1_Click" 
		 AlternateText="DrillDown RadChart" Skin="Web20" 
			 Width="900px" AutoLayout="True" AutoTextWrap="True" 
			 IntelligentLabelsEnabled="True" onitemdatabound="RadChart1_ItemDataBound">
			  <Legend Appearance-Visible="false">

			<Appearance GroupNameFormat="#VALUE">

			</Appearance>

		</Legend>
<Appearance>
<Border Color="103, 136, 190"></Border>
</Appearance>

<Series>
<telerik:ChartSeries Name="Cantidad" DataYColumn="Cantidad">

<Appearance>
<FillStyle FillType="ComplexGradient">
<FillSettings><ComplexGradient>
<telerik:GradientElement Color="213, 247, 255"></telerik:GradientElement>
<telerik:GradientElement Color="193, 239, 252" Position="0.5"></telerik:GradientElement>
<telerik:GradientElement Color="157, 217, 238" Position="1"></telerik:GradientElement>
</ComplexGradient>
</FillSettings>
</FillStyle>

<TextAppearance TextProperties-Color="103, 136, 190"></TextAppearance>
</Appearance>

</telerik:ChartSeries>
</Series>

<PlotArea>
<XAxis    DataLabelsColumn="Fecha">
<Appearance Color="149, 184, 206" MajorTick-Color="149, 184, 206">
<MajorGridLines Width="0" Color="209, 221, 238"></MajorGridLines>
</Appearance> 
</XAxis>

<YAxis>
<Appearance Color="149, 184, 206" MinorTick-Color="149, 184, 206" MajorTick-Color="149, 184, 206">
<MajorGridLines Color="209, 221, 238"></MajorGridLines>

<MinorGridLines Color="209, 221, 238"></MinorGridLines>
</Appearance>
</YAxis>

<Appearance>
<FillStyle MainColor="249, 250, 251" FillType="Solid"></FillStyle>

<Border Color="149, 184, 206"></Border>
</Appearance>
</PlotArea>

<ChartTitle Visible="False">
<Appearance Visible="False">
<FillStyle MainColor=""></FillStyle>
</Appearance>

<TextBlock>
<Appearance TextProperties-Color="0, 0, 79"></Appearance>
</TextBlock>
</ChartTitle>


	  
	</telerik:RadChart>



		 <%--<telerik:RadChart ID="RadChart2" runat="server" 
		DataSourceID="UsersGraphDataSource" AlternateText="DrillDown RadChart" Skin="Web20" 
			 Width="900px" AutoLayout="True" AutoTextWrap="True" 
			 IntelligentLabelsEnabled="True">
			  <Legend Appearance-Visible="false">

			<Appearance GroupNameFormat="#VALUE">

			</Appearance>

		</Legend>
<Appearance>
<Border Color="103, 136, 190"></Border>
</Appearance>

<Series>
<telerik:ChartSeries Name="Usuarios" DataYColumn="RowNumber" DataLabelsColumn="Usuario">

<Appearance>
<FillStyle FillType="ComplexGradient">
<FillSettings><ComplexGradient>
<telerik:GradientElement Color="213, 247, 255"></telerik:GradientElement>
<telerik:GradientElement Color="193, 239, 252" Position="0.5"></telerik:GradientElement>
<telerik:GradientElement Color="157, 217, 238" Position="1"></telerik:GradientElement>
</ComplexGradient>
</FillSettings>
</FillStyle>

<TextAppearance TextProperties-Color="103, 136, 190"></TextAppearance>
</Appearance>

</telerik:ChartSeries>
</Series>

<PlotArea>
<XAxis    DataLabelsColumn="Fecha">
<Appearance Color="149, 184, 206" MajorTick-Color="149, 184, 206">
<MajorGridLines Width="0" Color="209, 221, 238"></MajorGridLines>
</Appearance> 
</XAxis>

<YAxis>
<Appearance Color="149, 184, 206" MinorTick-Color="149, 184, 206" MajorTick-Color="149, 184, 206">
<MajorGridLines Color="209, 221, 238"></MajorGridLines>

<MinorGridLines Color="209, 221, 238"></MinorGridLines>
</Appearance>
</YAxis>

<Appearance>
<FillStyle MainColor="249, 250, 251" FillType="Solid"></FillStyle>

<Border Color="149, 184, 206"></Border>
</Appearance>
</PlotArea>

<ChartTitle Visible="False">
<Appearance Visible="False">
<FillStyle MainColor=""></FillStyle>
</Appearance>

<TextBlock>
<Appearance TextProperties-Color="0, 0, 79"></Appearance>
</TextBlock>
</ChartTitle>


	  
	</telerik:RadChart>




	 </div>--%>

	<div style="float:left;width:90%; "  id="Grid">
	<telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" 
	AllowPaging="True" AllowSorting="True" CellSpacing="0" 
	
   ShowStatusBar="true" 
   OnItemCommand="RadGrid1_ItemCommand"
 
 AllowMultiRowSelection="false"
	 GridLines="None" ShowGroupPanel="True" 
 PageSize="100" ShowFooter="True" 
		Skin="Outlook">
	   

		<GroupingSettings UnGroupButtonTooltip="Desagrupar" />
   
	<MasterTableView CommandItemDisplay="Top" autogeneratecolumns="False" 
	ClientDataKeyNames="Cliente,Anio,Mes,Dia,Hora">

 
		<CommandItemSettings ExportToPdfText="Export to PDF"  ShowExportToExcelButton="true" ShowExportToPdfButton="true" ShowExportToWordButton="true" ShowAddNewRecordButton="false">
		</CommandItemSettings>

	   
		<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
			<HeaderStyle Width="20px"></HeaderStyle>
		</RowIndicatorColumn>
		<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
			<HeaderStyle Width="20px"></HeaderStyle>
		</ExpandCollapseColumn>
		<Columns>
					   
			<telerik:GridBoundColumn DataField="Tipo"  ItemStyle-Width="50"
				FilterControlAltText="Filter Tipo column" HeaderText="Tipo" 
				SortExpression="Tipo" UniqueName="Tipo" ReadOnly="True">
			</telerik:GridBoundColumn>
			 <telerik:GridBoundColumn DataField="Cantidad" DataType="System.Int32" ItemStyle-Width="10"
				FilterControlAltText="Filter Cantidad column" HeaderText="Cantidad" 
				SortExpression="Cantidad" UniqueName="Cantidad" ReadOnly="True">
			</telerik:GridBoundColumn>
			<telerik:GridBoundColumn DataField="Anio" ItemStyle-Width="10"
				FilterControlAltText="Filter Anio column" HeaderText="Anio" 
				SortExpression="Anio" UniqueName="Anio" DataType="System.Int32" 
				ReadOnly="True">
			</telerik:GridBoundColumn>
			<telerik:GridBoundColumn DataField="Mes"  ItemStyle-Width="10"
				FilterControlAltText="Filter Mes column" HeaderText="Mes" 
				SortExpression="Mes" UniqueName="Mes" DataType="System.Int32" 
				ReadOnly="True">
			</telerik:GridBoundColumn>
			<telerik:GridBoundColumn DataField="Dia" DataType="System.Int32"  ItemStyle-Width="10" 
				FilterControlAltText="Filter Dia column" HeaderText="Dia" 
				SortExpression="Dia" UniqueName="Dia" ReadOnly="True">
			</telerik:GridBoundColumn>
			<telerik:GridBoundColumn DataField="Hora" DataType="System.Int32"  ItemStyle-Width="10"
				FilterControlAltText="Filter Hora column" HeaderText="Hora" 
				SortExpression="Hora" UniqueName="Hora" ReadOnly="True">
			</telerik:GridBoundColumn>
				<%--   <telerik:GridBoundColumn DataField="Server"  ItemStyle-Width="50"
				FilterControlAltText="Filter Server column" HeaderText="Server" 
				SortExpression="Server" UniqueName="Server">
			</telerik:GridBoundColumn>
			<telerik:GridBoundColumn DataField="Base" 
				FilterControlAltText="Filter Cliente column" HeaderText="Base"  ItemStyle-Width="50"
				SortExpression="Base" UniqueName="Base">
			</telerik:GridBoundColumn>--%>
		   </Columns>
		<EditFormSettings>
			<EditColumn FilterControlAltText="Filter EditCommandColumn column">
			</EditColumn>
		</EditFormSettings>
	</MasterTableView>
	<FilterMenu EnableImageSprites="False">
	</FilterMenu>
	<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
	</HeaderContextMenu>
	  <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
			<ClientEvents OnGridCreated="GridCreated" OnColumnResized="ColumnResized" OnColumnSwapping="ColumnSwapping"
				OnColumnSwapped="ColumnSwapped" OnColumnClick="ColumnClick" OnColumnDblClick="ColumnDblClick"
				OnColumnMouseOver="ColumnMouseOver" OnColumnMouseOut="ColumnMouseOut" OnRowResized="RowResized"
				OnRowClick="RowClick" OnRowDblClick="RowDblClick" OnRowMouseOver="RowMouseOver"
				OnRowMouseOut="RowMouseOut" OnRowSelected="RowSelected" OnRowDeselected="RowDeselected"
				OnRowContextMenu="RowContextMenu" OnColumnContextMenu="ColumnContextMenu"></ClientEvents>
			<Selecting AllowRowSelect="True"></Selecting>
			<Resizing AllowRowResize="True" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
				AllowColumnResize="True"></Resizing>
		</ClientSettings>
		 <PagerStyle Mode="NumericPages"></PagerStyle>

</telerik:RadGrid>
</div>

	<div style="float:right;" id="GridUsers">
  <div class="closediv" id="closediv"></div> 
	   <span class="ecs_tooltip">Presione Esc para cerrar<span class="arrow"></span></span> 
   

<ul id="UserList" >
</ul>
</div>

	<div class="loader"></div>

<%--<asp:SqlDataSource ID="SQLDataSourceUCM" runat="server" 
	ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
	
        SelectCommand="select Cliente,Server, Base,Anio, Mes, Dia, Hora, [Tipo], SUM(Cantidad) as Cantidad from (
select 
 Client as Cliente,Server, Base, CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END as Tipo,
  count(1) as Cantidad,
 YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora 
 from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia], Client,Server, Base, UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio')
 group by type,user_id, winuser, Client,Server, Base, UpdateDate) as LicxTipoSinUsuaDupxPC WHERE ([Client] = @Client  and YEAR(updatedate) = @Year  and Month(updatedate) = @Month)
 group by Client,Server, Base,UpdateDate, [TipoLicencia] 
 )
 as Sub where Hora > 8 and Hora <19
 group by Cliente,Server, Base,Anio, Mes, Dia, Hora, [Tipo] order by Cliente, Server, Base, Anio, Mes, Dia, Hora" 
        onfiltering="SQLDataSourceUCM_Filtering" 
        onselecting="SQLDataSourceUCM_Selecting" 
        onselected="SQLDataSourceUCM_Selected">
	<SelectParameters>
		<asp:QueryStringParameter Name="Client" QueryStringField="Cliente" 
			Type="String" />
          <asp:ControlParameter ControlID="DropDownList1" Name="Year" 
              PropertyName="SelectedValue" Type="String" />
          <asp:ControlParameter ControlID="DropDownList2" Name="Month" 
              PropertyName="SelectedValue" Type="String" />
	</SelectParameters>
</asp:SqlDataSource>--%>

<%--<asp:SqlDataSource ID="MaxDataSource" runat="server" 
	ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
	SelectCommand="select Cliente, MAX(Cantidad) as Maximo,MIN(Cantidad) as Minimo,AVG(Cantidad) as Promedio from (
select Cliente,Anio, Mes, Dia, Hora, SUM(Cantidad) as Cantidad from (
select 
 Client as Cliente,Server, Base, CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END as Tipo,
  count(1) as Cantidad,
 YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora 
 from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia], Client,Server, Base, UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio')
 group by type,user_id, winuser, Client,Server, Base, UpdateDate) as LicxTipoSinUsuaDupxPC WHERE ([Client] = @Client  and YEAR(updatedate) = @Year  and Month(updatedate) = @Month)
 group by Client,Server, Base,UpdateDate, [TipoLicencia] 
 )
 as Sub where Hora > 8 and Hora <19
 group by Cliente,Anio, Mes, Dia, Hora 
 )
 as Sub2
  group by Cliente 
  order by Cliente" onfiltering="MaxDataSource_Filtering" 
        onselecting="MaxDataSource_Selecting" onselected="MaxDataSource_Selected">
	<SelectParameters>
		<asp:QueryStringParameter Name="Client" QueryStringField="Cliente" 
			Type="String" />
          <asp:ControlParameter ControlID="DropDownList1" Name="Year" 
              PropertyName="SelectedValue" Type="String" />
          <asp:ControlParameter ControlID="DropDownList2" Name="Month" 
              PropertyName="SelectedValue" Type="String" />
	</SelectParameters>
</asp:SqlDataSource>--%>

<%--<asp:SqlDataSource ID="GraphDataSource" runat="server" 
	ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
	SelectCommand="select (CONVERT(NVARCHAR,Dia ) + '/' + CONVERT(NVARCHAR,Mes) + '/' + CONVERT(NVARCHAR,Anio) + ' ' + CONVERT(NVARCHAR,Hora)) as Fecha, [Tipo], SUM(Cantidad) as Cantidad from (
select 
 Client as Cliente,Server, Base, CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END as Tipo,
  count(1) as Cantidad,
 YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes, DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora 
 from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia], Client,Server, Base, UpdateDate FROM UCMClientSset where winuser not in ('bpm-1','Servicio')
 group by type,user_id, winuser, Client,Server, Base, UpdateDate) as LicxTipoSinUsuaDupxPC WHERE ([Client] = @Client and YEAR(updatedate) = @Year  and Month(updatedate) = @Month)
 group by Client,Server, Base,UpdateDate, [TipoLicencia] 
 )
 as Sub where Hora > 8 and Hora <19
 group by Anio, Mes, Dia, Hora, [Tipo] order by   Anio, Mes, Dia, Hora" 
        onfiltering="GraphDataSource_Filtering" onselected="GraphDataSource_Selected" 
        onselecting="GraphDataSource_Selecting">
	<SelectParameters>
		<asp:QueryStringParameter Name="Client" QueryStringField="Cliente" 
			Type="String" />
          <asp:ControlParameter ControlID="DropDownList1" Name="Year" 
              PropertyName="SelectedValue" Type="String" />
          <asp:ControlParameter ControlID="DropDownList2" Name="Month" 
              PropertyName="SelectedValue" Type="String" />
	</SelectParameters>
</asp:SqlDataSource>--%>

<%--<asp:SqlDataSource ID="UsersGraphDataSource" runat="server" 
	ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
	SelectCommand="select (CONVERT(NVARCHAR,Dia ) + '/' + CONVERT(NVARCHAR,Mes) + '/' + CONVERT(NVARCHAR,Anio) + ' ' + CONVERT(NVARCHAR,Hora) ) as Fecha,
  Usuario, rownumber from (
 
  select  Client,  YEAR(UpdateDate) as Anio, MONTH(UpdateDate) as Mes,
   DAY(UpdateDate) as Dia,  datepart(hh,UpdateDate) as Hora,
   Usuario  from (
   
   SELECT winuser as Usuario, Client,UpdateDate FROM UCMClientSset 
   where winuser not in ('bpm-1','Servicio') 
    group by user_id, winuser, Client, UpdateDate, winuser
    
    ) as LicxTipoSinUsuaDupxPC 
    WHERE ([Client] = @Client and YEAR(updatedate) = @Year  and Month(updatedate) = @Month) 
     group by Client,UpdateDate, Usuario  )  as Sub inner join (select distinct rownumber = ROW_NUMBER() OVER (ORDER BY u.winuser asc)
,u.winuser from (select distinct winuser from ucmclientsset where client  = @Client and YEAR(updatedate) = @Year  and Month(updatedate) = @Month) u
) as sub2 on sub.Usuario = Sub2.winuser
     where Hora > 8 and Hora <19        group by Anio, Mes, Dia, Hora, Usuario, rownumber order by   Anio, Mes, Dia, Hora">
	<SelectParameters>
		<asp:QueryStringParameter Name="Client" QueryStringField="Cliente" 
			Type="String" />
          <asp:ControlParameter ControlID="DropDownList1" Name="Year" 
              PropertyName="SelectedValue" Type="String" />
          <asp:ControlParameter ControlID="DropDownList2" Name="Month" 
              PropertyName="SelectedValue" Type="String" />
	</SelectParameters>
</asp:SqlDataSource>--%>

<asp:SqlDataSource ID="MonthDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            
    SelectCommand="SELECT DISTINCT Month(updatedate) as Month FROM [UCMCLIENTSSet] WHERE ([Client] = @Client) and YEAR(updatedate) = @Year order by Month(updatedate) desc">
      <SelectParameters>
		<asp:QueryStringParameter Name="Client" QueryStringField="Cliente" 
			Type="String" />
          <asp:ControlParameter ControlID="DropDownList1" Name="Year" 
              PropertyName="SelectedValue" Type="String" />
      </SelectParameters>
        </asp:SqlDataSource>

<asp:SqlDataSource ID="YearDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            SelectCommand="select distinct YEAR(updatedate) as Year from ucmclientsset where client = @Client order by YEAR(updatedate) desc" >
	<SelectParameters>
		<asp:QueryStringParameter Name="Client" QueryStringField="Cliente" 
			Type="String" />
	</SelectParameters>
        </asp:SqlDataSource>
</asp:Content>
