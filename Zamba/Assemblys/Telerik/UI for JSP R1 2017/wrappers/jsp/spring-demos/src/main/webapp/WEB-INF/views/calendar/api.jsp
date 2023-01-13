<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.Calendar"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />

<div class="box wide">
    <div class="box-col">
        <h4>Get value</h4>
        <ul class="options">
            <li>
                <button id="get" class="k-button">Get date</button>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Set value</h4>
        <ul class="options">
            <li>
                <input id="value" value="10/10/2000" />
                <button id="set" class="k-button">Set date</button>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Navigation</h4>
        <ul class="options">
            <li>
                <select id="direction">
                    <option value="up">upper view</option>
                    <option value="down">lower view</option>
                    <option value="past">past</option>
                    <option value="future" selected="selected">future</option>
                </select>
                <button id="navigate" class="k-button">Navigate</button>
            </li>
        </ul>
    </div>
</div>
<div class="demo-section k-content" style="text-align: center;">
    <h4>Pick a date</h4>
	<kendo:calendar name="calendar" value="<%= Calendar.getInstance().getTime() %>">
	</kendo:calendar>
</div>
<script>
	$(document).ready(function() {
	    var calendar = $("#calendar").data("kendoCalendar"),
	    navigate = function () {
	        var value = $("#direction").val();
	        switch(value) {
	            case "up":
	                calendar.navigateUp();
	                break;
	            case "down":
	                calendar.navigateDown(calendar.value());
	                break;
	            case "past":
	                calendar.navigateToPast();
	                break;
	            default:
	                calendar.navigateToFuture();
	                break;
	        }
	    },
	    setValue = function () {
	        calendar.value($("#value").val());
	    };
	
	    $("#get").click(function() {
	        alert(calendar.value());
	    });
	
	    $("#value").kendoDatePicker({
	        change: setValue
	    });
	
	    $("#set").click(setValue);
	
	    $("#direction").kendoDropDownList({
	        change: navigate
	    });
	
	    $("#navigate").click(navigate);
	});
</script>

<demo:footer />