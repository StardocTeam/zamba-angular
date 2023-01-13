<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.Date"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />
    <script>
    function isInArray(date, dates) {
    	for(var idx = 0, length = dates.length; idx < length; idx++) {
    		if (+date === +dates[idx]) {
    			return true;
    		}
    	}
    	
    	return false;
    }
    </script>
        <%
        String template = "# if (isInArray(+data.date, data.dates)) { #" +
                          "<div class='" +
                             "# if (data.value < 10) { #" +
                                 "exhibition" +
                             "# } else if ( data.value < 20 ) { #" +
                                 "party" +
                             "# } else { #" +
                                 "cocktail" +
                             "# } #" +
                          "'>#= data.value #</div>" +
                       "# } else { #" +
                       "#= data.value #" +
                       "# } #";
         String weekNumberTemplate = "<a class='italic'>#= data.weekNumber #</a>";
           
        %>
         <div class="demo-section k-content">
            <div id="special-days">
				<kendo:calendar name="calendar" weekNumber="true" value="<%= new Date()%>" dates="${dates}" footer="<#= false#>">
					<kendo:calendar-month weekNumber="<%=weekNumberTemplate%>" content="<%=template%>"/>
				</kendo:calendar>
            </div>
        </div>
             
<style>

    #calendar {
         width: 100%;
    }

    /* Template Days */

    .exhibition,
    .party,
    .cocktail {
        font-weight: bold;
    }

    .exhibition {
        color: #ff9e00;
    }

    .party {
        color: #ff4081;
    }

    .cocktail {
        color: #00a1e8;
    }
    .italic {
        font-style: italic;
    }

</style>
<demo:footer />