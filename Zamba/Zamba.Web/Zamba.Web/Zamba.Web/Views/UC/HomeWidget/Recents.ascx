<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Recents.ascx.cs" Inherits="Zamba.Web.Views.UC.HomeWidget.Recents" %>


    <script src="../../Scripts/jquery-2.2.2.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/zamba.js?v=263"></script>



<div class="recents">
    <h4 style="color:#60a723;text-align:center;"><i class="fa fa-clock-o" aria-hidden="true"></i> Recientes</h4>
      <ul id="listRecents" >

      </ul>
</div>

<style>

    #listRecents li {
        list-style:initial;
        color:#676767;
        cursor:pointer;
    }
   
      #listRecents li:before {
        color:#676767;
        
    }
    /*#listRecents li a{

        color:white !important;
        font-size:14px;
    }*/


</style>