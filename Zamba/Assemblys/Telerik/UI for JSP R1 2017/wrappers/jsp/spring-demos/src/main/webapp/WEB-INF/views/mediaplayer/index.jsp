<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
<div class="demo-section k-content wide" style="width: 644px;">
	<kendo:mediaPlayer name="mediaPlayer" navigatable="true"
		autoPlay="true">
		<kendo:mediaPlayer-media
			title="Recap of Progress Ringing The Bell at Nasdaq (2016)"
			source="https://www.youtube.com/watch?v=tc3xhD24iTU" />
	</kendo:mediaPlayer>
</div>

<style>
.k-mediaplayer {
	width: 640px;
	height: 360px;
}
</style>
<demo:footer />