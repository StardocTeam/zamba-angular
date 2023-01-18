<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />
     <div class="demo-section k-content">
	    <div class="climate">
	        <h4>
	            Climate control history
	        </h4>
	        <table class="history">
	            <tr>
	                <td class="spark">
						<kendo:sparkline name="press-log" data="${pressureData}">
						</kendo:sparkline>
	                </td>
	                <td class="value">980<span>mb</span></td>
	            </tr>
            <tr>
                <td class="spark">
	                <kendo:sparkline name="temp-log" type="column" data="${temperatureData}">
	                	<kendo:sparkline-tooltip format="{0} &deg;C"></kendo:sparkline-tooltip>
	                </kendo:sparkline>
                </td>
                <td class="value">21<span>&deg;C</span></td>
            </tr>
            <tr>
                <td class="spark">
					<kendo:sparkline name="hum-log" type="area" data="${humidityData}">
						<kendo:sparkline-tooltip format="{0} %"></kendo:sparkline-tooltip>
					</kendo:sparkline>
                </td>
                <td class="value">32<span>%</span></td>
            </tr>
        </table>
    </div>
    <div class="temperature">
        <h4>
            Temperature control
        </h4>
        <div class="stats">
			<kendo:sparkline name="temp-range" type="bullet" data="${temperatureRange}">
				<kendo:sparkline-valueAxis>
				    <kendo:sparkline-valueAxisItem min="0" max="30">
						<kendo:sparkline-valueAxisItem-plotBands>
							<kendo:sparkline-valueAxisItem-plotBand from="0" to="15" color="#787878" opacity="0.15" />
							<kendo:sparkline-valueAxisItem-plotBand from="15" to="22" color="#787878" opacity="0.3" />
							<kendo:sparkline-valueAxisItem-plotBand from="22" to="30" color="#787878" opacity="0.15" />
						</kendo:sparkline-valueAxisItem-plotBands>
				    </kendo:sparkline-valueAxisItem>
				</kendo:sparkline-valueAxis>
			</kendo:sparkline>
        </div>
    </div>
    <div class="conditioner">
        <h4>
            Conditioner working time
        </h4>
        <ul class="pie-list stats">
            <li>MON
	            <kendo:sparkline name="stats-mon" type="pie" data='${acStats["mon"]}'>
	            </kendo:sparkline>
            </li>
            <li>TUE
	            <kendo:sparkline name="stats-tue" type="pie" data='${acStats["tue"]}'>
	            </kendo:sparkline>
            </li>
            <li>WED
	            <kendo:sparkline name="stats-wed" type="pie" data='${acStats["wed"]}'>
	            </kendo:sparkline>
            </li>
            <li>THU
	            <kendo:sparkline name="stats-thu" type="pie" data='${acStats["thu"]}'>
	            </kendo:sparkline>
            </li>
            <li>FRI
	            <kendo:sparkline name="stats-fri" type="pie" data='${acStats["fri"]}'>
	            </kendo:sparkline>
            </li>
            <li>SAT
	            <kendo:sparkline name="stats-sat" type="pie" data='${acStats["sat"]}'>
	            </kendo:sparkline>
            </li>
            <li>SUN
	            <kendo:sparkline name="stats-sun" type="pie" data='${acStats["sun"]}'>
	            </kendo:sparkline>
            </li>
        </ul>
    </div>
</div>

<style>
    .temperature, .conditioner {
        margin: 0;
        padding: 30px 0 0 0;
    }

    .history {
        border-collapse: collapse;
        width: 100%;
    }
    .history td {
        padding: 0;
        vertical-align: bottom;
    }
    .history td.spark {
        line-height: 30px;
    }
    .history td.value {
        font-size: 1.6em;
        font-weight: normal;
        line-height: 20px;
    }
    .history td.value span {
        font-size: .5em;
        vertical-align: top;
        line-height: 30px;
    }
    .stats {
        text-align: center;
    }
    .pie-list {
        margin: 0;
        padding: 0;
        list-style-type: none;
    }
    .pie-list li {
        display: inline-block;
        text-align: center;
        width: 34px;
        font-size: 10px;
    }
    #stats-mon,
    #stats-tue,
    #stats-wed,
    #stats-thu,
    #stats-fri,
    #stats-sat,
    #stats-sun {
        display: block;
        width: 34px;
        line-height: 30px;
    }
    #temp-range {
        width: 100%;
        line-height: 30px;
    }
</style>


<demo:footer />
