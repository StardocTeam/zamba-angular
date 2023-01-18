<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />

<c:url value="/resources/dataviz/map/countries-users.geo.json" var="readUrl" />
<c:url value="/resources/dataviz/map/js/chroma.min.js" var="chroma" />
<c:url value="/resources/shared/js/pako.min.js" var="pako" />
<c:url value="/map/export/save" var="saveUrl" />

<div class="box wide">
    <h4>Export options</h4>
    <div class="box-col">
        <button class='export-pdf k-button'>Export as PDF</button>
    </div>
    <div class="box-col">
        <button class='export-img k-button'>Export as Image</button>
    </div>
    <div class="box-col">
        <button class='export-svg k-button'>Export as SVG</button>
    </div>
</div>


<kendo:map name="map" center="<%= new double[] {30.2681, -97.7448} %>" zoom="3"
           shapeCreated="onShapeCreated"
           shapeMouseEnter="onShapeMouseEnter"
           shapeMouseLeave="onShapeMouseLeave">
    <kendo:map-layers>
        <kendo:map-layer type="shape">
            <kendo:dataSource type="geojson">
                <kendo:dataSource-transport>
                    <kendo:dataSource-transport-read url="${readUrl}" />
                </kendo:dataSource-transport>
            </kendo:dataSource>
            <kendo:map-layer-style>
                <kendo:map-layer-style-fill opacity="0.7"/>
            </kendo:map-layer-style>
        </kendo:map-layer>
    </kendo:map-layers>
</kendo:map>


<!-- Chroma.js used to colorize the map in the demo -->
<script src="${chroma}"></script>

<!-- Load Pako ZLIB library to enable PDF compression -->
<script src="${pako}"></script>

<script>
$(".export-pdf").click(function() {
    // Convert the DOM element to a drawing using kendo.drawing.drawDOM
    kendo.drawing.drawDOM($("#map"))
    .then(function(group) {
        // Render the result as a PDF file
        return kendo.drawing.exportPDF(group, {
            paperSize: "auto",
            margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
        });
    })
    .done(function(data) {
        // Save the PDF file
        kendo.saveAs({
            dataURI: data,
            fileName: "Map.pdf",
            proxyURL: "${saveUrl}"
        });
    });
});

$(".export-img").click(function() {
    // Convert the DOM element to a drawing using kendo.drawing.drawDOM
    kendo.drawing.drawDOM($("#map"))
    .then(function(group) {
        // Render the result as a PNG image
        return kendo.drawing.exportImage(group);
    })
    .done(function(data) {
        // Save the image file
        kendo.saveAs({
            dataURI: data,
            fileName: "Map.png",
            proxyURL: "${saveUrl}"
        });
    });
});

$(".export-svg").click(function() {
    // Convert the DOM element to a drawing using kendo.drawing.drawDOM
    kendo.drawing.drawDOM($("#map"))
    .then(function(group) {
        // Render the result as a SVG document
        return kendo.drawing.exportSVG(group);
    })
    .done(function(data) {
        // Save the SVG document
        kendo.saveAs({
            dataURI: data,
            fileName: "Map.svg",
            proxyURL: "${saveUrl}"
        });
    });
});

var scale = chroma
.scale(["white", "green"])
.domain([1, 1000]);

function onShapeCreated(e) {
    var shape = e.shape;
    var users = shape.dataItem.properties.users;
    if (users) {
        var color = scale(users).hex();
        shape.options.fill.set("color", color);
    }
}

function onShapeMouseEnter(e) {
    e.shape.options.set("fill.opacity", 1);
}

function onShapeMouseLeave(e) {
    e.shape.options.set("fill.opacity", 0.7);
}

</script>

<demo:footer />
