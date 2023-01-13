<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
      <div class="demo-section k-content wide">
         <% 
             String[] categories = {
        		 "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
             };
         %>
         <kendo:chart name="chart">
             <kendo:chart-title text="Site Visitors Stats /thousands/" />
             <kendo:chart-legend position="bottom">
             	<kendo:chart-legend-item visual="createLegendItem"/>
             </kendo:chart-legend>
             <kendo:chart-seriesDefaults type="column" stack="true" visual="columnVisual" />
             <kendo:chart-series>
                <kendo:chart-seriesItem data="<%= new double[] { 56000, 63000, 74000, 91000, 117000, 138000, 128000, 115000, 102000, 98000, 123000, 113000 } %>" name="Total Visits">
                	<kendo:chart-seriesItem-highlight toggle="toggleHandler"/>
                </kendo:chart-seriesItem>
                <kendo:chart-seriesItem data="<%= new double[] { 52000, 34000, 23000, 48000, 67000, 83000, 40000, 50000, 64000, 72000, 75000, 81000 } %>" name="Unique visitors">
                	<kendo:chart-seriesItem-highlight toggle="toggleHandler"/>
                </kendo:chart-seriesItem>
             </kendo:chart-series>
             <kendo:chart-panes>
             	<kendo:chart-pane clip="false"/>
             </kendo:chart-panes>
             <kendo:chart-categoryAxis>
                <kendo:chart-categoryAxisItem categories="<%= categories %>">
                	<kendo:chart-categoryAxisItem-majorGridLines visible="false"/>
                	<kendo:chart-categoryAxisItem-line visible="false"/>
                </kendo:chart-categoryAxisItem>
             </kendo:chart-categoryAxis>
             <kendo:chart-valueAxis>
                <kendo:chart-valueAxisItem>
                    <kendo:chart-valueAxisItem-line visible="false"/>
                </kendo:chart-valueAxisItem>
             </kendo:chart-valueAxis>
             <kendo:chart-tooltip visible="true" />
         </kendo:chart>
     </div>
	 <script>
	    var drawing = kendo.drawing;
	    var geometry = kendo.geometry;

	    function columnVisual(e) {
	        return createColumn(e.rect, e.options.color);
	    }

	    function toggleHandler(e) {
	        e.preventDefault();
	        var visual = e.visual;
	        var opacity = e.show ? 0.8 : 1;

	        visual.opacity(opacity);
	    }

	    function createLegendItem(e) {
	        var color = e.options.markers.background;
	        var labelColor = e.options.labels.color;
	        var rect = new geometry.Rect([0, 0], [120, 50]);
	        var layout = new drawing.Layout(rect, {
	            spacing: 5,
	            alignItems: "center"
	        });

	        var overlay = drawing.Path.fromRect(rect, {
	            fill: {
	                color: "#fff",
	                opacity: 0
	            },
	            stroke: {
	                color: "none"
	            },
	            cursor: "pointer"
	        });

	        var column = createColumn(new geometry.Rect([0, 0], [15, 10]), color);
	        var label = new drawing.Text(e.series.name, [0, 0], {
	            fill: {
	                color: labelColor
	            }
	        })

	        layout.append(column, label);
	        layout.reflow();

	        var group = new drawing.Group().append(layout, overlay);

	        return group;
	    }

	    function createColumn(rect, color) {
	        var origin = rect.origin;
	        var center = rect.center();
	        var bottomRight = rect.bottomRight();
	        var radiusX = rect.width() / 2;
	        var radiusY = radiusX / 3;
	        var gradient = new drawing.LinearGradient({
	            stops: [{
	                offset: 0,
	                color: color
	            }, {
	                offset: 0.5,
	                color: color,
	                opacity: 0.9
	            }, {
	                offset: 0.5,
	                color: color,
	                opacity: 0.9
	            }, {
	                offset: 1,
	                color: color
	            }]
	        });

	        var path = new drawing.Path({
	            fill: gradient,
	            stroke: {
	                color: "none"
	            }
	        }).moveTo(origin.x, origin.y)
	            .lineTo(origin.x, bottomRight.y)
	            .arc(180, 0, radiusX, radiusY, true)
	            .lineTo(bottomRight.x, origin.y)
	            .arc(0, 180, radiusX, radiusY);

	        var topArcGeometry = new geometry.Arc([center.x, origin.y], {
	            startAngle: 0,
	            endAngle: 360,
	            radiusX: radiusX,
	            radiusY: radiusY
	        });

	        var topArc = new drawing.Arc(topArcGeometry, {
	            fill: {
	                color: color
	            },
	            stroke: {
	                color: "#ebebeb"
	            }
	        });
	        var group = new drawing.Group();
	        group.append(path, topArc);
	        return group;
	    }	 	
	 </script>
<demo:footer />
