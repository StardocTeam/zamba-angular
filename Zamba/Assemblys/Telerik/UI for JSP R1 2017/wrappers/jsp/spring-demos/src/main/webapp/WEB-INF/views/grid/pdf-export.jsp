<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/resources/web/grid/clientsDb.png" var="backgroundUrl" />
<c:url value="/grid/customers/" var="transportReadUrl" />

<demo:header />

    <div class="box wide">
        <p style="margin-bottom: 1em"><b>Important:</b></p>

        <p style="margin-bottom: 1em">
            This page loads
            <a href="https://github.com/nodeca/pako">pako zlib library</a> (pako_deflate.min.js)
            to enable compression in the PDF. This is highly recommended as it improves
            performance and rises the limit on the size of the content that can be exported.
        </p>

        <p>
            The Standard PDF fonts do not include Unicode support.

            In order for the output to match what you see in the browser
            you must provide source files for TrueType fonts for embedding.

            Please read the documentation about
            <a href="http://docs.telerik.com/kendo-ui/framework/drawing/drawing-dom#custom-fonts-and-pdf">custom fonts</a>
            and
            <a href="http://docs.telerik.com/kendo-ui/framework/drawing/pdf-output#using-custom-fonts">drawing</a>.
        </p>
    </div>

    <style>
        /*
            Use the DejaVu Sans font for display and embedding in the PDF file.
            The standard PDF fonts have no support for Unicode characters.
        */
        .k-grid {
            font-family: "DejaVu Sans", "Arial", sans-serif;
        }

        /* Hide the Grid header and pager during export */
        .k-pdf-export .k-grid-toolbar,
        .k-pdf-export .k-pager-wrap
        {
            display: none;
        }
    </style>

    <!-- Load Pako ZLIB library to enable PDF compression -->
    <script src="../resources/shared/js/pako.min.js"></script>

    <script type="x/kendo-template" id="page-template">
      <div class="page-template">
        <div class="header">
          <div style="float: right">Page #: pageNum # of #: totalPages #</div>
          Multi-page grid with automatic page breaking
        </div>
        <div class="watermark">KENDO UI</div>
        <div class="footer">
          Page #: pageNum # of #: totalPages #
        </div>
      </div>
    </script>

    <kendo:grid name="grid" groupable="true" sortable="true" style="height:550px;">
        <kendo:grid-toolbar>
            <kendo:grid-toolbarItem name="pdf" />
        </kendo:grid-toolbar>
        <kendo:grid-pdf allPages="true" avoidLinks="true" paperSize="A4"
            landscape="true" repeatHeaders="true" template="page-template" scale="0.8"
            proxyURL="${saveUrl}" fileName="Kendo UI Grid Export.pdf">
            <kendo:grid-pdf-margin top="2cm" left="1cm" right="1cm" bottom="1cm" />
        </kendo:grid-pdf>
        <kendo:grid-pageable refresh="true" pageSizes="true" buttonCount="5">
        </kendo:grid-pageable>
        <kendo:grid-columns>
            <kendo:grid-column title="Contact Name" field="contactName" width="240"
                template="<div class='customer-photo' style='background-image: url(../resources/web/Customers/#:data.customerId#.jpg);'></div><div class='customer-name'>#: contactName #</div>">
            </kendo:grid-column>
            <kendo:grid-column title="Contact Title" field="contactTitle" />
            <kendo:grid-column title="Company Name" field="companyName" />
            <kendo:grid-column title="Country" field="country" width="150" />
        </kendo:grid-columns>
        <kendo:dataSource pageSize="10">
             <kendo:dataSource-schema>
                <kendo:dataSource-schema-model>
                    <kendo:dataSource-schema-model-fields>
                        <kendo:dataSource-schema-model-field name="contactName" type="string" />
                        <kendo:dataSource-schema-model-field name="contactTitle" type="string" />
                        <kendo:dataSource-schema-model-field name="companyName" type="string" />
                        <kendo:dataSource-schema-model-field name="country" type="string" />
                    </kendo:dataSource-schema-model-fields>
                </kendo:dataSource-schema-model>
            </kendo:dataSource-schema>
            <kendo:dataSource-transport>
                <kendo:dataSource-transport-read url="${transportReadUrl}"/>
            </kendo:dataSource-transport>
        </kendo:dataSource>
    </kendo:grid>


     <style type="text/css">
        /* Page Template for the exported PDF */
        .page-template {
          font-family: "DejaVu Sans", "Arial", sans-serif;
          position: absolute;
          width: 100%;
          height: 100%;
          top: 0;
          left: 0;
        }
        .page-template .header {
          position: absolute;
          top: 30px;
          left: 30px;
          right: 30px;
          border-bottom: 1px solid #888;
          color: #888;
        }
        .page-template .footer {
          position: absolute;
          bottom: 30px;
          left: 30px;
          right: 30px;
          border-top: 1px solid #888;
          text-align: center;
          color: #888;
        }
        .page-template .watermark {
          font-weight: bold;
          font-size: 400%;
          text-align: center;
          margin-top: 30%;
          color: #aaaaaa;
          opacity: 0.1;
          transform: rotate(-35deg) scale(1.7, 1.5);
        }

        /* Content styling */
        .customer-photo {
            display: inline-block;
            width: 32px;
            height: 32px;
            border-radius: 50%;
            background-size: 32px 35px;
            background-position: center center;
            vertical-align: middle;
            line-height: 32px;
            box-shadow: inset 0 0 1px #999, inset 0 0 10px rgba(0,0,0,.2);
            margin-left: 5px;
        }

        .customer-name {
            display: inline-block;
            vertical-align: middle;
            line-height: 32px;
            padding-left: 3px;
        }
    </style>
<demo:footer />
