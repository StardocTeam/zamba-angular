<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />
<div class="demo-section k-content wide" style="width: 925px;">

	<kendo:mediaPlayer name="mediaPlayer" autoPlay="true">
	</kendo:mediaPlayer>
	<div class="k-list-container playlist">
		<kendo:listView name="listView" template="template" selectable="true"
			dataBound="onDataBound" change="onChange">
			<kendo:dataSource data="${videos}">
			</kendo:dataSource>
		</kendo:listView>
	</div>
</div>

<script type="text/x-kendo-template" id="template">
    <div class="k-item k-state-default" onmouseover="$(this).addClass('k-state-hover')"
        onmouseout="$(this).removeClass('k-state-hover')">
        <span>
            <img src="#:poster#" />
            <h5>#:title#</h5>
        </span>
    </div>
</script>

<script>
	function onChange() {
		var index = this.select().index();
		var dataItem = this.dataSource.view()[index];
		$("#mediaPlayer").data("kendoMediaPlayer").media(dataItem);
	}

	function onDataBound() {
		this.select(this.element.children().first());
	}
</script>

<style>
.k-mediaplayer {
    float: left;
	width: 640px;
	height: 360px;
}

.playlist {
	float: left;
	width: 280px;
	height: 360px;
	overflow: auto;
}

.playlist .k-item {
	border-bottom-style: solid;
	border-bottom-width: 1px;
	padding: 14px 15px;
}

.playlist .k-item:last-child {
	border-bottom-width: 0;
}

.playlist span {
	cursor: pointer;
	display: block;
	overflow: hidden;
	text-decoration: none;
}

.playlist span img {
	border: 0 none;
	display: block;
	height: 56px;
	object-fit: cover;
	width: 100px;
	float: left;
}

.playlist h5 {
	display: block;
	font-weight: normal;
	margin: 0;
	overflow: hidden;
	padding-left: 10px;
	text-align: left;
}
</style>

<demo:footer />