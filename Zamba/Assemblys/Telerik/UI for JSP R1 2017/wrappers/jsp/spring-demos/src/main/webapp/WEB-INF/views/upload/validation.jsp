<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<c:url value="/upload/validation/save" var="saveUrl" />
<c:url value="/upload/validation/remove" var="removeUrl" />
<% String[] pdfExtensions = {".pdf"}; %>
<% String[] imageExtensions = {".gif", ".jpg", ".png"}; %>
<div class="demo-section k-content">
	<h4>Upload PDF</h4>
	    <kendo:upload name="files-pdf">
	        <kendo:upload-async saveField="files" autoUpload="true" saveUrl="${saveUrl}" removeUrl="${removeUrl}"/>
	        <kendo:upload-validation allowedExtensions="<%= pdfExtensions %>" />
	    </kendo:upload>
    <div class="demo-hint">You can only upload <strong>PDF</strong> files.</div>
    <h4 style="margin-top: 2em;">Upload Image</h4>
	    <kendo:upload name="files-images">
	        <kendo:upload-async saveField="files" autoUpload="true" saveUrl="${saveUrl}" removeUrl="${removeUrl}"/>
	        <kendo:upload-validation allowedExtensions="<%= imageExtensions %>" />
	    </kendo:upload>
    <div class="demo-hint">You can only upload <strong>GIF</strong>, <strong>JPG</strong>, <strong>PNG</strong> files.</div>
</div>
<div class="demo-section k-content">
   <h4>Upload Documents</h4>
   <kendo:upload name="files-documents">
        <kendo:upload-async saveField="files" autoUpload="true" saveUrl="${saveUrl}" removeUrl="${removeUrl}"/>
        <kendo:upload-validation maxFileSize="4194304" />
    </kendo:upload>
   	<div class="demo-hint">Maximum allowed file size is <strong>4MB</strong>.</div>
</div>

<demo:footer />
