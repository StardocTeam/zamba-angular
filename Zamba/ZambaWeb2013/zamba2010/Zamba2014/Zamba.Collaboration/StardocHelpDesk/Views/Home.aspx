<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Zamba.Collaboration.StardocHelpDesk.Views.Home" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="utf-8" http-equiv="X-UA-Compatible" content="IE=edge" />


    <script src="../Js/jquery-2.2.2.min.js"></script>
    <link href="../Css/Styles.css" rel="stylesheet" />
    <script src="../Js/bootstrap.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
    <link href="../Css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Js/toastr.js"></script>
    <link href="../Css/toastr.min.css" rel="stylesheet" />
   
</head>
  <%--  <script type="text/javascript">
        $(document).ready(function(){
          

        })

    </script>--%>
<body>
    <form runat="server">
    <asp:HiddenField runat="server" ID="ThisDomain"/>
    </form>
    <nav class="navbar navbar-default navbar-static-top" style="background-color:white">
        <div class="container">
            <a class="navbar-brand " href="#">
                <img src="../Content/Images/zamba.png" style="display:inline-block;height:30px;bottom:5px;position:relative" />
                <span style="display:inline-block;position:relative;bottom:3px;margin-left:10px;color:#7d7d7d">Zamba Invitacion</span>
            </a>
        </div>
    </nav>

    <div id="FormNewUser" class="col-xs-10 col-xs-offset-1" style="position:relative;top:50px">
     
        <div class="row ">
            <div class="col-xs-12 col-sm-3 col-xs-offset-1 ImageZone uploadWidget" id="">
                <label class="btn btn-default btn-file col-xs-12" style="display:none">Subir Foto <input type="file" id="locId" style="display:none;" /></label>
                <img data-toggle="tooltip" title="Subir Imagen" id="imgupload" src="" style="height:250px;width:250px;border-radius:50%;border:1px solid #cccccc;" />

                <a href='#'><span id="DeleteImg" data-toggle="tooltip" title="Borrar Imagen" class='glyphicon glyphicon-remove gi-2x' style="display:none; position:relative;left:42%;top:20px;color:#b1afaf;"></span></a>
            </div>
            <br />
            <form id="Form1">
                <div class="col-sm-offset-5  col-xs-offset-1">
        

                    <input data-toggle="tooltip" title="Nombre" class="form-control col-xs-4  inputs Nombre" placeholder="Nombre" required />

                    <input data-toggle="tooltip" title="Apellido" class="form-control col-xs-4 inputs Apellido" placeholder="Apellido" required />

                    <input data-toggle="tooltip" title="Cliente" class="form-control col-xs-4 inputs Cliente" placeholder="Cliente" required />

                    <input data-toggle="tooltip" title="Sector" class="form-control col-xs-4 inputs Sector" placeholder="Sector" required />

                    <input data-toggle="tooltip" title="Puesto" class="form-control col-xs-4 inputs Puesto" placeholder="Puesto" required />

                    <input data-toggle="tooltip" title="Mail" type="email" class="form-control col-xs-4 inputs Email" placeholder="Email" required />

                    <input data-toggle="tooltip" title="Telefono" class="form-control col-xs-4 inputs Telefono" placeholder="Telefono" required />

                    <input data-toggle="tooltip" title="Interno (No requerido)" class="form-control col-xs-4 inputs Interno (No requerido)" placeholder="Interno (Opcional)" />

                    <input data-toggle="tooltip" title="Celular (No requerido)" class="form-control col-xs-4 inputs Celular " placeholder="Celular (Opcional) " />

                    <input data-toggle="tooltip" type="password" title="Password" class="form-control col-xs-4 inputs Password" placeholder="Password " required />

                </div>
             
              <button type="submit" class="btnRegistro btn col-xs-3 col-xs-offset-1 btn-lg " style="position:relative;bottom:50px;background-color:rgba(82, 82, 82, 0.63);color:white"  >
                 Enviar 
              </button>
                    
            </form>
        </div>

    </div>

</body>

</html>
  


<script type="text/javascript">
    var ThisDomain = $("[id$=ThisDomain]").val();
    var ZambaWebRestApiURL = $("[id$=ZambaWebRestApiURL]").val();

    $(document).ready(function () {
        var ThisDomain = $("[id$=ThisDomain]").val();
        $("#imgupload").attr('src', src = ThisDomain + "StardocHelpDesk/Content/Images/businessman.png");
        $('[data-toggle="tooltip"]').tooltip({
            'delay': { show: 200, hide: 100 }
        });
    });

    $("#imgupload").click(function () {
        $("#locId").trigger('click')
    });

    $("#DeleteImg").click(function () {

        $("#imgupload").attr('src', src = ThisDomain + "StardocHelpDesk/Content/Images/businessman.png");
        $("#DeleteImg").fadeOut("slow");
    });
     
    var url = ZambaWebRestApiURL + "/InvitedUser/";
        $("#locId").change(function () {

            var file = document.getElementById("locId").value;
            var extFile = (file).substring(file.lastIndexOf(".") + 1).toLowerCase();

            var reader = new FileReader();
            reader.readAsDataURL(document.getElementById("locId").files[0]);
            reader.onloadend = function (event) {
                var img = event.target.result;
                if (img.length < 1400000) { //Que no pese mas que un mega
                    $("#imgupload").attr('src', img);
                    $("#DeleteImg").fadeIn("slow")
                }
                else {
                    toastr.error('Error al subir imagen', 'Compruebe que la imagen pese menos que 1Mb');
                }
            }
        })
        $(".btnRegistro").click(function () {
            var InvitedUser = {
            
                'Nombre': $(".Nombre").val(),
                'Apellido': $(".Apellido").val(),
                'Cliente': $(".Cliente").val(),
                'Sector': $(".Sector").val(),
                'Puesto': $(".Puesto").val(),
                'Email': $(".Email").val(),
                'Telefono': $(".Telefono").val(),
                'Interno': $(".Interno").val(),
                'Celular': $(".Celular").val(),
                'Password': $(".Password").val(),
                'img':  $("#imgupload").attr('src'),
            };
            if (InvitedUser.Nombre != "" && InvitedUser.Mail != "" && InvitedUser.Password != "") {
                $.ajax({
                    type: "POST",
                    crossDomain: true,
                    //async: false,
                    url: url + 'Create',
                    dataType: 'json',
                    contentType: "application/json",
                    //headers: { 'Content-Type': 'text/plain' },
                    data: JSON.stringify(InvitedUser),
                    //data: { 'userId': 10 },
                    success: function (data) {
                        toastr.success('Su solicitud se proceso correctamente');
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        toastr.error('Ocurrio un error en el proceso de su solicitud');
                    }
                });
            }
        })

</script>
