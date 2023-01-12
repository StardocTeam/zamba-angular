<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div id="example">

<style>
    .k-editor p {
        margin: 1em 0px;
    }

    .k-editor *[contenteditable='false'] {
        opacity: 0.5;
        cursor: default;
    }
    
    
    .k-editor .selected-immutable {
        background: rgba(128, 128, 128, 0.1);
    }
</style>

<kendo:editor name="editor" style="height:400px" tag="div" select="onSelect">
	<kendo:editor-immutables serialization="# return immutablesSerialization(data); #" deserialization="immutablesDeserialization" />
    <kendo:editor-serialization custom="customSerialization" />
    <kendo:editor-value>
         <div class="headerField" contenteditable="false" style="font-size:32px; color: #078e23; text-align:center;">
             <p>Company Name</p>
             <hr />
         </div>

         <div class="addressField" contenteditable="false" style="width: 100%; text-align:right;">
             1010 Street,<br />
             City,<br />
             Country, <br />
             +000 000 555 666<br />
         </div>

         <p><span contenteditable="false">Dear </span> Your Name,</p>

         <p>
             Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vel nunc velit. Proin dictum ac justo eu varius. Fusce vehicula, erat ac sagittis consequat, leo libero tristique dolor, et maximus eros ante quis quam. Aliquam erat volutpat. Cras nec mattis dui, sed rhoncus magna. Maecenas eget tristique nibh. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras nec nisl elementum, rhoncus ligula at, fringilla odio. Curabitur semper ultricies tristique. Donec dapibus placerat dui nec fermentum. Nunc non cursus nibh. Nulla rhoncus vitae erat id facilisis.
         </p>
         <p>
             Aenean lacinia eros eu interdum placerat. Pellentesque elit tellus, condimentum eu nulla quis, iaculis gravida sapien. Aliquam elementum dapibus eros sed pretium. Praesent porta odio magna, nec efficitur arcu pharetra a. Nulla lacus orci, pretium eu pellentesque id, ornare eget turpis. Quisque condimentum quam ante, ornare cursus nisl suscipit ac. Nulla non leo semper, finibus mauris eu, interdum ex.
         </p>

         <p contenteditable="false">Best regards,</p>
         <p>Your Name</p>
         <div class="addressField" contenteditable="false" style="width: 100%; text-align:left;">
             1010 Street,<br />
             City,<br />
             Country, <br />
             +000 000 555 666<br />
         </div>
    </kendo:editor-value>
    <kendo:editor-tools>
		<kendo:editor-tool name="bold" />
		<kendo:editor-tool name="italic"/>
		<kendo:editor-tool name="underline"/>
		<kendo:editor-tool name="justifyLeft"/>
		<kendo:editor-tool name="justifyCenter"/>
		<kendo:editor-tool name="justifyRight"/>
		<kendo:editor-tool name="justifyFull"/>
		<kendo:editor-tool name="indent"/>
		<kendo:editor-tool name="outdent"/>
		<kendo:editor-tool name="createLink"/>
		<kendo:editor-tool name="unlink"/>
		<kendo:editor-tool name="createTable"/>
		<kendo:editor-tool name="addRowAbove"/>
		<kendo:editor-tool name="addRowBelow"/>
		<kendo:editor-tool name="addColumnLeft"/>
		<kendo:editor-tool name="addColumnRight"/>
		<kendo:editor-tool name="deleteRow"/>
		<kendo:editor-tool name="deleteColumn"/>
		<kendo:editor-tool name="viewHtml"/>
    </kendo:editor-tools>
</kendo:editor>

<script>
    function immutablesSerialization(node){
    	$("[contenteditable='false']", editor.body).removeClass("selected-immutable");
        var immutableName = node.className || node.nodeName.toLowerCase();
        var textAlign = node.style.textAlign;
        var result = textAlign ?
                        kendo.format("<{0} style='text-align:{1};'></{0}>", immutableName, textAlign) :
                        kendo.format("<{0}></{0}>", immutableName);

        return result;
    }
    
    function immutablesDeserialization(node, immutable){
    	immutable.style.textAlign = node.style.textAlign;
    }
    
    function onSelect(e){
    	var editor = e.sender;
        var selection = e.sender.getSelection();
        var selectedNode = selection.anchorNode;

        $("[contenteditable='false']", editor.body).removeClass("selected-immutable");
        $(selectedNode).closest("[contenteditable='false']").addClass("selected-immutable");
    }
    
    function customSerialization(html){
    	return html.replace(/selected-immutable/, "");
    }
</script>

</div>


<demo:footer />
