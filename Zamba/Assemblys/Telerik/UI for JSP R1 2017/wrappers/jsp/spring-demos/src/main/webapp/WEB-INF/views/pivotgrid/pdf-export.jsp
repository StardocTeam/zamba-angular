<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="http://demos.telerik.com/olap/msmdpump.dll" var="transportReadUrl" />

<c:url value="/pivotgrid/pdf-export/save" var="saveUrl" />

<demo:header />
	<div class="responsive-message"></div>
    <button id="export" class="k-button k-button-icontext hidden-on-narrow"><span class="k-icon k-i-pdf"></span>Export to PDF</button>
    <kendo:pivotGrid name="pivotgrid" columnWidth="200" height="570" class="hidden-on-narrow"
    filterable="true" sortable="true">
        <kendo:pivotGrid-pdf proxyURL="${saveUrl}" fileName="Kendo UI PivotGrid Export.pdf" />
        <kendo:pivotDataSource type="xmla">
            <kendo:pivotDataSource-columns>
                <kendo:pivotDataSource-column name="[Date].[Calendar]" expand="true"/>
            </kendo:pivotDataSource-columns>
            <kendo:pivotDataSource-rows>
                <kendo:pivotDataSource-row name="[Product].[Category]" expand="true"/>
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
                $("#pivotgrid").getKendoPivotGrid().saveAsPDF();
            });
        });
    </script>
    <style>
       #export
        {
            margin: 0 0 10px 1px;
        }

       /*
           Use the DejaVu Sans font for display and embedding in the PDF file.
           The standard PDF fonts have no support for Unicode characters.
       */
       .k-pivot {
           font-family: "DejaVu Sans", "Arial", sans-serif;
       }
    </style>

    <!-- Load Pako ZLIB library to enable PDF compression -->
    <script src="../resources/shared/js/pako.min.js"></script>
<demo:footer />
