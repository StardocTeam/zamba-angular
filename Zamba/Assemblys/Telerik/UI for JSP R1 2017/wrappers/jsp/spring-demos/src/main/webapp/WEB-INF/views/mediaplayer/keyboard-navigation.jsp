<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
<div class="demo-section k-content wide" style="width: 644px;">
	<kendo:mediaPlayer name="mediaPlayer" autoPlay="true"
		navigatable="true">
	</kendo:mediaPlayer>
</div>
<div class="box wide" style="max-width: 644px;">
	<div class="box-col">
		<h4>Focus</h4>
		<ul class="keyboard-legend">
			<li><span class="button-preview"> <span
					class="key-button wider">Alt</span> + <span class="key-button">W</span>
			</span> <span class="button-descr"> Focuses the widget </span></li>
		</ul>
	</div>
	<div class="box-col">
		<h4>Supported keys and user actions</h4>
		<ul class="keyboard-legend">
			<li><span class="button-preview"> <span
					class="key-button wider">Space</span>
			</span> <span class="button-descr"> Toggles Play and Pause </span></li>
			<li><span class="button-preview"> <span
					class="key-button wider">Enter</span>
			</span> <span class="button-descr"> Opens the video in the FullScreen
					mode </span></li>
			<li><span class="button-preview"> <span
					class="key-button wider">Esc</span>
			</span> <span class="button-descr"> Exits the FullScreen mode </span></li>
			<li><span class="button-preview"> <span
					class="key-button wider">M</span>
			</span> <span class="button-descr"> Toggles Mute and Unmute </span></li>
		</ul>
	</div>
</div>
<script>
$(document).ready(function () {
    //focus the widget
    $(document).on("keydown.examples", function (e) {
        if (e.altKey && e.keyCode === 87 /* w */) {
            $("#mediaPlayer").focus();
        }
    });
    $("#mediaPlayer").data("kendoMediaPlayer").media({
        title: "Digital Factory",
        source: [
          {
              quality: "360p",
              url: "http://telerik-media.s3.amazonaws.com/digital-factory/digital-factory-360.mp4"
          },
          {
              quality: "480p",
              url: "http://telerik-media.s3.amazonaws.com/digital-factory/digital-factory-480.mp4"
          },
          {
              quality: "720p",
              url: "http://telerik-media.s3.amazonaws.com/digital-factory/digital-factory-720.mp4"
          }]
    });
});
</script>
<style>
.k-mediaplayer {
	width: 640px;
	height: 360px;
}
</style>
<demo:footer />