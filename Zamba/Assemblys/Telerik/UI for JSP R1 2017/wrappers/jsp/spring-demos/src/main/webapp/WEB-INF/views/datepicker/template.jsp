<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.Date"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />
	 
	
    <script>
	   var today = new Date(),
	   birthdays = [
	       +new Date(today.getFullYear(), today.getMonth(), 11),
	       +new Date(today.getFullYear(), today.getMonth() + 1, 6),
	       +new Date(today.getFullYear(), today.getMonth() + 1, 27),
	       +new Date(today.getFullYear(), today.getMonth() - 1, 3),
	       +new Date(today.getFullYear(), today.getMonth() - 2, 22)
	   ];

	   function isInArray(date, dates) {
	    	for(var idx = 0, length = dates.length; idx < length; idx++) {
	    		if (+date === +dates[idx]) {
	    			return true;
	    		}
	    	}
	    	
	    	return false;
	    }

	    function onOpen() {
	       var dateViewCalendar = this.dateView.calendar;
	       if (dateViewCalendar) {
	            dateViewCalendar.element.width(340);
	        }
		}
    </script>
        <%
        String template = "# if (isInArray(+data.date, data.dates)) { #" +
                         "<div class=\"birthday\"></div>" +
                     "# } #" +
                     "#= data.value #";
    
    String footer = "Today - #=kendo.toString(data, 'd') #";
    
    String weekNumberTemplate = "<a class='italic'>#= data.weekNumber #</a>";
    %>
     <div class="demo-section k-content">
        <h4>Birthday Calendar</h4>
        <kendo:datePicker name="datepicker" weekNumber="true" value="<%=new Date()%>" dates="${dates}" footer="<%=footer%>" open="onOpen" style="width: 100%;">
            <kendo:datePicker-month content="<%=template%>" weekNumber="<%=weekNumberTemplate%>"/>
        </kendo:datePicker>
    </div>
	
<style>
     .birthday {
         background: transparent url(../content/web/calendar/cake.png) no-repeat 0 50%;
         display: inline-block;
         width: 16px;
         height: 16px;
         vertical-align: middle;
         margin-right: 3px;
     }
     .italic {
        font-style: italic;
      }
 </style>
<demo:footer />