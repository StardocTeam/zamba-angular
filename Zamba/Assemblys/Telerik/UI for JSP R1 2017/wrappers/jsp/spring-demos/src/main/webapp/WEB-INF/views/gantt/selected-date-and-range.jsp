<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.HashMap"%>
<%@page import="java.util.ArrayList"%>
<%@page import="java.util.Date"%>
<%@page import="java.text.SimpleDateFormat"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<%
	Date dayStartDate = new SimpleDateFormat("yyyy/MM/dd").parse("2014/6/2");
	Date dayEndDate = new SimpleDateFormat("yyyy/MM/dd").parse("2014/6/8");
	Date weekStartDate = new SimpleDateFormat("yyyy/MM/dd").parse("2014/6/1");
	Date weekEndDate = new SimpleDateFormat("yyyy/MM/dd").parse("2014/7/13");
	Date monthStartDate = new SimpleDateFormat("yyyy/MM/dd").parse("2014/5/18");
	Date monthEndDate = new SimpleDateFormat("yyyy/MM/dd").parse("2014/8/3");
%>

<c:url value="/gantt/tasks/read" var="tasksReadUrl" />

<c:url value="/gantt/dependencies/read" var="dependencyReadUrl" />

<demo:header />

<div class="box wide">
    <div class="box-col">
        <h4>Set Visible Range</h4>
        <p>Start date of range</p>
        <kendo:datePicker name="start-range" value="<%= weekStartDate %>" disableDates="startDisabledDatesHandler" open="openHandler" change="changeStartHandler" >
		</kendo:datePicker>
        <br />
        <br />
        <p>End date of range</p>
        <kendo:datePicker name="end-range" value="<%= weekEndDate %>" disableDates="endDisabledDatesHandler" open="openHandler" change="changeEndHandler" >
		</kendo:datePicker>
    </div>
    <div class="box-col">
        <h4>Set Selected Date</h4>
        <br />
        <kendo:datePicker name="selected-date" value="<%= weekStartDate %>" disableDates="dateDisabledDatesHandler" open="openHandler" change="changeDateHandler" >
		</kendo:datePicker>
    </div>
</div>

    <kendo:gantt name="gantt" height="700" showWorkDays="false" showWorkHours="false" snap="false" editable="false" navigate="onNavigate">
    	<kendo:gantt-views>
    		<kendo:gantt-view type="day" date="<%= dayStartDate %>" >
    			<kendo:gantt-view-range start="<%= dayStartDate %>" end="<%= dayEndDate %>"/>
    		</kendo:gantt-view>
    		<kendo:gantt-view type="week" selected="true" date="<%= weekStartDate %>" >
    			<kendo:gantt-view-range start="<%= weekStartDate %>" end="<%= weekEndDate %>"/>
    		</kendo:gantt-view>
    		<kendo:gantt-view type="month"  date="<%= monthStartDate %>" >
    			<kendo:gantt-view-range start="<%= monthStartDate %>" end="<%= monthEndDate %>"/>
    		</kendo:gantt-view>
    	</kendo:gantt-views>
    	
    	<kendo:gantt-columns>
    		<kendo:gantt-column field="id" title="ID" width="50" />
    		<kendo:gantt-column field="title" title="Title" editable="true" />
    		<kendo:gantt-column field="start" title="Start Time" format="{0:MM/dd/yyyy}" width="100" />
    		<kendo:gantt-column field="end" title="End Time" format="{0:MM/dd/yyyy}" width="100" />
    	</kendo:gantt-columns>
    	
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
                <kendo:dataSource-transport-read url="${tasksReadUrl}" dataType="json" type="POST" contentType="application/json" />
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
                <kendo:dataSource-transport-read url="${dependencyReadUrl}" dataType="json" type="POST" contentType="application/json" />
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
    
    <script>
    var gantt, startRange, endRange, date;

    function onNavigate(e) {
        var viewsOptions = e.sender.options.views;
        viewsOptions.forEach(function (view) {
            if (view.type === e.view) {
                startRange.value(view.range.start);
                endRange.value(view.range.end);
                date.value(view.date);
                return;
            }
        });
    }

    function openHandler(e) {
        e.sender.setOptions(e.sender.options);
    };

    function startDisabledDatesHandler(date) {
        var end = endRange ? endRange.value() : new Date("2014/7/13");

        if (date >= end) {
            return true;
        } else {
            return false;
        }
    };

    function changeStartHandler(e) {
        var range = gantt.range();
        range.start = this.value();
        gantt.range(range);
        if (this.value() > date.value()) {
            date.value("");
        }
    };

    function endDisabledDatesHandler(date) {
        var start = startRange ? startRange.value() : new Date("2014/6/1");

        if (date <= start) {
            return true;
        } else {
            return false;
        }
    };

    function changeEndHandler(e) {
        var range = gantt.range();
        range.end = this.value();
        gantt.range(range);
        if (this.value() <= date.value()) {
            date.value("");
        }
    };

    function dateDisabledDatesHandler(date) {
        var start = startRange ? startRange.value() : new Date("2014/6/1");
        var end = endRange ? endRange.value() : new Date("2014/7/13");

        if (date < start || date >= end) {
            return true;
        } else {
            return false;
        }
    };

    function changeDateHandler(e) {
        gantt.date(this.value());
    };

    $(document).ready(function () {
        gantt = $('#gantt').data('kendoGantt');
        startRange = $('#start-range').data('kendoDatePicker');
        endRange = $('#end-range').data('kendoDatePicker');
        date = $('#selected-date').data('kendoDatePicker');
    });
    </script>
    
<demo:footer />