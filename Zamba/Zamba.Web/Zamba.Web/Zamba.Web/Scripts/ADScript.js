var finalurl = ZambaWebRestApiURL+"/ActiveDirectory/";

function Login()
{
    //var authorizationToken = JSON.parse(localStorage["ls.authorizationData"]).token;

    var Name = $("#name").val();
    //var Password = $("#pass").val();
    var username = JSON.stringify(Name);
    
    $.ajax({
        type: "POST",
        url: finalurl + 'UserLogin',
        data: username,
        dataType:'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == true)
            {
                toastr.success("usuario correcto");
                $("#name").val("");
            }
            else
                toastr.error("usuario incorrecto");
        },
        error: function (request, status, error) {
            //alert(request.responseText);
        },
        //beforeSend: function (request) {
        //    if (authorizationToken) {
        //        request.withCredentials = true;
        //        request.setRequestHeader("Authorization", "Bearer " + authorizationToken);
        //    }
        //}
    });

}

function GetAllUsers()
{
    //var authorizationToken = JSON.parse(localStorage["ls.authorizationData"]).token;
    var username = $("#name").val();
    $.ajax({
        type: "POST",
        url: finalurl + 'GetUsers',
        data: username,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        //headers: { 'X-CSRF-Token': authorizationToken },

        success: function (data)
        {

            
            for (var i = 0; i < data.length; i++) {
               
                $("#userlist").append("<li><p>" + data[i] + "</p></li");
              
            }
 
        },
        error: function (request, status, error) {
            //alert(request.responseText);
        },
        //beforeSend: function (request) {
        //    if (authorizationToken) {
        //        request.withCredentials = true;
        //        request.setRequestHeader("Authorization", "Bearer " + authorizationToken);
        //    }
        //}

    });
}

function GetAllGroups()
{
    //var authorizationToken = JSON.parse(localStorage["ls.authorizationData"]).token;
    var username = $("#name").val();
    $.ajax({
        type: "POST",
        url: finalurl + 'GetAllGroup',
        data: username,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
          
            $.each(data, function (key, value) {
                $("#grouplist").append("<li><p>" + key + "</p></li");
            });
        },
        error: function (request, status, error) {
            //alert(request.responseText);
        },
        //beforeSend: function (request) {
        //    if (authorizationToken) {
        //        request.withCredentials = true;
        //        request.setRequestHeader("Authorization", "Bearer " + authorizationToken);
        //    }
        //}

    });

}

function GetUserGroup()
{
    //var authorizationToken = JSON.parse(localStorage["ls.authorizationData"]).token;

    var Name = $("#username").val();
    //var Password = $("#pass").val();
    var Username = JSON.stringify(Name);

    $.ajax({
        type: "POST",
        url: finalurl + 'GetUserGroup',
        data: Username,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null)
                $("#Usergroup").val(data);
        },
        error: function (request, status, error) {
            //alert(request.responseText);
        },
        //beforeSend: function (request) {
        //    if (authorizationToken) {
        //        request.withCredentials = true;
        //        request.setRequestHeader("Authorization", "Bearer " + authorizationToken);
        //    }
        //}
    });

}

function GetUsersFromGroup()
{
    //var authorizationToken = JSON.parse(localStorage["ls.authorizationData"]).token;

    var Name = $("#groupname").val();
    //var Password = $("#pass").val();
    var Username = JSON.stringify(Name);

    $.ajax({
        type: "POST",
        url: finalurl + 'GetUsersFromGroup',
        data: Username,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        if (data.length > 0)
         $("#usersfromgroup").append("<li><p>" + data + "</p></li");           
            
        else
        {
            toastr.error("Error al ejecutar consulta")
        }
          
        },
        error: function (request, status, error) {
            //alert(request.responseText);
        },
        //beforeSend: function (request) {
        //    if (authorizationToken) {
        //        request.withCredentials = true;
        //        request.setRequestHeader("Authorization", "Bearer " + authorizationToken);
        //    }
        //}

    });

}

function cleanListUser() {

    $("#userlist").empty();

}

function cleanListGroups() {

    $("#grouplist").empty();

}

function cleanUsersFromGroup() {

    $("#usersfromgroup").empty();

}