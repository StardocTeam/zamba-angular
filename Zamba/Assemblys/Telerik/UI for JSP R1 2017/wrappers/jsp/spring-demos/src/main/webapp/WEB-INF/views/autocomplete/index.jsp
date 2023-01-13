<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/resources/web/autocomplete/shipping.png" var="shippingImg"/>
<demo:header />
    <div class="demo-section k-content">
    <h4>Choose shipping countries:</h4>
		   
		<%
		String[] countries = {
		    "Albania",
		    "Andorra",
		    "Armenia",
		    "Austria",
		    "Azerbaijan",
		    "Belarus",
		    "Belgium",
		    "Bosnia & Herzegovina",
		    "Bulgaria",
		    "Croatia",
		    "Cyprus",
		    "Czech Republic",
		    "Denmark",
		    "Estonia",
		    "Finland",
		    "France",
		    "Georgia",
		    "Germany",
		    "Greece",
		    "Hungary",
		    "Iceland",
		    "Ireland",
		    "Italy",
		    "Kosovo",
		    "Latvia",
		    "Liechtenstein",
		    "Lithuania",
		    "Luxembourg",
		    "Macedonia",
		    "Malta",
		    "Moldova",
		    "Monaco",
		    "Montenegro",
		    "Netherlands",
		    "Norway",
		    "Poland",
		    "Portugal",
		    "Romania",
		    "Russia",
		    "San Marino",
		    "Serbia",
		    "Slovakia",
		    "Slovenia",
		    "Spain",
		    "Sweden",
		    "Switzerland",
		    "Turkey",
		    "Ukraine",
		    "United Kingdom",
		    "Vatican City"
		};
		%>
		<kendo:autoComplete name="countries" filter="startswith" placeholder="Select country..." separator=", ">
		    <kendo:dataSource data="<%=countries%>">
		    </kendo:dataSource>
		</kendo:autoComplete>
	    
		<div class="demo-hint">Start typing the name of an European country</div>
	    
    </div>
	<style>
		.k-autocomplete {
			width: 100%;
		}
	</style>
<demo:footer />