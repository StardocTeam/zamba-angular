
//configFunction.$inject = ['$routeProvider'];

var helperApp = angular.module('helperApp', ["ngRoute"])
    //The Factory used to define the value to
    //Communicate and pass data across controllers
    .factory("ShareData", function () {
        return { value: 0 }
    })

   .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
       // configure the routing rules here
       $routeProvider.when('/home/edit:id', {
           //  templateUrl: 'home/edit/:id'
           // templateUrl: function (params) { return '/home/edit?id=' + params.id; },
           controller: 'HelperEditController'
       });

       // enable HTML5mode to disable hashbang urls
       $locationProvider.html5Mode(true);
   }])

    //Para confirmar la eliminacion de helper
    .directive('ngConfirmClick', [
        function () {
            return {
                link: function (scope, element, attr) {
                    var msg = attr.ngConfirmClick || "Are you sure?";
                    var clickAction = attr.confirmedClick;
                    element.bind('click', function (event) {
                        bootbox.confirm(msg, function (result) {
                            if (result)
                                scope.$eval(clickAction);
                        });
                    });
                }
            };
        }])


 .controller('HelperAddController', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams, $location) {
     $scope.addSubmit = function () {
         var fn = $scope.Function;
         var mod = $scope.Module;
         var app = $scope.Application;
         if (app == undefined) {
             toastr.error("Seleccione la aplicacion", "Zamba Helper");
             return;
         }
         if (fn == undefined || !(typeof (fn) == "number" || typeof (fn) == "string")) {
             //Compruebo modulo
             if (mod == undefined || !(typeof (mod) == "number" || typeof (mod) == "string")) {
                 var undMod = false;
                 var mods = $scope.Modules || this.Data.Modules();
                 if (mods != undefined) {
                     for (var m = 0; m <= mods.length - 1; m++) {
                         if (mods[m].Module == "Sin definir") {
                             $scope.Module = mod = mods[m].Id;
                             undMod = true;
                             break;
                         }
                     }
                 }
                 if (!undMod) {
                     $scope.Module = mod = $scope.AddUndefinedMod(app);
                 }
             }
             var undFn = false;
             var funs = $scope.Functions || this.Data.Functions();
             if (funs != undefined) {
                 for (var f = 0; f <= funs.length - 1; f++) {
                     if (funs[f].Function == "Sin definir") {
                         $scope.Function = fn = funs[f].Id;
                         undFn = true;
                         break;
                     }
                 }
             }
             if (!undFn) {
                 $scope.Function = fn = $scope.AddUndefinedFun(mod);
             }
         }
         var st = $scope.Types;
         if ($scope.Type == undefined) {
             for (var i = 0; i <= st.length - 1; i++) {
                 if (st[i].Type == "Sin definir") {
                     $scope.Type = st[i].Id;
                     break;
                 }
             }
         }
         var helper = {
             "Code": $scope.Code || "",
             "Title": $scope.Title || "",
             "Name": $scope.Name || "",
             "HelpFunctionId": $scope.Function,
             "HelpTypeId": $scope.Type,
             "ShortContent": GetTinyMCEContent('ShortContent'),
             "FullContent": GetTinyMCEContent("FullContent"),
             "ForAllUsers": $scope.ForAllUsers,
         }
         $http.post('../api/helper', helper).
             success(function (data, status, headers, config) {
                 toastr.success("Se genero el helper N°" + data.Id, "Zamba Helper");
                 setTimeout(function () { window.location.replace(LocalhostURL()); }, 1000);
             }).
             error(function (data, status, headers, config) {
                 toastr.error("Se produjo un error al generar el helper", "Zamba Helper");
             });
     };

     $scope.AddUndefinedMod = function (appId) {
         var modId;
         $.ajax({
             type: "POST",
             async: false,
             url: LocalhostURL() + '/ListOptionsCRUD/AddUndefinedMod',
             data: { appid: appId },
             success: function (result) {
                 modId = result;
             }
         });
         this.Data.addModule(appId, modId, "Sin definir");
         return modId;
     }
     $scope.AddUndefinedFun = function (modId) {
         var funId;
         $.ajax({
             type: "POST",
             async: false,
             url: LocalhostURL() + '/ListOptionsCRUD/AddUndefinedFun',
             data: { modid: modId },
             success: function (result) {
                 funId = result;
             }
         });
         this.Data.addFunction(modId, funId, "Sin definir");
         return funId;
     }
     $scope.GetListOptions = function () {
         $http.get('../api/helper/GetListOptions').
          success(function (data, status, headers, config) {
              $scope.Applications = data.Applications;
              $scope.Types = data.Types;
          }).
          error(function (data, status, headers, config) {
              toastr.error("Se produjo un error al obtener lista de parametros", "Zamba Helper");
          });
     };

     $scope.GetListOptions();

     $scope.LoadModule = function (appId) {
         $scope.Module, $scope.Function = [];
         if (!appId)
             toastr.info("Por favor seleccione una aplicacion", "Zamba Helper");
         else
             this.Data.LoadModules(appId);
     };

     //$scope.LoadFunction = function (moduleId) {
     //    $http.get('../api/helper/GetHelpFunctions?modId=' + moduleId).
     //      success(function (data, status, headers, config) {
     //          $scope.Functions = data;
     //      });
     //};
     $scope.LoadFunction = function (modId) {
         if (!modId) {
             toastr.info("Por favor seleccione un modulo", "Zamba Helper");
             return;
         }
         this.Data.LoadFunctions(modId);
     };

     $scope.GetModOptions = function (appid) {
         if (appid == undefined) {
             toastr.info("Por favor seleccione la aplicacion", "Zamba Helper");
             return;
         }
         $http.get(LocalhostURL() + '/api/helper/GetHelpModules?appid=' + appid).
         success(function (data, status, headers, config) {
             if (!data.length) //Creacion
                 $scope.Modules = $scope.Module = $scope.Functions = $scope.Module = [];
             else
                 $scope.Modules = data;
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener lista de parametros", "Zamba Helper");
         });
     }

     $scope.GetFunOptions = function (modid) {
         if (modid == undefined) {
             toastr.info("Por favor seleccione aplicacion y modulo", "Zamba Helper");
             return;
         }
         $http.get(LocalhostURL() + '/api/helper/GetHelpFunctions?modid=' + modid).
         success(function (data, status, headers, config) {
             $scope.Functions = data;
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener lista de parametros", "Zamba Helper");
         });
     }

     $scope.GetTemplates = function () {
         $http.get('../api/helper/GetTemplates').
         success(function (data, status, headers, config) {
             var template = data[0];
             SetTinyMCEContent("ShortContent", template);
             SetTinyMCEContent("FullContent", template);
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener los templates", "Zamba Helper");
         });
     }
     $scope.GetTemplates();
 }])

.controller('HelperIndexController', ['$scope', '$http', function ($scope, $http) {
    $scope.fillHelpers = function () {
        ///GetUserRigths()
        $http.get(LocalhostURL() + 'api/helper', {
        }).
         success(function (data, status, headers, config) {
             $scope.Helpers = data.HelpItems;
             $scope.CanEdit = data.Edit;
             $scope.CanDelete = data.Delete;
             $scope.CanCreate = data.Create;
             if (!$scope.CanCreate) $('#btnCreate').attr('disabled', 'disabled');
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al cargar helpers", "Zamba Helper");
         });
    }
    $scope.fillHelpers();
    $scope.Columns = [
        "Id", "Codigo", "Titulo", "Nombre", "Tipo", "Aplicacion", "Modulo", "Funcion"
    ];
    $scope.removeHelper = function (id) {

        var url = LocalhostURL() + 'api/helper/' + id;
        $http({
            method: 'DELETE',
            url: url,
            headers: {
                'Content-type': 'application/json;charset=utf-8'
            }
        })
           .then(function (response) {
               $scope.fillHelpers();
               toastr.success("Registro eliminado con éxito", "Zamba Helper");
           }, function (rejection) {
               toastr.error("Se produjo un error al eliminar el registro", "Zamba Helper");
           });
    };
}])

.controller('HelperSharedController', function ($scope, $http, Data) {

    $scope.Data = Data;

    $scope.NewApplication = function () {
        var name = this.NewApp;
        if (name == undefined || name == "") {
            toastr.info("Por favor agregar un nombre", "Zamba Helper");
            return;
        }

        var apps = this.Data.Applications();
        for (var i = 0; i <= apps.length - 1; i++) {
            if (apps[i].Application == name) {
                toastr.info("La aplicacion " + name + " ya se encuentra cargada", "Zamba Helper");
                return;
            }
        }
        var d;
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/application',
            data: { id: 0, method: 'insert', value: name },
            success: function (res) {
                d = res;
            }
        });
        if (d == undefined)
            toastr.error("Se produjo un error al crear la aplicacion");
        else {
            this.Data.addApplication(d.Id, d.Application);
            toastr.info("Aplicacion: " + name + ", insertada correctamente", "Zamba Helper");
            $("#editOptionsModal").find(".close").click();
        }
    }
    $scope.NewModule = function (app) {
        var name = this.NewMod;
        if (name == undefined || name == "") {
            toastr.info("Por favor agregar un nombre", "Zamba Helper");
            return;
        }
        //$scope.GetModuleInfo(appId, false);        
        var mods = this.Data.Modules();
        if (mods != undefined) {
            for (var i = 0; i <= mods.length - 1; i++) {
                if (mods[i].Module == name) {
                    toastr.info("El modulo " + name + " ya se encuentra cargado", "Zamba Helper");
                    return;
                }
            }
        }
        var d;
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/module',
            data: { appid: app, id: 0, method: 'insert', value: name },
            success: function (result) {
                d = result;
            }
        });
        if (d == undefined) {
            toastr.info("Se produjo un error al agregar el modulo", "Zamba Helper");
        }
        else {
            toastr.info("Se agrego el nuevo modulo", "Zamba Helper");
            $("#editOptionsModal").find(".close").click();
            this.Data.addModule(d.HelpApplicationId, d.Id, d.Module);
        }
    }
    $scope.NewFunction = function (modId) {
        var name = this.NewFun;
        if (name == undefined || name == "") {
            toastr.info("Por favor agregar un nombre", "Zamba Helper");
            return;
        }
        var fns = this.Data.Functions();
        if (fns != undefined) {
            for (var i = 0; i <= fns.length - 1; i++) {
                if (fns[i].Function == name) {
                    toastr.info("La funcion " + name + " ya se encuentra cargada", "Zamba Helper");
                    return;
                }
            }
        }
        var d;
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/function',
            data: { modid: modId, id: 0, method: 'insert', value: name },
            success: function (res) {
                d = res;
            }
        });
        if (d != undefined) {
            this.Data.addFunction(d.HelpModuleId, d.Id, d.Function);
            toastr.info("Se inserto la funcion correctamente", "Zamba Helper");
            $("#editOptionsModal").find(".close").click();
        }
        else
            toastr.error("Se produjo un error al grabar la funcion")
    }

    $scope.setForAllUsers = function () {
        $scope.ForAllUsers = true;
    }

    $scope.editApplications = function () {
        $scope.NewApp = "";
        $scope.ModalTitle = "Editar aplicaciones";
        $scope.ModuleType = "Application";
        $("#editOptionsModal").modal();
        //$(".modal-backdrop.fade.in").remove();
    }
    $scope.editModules = function (app) {
        app = typeof (app) == "object" ? app.Id : app;
        $scope.NewMod = "";
        if (app != undefined) {
            $scope.ModuleType = "Module";
            $scope.ModalTitle = "Editar modulos";
            $scope.Application = app;
            $("#editOptionsModal").modal();
        }
        else
            toastr.info("Por favor seleccione la aplicacion para editar modulos disponibles", "Zamba Helper");
    }
    $scope.editFunctions = function (mod) {
        mod = typeof (mod) == "object" ? mod.Id : mod;
        if (mod != undefined && mod != "") {
            $scope.NewFun = "";
            $scope.ModuleType = "Function";
            $scope.ModalTitle = "Editar funciones";
            $scope.Module = mod;
            $("#editOptionsModal").modal();
        }
        else
            toastr.info("Por favor seleccione el modulo para editar funciones disponibles", "Zamba Helper");
    }

    $scope.EditApp = function (elem) {
        var name = elem.Application;
        var id = elem.Id;
        if (name == undefined || name == "") {
            toastr.info("Por favor agregar un nombre", "Zamba Helper");
            return;
        }
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/application',
            data: { id: id, method: 'update', value: name },
            success: function (result) {
                toastr.info("Aplicacion editada", "Zamba Helper");
                $("#editOptionsModal").find(".close").click();
            }
        });
        this.Data.editApplication(id, name);
    }
    $scope.RemoveApp = function (elem) {
        var id = elem.Id;
        if (this.Data.removeApplication(id)) {
            $.ajax({
                type: "POST",
                async: false,
                url: LocalhostURL() + '/ListOptionsCRUD/application',
                data: { id: id, method: 'remove', value: '' },
                success: function (result) {
                    toastr.info("Aplicacion eliminada", "Zamba Helper");
                    $("#editOptionsModal").find(".close").click();
                }
            });
        }
    }

    $scope.EditMod = function (elem) {
        var name = elem.Module;
        var id = elem.Id;
        if (name == undefined || name == "") {
            toastr.info("Por favor agregar un nombre", "Zamba Helper");
            return;
        }
        var d;
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/module',
            data: { appid: elem.HelpApplicationId, id: id, method: 'update', value: name },
            success: function (res) {
                d = res;
            }
        });
        if (d != undefined) {
            this.Data.editModule(d.Id, d.Module);
            toastr.info("Modulo editado", "Zamba Helper");
            $("#editOptionsModal").find(".close").click();
        }
    }
    $scope.RemoveMod = function (elem) {
        if (this.Data.removeModule(elem.Id)) {
            $.ajax({
                type: "POST",
                async: false,
                url: LocalhostURL() + '/ListOptionsCRUD/module',
                data: { appid: 0, id: elem.Id, method: 'remove', value: '' },
                success: function (result) {
                    toastr.info("Modulo eliminado exitosamente", "Zamba Helper");
                    $("#editOptionsModal").find(".close").click();
                }
            });
        }
    }

    $scope.EditFun = function (elem) {
        var name = elem.Function;
        var id = elem.Id;
        if (name == undefined || name == "") {
            toastr.info("Por favor agregar un nombre", "Zamba Helper");
            return;
        }
        var d;
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/function',
            data: { modid: elem.HelpModuleId, id: id, method: 'update', value: name },
            success: function (res) {
                d = res;
            }
        });
        if (d == undefined) {
            toastr.error("Error al editar la funcion");
        }
        else {
            this.Data.editFunction(d.Id, d.Function);
            toastr.info("Se edito la funcion correctamente", "Zamba Helper");
            $("#editOptionsModal").find(".close").click();
        }
    }
    $scope.RemoveFun = function (elem) {
        var id = elem.Id;
        $.ajax({
            type: "POST",
            async: false,
            url: LocalhostURL() + '/ListOptionsCRUD/function',
            data: { modid: 0, id: id, method: 'remove', value: '' },
            success: function (result) {
                toastr.info("Se elimino la funcion correctamente", "Zamba Helper");
                $("#editOptionsModal").find(".close").click();
            }
        });
        this.Data.removeFunction(elem.Id);
    }
})

 .controller('HelperEditController', ['$scope', '$http', '$routeParams', '$location', 'Data', function ($scope, $http, $routeParams, $location, Data) {

     $scope.Data = Data;

     $scope.Columns = ["Id", "Codigo", "Titulo", "Nombre", "Funcion", "Tipo",
          "Aplicacion", "Modulo", "Contenido Breve", "Contenido"];

     $scope.FillHelperData = function () {
         $http.get(LocalhostURL() + '/api/helper/' + $location.$$path.replace(/^\D+/g, '')).
          success(function (data, status, headers, config) {
              $scope.Helpers = data;
              $scope.HelpType = data.HelpType;
              $scope.Function = data.HelpFunction;
              $scope.Module = data.HelpFunction.HelpModule;
              $scope.Application = data.HelpFunction.HelpModule.HelpApplication;

              $scope.Data.LoadModules($scope.Application.Id);
              $scope.Data.LoadFunctions($scope.Module.Id);

              SetTinyMCEContent('ShortContent', data.ShortContent);
              SetTinyMCEContent('FullContent', data.FullContent);
          }).
          error(function (data, status, headers, config) {
              toastr.error("Se produjo un error al obtener datos", "Zamba Helper");
          });
     }
     $scope.FillHelperData();

     $scope.GetModOptions = function (appId) {
         var app = appId || $scope.Application.Id;
         if (app == undefined) {
             toastr.error("Por favor seleccione una aplicacion", "Zamba Helper");
             return;
         }
         $http.get(LocalhostURL() + '/api/helper/GetHelpModules?appid=' + app).
         success(function (data, status, headers, config) {
             $scope.Modules = data;
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener lista de parametros", "Zamba Helper");
         });
     }

     $scope.GetFunOptions = function (modid, event) {
         if (modid == undefined) {
             modid = $scope.Helpers.HelpFunction.HelpModuleId;
         }
         $http.get(LocalhostURL() + '/api/helper/GetHelpFunctions?modid=' + modid).
         success(function (data, status, headers, config) {
             $scope.Functions = data;
             $(event.target).click();
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener lista de parametros", "Zamba Helper");
         });
     }

     $scope.LoadModule = function (appId) {
         $scope.Module = $scope.Function = [];
         this.Data.clearModules();
         if (!appId)
             toastr.info("Por favor seleccione una aplicacion", "Zamba Helper");
         else
             this.Data.LoadModules(appId);
     };

     $scope.LoadFunction = function (modId) {
         $scope.Function = [];
         if (!modId) {
             toastr.info("Por favor seleccione un modulo", "Zamba Helper");
             return;
         }
         this.Data.LoadFunctions(modId);
     };

     $scope.editSubmit = function () {
         var helper = {
             "Id": $scope.Helpers.Id,
             "Code": $scope.Helpers.Code,
             "Title": $scope.Helpers.Title,
             "Name": $scope.Helpers.Name,
             "HelpFunctionId": typeof ($scope.Function) == "number" ? $scope.Function : $scope.Function.Id,
             "HelpTypeId": typeof ($scope.HelpType) == "number" ? $scope.HelpType : $scope.HelpType.Id,
             "Application": $scope.Application,
             "ShortContent": GetTinyMCEContent('ShortContent'),
             "FullContent": GetTinyMCEContent("FullContent"),
         };

         var url = LocalhostURL() + '/api/helper/' + $scope.Helpers.Id;
         $http({
             method: 'PUT',
             url: url,
             data: helper,
             headers: {
                 'Content-type': 'application/json;charset=utf-8'
             }
         })
         .then(function (response) {
             toastr.success("Registro actualizado con éxito", "Zamba Helper");
             setTimeout(function () { window.location.replace(LocalhostURL()); }, 1000);
         }, function (rejection) {
             toastr.error("Se produjo un error al modificar el registro", "Zamba Helper");
         });
     };
 }])

.service('Data', function ($http) {
    Applications = [];
    Modules = [];
    Functions = [];
    Types = [];
    function LoadModules(appId) {
        $http.get(LocalhostURL() + '/api/helper/GetHelpModules?appId=' + appId).
      success(function (data, status, headers, config) {
          if (!data.length)
              Modules = Functions = [];
          else
              Modules = data;
      });
    }
    function LoadFunctions(modId) {
        $http.get(LocalhostURL() + '/api/helper/GetHelpFunctions?modId=' + modId).
          success(function (data, status, headers, config) {
              Functions = data;
          });
    }
    function GetListOptions() {
        $http.get(LocalhostURL() + '/api/helper/GetListOptions').
         success(function (data, status, headers, config) {
             Applications = data.Applications;
             Types = data.Types;
         }).
         error(function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener lista de parametros", "Zamba Helper");
         });
    }
    GetListOptions();

    function NewApp(id, name) {
        var app = new Object();
        app.Id = id;
        app.Application = name;
        app.Modules = [];
        return app;
    }
    function EditApp(id, name) {
        for (var i = 0; i <= Applications.length - 1; i++) {
            if (Applications[i].Id == id) {
                Applications[i].Application = name;
                break;
            }
        }
    }
    function RemoveApp(id) {
        for (var i = 0; i <= Applications.length - 1; i++) {
            if (Applications[i].Id == id) {
                if (!Modules.filter(function (x) { x.HelpApplicationId == id }).length) {
                    Applications.splice(i, 1);
                    return true;
                }
                else {
                    toastr.error("Por favor elimine los modulos que contiene", "No se pudo eliminar la aplicacion");
                    return false;
                }
                break;
            }
        }
    }

    function NewMod(appid, id, name) {
        var mod = new Object();
        mod.Functions = [];
        for (var i = 0; i <= Applications.length - 1; i++) {
            if (Applications[i].Id == appid) {
                mod.HelpApplication = Applications[i];
                mod.HelpApplicationId = Applications[i].Id;
                break;
            }
        }
        mod.Id = id;
        mod.Module = name;
        return mod;
    }
    function EditMod(id, name) {
        for (var i = 0; i <= Modules.length - 1; i++) {
            if (Modules[i].Id == id) {
                Modules[i].Module = name;
                break;
            }
        }
    }
    function RemoveMod(id) {
        for (var i = 0; i <= Modules.length - 1; i++) {
            if (Modules[i].Id == id) {
                if (!Functions.filter(function (x) { x.HelpModuleId == id }).length) {
                    Modules.splice(i, 1);
                    return true;
                }
                else {
                    toastr.error("Por favor elimine las funciones que contiene", "No se pudo eliminar el modulo");
                    return false;
                }
                break;
            }
        }
    }
    function ClearMod() {
        ClearFun();
        Modules = [];
    }

    function NewFun(modid, id, name) {
        var fun = new Object();
        fun.Function = name;
        for (var i = 0; i <= Modules.length - 1; i++) {
            if (Modules[i].Id == modid) {
                fun.HelpModule = Modules[i];
                fun.HelpModuleId = Modules[i].Id;
                break;
            }
        }
        fun.Id = id;
        fun.Items = [];
        return fun;
    }
    function EditFun(id, name) {
        for (var i = 0; i <= Functions.length - 1; i++) {
            if (Functions[i].Id == id) {
                Functions[i].Function = name;
                break;
            }
        }
    }
    function RemoveFun(id) {
        for (var i = 0; i <= Functions.length - 1; i++) {
            if (Functions[i].Id == id) {
                Functions.splice(i, 1);
                break;
            }
        }
    }
    function ClearFun() {
        Functions = [];
    }

    return {
        Applications: function () {
            return Applications;
        },
        Modules: function () {
            return Modules;
        },
        Functions: function () {
            return Functions;
        },
        Types: function () {
            return Types;
        },
        addApplication: function (id, app) {
            Applications.unshift(NewApp(id, app));
        },
        editApplication: function (id, app) {
            EditApp(id, app);
        },
        removeApplication: function (id) {
            return RemoveApp(id);
        },
        addModule: function (appid, id, mod) {
            Modules.unshift(NewMod(appid, id, mod));
        },
        editModule: function (id, mod) {
            EditMod(id, mod);
        },
        removeModule: function (id) {
            return RemoveMod(id);
        },
        clearModules: function () {
            ClearMod();
        },
        addFunction: function (modid, id, fun) {
            Functions.unshift(NewFun(modid, id, fun));
        },
        editFunction: function (id, fun) {
            EditFun(id, fun);
        },
        removeFunction: function (id) {
            RemoveFun(id);
        },
        clearFunctions: function () {
            ClearFun();
        },

        LoadModules: function (appId) {
            LoadModules(appId);
        },
        LoadFunctions: function (modId) {
            LoadFunctions(modId);
        }
    };
});
