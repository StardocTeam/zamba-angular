<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestServerUpload.aspx.cs" Inherits="Zamba.Web.Views.Insert.InsertApi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <%:Scripts.Render("~/bundles/jqueryCore") %>      
    <%: Scripts.Render("~/Scripts/angular.min.js") %>
    <script src="../../Scripts/toastr.min.js"></script>
    <link href="../../Content/toastr.min.css" rel="stylesheet" />
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="hdnUserId"/>
    </form>
       <div> 
   
          <center>
        <input style="margin-top:50px" name="Upload" id="image"  onchange="UploadIMG(this)" type="file" /> </center>
            
           <center style="position:relative;right:80px;">
               <label>UserId<input  class="form-control col-xs-3 " id="userid" /></label>
               <label>Docid<input  class="form-control col-xs-3 " id="id" /></label></center>
               <div id="" class=" col-xs-12 col-xs-offset-2">
              
                 <div class="col-xs-10">
                 <div class="col-xs-4 indicesid"><label>ID del indice</label><input class="form-control" /></div>
                   <div class=" col-xs-4 indicesVal"> <label>  Valor del indice</label><input class="form-control col-xs-4"/></div>
                  </div>
              
                <div class="col-xs-10">
                 <div class=" col-xs-4 indicesid"><label>ID del indice</label><input class="form-control" /></div>
                   <div class=" col-xs-4 indicesVal"> <label> Valor del indice</label><input class="form-control col-xs-4"/></div>
                  </div>
                <div class="col-xs-10">
                 <div class=" col-xs-4 indicesid"><label>ID del indice</label><input class="form-control" /></div>
                   <div class=" col-xs-4 indicesVal"> <label> Valor del indice</label><input class="form-control col-xs-4"/></div>
                  </div>
                <div class="col-xs-10">
                 <div class=" col-xs-4 indicesid"><label>ID del indice</label><input class="form-control" /></div>
                   <div class=" col-xs-4 indicesVal"> <label> Valor del indice</label><input class="form-control col-xs-4"/></div>
                  </div>
              <div class="row col-xs-12 col-xs-offset-2" style="margin-top:20px">
                       <span id="sendinfo" class="btn btn-success col-xs-2" onclick="sendinfo()">Insertar</span>

              </div>

           </div>
        </div>
   
</body>
</html>

<style>


</style>

<script>
    var fileSelected = "";
    var fileExtencion = "";
    function UploadIMG(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var img64 = e.target.result;
                if (e.total > 400000) {
                    var img = new Image();
                    img.src = img64;
                    var width = img.width;
                    var height = img.height;
                    widthext = 500;
                    if (width > widthext) {
                        var oc = document.createElement('canvas'), octx = oc.getContext('2d');
                        oc.width = widthext;
                        oc.height = img.height;
                        octx.drawImage(img, 0, 0);
                        while (oc.width * 0.5 > widthext) {
                            oc.width *= 0.5;
                            oc.height *= 0.5;
                            octx.drawImage(oc, 0, 0, oc.width, oc.height);
                        }
                        oc.width = widthext;
                        oc.height = oc.width * img.height / img.width;
                        octx.drawImage(img, 0, 0, oc.width, oc.height);
                        img64 = oc.toDataURL();
                    }
                }
                fileSelected = img64;
                fileExtencion = $("#image").val().split('.').pop();
            }
            reader.readAsDataURL(input.files[0]);
        }

    }


    function sendinfo() {

        var indexid = $(".indicesid :input"); 
        var indexval = $(".indicesVal :input"); 
        var listindex = [];
 
        for (var i = 0; i < indexid.length; i++) {
            for (var i = 0; i < indexval.length; i++) {
                if (indexid[i].value && indexval[i].value != "")
                    listindex.push({
                        id: indexid[i].value,
                        value: indexval[i].value
                    });
            }
        }

        var doctypeid = $("#id").val();
        var CurrentUserId = $("#userid").val();
        var File = { data: fileSelected, extension: fileExtencion }
        var insert = { file: File, DocTypeId: doctypeid, userid: CurrentUserId, indexs: listindex };

        //var resturl = "http://10.6.110.213/zambaweb.restapi/api/InsertFiles/";
        var resturl = "http://imageapp/zambaweb.restapi/api/InsertFiles/";

        //var authorizationToken = JSON.parse(localStorage["ls.authorizationData"]).token;
                $.ajax({
                    type: "POST",
                    url: resturl + 'UploadFile',
                    dataType: 'json',
                    data: JSON.stringify(insert),
                    contentType: "application/json",
                    success: function (data) {
                        if(data != false)
                            toastr.success("archivo se inserto con exito");
                        else
                            toastr.error('Ocurrio un error en el proceso de su solicitud');

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        toastr.error('Ocurrio un error en el proceso de su solicitud');
                    },

                    //beforeSend: function (request)
                    //{
                    //    if (authorizationToken) {
                    //        request.withCredentials = true;
                    //        request.setRequestHeader("Authorization", "Bearer " + authorizationToken);
                    //    }
                    //}
                });
            }
  
</script>