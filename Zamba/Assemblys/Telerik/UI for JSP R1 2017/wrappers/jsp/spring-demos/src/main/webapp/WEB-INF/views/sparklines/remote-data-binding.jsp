<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />
     <div class="demo-section k-content wide">
    <table id="weather" class="weather">
        <thead>
            <tr>
                <th class="month">MONTH</th>
                <th>MAX TEMP &deg;C</th>
                <th>WIND SPEED KM/H</th>
                <th>RAINFALL MM</th>
            </tr>
        </thead>
        <tbody>
<%
String[] months = { "January", "February", "March", "April", "May", "June" };
for (int i = 0; i < months.length; i++) {
    int monthNumber = i + 1;
    String readUrl = "remote-data-binding/read?station=SOFIA&year=2012&month=" + monthNumber;
%>
        <tr>
            <td class="month"><%= months[i] %></td>
            <td>
            	<% String tmaxId = "sparkline-tmax-" + i; %>
            	<kendo:sparkline name="<%= tmaxId %>">
            		<kendo:dataSource>
            			<kendo:dataSource-transport>
            				<kendo:dataSource-transport-read url="<%= readUrl %>" dataType="json" type="POST" contentType="application/json" />
            			</kendo:dataSource-transport>
            		</kendo:dataSource>
            		<kendo:sparkline-series>
            			<kendo:sparkline-seriesItem type="column" field="TMax" color="#ff0000" negativeColor="#0099ff" />
            		</kendo:sparkline-series>
            	</kendo:sparkline>
            </td>
            <td>
            	<% String wndId = "sparkline-wnd-" + i; %>
            	<kendo:sparkline name="<%= wndId %>">
            		<kendo:dataSource>
            			<kendo:dataSource-transport>
            				<kendo:dataSource-transport-read url="<%= readUrl %>" dataType="json" type="POST" contentType="application/json" />
            			</kendo:dataSource-transport>
            		</kendo:dataSource>
            		<kendo:sparkline-series>
            			<kendo:sparkline-seriesItem field="Wind" color="#5b8f00" />
            		</kendo:sparkline-series>
            	</kendo:sparkline>
            </td>
            <td>
            	<% String rainId = "sparkline-rain-" + i; %>
            	<kendo:sparkline name="<%= rainId %>">
            		<kendo:dataSource>
            			<kendo:dataSource-transport>
            				<kendo:dataSource-transport-read url="<%= readUrl %>" dataType="json" type="POST" contentType="application/json" />
            			</kendo:dataSource-transport>
            		</kendo:dataSource>
            		<kendo:sparkline-series>
            			<kendo:sparkline-seriesItem type="area" field="Rain" color="#0099ff" />
            		</kendo:sparkline-series>
            	</kendo:sparkline>
            </td>
        </tr>
<%
}
%>
        </tbody>
    </table>
</div>

    <style>
        .weather {
            border-collapse: collapse;
            line-height: 50px;
            width: 100%;
        }
        .weather td, .weather th {
            padding: 0;
            text-align: center;
        }
        .weather .month, .weather .month {
            text-align: right;
            padding-right: 20px;
        }
    </style>


<demo:footer />
