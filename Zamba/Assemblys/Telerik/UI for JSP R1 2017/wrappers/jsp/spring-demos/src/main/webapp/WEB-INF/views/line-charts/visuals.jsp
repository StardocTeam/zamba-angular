<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
     <div class="demo-section k-content wide">
         <kendo:chart name="chart">
             <kendo:chart-title text="Forecast" />
             <kendo:dataSource data="${forecast}">
             </kendo:dataSource>
             <kendo:chart-series>
                <kendo:chart-seriesItem type="line" field="temperature" categoryField="day">
                    <kendo:chart-seriesItem-highlight toggle="toggleHandler"/>
                    <kendo:chart-seriesItem-markers size="32" visual="markerVisual"/>
                </kendo:chart-seriesItem>
             </kendo:chart-series>
             <kendo:chart-categoryAxis>
                <kendo:chart-categoryAxisItem>
               		<kendo:chart-categoryAxisItem-majorGridLines visible="false"/>
                </kendo:chart-categoryAxisItem>
             </kendo:chart-categoryAxis>
             <kendo:chart-valueAxis>
                <kendo:chart-valueAxisItem>
                    <kendo:chart-valueAxisItem-labels template="#:value#℃" />
                </kendo:chart-valueAxisItem>
             </kendo:chart-valueAxis>
             <kendo:chart-tooltip template="#= category #: #= value #℃"/>
         </kendo:chart>
     </div>
     
     <script>
	     function toggleHandler(e) {
	         e.preventDefault();
	         var visual = e.visual;
	         var transform = null;
	         if (e.show) {
	             transform = kendo.geometry.transform().scale(1.5, 1.5, visual.rect().center());
	         }
	         visual.transform(transform);
	     }
	     
	     function markerVisual(e) {
	         var src = kendo.format('<c:url value="/resources/dataviz/chart/images/{0}.png" />', e.dataItem.weather);
	         var image = new kendo.drawing.Image(src, e.rect);
	         return image;
	     }     
     </script>
     
<demo:footer />
