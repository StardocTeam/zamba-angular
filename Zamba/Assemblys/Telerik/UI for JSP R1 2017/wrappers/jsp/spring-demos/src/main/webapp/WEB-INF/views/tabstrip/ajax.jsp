<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<c:url value="/tabstrip/content/1" var="ajaxContent1" />
<c:url value="/tabstrip/content/2" var="ajaxContent2" />
<c:url value="/tabstrip/content/3" var="ajaxContent3" />

<div class="wrapper">
	<kendo:tabStrip name="tabstrip">
		<kendo:tabStrip-items>
		    <kendo:tabStrip-item text="Dimensions & Weights" selected="true" contentUrl="${ajaxContent1}"></kendo:tabStrip-item>
	        <kendo:tabStrip-item text="Engine" contentUrl="${ajaxContent2}"></kendo:tabStrip-item>
	        <kendo:tabStrip-item text="Chassis" contentUrl="${ajaxContent3}"></kendo:tabStrip-item>
	    </kendo:tabStrip-items>
	</kendo:tabStrip>
</div>

<style>
    .wrapper {
        height: 455px;
        margin: 20px auto;
        padding: 20px 0 0 0;
        background: url('<c:url value="/resources/web/tabstrip/bmw.png" />') no-repeat center 60px transparent;
    }
    #tabstrip {
        max-width: 400px;
        float: right;
        margin-bottom: 20px;
    }
    #tabstrip .k-content
    {
        height: 320px;
        overflow: auto;
    }
    .specification {
        max-width: 670px;
        margin: 10px 0;
        padding: 0;
    }
    .specification dt, dd {
        max-width: 140px;
        float: left;
        margin: 0;
        padding: 5px 0 8px 0;
    }
    .specification dt {
        clear: left;
        width: 100px;
        margin-right: 7px;
        padding-right: 0;
        opacity: 0.7;
    }
    .specification:after, .wrapper:after {
        content: ".";
        display: block;
        clear: both;
        height: 0;
        visibility: hidden;
    }
</style>

<demo:footer />