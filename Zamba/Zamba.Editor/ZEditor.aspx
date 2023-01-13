<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ZEditor.aspx.cs" Inherits="Zamba.Editor.Default" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="styles/default.css" rel="stylesheet" />
    <script src="scripts/ZEditor.js"></script>
    <script src="scripts/sweetalert.min.js"></script>
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <nav class="navbar bg-light">
        <div class="container">
            <figure class="figure">
                <img src="images/zamba.png" class=" zimg figure-img img-fluid rounded">

                <%--<asp:LinkButton ID="BtnExportToDOCXLink" OnClick="BtnExportToDocx_Click"  runat="server" CssClass="btn btn-primary form-control">--%>
                <asp:LinkButton ID="BtnExportToDOCXLink" OnClientClick="Save();"  runat="server" CssClass="btn btn-primary form-control">
<i class="material-icons" title="Guardar" >save</i> </asp:LinkButton>
            </figure>
        </div>
    </nav>
    
    <telerik:RadEditor ID="RadEditor1" runat="server" ContentFilters="DefaultFilters, PdfExportFilter"
     Language="es-ES" LocalizationPath="~/App_Data/languages_rad_editor/es-ES">
        <%--<telerik:RadEditor ID="RadEditor1" runat="server" ContentFilters="DefaultFilters, PdfExportFilter"
     >--%>
        
       <CssFiles><telerik:EditorCssFile Value="~/styles/EditorContentArea.css" />
           


       </CssFiles>
        
        <Tools>
            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="Undo" />
                <telerik:EditorTool Name="Redo" />
            </telerik:EditorToolGroup>
            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="SelectAll" />
                <telerik:EditorTool Name="Cut" />
                <telerik:EditorTool Name="Copy" />
                <telerik:EditorTool Name="Paste" />
            </telerik:EditorToolGroup>

            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="FontName" />
                <telerik:EditorTool Name="RealFontSize" />
            </telerik:EditorToolGroup>

            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="Bold" />
                <telerik:EditorTool Name="Italic" />
                <telerik:EditorTool Name="Underline" />
                <telerik:EditorTool Name="StrikeThrough" />
            </telerik:EditorToolGroup>

            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="BackColor" />
                <telerik:EditorTool Name="ForeColor" />
            </telerik:EditorToolGroup>

            <telerik:EditorToolGroup>                
                <telerik:EditorTool Name="JustifyFull" />
                <telerik:EditorTool Name="JustifyLeft" />
                <telerik:EditorTool Name="JustifyCenter" />
                <telerik:EditorTool Name="JustifyRight" />
                <telerik:EditorTool Name="JustifyNone" />

            </telerik:EditorToolGroup>

            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="Outdent" />
                <telerik:EditorTool Name="Indent" />
                <telerik:EditorTool Name="InsertUnorderedList" />
                <telerik:EditorTool Name="InsertOrderedList" />
            </telerik:EditorToolGroup>



            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="InsertSymbol" />                
                <telerik:EditorTool Name="InsertTable" />
            </telerik:EditorToolGroup>

            <telerik:EditorToolGroup>
                <telerik:EditorTool Name="ToggleScreenMode" />
                <telerik:EditorTool Name="Zoom" />
                <telerik:EditorTool Name="Print" />
            </telerik:EditorToolGroup>


        </Tools>
        <Languages>
            <telerik:SpellCheckerLanguage Code="es-ES" />
        </Languages>
        <ExportSettings FileName="RadEditorExport" OpenInNewWindow="true"></ExportSettings>

    </telerik:RadEditor>
    <script>
        var editor = null;
        function AddJavascriptFile(sfileUrl, oDocument) {
            var theDoc = oDocument != null ? oDocument : document;
            var oScript = theDoc.createElement("script");
            oScript.setAttribute("src", sfileUrl, 0);
            oScript.setAttribute("type", "text/javascript");
            var oHead = theDoc.getElementsByTagName("head")[0];
            oHead.appendChild(oScript);
        }

        Sys.Application.add_load(function () {
            var editor = $find("<%=RadEditor1.ClientID%>"); 
            var oDocument = editor.get_document(); //  
            AddJavascriptFile("scripts/onSheet.js", oDocument);
        }
        ); 
    </script>


    <style>

        .RadEditor {
            width: 100% !important;
            height: 100% !important;
        }

        .t-col-10 {
            width: 100% !important;
        }

        .container {
            height: 50px;
            /* color: aqua; */
            background-color: #f5f5f5;
        }

        .BtnExportToDOCX {
            color: white;
            background-color: #337ab7;
            /* width: 65px; */
            height: 41px;
            /* border-color: white; */
            border-radius: 9%;
            margin-top: 20px;
            margin-left: 45%;
        }

        .figure {
            margin-top: 0px;
            margin-bottom: 0px;
            height: 80px;
        }

        .zimg {
            max-height: 42px;
            vertical-align: top;
            margin-top: 3px
        }

        #ctl00_ContentPlaceHolder1_RadEditor1Center {
            height: 39.25rem !important
        }

        .circle {
            display: block;
            height: 40px;
            width: 40px;
            border-radius: 50%;
            border: 1px solid red;
            z-index: 20000000;
            position: relative;
        }

        .reTableDiv {
            display: none
        }
        .material-icons {
            font-size: 36px;
            color: #1976d2a8;
            float: right;
            border-radius: 16px;
            background-color: white;
            padding: 1px;
            margin-top: 4px;
        }
    </style>


   
</asp:Content>



