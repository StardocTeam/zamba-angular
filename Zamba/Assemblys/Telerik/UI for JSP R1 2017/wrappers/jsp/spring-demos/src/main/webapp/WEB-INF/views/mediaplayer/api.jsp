<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
<div class="demo-section k-content wide" style="width: 644px;">
	<kendo:mediaPlayer name="mediaPlayer" autoPlay="true">
		<kendo:mediaPlayer-media
			title="Why Telerik Analytics - Key Benefits For Your Applications"
			source="https://www.youtube.com/watch?v=CpHKm2NruYc" />
	</kendo:mediaPlayer>
</div>
<div class="box wide" style="max-width: 644px;">
	<h4>API Functions</h4>
	<div class="box-col">
		<ul class="options">
			<li>
				<button id="playButton" class="k-button">Play</button>
			</li>
			<li>
				<button id="stopButton" class="k-button">Stop</button>
			</li>
			<li>
				<button id="pauseButton" class="k-button">Pause</button>
			</li>
			<li>
				<button id="fullScreenButton" class="k-button">Full-screen</button>
			</li>
		</ul>
	</div>
	<div class="box-col">
		<ul class="options">
			<li>
				<button id="toggleMuteButton" class="k-button">Toggle mute</button>
			</li>
			<li>
				<button id="isEndedButton" class="k-button">Video has ended</button>
			</li>
			<li>
				<button id="isPausedButton" class="k-button">Video is
					paused</button>
			</li>
			<li>
				<button id="isPlayingButton" class="k-button">Video is
					playing</button>
			</li>
		</ul>
	</div>
	<div class="box-col">
		<ul class="options">
			<li><input id="volumeValue" value="50"
				style="float: none; width: 90px;" />
				<button id="volumeButton" class="k-button">Set volume</button></li>
			<li><input id="seekValue" value="50"
				style="float: none; width: 90px;" />
				<button id="seekButton" class="k-button">Seek to</button></li>
		</ul>
	</div>
</div>
<script>
$(document).ready(function () {
    var mediaPlayer = $("#mediaPlayer").data("kendoMediaPlayer");

    var setVolume = function () {
        mediaPlayer.volume($("#volumeValue").val());
    };

    var seekTo = function () {
        mediaPlayer.seek($("#seekValue")
                .data("kendoNumericTextBox").value() * 1000);
    };

    $("#playButton").click(function () {
        mediaPlayer.play();
    });

    $("#stopButton").click(function () {
        mediaPlayer.stop();
    });

    $("#pauseButton").click(function () {
        mediaPlayer.pause();
    });

    $("#fullScreenButton").click(function () {
        mediaPlayer.fullScreen(true);
    });

    $("#toggleMuteButton").click(function () {
        mediaPlayer.mute(!mediaPlayer.mute());
    });

    $("#isEndedButton").click(function () {
        alert(mediaPlayer.isEnded());
    });

    $("#isPausedButton").click(function () {
        alert(mediaPlayer.isPaused());
    });

    $("#isPlayingButton").click(function () {
        alert(mediaPlayer.isPlaying());
    });

    $("#volumeValue").kendoNumericTextBox({
        change: setVolume,
        min: 0,
        max: 100,
        format: "#",
        decimals: 0
    });

    $("#seekValue").kendoNumericTextBox({
        change: seekTo,
        min: 0,
        max: 97,
        format: "#",
        decimals: 0
    });

    $("#seekButton").click(seekTo);

    $("#volumeButton").click(setVolume);
});
</script>
<style>
.k-mediaplayer {
	width: 640px;
	height: 360px;
}
</style>
<demo:footer />