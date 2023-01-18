<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />

<c:url value="/resources/dataviz/map/us.geo.json" var="readUrl" />

<kendo:map name="map" style="height: 300px" center="<%= new double[] {39.6924, -97.3370} %>" zoom="4"
    click="onClick"
    reset="onReset"
    pan="onPan"
    panEnd="onPanEnd"
    shapeClick="onShapeClick"
    shapeCreated="onShapeCreated"
    shapeFeatureCreated="onShapeFeatureCreated"
    shapeMouseEnter="onShapeMouseEnter"
    shapeMouseLeave="onShapeMouseLeave"
    zoomStart="onZoomStart"
    zoomEnd="onZoomEnd">
    <kendo:map-layers>
        <kendo:map-layer type="shape">
            <kendo:dataSource type="geojson">
                <kendo:dataSource-transport>
                    <kendo:dataSource-transport-read url="${readUrl}" />
                </kendo:dataSource-transport>
            </kendo:dataSource>
            <kendo:map-layer-style>
                <kendo:map-layer-style-stroke color="#ccebc5"/>
                <kendo:map-layer-style-fill color="#b3cde3"/>
            </kendo:map-layer-style>
        </kendo:map-layer>
    </kendo:map-layers>
</kendo:map>

<div class="box wide">
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<script>
function onClick(e) {
    kendoConsole.log("Click at ...");
}

function onReset(e) {
    kendoConsole.log("Reset");
}

function onPan(e) {
    kendoConsole.log("Pan");
}

function onPanEnd(e) {
    kendoConsole.log("Pan end");
}

function onShapeClick(e) {
    kendoConsole.log(kendo.format(
        "Shape click :: {0}", e.shape.dataItem.properties.name
    ));
}

function onShapeCreated(e) {
    kendoConsole.log(kendo.format(
        "Shape created :: {0}", e.shape.dataItem.properties.name
    ));
}

function onShapeFeatureCreated(e) {
    kendoConsole.log(kendo.format(
    "Feature created :: {0} with {1} child shape(s)",
        e.properties.name,
        e.group.children.length
    ));
}

function onShapeMouseEnter(e) {
    kendoConsole.log(kendo.format(
        "Shape mouseEnter :: {0}", e.shape.dataItem.properties.name
    ));
}

function onShapeMouseLeave(e) {
    kendoConsole.log(kendo.format(
        "Shape mouseLeave :: {0}", e.shape.dataItem.properties.name
    ));
}

function onZoomStart(e) {
    kendoConsole.log("Zoom start");
}

function onZoomEnd(e) {
    kendoConsole.log("Zoom end");
}

</script>

<demo:footer />
