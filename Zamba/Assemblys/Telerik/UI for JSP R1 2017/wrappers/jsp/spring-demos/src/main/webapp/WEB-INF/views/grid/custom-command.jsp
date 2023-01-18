<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/grid/custom-command/read" var="transportReadUrl" />

<demo:header />

    <kendo:grid name="grid" height="400px" pageable="true">
        <kendo:grid-columns>
            <kendo:grid-column title="First Name" field="firstName" width="140px" />
            <kendo:grid-column title="Last Name" field="lastName" width="140px"  />
            <kendo:grid-column title="Title" field="title" />
            <kendo:grid-column width="180">
                <kendo:grid-column-command>
                   <kendo:grid-column-commandItem name="viewDetails" text="View Details">
                        <kendo:grid-column-commandItem-click>
                            <script>
                            function showDetails(e) {
                                var detailsTemplate = kendo.template($("#template").html());

                                e.preventDefault();

                                var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                                var wnd = $("#details").data("kendoWindow");

                                wnd.content(detailsTemplate(dataItem));
                                wnd.center().open();
                            }
                            </script>
                        </kendo:grid-column-commandItem-click>
                   </kendo:grid-column-commandItem>
                </kendo:grid-column-command>
            </kendo:grid-column>
        </kendo:grid-columns>
        <kendo:dataSource>
            <kendo:dataSource-schema data="data" total="total" />
            <kendo:dataSource-transport>
                <kendo:dataSource-transport-read url="${transportReadUrl}" type="POST"  contentType="application/json"/>
                <kendo:dataSource-transport-parameterMap>
	            	<script>
	             		function parameterMap(options) { 
	            			return JSON.stringify(options);
	             		}
	            	</script>
	            </kendo:dataSource-transport-parameterMap>
            </kendo:dataSource-transport>
        </kendo:dataSource>
    </kendo:grid>

    <script type="text/x-kendo-template" id="template">
    <div id="details-container">
        <h2>#= firstName # #= lastName #</h2>
        <em>#= title #</em>
        <dl>
            <dt>City: #= city #</dt>
            <dt>Address: #= address #</dt>
        </dl>
    </div>
    </script>

    <kendo:window name="details" modal="true" draggable="true" visible="false" />
<style type="text/css">
     #details-container
     {
         padding: 10px;
     }

     #details-container h2
     {
         margin: 0;
     }

     #details-container em
     {
         color: #8c8c8c;
     }

     #details-container dt
     {
         margin:0;
         display: inline;
     }
 </style>
<demo:footer />
