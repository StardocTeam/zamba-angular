<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
     <div class="demo-section k-content wide">
         <kendo:chart name="chart">
             <kendo:dataSource data="${data}" />
             <kendo:chart-series>
                <kendo:chart-seriesItem type="scatterLine" xfield="x" yfield="y" style="smooth">
                    <kendo:chart-seriesItem-markers visible="false" />
                </kendo:chart-seriesItem>
             </kendo:chart-series>
             <kendo:chart-xAxis>
                <kendo:chart-xAxisItem min="-2" max="2">
                    <kendo:chart-xAxisItem-labels format="{0:n1}" />
                </kendo:chart-xAxisItem>
             </kendo:chart-xAxis>
             <kendo:chart-yAxis>
                <kendo:chart-yAxisItem>
                    <kendo:chart-yAxisItem-labels format="{0:n2}" />
                </kendo:chart-yAxisItem>
             </kendo:chart-yAxis>
             <kendo:chart-pannable/>
             <kendo:chart-zoomable/>
         </kendo:chart>
     </div>
<demo:footer />
