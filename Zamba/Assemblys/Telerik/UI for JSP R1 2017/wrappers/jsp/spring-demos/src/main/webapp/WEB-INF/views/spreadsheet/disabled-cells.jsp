<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<c:url value="/resources/shared/js/jszip.min.js" var="jszip" />

<demo:header />
<%
	String[] mergedCells = { "A1:G1", "C15:E15" };
%>
<div class="box wide">
    <div class="box-col">
    <h4>Disable cells</h4>
    <ul class="options">
        <li>
            <button class="k-button" id="toggle">Disable / Enable 'Quantity' column</button>
        </li>
    </ul>
    </div>
</div>
<kendo:spreadsheet name="spreadsheet" style="width: 100%;">
	<kendo:spreadsheet-sheets>
		<kendo:spreadsheet-sheet name="Food Order"
			mergedCells="<%=mergedCells%>">
			<kendo:spreadsheet-sheet-rows>

				<kendo:spreadsheet-sheet-row height='70'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell index='0'
							value='Invoice #52 - 06/23/2015' fontSize='32' textAlign='center'
							background='rgb(96,181,255)' color='white' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='ID' textAlign='center'
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Product'
							textAlign='center' background='rgb(167,214,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Quantity'
							textAlign='center' background='rgb(167,214,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Price' textAlign='center'
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Tax' textAlign='center'
							background='rgb(167,214,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Amount'
							textAlign='center' background='rgb(167,214,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(167,214,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='216321'
							textAlign='center' background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Calzone'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='1' textAlign='center'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='12.39'
							background='rgb(255,255,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C3*D3*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C3*D3+E3' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='546897'
							textAlign='center' background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Margarita'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='2' textAlign='center'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='8.79'
							background='rgb(229,243,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C4*D4*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C4*D4+E4' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='456231'
							textAlign='center' background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Pollo Formaggio'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='1' textAlign='center'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='13.99'
							background='rgb(255,255,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C5*D5*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C5*D5+E5' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='455873'
							textAlign='center' background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Greek Salad'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='1' textAlign='center'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='9.49'
							background='rgb(229,243,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C6*D6*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C6*D6+E6' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='456892'
							textAlign='center' background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Spinach and Blue Cheese'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='3' textAlign='center'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='11.49'
							background='rgb(255,255,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C7*D7*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C7*D7+E7' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='546564'
							textAlign='center' background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Rigoletto'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='1' textAlign='center'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='10.99'
							background='rgb(229,243,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C8*D8*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C8*D8+E8' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='789455'
							textAlign='center' background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Creme Brulee'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='5' textAlign='center'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='6.99'
							background='rgb(255,255,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C9*D9*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C9*D9+E9' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='123002'
							textAlign='center' background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Radeberger Beer'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='4' textAlign='center'
							background='rgb(229,243,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='4.99'
							background='rgb(229,243,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C10*D10*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' formula='C10*D10+E10' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell value='564896'
							textAlign='center' background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Budweiser Beer'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='3' textAlign='center'
							background='rgb(255,255,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='4.49'
							background='rgb(255,255,255)' color='rgb(0,62,117)'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C11*D11*0.2' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' formula='C11*D11+E11' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(229,243,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(255,255,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='25'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell background='rgb(193,226,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(193,226,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(193,226,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(193,226,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell value='Tip:' textAlign='right'
							background='rgb(193,226,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(193,226,255)'
							color='rgb(0,62,117)' formula='SUM(F3:F11)*0.1'
							format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell background='rgb(193,226,255)'
							color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

				<kendo:spreadsheet-sheet-row height='50'>
					<kendo:spreadsheet-sheet-row-cells>
						<kendo:spreadsheet-sheet-row-cell index='0'
							background='rgb(193,226,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell index='1'
							background='rgb(193,226,255)' color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell index='2' value='Total Amount:'
							fontSize='20' textAlign='right' background='rgb(193,226,255)'
							color='rgb(0,62,117)' />
						<kendo:spreadsheet-sheet-row-cell index='5' fontSize='20'
							background='rgb(193,226,255)' color='rgb(0,62,117)'
							formula='SUM(F3:F14)' format='$#,##0.00' />
						<kendo:spreadsheet-sheet-row-cell index='6'
							background='rgb(193,226,255)' color='rgb(0,62,117)' />
					</kendo:spreadsheet-sheet-row-cells>
				</kendo:spreadsheet-sheet-row>

			</kendo:spreadsheet-sheet-rows>
			
			<kendo:spreadsheet-sheet-columns>
				<kendo:spreadsheet-sheet-column width="100"/>
				<kendo:spreadsheet-sheet-column width="215"/>
				<kendo:spreadsheet-sheet-column width="115"/>
				<kendo:spreadsheet-sheet-column width="115"/>
				<kendo:spreadsheet-sheet-column width="115"/>
				<kendo:spreadsheet-sheet-column width="155"/>
			</kendo:spreadsheet-sheet-columns>
		</kendo:spreadsheet-sheet>
	</kendo:spreadsheet-sheets>
</kendo:spreadsheet>

<!-- Load JSZIP library to enable Excel Export -->
<script src="${jszip}"></script>
<script>
  $(function() {
	  $("#toggle").click(function() {
          var range = $("#spreadsheet").data("kendoSpreadsheet").activeSheet().range("C3:C11");
          var enabled = range.enable();

          if (enabled === null) {
              enabled = true;
          }

          //Enable / disable specified range
          range.enable(!enabled);
      });
  })
</script>

<demo:footer />