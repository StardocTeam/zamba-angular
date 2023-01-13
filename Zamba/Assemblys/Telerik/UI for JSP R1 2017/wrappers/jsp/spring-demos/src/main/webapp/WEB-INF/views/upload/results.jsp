<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">
<h4>Uploaded files</h4>

<c:if test="${!files.isEmpty()}">
	<ul>
	    <c:forEach var="file" items="${files}">
         <li>
            ${file.getOriginalFilename()} (${file.getSize()} bytes)
         </li>
	    </c:forEach>
	</ul>
</c:if>

<c:if test="${files.isEmpty()}">
-- None --
</c:if>

<p style="margin-top: 1em;">
    <a href="<c:url value='/upload/' />" class="k-button k-primary">Go back</a>
</p>
</div>
<demo:footer />
