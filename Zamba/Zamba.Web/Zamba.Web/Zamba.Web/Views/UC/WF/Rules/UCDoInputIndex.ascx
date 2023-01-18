<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UC_WF_Rules_UCDoInputIndex" CodeBehind="UCDoInputIndex.ascx.cs" %>

<style>
    #ui-datepicker-div {
        z-index: 1051 !important;
        /*z-index:1002 !important;*/
    }

    span {
        white-space: nowrap;
    }

    #openModalIFUcRules {
        margin-top: 80px !important;
        position: absolute !important;
        display: block !important;
        padding-right: 12px !important;
        overflow-y: scroll !important;
        padding-left: 15% !important;
        padding-right: 15% !important;
    }
</style>

<asp:HiddenField runat="server" ID="hddocId" />
<asp:HiddenField runat="server" ID="hdDTId" />

<div class="container-fluid">
    <div class="row">

        <div class="col-xs-12">
            <h3>
                <asp:Label ID="lblmessage" name="_message" runat="server"></asp:Label>
            </h3>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12">
            <asp:TextBox ID="txtvalue" runat="server" TextMode="MultiLine" Width="359px"
                Height="112px" style="margin-top: 20px;background-color: #f1f1f1 !important;"></asp:TextBox>
        </div>
    </div>
    <div class="row">
    </div>
    <div class="row">
        <div class="col-xs-12" style="margin-top: 40px">
            <asp:Button ID="_btnok" Text="Ok" runat="server" OnClick="_btnOk_Click" Width="100px" Height="35px" style="background: rgb(43, 153, 46); color: white; border-color: rgb(43, 153, 46);"/>
            <asp:Button ID="_btnCancel" Text="Cancel" runat="server" OnClick="_btnCancel_Click"  Width="102px" Height="35px" UseSubmitBehavior="false" style="background: #337ab7; color: white; border-color: #337ab7;" />
        </div>
    </div>
</div>
