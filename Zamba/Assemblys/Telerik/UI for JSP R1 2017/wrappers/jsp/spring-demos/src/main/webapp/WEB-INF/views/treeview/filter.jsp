<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">
	<kendo:dialog name="dialog" title="Categories" width="400px" visible="false" close="onClose">
		<kendo:dialog-actions>
			<kendo:dialog-action text="Cancel" action="onCancelClick" />
			<kendo:dialog-action text="OK" primary="true" action="onOkClick" />
		</kendo:dialog-actions>
		<kendo:dialog-content>
			<input id="filterText" type="text" placeholder="Search categories" />
			<div class="selectAll">
				<input type="checkbox" id="chbAll" class="k-checkbox" onchange="chbAllOnChange()" />
				<label class="k-checkbox-label" for="chbAll">Select All</label>
				<span id="result">0 categories selected</span>
			</div>
			<kendo:treeView name="treeview" check="onCheck">
			<kendo:treeView-checkboxes checkChildren="true" />
			<kendo:treeView-items>
				<kendo:treeView-item id="1" text="Furniture" expanded="true">
					<kendo:treeView-items>
						<kendo:treeView-item id="2" text="tables & chairs" />
						<kendo:treeView-item id="3" text="sofas" />
						<kendo:treeView-item id="4" text="occasional furniture" />
						<kendo:treeView-item id="5" text="childrens furniture" />
						<kendo:treeView-item id="6" text="beds" />
					</kendo:treeView-items>
				</kendo:treeView-item>
				<kendo:treeView-item id="7" text="Decor" expanded="true">
					<kendo:treeView-items>
						<kendo:treeView-item id="8" text="bed linen" />
						<kendo:treeView-item id="9" text="throws" />
						<kendo:treeView-item id="10" text="curtains & blinds" />
						<kendo:treeView-item id="11" text="rugs" />
						<kendo:treeView-item id="12" text="carpets" />
					</kendo:treeView-items>
				</kendo:treeView-item>
				<kendo:treeView-item id="13" text="Storage" expanded="true">
					<kendo:treeView-items>
						<kendo:treeView-item id="14" text="wall shelving" />
						<kendo:treeView-item id="15" text="kids storage" />
						<kendo:treeView-item id="16" text="multimedia storage" />
						<kendo:treeView-item id="17" text="floor shelving" />
						<kendo:treeView-item id="18" text="toilet roll holders" />
						<kendo:treeView-item id="19" text="storage jars" />
						<kendo:treeView-item id="20" text="drawers" />
						<kendo:treeView-item id="21" text="boxes" />
					</kendo:treeView-items>
				</kendo:treeView-item>
			</kendo:treeView-items>
			</kendo:treeView>
		</kendo:dialog-content>
	</kendo:dialog>
	<kendo:multiSelect name="multiselect" dataTextField="text" dataValueField="id">
	</kendo:multiSelect>
	<br />
	<kendo:button name="openWindow" class="k-primary">
		<kendo:button-content>
			SELECT CATEGORIES
		</kendo:button-content>
	</kendo:button>
</div>
<script>      
	$(document).ready(function () {
		var dialog = $("#dialog");
		var multiSelect = $("#multiselect").data("kendoMultiSelect");

		multiSelect.readonly(true);

		$("#openWindow").click(function () {
			dialog.data("kendoDialog").open();
			$(this).fadeOut();
		});

		dialog.data("kendoDialog").open();
	});

	function onCancelClick(e) {
		e.sender.close();
	}

	function onOkClick(e) {
		var checkedNodes = [];
		var treeView = $("#treeview").data("kendoTreeView");

		getCheckedNodes(treeView.dataSource.view(), checkedNodes);
		populateMultiSelect(checkedNodes);

		e.sender.close();
	}

	function onClose() {
		$("#openWindow").fadeIn();
	}

	function populateMultiSelect(checkedNodes) {
		var multiSelect = $("#multiselect").data("kendoMultiSelect");
		multiSelect.dataSource.data([]);

		var multiData = multiSelect.dataSource.data();
		if (checkedNodes.length > 0) {
			var array = multiSelect.value().slice();
			for (var i = 0; i < checkedNodes.length; i++) {
				multiData.push({ text: checkedNodes[i].text, id: checkedNodes[i].id });
				array.push(checkedNodes[i].id.toString());
			}

			multiSelect.dataSource.data(multiData);
			multiSelect.dataSource.filter({});
			multiSelect.value(array);
		}
	}

	function checkUncheckAllNodes(nodes, checked) {
		for (var i = 0; i < nodes.length; i++) {
			nodes[i].set("checked", checked);

			if (nodes[i].hasChildren) {
				checkUncheckAllNodes(nodes[i].children.view(), checked);
			}
		}
	}

	function chbAllOnChange() {
		var checkedNodes = [];
		var treeView = $("#treeview").data("kendoTreeView");
		var isAllChecked = $('#chbAll').prop("checked");

		checkUncheckAllNodes(treeView.dataSource.view(), isAllChecked)

		if (isAllChecked) {
			setMessage($('#treeview input[type="checkbox"]').length);
		}
		else {
			setMessage(0);
		}
	}

	function getCheckedNodes(nodes, checkedNodes) {
		var node;

		for (var i = 0; i < nodes.length; i++) {
			node = nodes[i];

			if (node.checked) {
				checkedNodes.push({ text: node.text, id: node.id });
			}

			if (node.hasChildren) {
				getCheckedNodes(node.children.view(), checkedNodes);
			}
		}
	}

	function onCheck() {
		var checkedNodes = [];
		var treeView = $("#treeview").data("kendoTreeView");

		getCheckedNodes(treeView.dataSource.view(), checkedNodes);
		setMessage(checkedNodes.length);
	}

	function setMessage(checkedNodes) {
		var message;

		if (checkedNodes > 0) {
			message = checkedNodes + " categories selected";
		}
		else {
			message = "0 categories selected";
		}

		$("#result").html(message);
	}

	$("#filterText").keyup(function (e) {
		var filterText = $(this).val();

		if (filterText !== "") {
			$(".selectAll").css("visibility", "hidden");

			$("#treeview .k-group .k-group .k-in").closest("li").hide();
			$("#treeview .k-group").closest("li").hide();
			$("#treeview .k-group .k-group .k-in:contains(" + filterText + ")").each(function () {
				$(this).parents("ul, li").each(function () {
					$(this).show();
				});
			});
			$("#treeview .k-group .k-in:contains(" + filterText + ")").each(function () {
				$(this).parents("ul, li").each(function () {
					$(this).show();
				});
			});
		}
		else {
			$("#treeview .k-group").find("li").show();
			$(".selectAll").css("visibility", "visible");
		}
	});
</script>
<style>
	html .k-dialog .k-window-titlebar {
		padding-left: 17px;
	}

	.k-dialog .k-content {
		padding: 17px;
	}

	#filterText {
		width: 100%;
		box-sizing: border-box;
		padding: 6px;
		border-radius: 3px;
		border: 1px solid #d9d9d9;
	}

	.selectAll {
		margin: 17px 0;
	}

	#result {
		color: #9ca3a6;
		float: right;
	}

	#treeview {
		height: 300px;
		overflow-y: auto;
		border: 1px solid #d9d9d9;
	}

	#openWindow {
		min-width: 180px;
	}
</style>
<demo:footer />
