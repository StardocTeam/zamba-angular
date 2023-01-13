<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

       <div class="demo-section k-content">
    	<h4>TreeView with images</h4>
    	
        <kendo:treeView name="treeview-images">
            <kendo:treeView-items>
                <kendo:treeView-item text="Inbox" imageUrl="../resources/web/treeview/mail.png">
                    <kendo:treeView-items>
                        <kendo:treeView-item text="Read Mail" imageUrl="../resources/web/treeview/readmail.png" />
                    </kendo:treeView-items>
                </kendo:treeView-item>
                <kendo:treeView-item text="Drafts" imageUrl="../resources/web/treeview/edit.png" />
                <kendo:treeView-item text="Search Folders" imageUrl="../resources/web/treeview/search.png" expanded="true">
                    <kendo:treeView-items>
                        <kendo:treeView-item text="Categorized Mail" imageUrl="../resources/web/treeview/search.png" />
                        <kendo:treeView-item text="Large Mail" imageUrl="../resources/web/treeview/search.png" />
                        <kendo:treeView-item text="Unread Mail" imageUrl="../resources/web/treeview/search.png" />
                    </kendo:treeView-items>
                </kendo:treeView-item>
           		<kendo:treeView-item text="Settings" imageUrl="../resources/web/treeview/settings.png" />
            </kendo:treeView-items>
        </kendo:treeView>
    </div>
    
     <div class="demo-section k-content">
    	<h4>TreeView with sprites</h4>
    	
	    <kendo:treeView name="treeview-sprites">
	        <kendo:treeView-items>
	            <kendo:treeView-item text="My Documents" spriteCssClass="rootfolder" expanded="true">
	                <kendo:treeView-items>
	                    <kendo:treeView-item text="Kendo UI Project" spriteCssClass="folder" expanded="true">
	                        <kendo:treeView-items>
	                            <kendo:treeView-item text="about.html" spriteCssClass="html" />
	                            <kendo:treeView-item text="index.html" spriteCssClass="html" />
	                            <kendo:treeView-item text="logo.png" spriteCssClass="html" />
	                        </kendo:treeView-items>
	                    </kendo:treeView-item>
	                    <kendo:treeView-item text="New Web Site" spriteCssClass="folder" expanded="true">
	                        <kendo:treeView-items>
	                            <kendo:treeView-item text="mockup.jpg" spriteCssClass="image" expanded="true" />
	                            <kendo:treeView-item text="Research.pdf" spriteCssClass="pdf" />
	                            <kendo:treeView-item text="Research.pdf" spriteCssClass="pdf" />
	                        </kendo:treeView-items>
	                    </kendo:treeView-item>
	                    <kendo:treeView-item text="Reports" spriteCssClass="folder" expanded="true">
	                        <kendo:treeView-items>
	                            <kendo:treeView-item text="February.pdf" spriteCssClass="pdf" expanded="true" />
	                            <kendo:treeView-item text="March.pdf" spriteCssClass="pdf" />
	                            <kendo:treeView-item text="April.pdf" spriteCssClass="pdf" />
	                        </kendo:treeView-items>
	                    </kendo:treeView-item>
	                </kendo:treeView-items>
	            </kendo:treeView-item>
	        </kendo:treeView-items>
	    </kendo:treeView>
    </div>

    <style>
        #treeview-sprites .k-sprite {
            background-image: url("../resources/web/treeview/coloricons-sprite.png");
        }

        .rootfolder { background-position: 0 0; }
        .folder { background-position: 0 -16px; }
        .pdf { background-position: 0 -32px; }
        .html { background-position: 0 -48px; }
        .image { background-position: 0 -64px; }

    </style>
<demo:footer />
