<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Bookmarks.ascx.cs" Inherits="Zamba.Web.Views.UC.HomeWidget.Bookmarks" %>

   <script src="../../Scripts/jquery-2.2.2.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/Zamba.js"></script>

    <script>
          $(document).ready(function () {
              $(function () {
                  function loop() {
                      $('.fa-heart')
                          .animate({ marginTop: 10 }, 1000)
                          .animate({ marginTop: 0 }, 1000, loop);
                  }
                  loop();
              });
          });
    </script>
<div class="HRWidgetContent">
      <h4  style="color:rgba(237, 76, 106, 0.88);text-align:center"><i id="heart" class="fa fa-heart" aria-hidden="true"></i> Favoritos</h4>
        <ul id="listFavorites">

        </ul>
</div>

<style>
    #ListItem{

        cursor:pointer;
    }
    HWWidgetBookmarks  > ul > li
    {
        color:#ef617c;
    }
    ul > li a 
     {
       color:#a2a2a2;
     }
</style>


