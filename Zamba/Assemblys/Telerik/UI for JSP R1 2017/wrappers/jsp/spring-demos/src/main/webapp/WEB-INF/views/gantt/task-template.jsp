<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.HashMap"%>
<%@page import="java.util.ArrayList"%>
<%@page import="java.util.Date"%>
<%@page import="java.text.SimpleDateFormat"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>


<c:url value="/gantt/tasks/read" var="tasksReadUrl" />
<c:url value="/gantt/tasks/create" var="tasksCreateUrl" />
<c:url value="/gantt/tasks/update" var="tasksUpdateUrl" />
<c:url value="/gantt/tasks/destroy" var="tasksDestroyUrl" />

<c:url value="/gantt/dependencies/read" var="dependencyReadUrl" />
<c:url value="/gantt/dependencies/create" var="dependencyCreateUrl" />
<c:url value="/gantt/dependencies/update" var="dependencyUpdateUrl" />
<c:url value="/gantt/dependencies/destroy" var="dependencyDestroyUrl" />

<c:url value="/gantt/resources/read" var="resourceReadUrl" />

<c:url value="/gantt/assignments/read" var="assignmentReadUrl" />
<c:url value="/gantt/assignments/create" var="assignmentCreateUrl" />
<c:url value="/gantt/assignments/update" var="assignmentUpdateUrl" />
<c:url value="/gantt/assignments/destroy" var="assignmentDestroyUrl" />

<demo:header />

    <script id="task-template" type="text/x-kendo-template">
        # if (resources[0]) { #
        <div class="template" style="background-color: #= resources[0].color #;">
            <img class="resource-img" src="../resources/web/gantt/resources/#:resources[0].id#.jpg" alt="#: resources[0].id #" />
            <div class="wrapper">
                <strong class="title">#= title # </strong>
                <span class="resource">#= resources[0].name #</span>
            </div>
            <div class="progress" style="width:#= (100 * parseFloat(percentComplete)) #%"> </div>
        </div>
        # } else { #
        <div class="template">
            <div class="wrapper">
                <strong class="title">#= title # </strong>
                <span class="resource">no resource assigned</span>
            </div>
            <div class="progress" style="width:#= (100 * parseFloat(percentComplete)) #%"> </div>
        </div>
        # } #
    </script>
        
    <kendo:gantt name="gantt" height="700" rowHeight="62" showWorkDays="false" taskTemplate="task-template" showWorkHours="false" snap="false">
    	<kendo:gantt-views>
    		<kendo:gantt-view type="day" />
    		<kendo:gantt-view type="week" selected="true" />
    	</kendo:gantt-views>
    	
    	<kendo:gantt-columns>
    		<kendo:gantt-column field="title" title="Title" editable="true" sortable="true" width="200" />
    		<kendo:gantt-column field="resources" title="Assigned Resources" editable="true" />
    	</kendo:gantt-columns>
    	
    	<kendo:gantt-resources field="resources" dataColorField="color" dataTextField="name">
    	 	<kendo:dataSource batch="false">
    	 		<kendo:dataSource-schema>
	                <kendo:dataSource-schema-model id="id">
	                     <kendo:dataSource-schema-model-fields>
	                         <kendo:dataSource-schema-model-field name="id" type="number" />
	                    </kendo:dataSource-schema-model-fields>
	                </kendo:dataSource-schema-model>
            	</kendo:dataSource-schema>
            	<kendo:dataSource-transport>
                	<kendo:dataSource-transport-read url="${resourceReadUrl}" dataType="json" type="POST" contentType="application/json" />
                </kendo:dataSource-transport>
    	 	</kendo:dataSource>
    	</kendo:gantt-resources>
    	
    	<kendo:gantt-assignments dataTaskIdField="taskId" dataResourceIdField="resourceId" dataValueField="units" >
    		<kendo:dataSource batch="false">
    	 		<kendo:dataSource-schema>
	                <kendo:dataSource-schema-model id="id">
	                     <kendo:dataSource-schema-model-fields>
	                         <kendo:dataSource-schema-model-field name="id" type="number" />
	                         <kendo:dataSource-schema-model-field name="resourceId" type="number" />
	                         <kendo:dataSource-schema-model-field name="taskId" type="number" />
	                         <kendo:dataSource-schema-model-field name="units" type="number" />
	                    </kendo:dataSource-schema-model-fields>
	                </kendo:dataSource-schema-model>
            	</kendo:dataSource-schema>
            	<kendo:dataSource-transport>
                	<kendo:dataSource-transport-create url="${assignmentCreateUrl}" dataType="json" type="POST" contentType="application/json" />
	                <kendo:dataSource-transport-read url="${assignmentReadUrl}" dataType="json" type="POST" contentType="application/json" />
	                <kendo:dataSource-transport-update url="${assignmentUpdateUrl}" dataType="json" type="POST" contentType="application/json" />
	                <kendo:dataSource-transport-destroy url="${assignmentDestroyUrl}" dataType="json" type="POST" contentType="application/json" />
	                <kendo:dataSource-transport-parameterMap>
	                	<script>
		                	function parameterMap(options, type) {
	                			return JSON.stringify(options.models || [ options ]);
		                	}
	                	</script>
	                </kendo:dataSource-transport-parameterMap>    
                </kendo:dataSource-transport>  
           	</kendo:dataSource>  	
    	</kendo:gantt-assignments>
    	
        <kendo:dataSource batch="false">
             <kendo:dataSource-schema>
                <kendo:dataSource-schema-model id="id">
                     <kendo:dataSource-schema-model-fields>
                         <kendo:dataSource-schema-model-field name="id" type="number" />
                         <kendo:dataSource-schema-model-field name="orderId" type="number" />
                         <kendo:dataSource-schema-model-field name="parentId" defaultValue="null" nullable="true" type="number" />
                         <kendo:dataSource-schema-model-field name="start" type="date" />
                         <kendo:dataSource-schema-model-field name="end" type="date" />
                         <kendo:dataSource-schema-model-field name="title" defaultValue="No title" type="string" />
                         <kendo:dataSource-schema-model-field name="percentComplete" type="number" />
                         <kendo:dataSource-schema-model-field name="expanded" type="boolean" defaultValue="true" />
                         <kendo:dataSource-schema-model-field name="summary" type="boolean" />
                    </kendo:dataSource-schema-model-fields>
                </kendo:dataSource-schema-model>
            </kendo:dataSource-schema>
            <kendo:dataSource-transport>
                <kendo:dataSource-transport-create url="${tasksCreateUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-read url="${tasksReadUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-update url="${tasksUpdateUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-destroy url="${tasksDestroyUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-parameterMap>
                	<script>
	                	function parameterMap(options, type) {
                			return JSON.stringify(options.models || [ options ]);
	                	}
                	</script>
                </kendo:dataSource-transport-parameterMap>              
            </kendo:dataSource-transport>
        </kendo:dataSource>
    	
        <kendo:dependencies batch="false">
             <kendo:dataSource-schema>
                <kendo:dataSource-schema-model id="id">
                     <kendo:dataSource-schema-model-fields>
                         <kendo:dataSource-schema-model-field name="id" type="number" />
                         <kendo:dataSource-schema-model-field name="predecessorId" type="number" />
                         <kendo:dataSource-schema-model-field name="successorId" type="number" />
                         <kendo:dataSource-schema-model-field name="type" type="number" />
                    </kendo:dataSource-schema-model-fields>
                </kendo:dataSource-schema-model>
            </kendo:dataSource-schema>
            <kendo:dataSource-transport>
                <kendo:dataSource-transport-create url="${dependencyCreateUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-read url="${dependencyReadUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-update url="${dependencyUpdateUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-destroy url="${dependencyDestroyUrl}" dataType="json" type="POST" contentType="application/json" />
                <kendo:dataSource-transport-parameterMap>
                	<script>
	                	function parameterMap(options, type) { 
                			return JSON.stringify(options.models || [ options ]);
	                	}
                	</script>
                </kendo:dataSource-transport-parameterMap>              
            </kendo:dataSource-transport>
        </kendo:dependencies>
    </kendo:gantt>
    
    <style type="text/css">

        /*center treelist cell content vertically*/
        .k-gantt .k-treelist td
        {
            vertical-align: middle;
        }

        /*hide the resource labels, as they are present in the task template*/
        .k-gantt .k-resource
        {
            display: none;
        }

        /*style the task template*/
        .k-task-template {
            height: 100%;
            padding: 0 !important;
        }

        .template {
            height: 100%;
            overflow: hidden;
        }

        .resource-img {
            float: left;
            width: 32px;
            height: 32px;
            border-radius: 50%;
            margin: 8px;
        }

        .wrapper {
            padding: 8px;
            color: #fff;
        }

        .k-task-template .wrapper > * {
            display: block;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .title {
            font-weight: bold;
            font-size: 13px;
        }

        .resource {
            text-transform: uppercase;
            font-size: 9px;
            margin-top: .5em;
        }

        .progress
        {
            position: absolute;
            left: 0;
            bottom: 0;
            width: 0%;
            height: 4px;
            background: rgba(0, 0, 0, .3);
        }
    </style>
    
<demo:footer />