<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
    <div class="box wide">
        <p>Use SHIFT + Mouse Drag Region Selection combination on mouse-enabled devices to zoom in data for a specific period of time</p>
        <p>Use the mousewheel to zoom in and out</p>
    </div>
    <div class="demo-section k-content wide">
         <kendo:chart name="chart">
             <kendo:dataSource data="${data}" />
             <kendo:chart-series>
                <kendo:chart-seriesItem type="column" field="value" categoryField="category" />
             </kendo:chart-series>
             <kendo:chart-categoryAxis>
                <kendo:chart-categoryAxisItem min="0" max="10">
                    <kendo:chart-categoryAxisItem-labels rotation="auto"/>
                </kendo:chart-categoryAxisItem>
             </kendo:chart-categoryAxis>
             <kendo:chart-pannable lock="y"/>
             <kendo:chart-zoomable>
                <kendo:chart-zoomable-mousewheel lock="y"/>
                <kendo:chart-zoomable-selection lock="y"/>
             </kendo:chart-zoomable>
         </kendo:chart>
     </div>
<demo:footer />
