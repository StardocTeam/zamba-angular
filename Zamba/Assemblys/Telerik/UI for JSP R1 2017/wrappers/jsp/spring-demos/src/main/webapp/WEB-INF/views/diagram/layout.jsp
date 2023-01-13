<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/diagram/layout/read" var="readUrl" />

<demo:header />

<div class="box wide">
    <div class="box-col">
        <h4>Layout: </h4>
           <select id="subtype">
                <option value="down">Tree Down</option>
                <option value="up">Tree Up</option>
                <option value="tipover">Tipover Tree</option>
           </select>
    </div>
</div>

<kendo:diagram name="diagram">
     <kendo:dataSource>
         <kendo:dataSource-transport>
             <kendo:dataSource-transport-read url="${readUrl}" type="POST"  contentType="application/json"/>
         </kendo:dataSource-transport>
         <kendo:dataSource-schema>
             <kendo:dataSource-schema-hierarchical-model children="items" />
         </kendo:dataSource-schema>
     </kendo:dataSource>
     <kendo:diagram-layout type="tree" subtype="down" horizontalSeparation="30" verticalSeparation="20" />
     <kendo:diagram-shapeDefaults width="40" height="40" />
 </kendo:diagram>

<script>
    $(document).ready(function() {
        $("#subtype").change(function() {
            $("#diagram").getKendoDiagram().layout({
                subtype: $(this).val(),
                type: "tree",
                horizontalSeparation: 30,
                verticalSeparation: 20
            });
        });
    });
</script>

<demo:footer />
