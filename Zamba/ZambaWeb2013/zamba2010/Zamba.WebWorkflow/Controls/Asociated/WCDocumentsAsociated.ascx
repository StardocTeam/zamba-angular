<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCDocumentsAsociated.ascx.cs" Inherits="Controls_Asociated_WCDocumentsAsociated" %>
<%@ Register Src="~/Controls/Core/WCResults.ascx" TagName="results" TagPrefix="my" %> 

<div style="overflow:auto ; width:300px; position:absolute ;" runat="server" >
    <my:results id="ucResults" runat="server"  Visible="true"  OnOnReloadValues="ucResult_OnReloadValues"/>
</div>