<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocViewer.aspx.cs" Inherits="DocViewer" MasterPageFile="~/MasterBlankPage.master" EnableSessionState="True" ValidateRequest="false" %>
<%@ Register Src="~/Views/UC/Common/ZDeleteButton.ascx" TagName="UCZDeleteButton" TagPrefix="ZDeleteButton" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Reference Control="~/Views/UC/Viewers/FormBrowser.ascx" %>
<%@ Reference Control="~/Views/UC/Viewers/DocViewer.ascx" %>

<asp:Content ID="contentDocViewer" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField runat="server" ID="hdnResultId"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnDocTypeId"></asp:HiddenField>
    
    <div id="Header" style="margin-top:5px;">
        <asp:UpdatePanel runat="server" ID="updHeader">
            <ContentTemplate>
                <button type="submit" id="BtnClose" runat="server" class="btn btn-danger btn-xs" tooltip="Cerrar"
                    onclick="ShowLoadingAnimation(); $('#__EVENTTARGET').val('BtnClose');" tabindex="-1">
                    Cerrar             
                </button>
                <ZDeleteButton:UCZDeleteButton ID="deleteCtrl" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="tbDoc" style="height: 500px" class=" row ">
        <div class="col-md-12">
            <asp:Panel runat="server" ID="pnlViewer"></asp:Panel>
        </div>
    </div>
</asp:Content>
