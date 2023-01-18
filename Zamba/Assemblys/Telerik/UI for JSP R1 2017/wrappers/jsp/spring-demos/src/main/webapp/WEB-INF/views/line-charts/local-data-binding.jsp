<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
     <div class="demo-section k-content wide">
         <kendo:chart name="chart">
             <kendo:chart-title text="Internet Users in United States"></kendo:chart-title>
             <kendo:chart-legend visible="false"></kendo:chart-legend>
             <kendo:dataSource data="${internetUsers}">
             </kendo:dataSource>
             <kendo:chart-series>
                <kendo:chart-seriesItem type="line" field="value" name="United States">
                    <kendo:chart-seriesItem-labels format="{0}%" visible="true"></kendo:chart-seriesItem-labels>
                </kendo:chart-seriesItem>
             </kendo:chart-series>
             <kendo:chart-categoryAxis>
                <kendo:chart-categoryAxisItem field="year">
                   <kendo:chart-categoryAxisItem-majorGridLines visible="false"></kendo:chart-categoryAxisItem-majorGridLines>
                </kendo:chart-categoryAxisItem>
             </kendo:chart-categoryAxis>
             <kendo:chart-valueAxis>
                <kendo:chart-valueAxisItem>
                    <kendo:chart-valueAxisItem-labels format="{0}%"></kendo:chart-valueAxisItem-labels>
                    <kendo:chart-valueAxisItem-line visible="false"></kendo:chart-valueAxisItem-line>
                </kendo:chart-valueAxisItem>
             </kendo:chart-valueAxis>
         </kendo:chart>
     </div>
<demo:footer />
