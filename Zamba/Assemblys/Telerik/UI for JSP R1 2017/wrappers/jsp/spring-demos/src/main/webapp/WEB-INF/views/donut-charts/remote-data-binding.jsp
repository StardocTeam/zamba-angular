<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/donut-charts/remote-data-binding/read" var="readUrl" />

<demo:header />
     <div class="demo-section k-content wide">
         <kendo:chart name="chart">
             <kendo:chart-title text="1024x768 screen resolution trends" />
             <kendo:chart-legend position="top" />
             <kendo:dataSource>
                 <kendo:dataSource-transport>
                     <kendo:dataSource-transport-read url="${readUrl}" dataType="json" type="POST" contentType="application/json" />
                 </kendo:dataSource-transport>
                 <kendo:dataSource-group>
                     <kendo:dataSource-groupItem field="year" dir="asc" />
                 </kendo:dataSource-group>
                 <kendo:dataSource-sort>
                     <kendo:dataSource-sortItem field="orderNumber" dir="asc" />
                 </kendo:dataSource-sort>
             </kendo:dataSource>
             <kendo:chart-series>
                <kendo:chart-seriesItem padding="10" type="donut" field="share" categoryField="resolution" visibleInLegendField="visibleInLegend" colorField="color" startAngle="270" />
             </kendo:chart-series>
             <kendo:chart-tooltip visible="true" template="#= dataItem.resolution #: #= value #% (#= dataItem.year #)" />
         </kendo:chart>
    </div>
 <demo:footer />