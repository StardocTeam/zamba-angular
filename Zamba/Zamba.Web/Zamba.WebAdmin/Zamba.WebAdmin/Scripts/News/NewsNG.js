
webadminApp
    .controller('NewsIndexController', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams, $location) {
        $scope.News = [];
        $scope.fillNews = function () {
            $http.get('../../api/news').
             then(function (d, status, headers, config) {
                 $scope.News = d.data;
             }),
             function (error) {
                 (function (data, status, headers, config) {
                     toastr.error("Se produjo un error al cargar noticias", "Zamba News");
                 });
             }
        }
        $scope.fillNews();
        $scope.Columns = [
            "Id", "Titulo", "Repeticion", "Importancia", "Creacion"
        ];
        $scope.OrderType = "id";
        $scope.SelectedItem = {
            "id": "1",
            "name": "Id",
        };
        $scope.changeFilter = function (_this, e) {
            switch (e.id) {
                case "1":
                    $scope.OrderType = "id";
                    break;
                case "2":
                    $scope.OrderType = "-id";
                    break;
                case "3":
                    $scope.OrderType = "title";
                    break;
                case "4":
                    $scope.OrderType = "-title";
                    break;
                case "5":
                    $scope.OrderType = "-important";
                    break;
                case "6":
                    $scope.OrderType = "important";
                    break;
                case "7":
                    $scope.OrderType = "-created";
                    break;
                case "8":
                    $scope.OrderType = "created";
                    break;
            }
        }
        $scope.FilterOpt = [
            { id: '1', name: 'Id -> Asc' },
            { id: '2', name: 'Id -> Desc' },
            { id: '3', name: 'Titulo -> Asc' },
            { id: '4', name: 'Titulo -> Desc' },
            { id: '5', name: 'Mas importante' },
            { id: '6', name: 'Menos importante' },
            { id: '7', name: 'Recientes' },
            { id: '8', name: 'Antiguos' }
        ];


        $scope.removeNew = function (id) {
            var url = LocalhostURL() + 'api/news/' + id;
            $http({
                method: 'DELETE',
                url: url,
                headers: {
                    'Content-type': 'application/json;charset=utf-8'
                }
            })
               .then(function (r) {
                   $scope.RemoveItem(r.data.id);
                   toastr.success("Registro eliminado con éxito", "Zamba News");
               }, function (rejection) {
                   toastr.error("Se produjo un error al eliminar el registro", "Zamba News");
               });
        };
        $scope.RemoveItem = function (id) {
            var n = $scope.News;
            for (var i = 0; i <= n.length - 1; i++) {
                if (n[i].id == id) {
                    $scope.News.splice(i, 1);
                    break;
                }
            }
        }
    }])

 .controller('NewsAddController', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams, $location) {
     $scope.addSubmitNews = function () {
         var title = $scope.title;
         if (title == undefined) {
             toastr.error("Escriba un titulo", "Zamba News");
             return;
         }
         var repeat = $scope.repeat;

         var thisNew = {
             "Title": title,
             "Repeat": repeat,
             "Important": $scope.important,
             "ShortContent": GetTinyMCEContent('ShortContent'),
             "FullContent": GetTinyMCEContent("FullContent"),
             "Created": new Date().toISOString()
         }
         $http.post('../api/news', thisNew).
             then(function (d, status, headers, config) {
                 toastr.success("Se genero la noticia N°" + d.data.id, "Zamba News");
                 setTimeout(function () { window.location.replace(LocalhostURL() + "/news"); }, 1000);
             }),
         function (error) {
             toastr.error("Se produjo un error al generar la noticia", "Zamba News");
         };
     };

     $scope.GetTemplates = function () {
         $http.get('../api/news/GetTemplates').
         then(function (d, status, headers, config) {
             var template = d.data[0];
             SetTinyMCEContent("ShortContent", template);
             SetTinyMCEContent("FullContent", template);
         }), (function (data, status, headers, config) {
             toastr.error("Se produjo un error al obtener los templates", "Zamba News");
         });
     }
     $scope.GetTemplates();
 }])

 .controller('NewsEditController', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {
     $scope.Columns = [
           "Id", "Titulo", "Repeticion", "Importancia"
     ];

     $scope.FillNewsData = function () {
         var id = $location.$$path.replace(/^\D+/g, '');
         if (id == undefined || isNaN(id) || id == "") id = $location.$$absUrl.replace(/^\D+/g, '');
         $http.get(LocalhostURL() + '/api/news/' + id).
          then(function (d, status, headers, config) {
              data = d.data;
              $scope.id = data.id;
              $scope.repeat = data.repeat;
              $scope.important = data.important;
              $scope.title = data.title;
              SetTinyMCEContent('ShortContent', data.shortContent);
              SetTinyMCEContent('FullContent', data.fullContent);
          }), function (data, status, headers, config) {
              toastr.error("Se produjo un error al obtener datos", "Zamba News");
          };
     }
     $scope.FillNewsData();

     $scope.editSubmit = function () {
         var thisNew = {
             "Id": $scope.id,
             "Title": $scope.title,
             "Repeat": $scope.repeat,
             "Important": $scope.important,
             "ShortContent": GetTinyMCEContent('ShortContent'),
             "FullContent": GetTinyMCEContent("FullContent"),
         };
         var url = LocalhostURL() + '/api/news/' + $scope.id;
         $http({
             method: 'PUT',
             url: url,
             data: thisNew,
             headers: {
                 'Content-Type': 'application/json',
                 'Accept': 'application/json'
             }
         })
         .then(function (response) {
             toastr.success("Registro actualizado con éxito", "Zamba News");
             setTimeout(function () { window.location.replace(LocalhostURL() + "/news"); }, 1000);
         }, function (r, err, ex) {
             toastr.error("Se produjo un error al modificar el registro", "Zamba News");
         });
     };
 }])