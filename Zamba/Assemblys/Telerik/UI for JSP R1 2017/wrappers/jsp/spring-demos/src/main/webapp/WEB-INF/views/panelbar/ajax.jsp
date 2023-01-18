<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
<c:url value="/panelbar/content/1" var="ajaxContent1" />
<c:url value="/panelbar/content/2" var="ajaxContent2" />
<c:url value="/panelbar/content/3" var="ajaxContent3" />
<c:url value="/panelbar/content/4" var="ajaxContent4" />
<c:url value="/resources/web/panelbar/astonmartin.png" var="astonmartin" />

<div class="wrapper">

	<kendo:panelBar name="panelbar" expandMode="single">				
		<kendo:panelBar-items>
			<kendo:panelBar-item text="BODY" contentUrl="${ ajaxContent1 }" />
			<kendo:panelBar-item text="ENGINE" contentUrl="${ ajaxContent2 }" />
			<kendo:panelBar-item text="TRANSMISSION" contentUrl="${ ajaxContent3 }" />
			<kendo:panelBar-item text="PERFORMANCE" contentUrl="${ ajaxContent4 }" />			
		</kendo:panelBar-items>
	</kendo:panelBar>
		
</div>

<style>
    .wrapper {
        height: 320px;
        margin: 20px auto;
        padding: 20px 0 0;
        background: url('${astonmartin}') no-repeat center 50px transparent;
    }
    #panelbar {
        width: 250px;
        float: right;
        margin-bottom: 20px;
    }
    #panelbar p {
        padding: 1em;
    }
</style>

<demo:footer />
