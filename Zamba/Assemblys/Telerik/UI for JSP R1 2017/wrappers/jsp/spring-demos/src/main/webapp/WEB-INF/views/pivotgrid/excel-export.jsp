<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="http://demos.telerik.com/olap/msmdpump.dll" var="transportReadUrl" />

<c:url value="/pivotgrid/excel-export/save" var="saveUrl" />

<script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

<demo:header />
	<div class="responsive-message"></div>
	<button id="export" class="k-button k-button-icontext hidden-on-narrow"><span class="k-icon k-i-excel"></span>Export to Excel</button>
	<kendo:pivotGrid name="pivotgrid" columnWidth="200" height="570" class="hidden-on-narrow"
	filterable="true" sortable="true">
		<kendo:pivotGrid-excel fileName="Kendo UI PivotGrid Export.xlsx" filterable="true" proxyURL="${saveUrl}" />
		<kendo:pivotDataSource type="xmla">
			<kendo:pivotDataSource-columns>
				<kendo:pivotDataSource-column name="[Date].[Calendar]" expand="true"/>
				<kendo:pivotDataSource-column name="[Product].[Category]"/>
			</kendo:pivotDataSource-columns>
			<kendo:pivotDataSource-rows>
				<kendo:pivotDataSource-row name="[Geography].[City]"/>
			</kendo:pivotDataSource-rows>
			<kendo:pivotDataSource-measures>
				<kendo:pivotDataSource-measure name="[Measures].[Reseller Freight Cost]"/>
			</kendo:pivotDataSource-measures>
			<kendo:pivotDataSource-schema type="xmla">
			</kendo:pivotDataSource-schema>
			<kendo:pivotDataSource-transport>
				<kendo:pivotDataSource-transport-connection catalog="Adventure Works DW 2008R2" cube="Adventure Works"/>
				<kendo:pivotDataSource-transport-discover url="${transportReadUrl}" dataType="text" contentType="text/xml" type="POST">
				</kendo:pivotDataSource-transport-discover>
				<kendo:pivotDataSource-transport-read url="${transportReadUrl}" dataType="text" contentType="text/xml" type="POST">
				</kendo:pivotDataSource-transport-read>
			</kendo:pivotDataSource-transport>
		</kendo:pivotDataSource>
	</kendo:pivotGrid>

	<script>
		$(function() {
			$("#export").click(function() {
                $("#pivotgrid").getKendoPivotGrid().saveAsExcel();
            });
		});
	</script>
	 <style>
	     #export
	        {
	            margin: 0 0 10px 1px;
	        }
      </style>

<demo:footer />
