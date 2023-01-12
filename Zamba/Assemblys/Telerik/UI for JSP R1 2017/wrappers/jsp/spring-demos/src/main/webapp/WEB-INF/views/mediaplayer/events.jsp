<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
<div class="demo-section k-content wide" style="width: 644px;">
	<kendo:mediaPlayer name="mediaPlayer" autoPlay="true" play="onPlay"
		pause="onPause" end="onEnd" ready="onReady" timeChange="onTimeChange"
		volumeChange="onVolumeChange">
		<kendo:mediaPlayer-media title="Take a Tour of the Telerik Platform"
			source="https://www.youtube.com/watch?v=rLtTuFbuf1c" />
	</kendo:mediaPlayer>
</div>
<div class="box wide" style="max-width: 644px; text-align: center;">
	<h4 class="title">Events log</h4>
	<div class="console"></div>
</div>

<script>
	function onPlay() {
		kendoConsole.log("event :: play");
	}

	function onPause() {
		kendoConsole.log("event :: pause");
	}

	function onEnd() {
		kendoConsole.log("event :: end");
	}

	function onReady() {
		kendoConsole.log("event :: ready");
	}

	function onTimeChange() {
		kendoConsole.log("event :: timeChange");
	}

	function onVolumeChange() {
		kendoConsole.log("event :: volumeChange");
	}
</script>
<style>
.k-mediaplayer {
	width: 640px;
	height: 360px;
}
</style>
<demo:footer />