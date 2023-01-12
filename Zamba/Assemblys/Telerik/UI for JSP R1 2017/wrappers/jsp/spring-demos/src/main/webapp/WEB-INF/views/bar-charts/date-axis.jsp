<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />
	<div class="demo-section k-content wide">
	    <kendo:chart name="chart">
	    	<kendo:dataSource data="${stats}" />
	 	 	<kendo:chart-series>
	 	 		<kendo:chart-seriesItem type="column" aggregate="avg" field="value" categoryField="date" />
	 	 	</kendo:chart-series>
	        <kendo:chart-categoryAxis>
	           <kendo:chart-categoryAxisItem type="date" baseUnit="weeks">
	           	<kendo:chart-categoryAxisItem-majorGridLines visible="false"/>
	           </kendo:chart-categoryAxisItem>
	        </kendo:chart-categoryAxis>
	        <kendo:chart-valueAxis>
	           <kendo:chart-valueAxisItem >
	               <kendo:chart-valueAxisItem-line visible="false"/>
	           </kendo:chart-valueAxisItem>
	        </kendo:chart-valueAxis>
	    </kendo:chart>
	</div>
    <div class="box wide">
        <div class="box-col">
        <h4>Base date unit</h4>
        <ul class="options">
            <li>
                <input id="baseUnitAuto" name="baseUnit"
                        type="radio" value="" autocomplete="off" />
                <label for="baseUnitAuto">Automatic (default)</label>
            </li>
            <li>
                <input id="baseUnitYears" name="baseUnit"
                        type="radio" value="years" autocomplete="off" />
                <label for="baseUnitYears">Years</label>
            </li>
            <li>
                <input id="baseUnitMonths" name="baseUnit"
                        type="radio" value="months" autocomplete="off" />
                <label for="baseUnitMonths">Months</label>
            </li>
            <li>
                <input id="baseUnitWeeks" name="baseUnit"
                        type="radio" value="weeks" checked="checked" autocomplete="off" />
                <label for="baseUnitWeeks">Weeks</label>
            </li>
            <li>
                <input id="baseUnitDays" name="baseUnit"
                        type="radio" value="days" autocomplete="off" />
                <label for="baseUnitDays">Days</label>
            </li>
        </ul>
        </div>
        <div class="box-col">
        <h4>Aggregate function</h4>
        <ul class="options">
            <li>
                <input id="aggregateMax" name="aggregate"
                        type="radio" value="max" autocomplete="off" />
                <label for="aggregateMax">Max (default)</label>
            </li>
            <li>
                <input id="aggregateMin" name="aggregate"
                        type="radio" value="min" autocomplete="off" />
                <label for="aggregateMin">Min</label>
            </li>
            <li>
                <input id="aggregateSum" name="aggregate"
                        type="radio" value="sum" autocomplete="off" />
                <label for="aggregateSum">Sum</label>
            </li>
            <li>
                <input id="aggregateAvg" name="aggregate"
                        type="radio" value="avg" checked="checked" autocomplete="off" />
                <label for="aggregateAvg">Avg</label>
            </li>
            <li>
                <input id="aggregateCount" name="aggregate"
                        type="radio" value="count" autocomplete="off" />
                <label for="aggregateCount">Count</label>
            </li>
        </ul>
        </div>
        <div class="box-col">
            <h4>&nbsp;</h4>
            <p>Custom aggregate functions are supported.</p>
        </div>
    </div>
    <script>
    $(".box").bind("change", refresh);
	
	    function refresh() {
	        var chart = $("#chart").data("kendoChart"),
	            series = chart.options.series,
	            categoryAxis = chart.options.categoryAxis,
	            baseUnitInputs = $("input:radio[name=baseUnit]"),
	            aggregateInputs = $("input:radio[name=aggregate]");
	
	        for (var i = 0, length = series.length; i < length; i++) {
	            series[i].aggregate = aggregateInputs.filter(":checked").val();
	        }
	
	        categoryAxis.baseUnit = baseUnitInputs.filter(":checked").val();
	
	        chart.refresh();
	    }
	</script>
<demo:footer />
