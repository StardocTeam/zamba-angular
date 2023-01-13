<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Important.ascx.cs" Inherits="Zamba.Web.Views.UC.HomeWidget.Important" %>

   <script src="../../Scripts/jquery-2.2.2.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/Zamba.js?v=257"></script>
<div class="">
         <div class="DivImportant">
          <h4 style="text-align:center;color:#4b8abf" ><i id="heart" class="fa fa-star" aria-hidden="true"></i> Importantes</h4>
             <ul id="listImportants">
 
           </ul>
        </div>
</div>

<style>
    #itemlist {
        cursor:pointer;
    }
    #list1 li {
        list-style:initial;
        color:white;
    }
    #list1 li a{
        color:white !important;
        font-size:14px;
    }
    #list1 li a:hover{
        text-decoration:none;
    }
</style>