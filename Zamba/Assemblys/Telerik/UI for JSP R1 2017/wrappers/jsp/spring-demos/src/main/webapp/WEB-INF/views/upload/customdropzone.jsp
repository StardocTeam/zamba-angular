<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<style>
        .dropZoneElement {
            position: relative;
            display: inline-block;
            background-color: #f8f8f8;
            border: 1px solid #c7c7c7;
            width: 230px;
            height: 110px;
            text-align: center;
        }

        .textWrapper {
            position: absolute;
            top: 50%;
            transform: translateY(-50%);
            width: 100%;
            font-size: 24px;
            line-height: 1.2em;
            font-family: Arial,Helvetica,sans-serif;
            color: #000;
        }

        .dropImageHereText {
            color: #c7c7c7;
            text-transform: uppercase;
            font-size: 12px;
        }

        .product {
            float: left;
            position: relative;
            margin: 0 10px 10px 0;
            padding: 0;
        }
        .product img {
            width: 110px;
            height: 110px;
        }

        .wrapper:after {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }
    </style>

<demo:header />

<c:url value="/upload/customdropzone/save" var="saveUrl" />
<c:url value="/upload/customdropzone/remove" var="removeUrl" />
<% String[] imageExtensions = {".jpg", ",jpeg", ".png", ".bmp", ".gif"}; %>
<div id="example" class="k-content">

    <div class="demo-section k-content wide">
        <div class="wrapper">
            <div id="products"></div>
            <div class="dropZoneElement">
                <div class="textWrapper">
                <p><span>+</span>Add Image</p>
                <p class="dropImageHereText">Drop image here to upload</p>
            </div>
            </div>
         </div>
    </div>

    <kendo:upload name="files" success="onSuccess" showFileList="false" dropZone=".dropZoneElement">
        <kendo:upload-async saveField="files" autoUpload="true" saveUrl="${saveUrl}" removeUrl="${removeUrl}"/>
        <kendo:upload-validation allowedExtensions="<%= imageExtensions %>" />
    </kendo:upload>
</div>

<demo:footer />
<script type="text/x-kendo-template" id="template">
	<div class="product">
		<img src="../resources/web/foods/#= name #" alt="#: name # image" />
	</div>       
</script>

<script>
    var template = kendo.template($("#template").html());
    var initialFiles = [{ name: "1.jpg" }, { name: "2.jpg" }, { name: "3.jpg" }, { name: "4.jpg" }, { name: "5.jpg" }, { name: "6.jpg" }];

    $("#products").html(kendo.render(template, initialFiles));

    function onSuccess(e) {
        if (e.operation == "upload") {
            for (var i = 0; i < e.files.length; i++) {
                var file = e.files[i].rawFile;

                if (file) {
                    var reader = new FileReader();

                    reader.onloadend = function () {
                        $("<div class='product'><img src=" + this.result + " /></div>").appendTo($("#products"));
                    };

                    reader.readAsDataURL(file);
                }
            }
        }
    }
</script>